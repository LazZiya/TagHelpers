using LazZiya.TagHelpers.Properties;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LazZiya.TagHelpers
{
    /// <summary>
    /// Tag helper component for client side localized validation scripts.
    /// </summary>
    public class LocalizationValidationScriptsTagHelperComponent : TagHelperComponent
    {

#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
        private readonly IHostingEnvironment _hosting;

        /// <summary>
        /// Tag helper component for client side localized validation scripts.
        /// </summary>
        /// <param name="hosting"></param>
        public LocalizationValidationScriptsTagHelperComponent(IHostingEnvironment hosting)
        {
            _hosting = hosting;
        }
#else
        private readonly IWebHostEnvironment _hosting;

        /// <summary>
        /// inserts all localizaiton validation scripts into relevant tag
        /// </summary>
        /// <param name="hosting"></param>
        public LocalizationValidationScriptsTagHelperComponent(IWebHostEnvironment hosting)
        {
            _hosting = hosting;
        }
#endif
        /// <summary>
        /// default order is 0
        /// </summary>
        public override int Order => 1;

        /// <summary>
        /// generate the taghelper
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (string.Equals(context.TagName, "localization-validation-scripts", StringComparison.OrdinalIgnoreCase))
            {
                //read source attribute
                var sourceAttribute = GetAttribute(context, "source", ScriptSource.JsDeliver);

                //get the value of the source property
                Enum.TryParse<ScriptSource>(sourceAttribute.Value.ToString(), out ScriptSource _scriptSource);
                //assign relevant script file accordingly
                var _script = _scriptSource == ScriptSource.JsDeliver
                    ? Resources.LocalizationValidationScripts_jsdeliver
                    : Resources.LocalizationValidationScripts_local;

                //read cldr-core-version attribute
                var cldrCoreVersionAttribute = GetAttribute(context, "cldr-core-version", "35.1.0");
                var cldrCoreVersion = cldrCoreVersionAttribute.Value.ToString();

                var culture = _scriptSource == ScriptSource.JsDeliver
                    ? CultureInfo.CurrentCulture.Name
                    : GetCultureName();

                // Some cultures do not have scripts in jsDelivr e.g. en-us, es-es,
                // so switch to parent culture
                string[] nonSupportedCultres = { "en-us", "es-es" };
                culture = nonSupportedCultres.Any(x => x.Equals(culture, StringComparison.OrdinalIgnoreCase))
                            ? culture.Split('-')[0]
                            : culture;

                output.PostContent.AppendHtml(_script.Replace("{culture}", culture)
                                                     .Replace("{cldr-core-version}", cldrCoreVersion));
            }
        }

        private TagHelperAttribute GetAttribute(TagHelperContext context, string tagName, object defaultValue)
        {
            //get the source property from the taghelper
            context.AllAttributes.TryGetAttribute(tagName, out TagHelperAttribute attribute);

            return attribute ?? new TagHelperAttribute(tagName, defaultValue);
        }

        /// <summary>
        /// find json files related to the current culture, if not found return parent culture, if not found return default culture.
        /// see ClientSideValidationScripts.html for how to configure paths
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
