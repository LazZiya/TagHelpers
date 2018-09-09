using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace LazZiya.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public bool EmailConfirmed { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";                                 // Replaces <email> with <a> tag

            var content = await output.GetChildContentAsync();
            var target = content.GetContent();

            output.Attributes.SetAttribute("href", "mailto:" + target);
            output.Content.SetContent(target);

            if (EmailConfirmed)
                output.PreContent.SetHtmlContent("<span class=\"fas fa-check-circle text-success\"></span>");
            else
                output.PreContent.SetHtmlContent("<span class=\"fas fa-exclamation text-warning\"></span>");
        }

    }
}
