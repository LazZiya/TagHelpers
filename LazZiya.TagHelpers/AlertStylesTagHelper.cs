using Microsoft.AspNetCore.Razor.TagHelpers;
using LazZiya.TagHelpers.Alerts;
using System.Threading.Tasks;

namespace LazZiya.TagHelpers
{
    /// <summary>
    /// Create primary alert
    /// Alert contents must be replaced between alert tags e.g. <![CDATA[<alert-success>job done!</alert-success>]]>
    /// </summary>
    public class AlertPrimaryTagHelper : AlertTagHelper
    {
        /// <summary>
        /// Create primary alert
        /// </summary>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.AlertStyle = AlertStyle.Primary;
            await base.ProcessAsync(context, output);
        }
    }

    /// <summary>
    /// Create secondary alert
    /// Alert contents must be replaced between alert tags e.g. <![CDATA[<alert-success>job done!</alert-success>]]>
    /// </summary>
    public class AlertSecondaryTagHelper : AlertTagHelper
    {
        /// <summary>
        /// Create secondary alert
        /// </summary>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.AlertStyle = AlertStyle.Secondary;
            await base.ProcessAsync(context, output);
        }
    }

    /// <summary>
    /// Create success alert
    /// Alert contents must be replaced between alert tags e.g. <![CDATA[<alert-success>job done!</alert-success>]]>
    /// </summary>
    public class AlertSuccessTagHelper : AlertTagHelper
    {
        /// <summary>
        /// Create success alert
        /// </summary>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.AlertStyle = AlertStyle.Success;
            await base.ProcessAsync(context, output);
        }
    }

    /// <summary>
    /// Create danger alert
    /// Alert contents must be replaced between alert tags e.g. <![CDATA[<alert-success>job done!</alert-success>]]>
    /// </summary>
    public class AlertDangerTagHelper : AlertTagHelper
    {
        /// <summary>
        /// Create danger alert
        /// </summary>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.AlertStyle = AlertStyle.Danger;
            await base.ProcessAsync(context, output);
        }
    }

    /// <summary>
    /// Create warning alert
    /// Alert contents must be replaced between alert tags e.g. <![CDATA[<alert-success>job done!</alert-success>]]>
    /// </summary>
    public class AlertWarningTagHelper : AlertTagHelper
    {
        /// <summary>
        /// Create danger alert
        /// </summary>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.AlertStyle = AlertStyle.Warning;
            await base.ProcessAsync(context, output);
        }
    }

    /// <summary>
    /// Create info alert
    /// Alert contents must be replaced between alert tags e.g. <![CDATA[<alert-success>job done!</alert-success>]]>
    /// </summary>
    public class AlertInfoTagHelper : AlertTagHelper
    {
        /// <summary>
        /// Create info alert
        /// </summary>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.AlertStyle = AlertStyle.Info;
            await base.ProcessAsync(context, output);
        }
    }

    /// <summary>
    /// Create light alert
    /// Alert contents must be replaced between alert tags e.g. <![CDATA[<alert-success>job done!</alert-success>]]>
    /// </summary>
    public class AlertLightTagHelper : AlertTagHelper
    {
        /// <summary>
        /// Create light alert
        /// </summary>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.AlertStyle = AlertStyle.Light;
            await base.ProcessAsync(context, output);
        }
    }

    /// <summary>
    /// Create dark alert
    /// Alert contents must be replaced between alert tags e.g. <![CDATA[<alert-success>job done!</alert-success>]]>
    /// </summary>
    public class AlertDarkTagHelper : AlertTagHelper
    {
        /// <summary>
        /// Create dark alert
        /// </summary>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.AlertStyle = AlertStyle.Dark;
            await base.ProcessAsync(context, output);
        }
    }
}
