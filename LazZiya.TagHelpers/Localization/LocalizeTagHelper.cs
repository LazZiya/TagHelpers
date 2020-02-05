using LazZiya.Common;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace LazZiya.TagHelpers.Localization
{
    /// <summary>
    /// Localization tag helper, localize text inside <![CDATA[<localize>Hellow</localize>]]>
    /// </summary>
    public class LocalizeTagHelper : LocalizationTagHelperBase
    {
        /// <summary>
        /// inject SharedCultureLocalizer
        /// </summary>
        /// <param name="provider"></param>
        public LocalizeTagHelper(IServiceProvider provider) : base(provider)
        {
        }

        /// <summary>
        /// process localize tag helper
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //replace <localize> tag with <span>
            output.TagName = "";
            await base.ProcessAsync(context, output);
        }
    }
}
