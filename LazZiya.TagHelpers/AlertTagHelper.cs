using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;

namespace LazZiya.TagHelpers
{
    public class AlertTagHelper : TagHelper
    {
        public AlertStyle AlertStyle { get; set; } = AlertStyle.Primary;

        public string AlertHeading { get; set; }

        public string AlertMessage { get; set; }

        public bool Dismissable { get; set; } = true;

        public ViewContext ViewContext { get; set; } = null;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            if (ViewContext != null)
            {
                var alerts = ViewContext.TempData.ContainsKey(Alert.TempDataKey)
                    ? (List<Alert>)ViewContext.TempData[Alert.TempDataKey]
                    : new List<Alert>();

                alerts.ForEach(x => output.Content.AppendHtml(AddAlert(x)));
            }


            if (!string.IsNullOrWhiteSpace(AlertMessage))
            {
                var manualAlert = AddAlert(new Alert
                {
                    AlertHeading = this.AlertHeading,
                    AlertMessage = this.AlertMessage,
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

            if (!string.IsNullOrWhiteSpace(alert.AlertHeading))
            {
                var _header = new TagBuilder("h4");
                _header.AddCssClass("alert-heading");
                _header.InnerHtml.Append(alert.AlertHeading);
                _alert.InnerHtml.AppendHtml(_header);
            }

            if (!string.IsNullOrWhiteSpace(alert.AlertMessage))
            {
                var _msg = new TagBuilder("p");
                _msg.InnerHtml.Append(alert.AlertMessage);
                _alert.InnerHtml.AppendHtml(_msg);
            }

            return _alert;
        }
    }
}
