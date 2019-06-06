using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LazZiya.TagHelpers
{
    public class AlertPrimaryTagHelper : AlertTagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.AlertStyle = AlertStyle.Primary;
            base.Process(context, output);
        }
    }

    public class AlertSecondaryTagHelper : AlertTagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.AlertStyle = AlertStyle.Secondary;
            base.Process(context, output);
        }
    }

    public class AlertSuccessTagHelper : AlertTagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.AlertStyle = AlertStyle.Success;
            base.Process(context, output);
        }
    }

    public class AlertDangerTagHelper : AlertTagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.AlertStyle = AlertStyle.Danger;
            base.Process(context, output);
        }
    }

    public class AlertWarningTagHelper : AlertTagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.AlertStyle = AlertStyle.Warning;
            base.Process(context, output);
        }
    }

    public class AlertInfoTagHelper : AlertTagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.AlertStyle = AlertStyle.Info;
            base.Process(context, output);
        }
    }

    public class AlertLightTagHelper : AlertTagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.AlertStyle = AlertStyle.Light;
            base.Process(context, output);
        }
    }

    public class AlertDarkTagHelper : AlertTagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.AlertStyle = AlertStyle.Dark;
            base.Process(context, output);
        }
    }
}
