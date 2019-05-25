#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;


namespace LazZiya.TagHelpers
{
    /// <summary>
    /// inserts all localizaiton validation scripts into relevant tag
    /// </summary>
    public class ExpressLocalizationTagHelperComponent : TagHelperComponent
    {
        private readonly IHostingEnvironment _hosting;

        /// <summary>
        /// inserts all localizaiton validation scripts into relevant tag
        /// </summary>
        /// <param name="hosting"></param>
        public ExpressLocalizationTagHelperComponent(IHostingEnvironment hosting)
        {
            _hosting = hosting;
        }

        public override int Order => 1;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (string.Equals(context.TagName, "localization-validation-scripts", StringComparison.OrdinalIgnoreCase))
            {
                //get the source property from the taghelper
                TagHelperAttribute _sourceProperty;
                context.AllAttributes.TryGetAttribute("source", out _sourceProperty);

                if (_sourceProperty == null) _sourceProperty = new TagHelperAttribute("source", ScriptSource.JsDeliver);

                //get the value of the source property
                var _scriptSource = ScriptSource.JsDeliver;
                Enum.TryParse<ScriptSource>(_sourceProperty.Value.ToString(), out _scriptSource);

                //assign relevant script file accordingly
                var _script = _scriptSource == ScriptSource.JsDeliver
                    ? "ExpressLocalizationValidationScripts_jsdeliver.html"
                    : "ExpressLocalizationValidationScripts_local.html";

                var assemblyFolderPath = GetCurrentAssemblyFolderPath();
                var scriptFilePath = Path.Combine(Path.GetDirectoryName(assemblyFolderPath), "Templates", _script);
                
                var script = await File.ReadAllTextAsync(scriptFilePath);
                var culture = GetCultureName();
                output.PostContent.AppendHtml(script.Replace("{culture}", culture));
            }
        }

        private string GetCurrentAssemblyFolderPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            return Uri.UnescapeDataString(uri.Path);
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