using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using System;

namespace LazZiya.ExpressLocalization.TagHelpers
{
    /// <summary>
    /// defines location to load localization valdiation scripts
    /// </summary>
    public enum ScriptSource
    {
        /// <summary>
        /// valdiation scripts are located under wwwroot/lib folder
        /// </summary>
        Local,

        /// <summary>
        /// valdiation scripts will be loaded from jsdelivr
        /// </summary>
        JsDeliver
    }

    /// <summary>
    /// Tag helper for client side localized validation scripts.
    /// </summary>
    [HtmlTargetElement("localization-validation-scripts")]
    public class LocalizationValidationScriptsTagHelperComponentTagHelper : TagHelperComponentTagHelper
    {
        /// <summary>
        /// (optional) define where to load scripts from, Local or JsDelivr.
        /// <para>Default: JsDelivr</para>
        /// </summary>
        [HtmlAttributeName("source")]
        public ScriptSource Source { get; set; } = ScriptSource.JsDeliver;

        /// <summary>
        /// (optional) set cldr version to load.
        /// <para>Default: 35.1</para>
        /// </summary>
        [HtmlAttributeName("cldr-core-version")]
        public string CldrVersion { get; set; } = "35.1.0";

        /// <summary>
        /// Tag helper for client side localized validation scripts.
        /// </summary>
        public LocalizationValidationScriptsTagHelperComponentTagHelper(ITagHelperComponentManager manager, ILoggerFactory loggerFactory) : base(manager, loggerFactory)
        {
        }
    }
}
