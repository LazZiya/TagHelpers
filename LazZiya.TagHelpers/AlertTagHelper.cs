using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using LazZiya.TagHelpers.Alerts;
using System.Threading.Tasks;
using LazZiya.TagHelpers.Utilities;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
                    //? JsonConvert.DeserializeObject<List<Alert>>(ViewContext.TempData[Alert.TempDataKey].ToString())
                    //? (List<Alert>)ViewContext.TempData[Alert.TempDataKey]
                    ? ViewContext.TempData.Get<List<Alert>>(Alert.TempDataKey)
                    : new List<Alert>();

                alerts.ForEach(x => output.Content.AppendHtml(AddAlert(x)));

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

            var alertStyle = Enum.GetName(typeof(AlertStyle), alert.AlertStyle).ToLower();
            _alert.AddCssClass($"alert alert-{alertStyle}");
            _alert.Attributes.Add("role", "alert");

            if (alert.Dismissable)
            {
                _alert.InnerHtml.AppendHtml("<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>");
            }

            if (!string.IsNullOrWhiteSpace(alert.AlertHeading))
            {
                _alert.InnerHtml.AppendHtml($"<h4 class='alert-heading'>{alert.AlertHeading}</h4>");
            }

            if (!string.IsNullOrWhiteSpace(alert.AlertMessage))
            {
                _alert.InnerHtml.AppendHtml($"<p class='mb-0'>{alert.AlertMessage}</p>");
            }

            return _alert;
        }
    }
}
