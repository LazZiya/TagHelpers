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
    /// The label to display for language dropdown list on language names
    /// </summary>
    public enum LanguageLabel
    {
        /// <summary>
        /// Culture name
        /// </summary>
        Name,

        /// <summary>
        /// Culture display name
        /// </summary>
        DisplayName,

        /// <summary>
        /// Culture English name
        /// </summary>
        EnglishName,

        /// <summary>
        /// Culture native name
        /// </summary>
        NativeName,

        /// <summary>
        /// Two letter ISO language name
        /// </summary>
        TwoLetterISOLanguageName
    }

    /// <summary>
    /// Defines where to redirect when language is changes
    /// </summary>
    public enum RedirectTo
    {
        /// <summary>
        /// redirects to home page in the project root
        /// </summary>
        HomePage,

        /// <summary>
        /// redirects to the same page and keep all filter like search
        /// </summary>
        SamePage,

        /// <summary>
        /// redirect to the same page and clear all filters (QueryString) values
        /// </summary>
        SamePageNoQueryString
    }
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
        /// route values and query string values will be collected in one dictionary,
        /// then we will replace culture parameter with the appropriate culture name value while creating the dropdown list.
        /// </summary>
        private IDictionary<string, object> _routeData;

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
            _routeData = new Dictionary<string, object>();
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
            _routeData = new Dictionary<string, object>();
        }
#endif
        /// <summary>
        /// start creating the language navigation dropdown
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var toggle = CreateToggle();
            var list = CreateLanguagesDropdownList();

            output.TagName = "div";
            output.Attributes.Add("class", "dropdown");

            output.Content.AppendHtml(toggle);
            output.Content.AppendHtml(list);
        }

        private void CreateRouteDataDictionary()
        {
            //redirect to same page or same page without query string
            foreach (var r in ViewContext.RouteData.Values)
            {
                _routeData.Add(r.Key, r.Value.ToString());
            }

            if (RedirectTo == RedirectTo.SamePage)
            {
                foreach (var q in ViewContext.HttpContext.Request.Query)
                {
                    _routeData.Add(q.Key, q.Value);
                }
            }
        }

        private TagBuilder CreateLanguagesDropdownList()
        {
            var div = new TagBuilder("div");
            div.AddCssClass("dropdown-menu dropdown-menu-right");
            div.Attributes.Add("aria-labeledby", "dropdownlang");

            if (RedirectTo != RedirectTo.HomePage)
                CreateRouteDataDictionary();
            else //we dont need route values, just culture info to redirect to home page at root level
            {
                _routeData.Clear();
                _routeData.Add(CultureKeyName, CultureInfo.CurrentCulture.Name);

                ViewContext.RouteData.Values.Clear();
                ViewContext.RouteData.Values.Add(CultureKeyName, CultureInfo.CurrentCulture.Name);
                ViewContext.RouteData.Values.Add("page", $"/{HomePageName}");
            }

            var urlHelper = new UrlHelper(ViewContext);

            var cultures = GetSupportedCultures();

            foreach (var cul in cultures.Where(x => x.Name != CultureInfo.CurrentCulture.Name))
            {
                var a = new TagBuilder("a");

                a.AddCssClass("dropdown-item small");

                //replace culture value with the relevant one for dropdown list
                _routeData[CultureKeyName] = cul.Name;

                var urlRoute = new UrlRouteContext { Values = _routeData };

#if NETCOREAPP2_2
                var url = _lg.GetPathByRouteValues(httpContext: ViewContext.HttpContext, "", _routeData);
#else
                var url = urlHelper.RouteUrl(urlRoute);
#endif

                a.Attributes.Add("href", url);

                var label = GetLanguageLabel(cul);
                a.InnerHtml.Append(label);

                div.InnerHtml.AppendHtml(a);
            }

            return div;
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
