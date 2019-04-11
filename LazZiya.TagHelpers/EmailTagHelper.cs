using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace LazZiya.TagHelpers
{
    /// <summary>
    /// creates email link with mark if email is confirmed
    /// </summary>
    public class EmailTagHelper : TagHelper
    {
        /// <summary>
        /// boolean value to indicate if the email is confirmed or not
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// process in creating email tag helper
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
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
