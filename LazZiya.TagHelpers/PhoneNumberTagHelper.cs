using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace LazZiya.TagHelpers
{
    public class PhoneNumberTagHelper : TagHelper
    {
        public bool PhoneNumberConfirmed { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";

            var content = await output.GetChildContentAsync();

            var target = content.GetContent();
            output.Content.SetContent(target.Replace("&#x2B;", "+"));
            output.Attributes.SetAttribute("dir", "ltr");

            if (PhoneNumberConfirmed)
                output.PreContent.SetHtmlContent("<span class=\"fas fa-check-circle text-success\"></span>");
            else
                output.PreContent.SetHtmlContent("<span class=\"fas fa-exclamation text-warning\"></span>");
        }

    }
}
