using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LazZiya.TagHelpers
{
    public class LanguageNavTagHelper : TagHelper
    {   
        /// <summary>
        /// string list of supported cultures list seperated by comma
        /// ar,tr,en
        /// </summary>
        public string SupportedCultures { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
        }
    }
}
