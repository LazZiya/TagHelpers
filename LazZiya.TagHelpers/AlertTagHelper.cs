using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using LazZiya.TagHelpers.Alerts;
using System.Threading.Tasks;
using LazZiya.TagHelpers.Utilities;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using System.Linq;

namespace LazZiya.TagHelpers
{
    /// <summary>
    /// Create alert messages styled with bootstrap 4.x
    /// Alert contents must be replaced between alert tags e.g. <![CDATA[<alert-success>job done!</alert-success>]]>
    /// </summary>
    public class AlertTagHelper : TagHelper
    {
        internal AlertStyle AlertStyle { get; set; } = AlertStyle.Primary;

        /// <summary>
        /// Header text for the alert
        /// </summary>
        public string AlertHeading { get; set; }

        /// <summary>
        /// Show closing button, default is true
        /// </summary>
        public bool Dismissable { get; set; } = true;

        /// <summary>
        /// Show multiple alerts as slides
        /// </summary>
        public bool SlideAlerts { get; set; } = true;

        /// <summary>
        /// Show alert icons from fontawesome.
        /// Requires fontawesome css
        /// </summary>
        public bool ShowIcons { get; set; } = true;

        /// <summary>
        /// Choose where to get icons source from. "Bootstrap" or "FontAwesome".
        /// </summary>
        public IconsSource IconsSource { get; set; } = IconsSource.Bootstrap;

        /// <summary>
        /// Parse localizer instance to localize alert message
        /// </summary>
        public IStringLocalizer Localizer { get; set; }

        /// <summary>
        /// <para>ViewContext property is not required to be passed as parameter, it will be assigned automatically by the tag helper.</para>
        /// <para>View context is required to access TempData dictionary that contains the alerts coming from backend</para>
        /// </summary>
        [ViewContext]
        public ViewContext ViewContext { get; set; } = null;

        /// <summary>
        /// Create alert messages styled with bootstrap 4.x
        /// </summary>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            if (ViewContext != null)
            {
                var alerts = ViewContext.TempData.ContainsKey(Alert.TempDataKey)
                    ? ViewContext.TempData.Get<List<Alert>>(Alert.TempDataKey)
                    : new List<Alert>();
                if(alerts.Any())
                if (SlideAlerts)
                {
                    output.Content.AppendHtml(AddAlertCarousel(alerts));
                }
                else
                {
                    alerts.ForEach(a => output.Content.AppendHtml(AddAlert(a)));
                }

                ViewContext.TempData.Remove(Alert.TempDataKey);
            }

            // read alerts contents from inner html
            var msg = await output.GetChildContentAsync();

            if (!string.IsNullOrWhiteSpace(msg.GetContent()))
            {
                var manualAlert = AddAlert(new Alert
                {
                    AlertHeading = this.AlertHeading,
                    AlertMessage = msg.GetContent(),
                    AlertStyle = this.AlertStyle,
                    Dismissable = this.Dismissable
                });
                output.Content.AppendHtml(manualAlert);
            }

        }

        private TagBuilder AddAlert(Alert alert)
        {
            var _alert = new TagBuilder("div");

            string alertIcon;
            switch (alert.AlertStyle)
            {
                case AlertStyle.Success: alertIcon = IconsSource == IconsSource.Bootstrap ? BootstrapIcons.Success : FontAwesomeIcons.Success; break;
                case AlertStyle.Warning: alertIcon = IconsSource == IconsSource.Bootstrap ? BootstrapIcons.Warning : FontAwesomeIcons.Warning; break;
                case AlertStyle.Info: alertIcon =  IconsSource == IconsSource.Bootstrap ? BootstrapIcons.Info : FontAwesomeIcons.Info; break;
                case AlertStyle.Danger: alertIcon = IconsSource == IconsSource.Bootstrap ? BootstrapIcons.Danger : FontAwesomeIcons.Danger; break;
                default: alertIcon = IconsSource == IconsSource.Bootstrap ? BootstrapIcons.Default : FontAwesomeIcons.Default; break;
            }

            var alertStyle = Enum.GetName(typeof(AlertStyle), alert.AlertStyle).ToLowerInvariant();
            _alert.AddCssClass($"alert alert-{alertStyle}");
            _alert.Attributes.Add("role", "alert");

            if (alert.Dismissable)
            {
                _alert.InnerHtml.AppendHtml("<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>");
            }

            if (!string.IsNullOrWhiteSpace(alert.AlertHeading))
            {
                var heading = Localizer == null ? alert.AlertHeading : Localizer[alert.AlertHeading];
                _alert.InnerHtml.AppendHtml($"<h4 class='alert-heading'>{heading}</h4>");
            }

            if (!string.IsNullOrWhiteSpace(alert.AlertMessage))
            {
                var msg = Localizer == null ? alert.AlertMessage : Localizer[alert.AlertMessage];
                var icon = ShowIcons ? $"<i class='bi {alertIcon}'></i>&nbsp;" : string.Empty;
                _alert.InnerHtml.AppendHtml($"<p class='mb-0'>{icon}{msg}</p>");
            }

            return _alert;
        }

        private TagBuilder AddAlertCarousel(List<Alert> alerts)
        {
            var carouselId = "alertCarouselControls";

            var indic = new TagBuilder("ol");
            indic.AddCssClass("carousel-indicators");

            for (int i = 0; i < alerts.Count; i++)
            {
                var indicItem = new TagBuilder("li");
                indicItem.Attributes.Add("data-target", $"#{carouselId}");
                indicItem.Attributes.Add("data-slide-to", $"{i}");
                if (i == 0)
                    indicItem.AddCssClass("active");
                indic.InnerHtml.AppendHtml(indicItem);
            }

            var carouselInner = new TagBuilder("div");
            carouselInner.AddCssClass("carousel-inner");
            for (int i = 0; i < alerts.Count; i++)
            {
                var slide = new TagBuilder("div");
                if (i == 0)
                    slide.AddCssClass("carousel-item active");
                else
                    slide.AddCssClass("carousel-item");

                // remove dismissabel property from inner alerts
                // only the most outer alert will have dismissable property
                alerts[i].Dismissable = false;
                slide.InnerHtml.AppendHtml(AddAlert(alerts[i]));

                carouselInner.InnerHtml.AppendHtml(slide);
            }

            var carouselDiv = new TagBuilder("div");
            carouselDiv.AddCssClass("carousel slide");
            carouselDiv.Attributes.Add("id", carouselId);
            carouselDiv.Attributes.Add("data-ride", "carousel");
            carouselDiv.InnerHtml.AppendHtml(indic);
            carouselDiv.InnerHtml.AppendHtml(carouselInner);

            // This is the main alert that will hold the alerts carousel
            var mainAlert = new TagBuilder("div");
            mainAlert.AddCssClass("alert alert-dismissible p-0");
            mainAlert.Attributes.Add("role", "alert");
            mainAlert.InnerHtml.AppendHtml(carouselDiv);
            mainAlert.InnerHtml.AppendHtml("<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>");

            return mainAlert;
        }
    }
}
