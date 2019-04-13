using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

#if NETCOREAPP2_2
using Microsoft.AspNetCore.Routing;
#endif

namespace LazZiya.TagHelpers
{
    /// <summary>
    /// creates a language navigation menu, depends on supported cultures
    /// </summary>
    public class LanguageNavTagHelper : TagHelper
    {
        /// <summary>
        /// optional: route data key name for culture, default "culture"
        /// default: cultute
        /// </summary>
        public string CultureKeyName { get; set; } = "culture";

        /// <summary>
        /// optional: manually specify list of supported cultures
        /// <example>
        /// en-US,tr, ar
        /// </example>
        /// </summary>
        public string SupportedCultures { get; set; }

        /// <summary>
        /// optional: what to display as label for language dropdown
        /// default: LanguageLabel.EnglishName
        /// </summary>
        public LanguageLabel LanguageLabel { get; set; } = LanguageLabel.EnglishName;

        /// <summary>
        /// optional: specify where to redirect when the language is changed
        /// <para>default value: RedirectTo.SamePage</para>
        /// </summary>
        public RedirectTo RedirectTo { get; set; } = RedirectTo.SamePage;

        /// <summary>
        /// optinal: name of the home page to redirect to when RedirectTo is HomePage
        /// </summary>
        public string HomePageName { get; set; } = "Index";

        /// <summary>
        /// current view context to access RouteData.Values and Request.Query collection
        /// </summary>
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// Choose render mode: classis for regular dropdown list, Bootstrap4 for HTML5 div with Bootstrap4 style.
        /// </summary>
        public RenderMode RenderMode { get; set; } = RenderMode.Bootstrap;

        /// <summary>
        /// required for listing supported cultures
        /// </summary>
        private readonly IOptions<RequestLocalizationOptions> _ops;
        private readonly ILogger _logger;

#if NETCOREAPP2_2
        private readonly LinkGenerator _lg;

        /// <summary>
        /// creates a language navigation menu, depends on supported cultures
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ops"></param>
        public LanguageNavTagHelper(ILogger<LanguageNavTagHelper> logger, IOptions<RequestLocalizationOptions> ops, LinkGenerator lg)
        {
            _logger = logger;
            _ops = ops;
            _lg = lg;
        }
#else
        /// <summary>
        /// creates a language navigation menu, depends on supported cultures
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ops"></param>
        public LanguageNavTagHelper(ILogger<LanguageNavTagHelper> logger, IOptions<RequestLocalizationOptions> ops)
        {
            _logger = logger;
            _ops = ops;
        }
#endif
        /// <summary>
        /// start creating the language navigation dropdown
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var langDictionary = CreateNavDictionary();

            if (RenderMode == RenderMode.Bootstrap)
            {
                CreateBootstrapItems(ref output, langDictionary);
            }
            else
            {
                CreateClassicItems(ref output, langDictionary);
            }
        }

        /// <summary>
        /// create classic list items list
        /// <example><![CDATA[<option value="/en-US/Index">English</option>]]></example>
        /// </summary>
        /// <param name="langDictionary">language name-URL dictionary</param>
        /// <param name="output">reference to TagHelperOuput</param>
        /// <returns></returns>
        private void CreateClassicItems(ref TagHelperOutput output, List<LanguageItem> langDictionary)
        {
            output.TagName = "select";
            foreach (var lang in langDictionary.OrderBy(x=>x.DisplayText))
            {
                var option = new TagBuilder("option");
                option.Attributes.Add("value", lang.Url);
                option.InnerHtml.AppendHtml(lang.DisplayText);

                if (CultureInfo.CurrentCulture.Name == lang.Name)
                    option.Attributes.Add("selected", "selected");

                output.Content.AppendHtml(option);
            }
        }

        /// <summary>
        /// create classic list items list
        /// <example><![CDATA[<a href="/en-US/Index" class="itemxyz">English</a>]]></example>
        /// </summary>
        /// <param name="langDictionary">language name-URL dictionary</param>
        /// <param name="output">reference to TagHelperOuput</param>
        /// <returns></returns>
        private void CreateBootstrapItems(ref TagHelperOutput output, List<LanguageItem> langDictionary)
        {
            var div = new TagBuilder("div");
            div.AddCssClass("dropdown-menu dropdown-menu-right");
            div.Attributes.Add("aria-labeledby", "dropdownlang");

            foreach (var lang in langDictionary.Where(x => x.Name != CultureInfo.CurrentCulture.Name).OrderBy(x=>x.DisplayText))
            {
                var a = new TagBuilder("a");
                a.AddCssClass("dropdown-item small");
                a.Attributes.Add("href", lang.Url);
                a.InnerHtml.Append(lang.DisplayText);

                div.InnerHtml.AppendHtml(a);
            }

            output.TagName = "div";
            output.Attributes.Add("class", "dropdown");

            var toggle = CreateToggle();
            output.Content.AppendHtml(toggle);

            output.Content.AppendHtml(div);
        }

        /// <summary>
        /// create dictonary for all supported cultures
        /// <para>Key: Language display name for label</para>
        /// <para>Value: Navigation URL</para>
        /// </summary>
        /// <returns></returns>
        private List<LanguageItem> CreateNavDictionary()
        {
            var _routeData = CreateRouteDataDictionary();

            // if we are redirecting to the home page, then we need
            // only culture paramter and home page name in route values
            if (RedirectTo == RedirectTo.HomePage)
            {
                ViewContext.RouteData.Values.Clear();
                ViewContext.RouteData.Values.Add(CultureKeyName, CultureInfo.CurrentCulture.Name);
                ViewContext.RouteData.Values.Add("page", $"/{HomePageName}");
            }

            var dic = new List<LanguageItem>();
            var urlHelper = new UrlHelper(ViewContext);
            var cultures = GetSupportedCultures();

            foreach (var cul in cultures.Where(x => x.Name != CultureInfo.CurrentCulture.Name))
            {
                //replace culture value with the relevant one for dropdown list
                _routeData[CultureKeyName] = cul.Name;

                var urlRoute = new UrlRouteContext { Values = _routeData };

#if NETCOREAPP2_2
                var url = _lg.GetPathByRouteValues(httpContext: ViewContext.HttpContext, "", _routeData);
#else
                var url = urlHelper.RouteUrl(urlRoute);
#endif
                var label = GetLanguageLabel(cul);
                dic.Add(new LanguageItem { Name = cul.Name, DisplayText = label, Url = url });
            }

            return dic;
        }

        /// <summary>
        /// Get route data values for the current route,
        /// then save all values to _routeData dictionary
        /// </summary>
        private Dictionary<string, object> CreateRouteDataDictionary()
        {
            var _routeData = new Dictionary<string, object>();

            if (RedirectTo == RedirectTo.HomePage)
                _routeData.Add(CultureKeyName, CultureInfo.CurrentCulture.Name);
            else
            {
                //redirect to same page or same page without query string
                foreach (var r in ViewContext.RouteData.Values)
                {
                    _routeData.Add(r.Key, r.Value);
                }

                if (RedirectTo == RedirectTo.SamePage)
                {
                    foreach (var q in ViewContext.HttpContext.Request.Query)
                    {
                        _routeData.Add(q.Key, q.Value);
                    }
                }
            }

            return _routeData;
        }

        /// <summary>
        /// private list of supported CultureInfo,
        /// </summary>
        private IEnumerable<CultureInfo> GetSupportedCultures()
        {
            // if the user didn't specify manually list of supported cultures, 
            // then create cultures list with reference to supported cultures defined in localization settings in startup</para>
            if (string.IsNullOrWhiteSpace(SupportedCultures))
                return _ops.Value.SupportedCultures;

            //if the user will specify supported cultures manually, then this list will be created accordingly
            var cList = new List<CultureInfo>();
            foreach (var c in SupportedCultures.Split(new[] { ',', '|', ';', ' ' }, System.StringSplitOptions.RemoveEmptyEntries))
            {
                cList.Add(new CultureInfo(c));
            }

            return cList;
        }

        private string GetLanguageLabel(CultureInfo cul)
        {
            switch (LanguageLabel)
            {
                case LanguageLabel.Name: return cul.Name;
                case LanguageLabel.DisplayName: return cul.DisplayName;
                case LanguageLabel.EnglishName: return cul.EnglishName;
                case LanguageLabel.NativeName: return cul.NativeName;
                case LanguageLabel.TwoLetterISOLanguageName: return cul.TwoLetterISOLanguageName;

                default: return cul.EnglishName;
            }
        }

        private TagBuilder CreateToggle()
        {
            var toggle = new TagBuilder("a");
            toggle.AddCssClass("btn-sm btn-default border border-secondary dropdown-toggle");
            toggle.Attributes.Add("id", "dropdownLang");
            toggle.Attributes.Add("href", "#");
            toggle.Attributes.Add("role", "button");
            toggle.Attributes.Add("data-toggle", "dropdown");
            toggle.Attributes.Add("aria-haspopup", "true");
            toggle.Attributes.Add("aria-expanded", "false");

            var label = GetLanguageLabel(CultureInfo.CurrentCulture);
            toggle.InnerHtml.Append(label);

            return toggle;
        }
    }
}
