#if NETCOREAPP2_1 || NETCOREAPP2_2
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;


namespace LazZiya.TagHelpers
{
    class ExpressLocalizationTagHelperComponent : TagHelperComponent
    {
        private readonly IHostingEnvironment _hosting;

        public ExpressLocalizationTagHelperComponent(IHostingEnvironment hosting)
        {
            _hosting = hosting;
        }

        public override int Order => 2;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (string.Equals(context.TagName, "body", StringComparison.OrdinalIgnoreCase))
            {
                var script = await File.ReadAllTextAsync("TagHelpers/ClientSideValidationScripts.html");
                output.PostContent.AppendHtml(script.Replace("{culture}", GetCultureName()));
            }
        }

        /// <summary>
        /// find json files related to the current culture, if not found return parent culture, if not found return default culture.
        /// see ClientSideValidationScripts.html for how to configure paths
        /// </para>
        /// </summary>
        /// <returns>culture name e.g. tr</returns>
        private string GetCultureName()
        {
            // use this pattern to check if the relevant json folder are available
            const string localePattern = "lib\\cldr-data\\main\\{0}";
            var currentCulture = CultureInfo.CurrentCulture;
            var cultureToUse = "en"; //Default regionalisation to use

            if (Directory.Exists(Path.Combine(_hosting.WebRootPath, string.Format(localePattern, currentCulture.Name))))
                cultureToUse = currentCulture.Name;
            else if (Directory.Exists(Path.Combine(_hosting.WebRootPath, string.Format(localePattern, currentCulture.TwoLetterISOLanguageName))))
                cultureToUse = currentCulture.TwoLetterISOLanguageName;

            return cultureToUse;
        }
    }
}
#endif