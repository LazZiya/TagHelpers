using LazZiya.TagHelpers.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace LazZiya.TagHelpers
{
    /// <summary>
    /// Creates a pagination control
    /// </summary>
    public class PagingTagHelper : TagHelper
    {
        private IConfiguration Configuration { get; }
        private readonly ILogger _logger;

        /// <summary>
        /// Dictonary object to hold all ajax attributes
        /// </summary>
        private AttributeDictionary AjaxAttributes { get; set; }

        /// <summary>
        /// <para>ViewContext property is not required to be passed as parameter, it will be assigned automatically by the tag helper.</para>
        /// <para>View context is required to access TempData dictionary that contains the alerts coming from backend</para>
        /// </summary>
        [ViewContext]
        public ViewContext ViewContext { get; set; } = null;

        /// <summary>
        /// Creates a pagination control
        /// </summary>
        public PagingTagHelper(IConfiguration configuration, ILogger<PagingTagHelper> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        #region Settings

        /// <summary>
        /// current page number.
        /// <para>default: 1</para>
        /// <para>example: p=1</para>
        /// </summary>
        public int PageNo { get; set; } = 1;

        /// <summary>
        /// how many items to get from db per page per request
        /// <para>default: 10</para>
        /// <para>example: pageSize=10</para>
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Total count of records in the db
        /// <para>default: 0</para>
        /// </summary>
        public int TotalRecords { get; set; } = 0;

        /// <summary>
        /// if count of pages is too much, restrict shown pages count to specific number
        /// <para>default: 10</para>
        /// </summary>
        public int? MaxDisplayedPages { get; set; }

        /// <summary>
        /// Gap size to start show first/last numbered page
        /// <para>default: 3</para>
        /// </summary>
        [Obsolete("This property is deprected. Use ShowGap instead.")]
        public int GapSize { get; set; }

        /// <summary>
        /// name of the settings section in appSettings.json
        /// <param>default: "default"</param>
        /// </summary>
        public string SettingsJson { get; set; } = "default";

        #endregion

        #region Page size navigation
        /// <summary>
        /// This property is deprected. Use PageSizeDropdownItems instead
        /// </summary>
        [Obsolete("This property is deprected. Use PageSizeDropdownItems instead")]
        public int PageSizeNavBlockSize { get; set; }

        /// <summary>
        /// This property is deprected. Use PageSizeDropdownItems instead
        /// </summary>
        [Obsolete("This property is deprected. Use PageSizeDropdownItems instead")]
        public int PageSizeNavMaxItems { get; set; }

        /// <summary>
        /// A list of dash delimitted numbers for page size dropdown. 
        /// default: "10-25-50"
        /// </summary>
        public string PageSizeDropdownItems { get; set; }

        /// <summary>
        /// action to take when page size dropdown changes
        /// <para>default: this.form.submit();</para>
        /// </summary>
        [Obsolete("This property is deprected. No onchange event. Just urls")]
        public string PageSizeNavOnChange { get; set; }

        #endregion

        #region QueryString

        /// <summary>
        /// Query string paramter name for current page.
        /// <para>default: p</para>
        /// <para>exmaple: p=1</para>
        /// </summary>
        public string QueryStringKeyPageNo { get; set; }

        /// <summary>
        /// Query string parameter name for page size
        /// <para>default: s</para>
        /// <para>example: s=25</para>
        /// </summary>
        public string QueryStringKeyPageSize { get; set; }

        /// <summary>
        /// query-string-value is obsolte and will be removed in a future release.
        /// </summary>
        [Obsolete("query-string-value is obsolte and will be removed in a future release")]
        public string QueryStringValue { get; set; }

        #endregion

        #region Display settings

        /// <summary>
        /// Show drop down list for different page size options
        /// <para>default: true</para>
        /// <para>options: true, false</para>
        /// </summary>
        public bool? ShowPageSizeNav { get; set; }

        /// <summary>
        /// Show a three dots after first page or before last page 
        /// when there is a gap in pages at the beginnig or end
        /// </summary>
        public bool? ShowGap { get; set; }

        /// <summary>
        /// Show/hide First-Last buttons
        /// <para>default: true, if set to false and total pages > max displayed pages it will be true</para>
        /// </summary>
        public bool? ShowFirstLast { get; set; }

        /// <summary>
        /// Show/hide Previous-Next buttons
        /// <para>default: true</para>
        /// </summary>
        public bool? ShowPrevNext { get; set; }

        /// <summary>
        /// Show or hide total pages count
        /// <para>default: true</para>
        /// </summary>
        public bool? ShowTotalPages { get; set; }

        /// <summary>
        /// Show or hide total records count
        /// <para>default: true</para>
        /// </summary>
        public bool? ShowTotalRecords { get; set; }

        /// <summary>
        /// Show last numbered page when total pages count is larger than max displayed pages
        /// <para>default: true</para>
        /// </summary>
        [Obsolete("This property is deprected. Use ShowGap instead.")]
        public bool? ShowLastNumberedPage { get; set; }

        /// <summary>
        /// Show first numbered page when total pages count is larger than max displayed pages
        /// <para>default: true</para>
        /// </summary>
        [Obsolete("This property is deprected. Use ShowGap instead.")]
        public bool? ShowFirstNumberedPage { get; set; }

        #endregion

        #region Texts
        /// <summary>
        /// The text to display at page size dropdown list label
        /// <para>default: Page size </para>
        /// </summary>
        public string TextPageSize { get; set; }


        /// <summary>
        /// Text to show on the "Go To First" Page button
        /// <para>
        /// <![CDATA[default: &laquo;]]></para>
        /// </summary>
        public string TextFirst { get; set; }

        /// <summary>
        /// Text to show on "Go to last page" button
        /// <para>
        /// <![CDATA[default: &raquo;]]></para>
        /// </summary>
        public string TextLast { get; set; }

        /// <summary>
        /// Next button text
        /// <para>
        /// <![CDATA[default: &rsaquo;]]></para>
        /// </summary>
        public string TextNext { get; set; }

        /// <summary>
        /// previous button text
        /// <para>
        /// <![CDATA[default: &lsaquo;]]></para>
        /// </summary>
        public string TextPrevious { get; set; }

        /// <summary>
        /// Display text for total pages label
        /// <para>default: page</para>
        /// </summary>
        public string TextTotalPages { get; set; }

        /// <summary>
        /// Display text for total records label
        /// <para>default: records</para>
        /// </summary>
        public string TextTotalRecords { get; set; }

        /// <summary>
        /// The number display format for page numbers. Use a list of numbers splitted by space e.g. "0 1 2 3 4 5 6 7 8 9" or use one from a pre-defined numbers formats in :
        /// <see cref="LazZiya.TagHelpers.Utilities.NumberFormats"/>
        /// </summary>
        public string NumberFormat { get; set; }
        #endregion

        #region Screen Reader
        /// <summary>
        /// Text for screen readers only
        /// </summary>
        public string SrTextFirst { get; set; }

        /// <summary>
        /// text for screen readers only
        /// </summary>
        public string SrTextLast { get; set; }

        /// <summary>
        /// text for screenreaders only
        /// </summary>
        public string SrTextNext { get; set; }

        /// <summary>
        /// text for screen readers only
        /// </summary>
        public string SrTextPrevious { get; set; }

        #endregion

        #region Styling

        /// <summary>
        /// add custom class to content div
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// css class for pagination div
        /// </summary>
        public string ClassPagingControlDiv { get; set; }

        /// <summary>
        /// css class for page count/record count div
        /// </summary>
        public string ClassInfoDiv { get; set; }

        /// <summary>
        /// styling class for page size div
        /// </summary>
        public string ClassPageSizeDiv { get; set; }

        /// <summary>
        /// pagination control class
        /// <para>default: pagination</para>
        /// </summary>
        public string ClassPagingControl { get; set; }

        /// <summary>
        /// class name for the active page
        /// <para>default: active</para>
        /// <para>examples: disabled, active, ...</para>
        /// </summary>
        public string ClassActivePage { get; set; }

        /// <summary>
        /// name of the class when jumping button is disabled.
        /// jumping buttons are prev-next and first-last buttons
        /// <param>default: disabled</param>
        /// <para>example: disabled, d-hidden</para>
        /// </summary>
        public string ClassDisabledJumpingButton { get; set; }

        /// <summary>
        /// css class for total records info
        /// <para>default: badge badge-light</para>
        /// </summary>
        public string ClassTotalRecords { get; set; }

        /// <summary>
        /// css class for total pages info
        /// <para>default: badge badge-light</para>
        /// </summary>
        public string ClassTotalPages { get; set; }

        #endregion

        #region Ajax
        /// <summary>
        /// Set to true to use ajax pagination
        /// </summary>
        public bool Ajax { get; set; } = false;

        /// <summary>
        /// The message to display in a confirmation window before a request is submitted.
        /// </summary>
        public string AjaxConfirm { get; set; }

        /*
        /// <summary>
        /// The HTTP request method ("Get" or "Post").
        /// </summary>
        public AjaxMethod AjaxMethod { get; set; } = AjaxMethod.get;
        */

        /// <summary>
        ///  The mode that specifies how to insert the response into the target DOM element. Valid values are before, after and replace. Default is replace
        /// </summary>
        public PagingAjaxMode AjaxMode { get; set; } = PagingAjaxMode.replace;

        /// <summary>
        /// A value, in milliseconds, that controls the duration of the animation when showing or hiding the loading element.
        /// </summary>
        public int AjaxLoadingDuration { get; set; }

        /// <summary>
        /// The id attribute of an HTML element that is displayed while the Ajax function is loading. Default is #loading-spinner
        /// </summary>
        public string AjaxLoading { get; set; } = "#loading-spinner";

        /// <summary>
        /// The name of the JavaScript function to call immediately before the page is updated.
        /// </summary>
        public string AjaxBegin { get; set; }

        /// <summary>
        /// The JavaScript function to call when response data has been instantiated but before the page is updated.
        /// </summary>
        public string AjaxComplete { get; set; }

        /// <summary>
        /// The JavaScript function to call if the page update fails.
        /// </summary>
        public string AjaxFailure { get; set; }

        /// <summary>
        /// The JavaScript function to call after the page is successfully updated.
        /// </summary>
        public string AjaxSuccess { get; set; }

        /// <summary>
        /// The ID of the DOM element to update by using the response from the server.
        /// </summary>
        public string AjaxUpdate { get; set; }

        /// <summary>
        /// The URL to make the request to.
        /// </summary>
        public string AjaxUrl { get; set; }
        #endregion

        private int TotalPages => (int)Math.Ceiling(TotalRecords / (double)PageSize);

        private class Boundaries
        {
            public int Start { get; set; }
            public int End { get; set; }
        }

        /// <summary>
        /// process creating paging tag helper
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            SetDefaults();

            if (TotalPages > 0)
            {
                var pagingControl = new TagBuilder("ul");
                pagingControl.AddCssClass($"{ClassPagingControl}");

                // If ajax is anabled, create a dictionary of all ajax attributes
                if (Ajax)
                {
                    // Add loader element
                    output.PreElement.SetHtmlContent("<span id=\"loading-spinner\" style=\"display:none;\"><i class=\"fas fa-spinner fa-spin\"></i></span>");

                    AjaxAttributes = SetupAjaxAttributes();
                }

                // show-hide first-last buttons on user options
                if (ShowFirstLast == true)
                {
                    ShowFirstLast = true;
                }

                if (ShowFirstLast == true)
                {
                    var first = CreatePagingLink(1, TextFirst, SrTextFirst, ClassDisabledJumpingButton);
                    pagingControl.InnerHtml.AppendHtml(first);
                }

                if (ShowPrevNext == true)
                {
                    var prevPage = PageNo - 1 <= 1 ? 1 : PageNo - 1;
                    var prev = CreatePagingLink(prevPage, TextPrevious, SrTextPrevious, ClassDisabledJumpingButton);
                    pagingControl.InnerHtml.AppendHtml(prev);
                }

                if (MaxDisplayedPages == 1)
                {
                    var numTag = CreatePagingLink(PageNo, null, null, ClassActivePage);
                    pagingControl.InnerHtml.AppendHtml(numTag);
                }
                else if (MaxDisplayedPages > 1)
                {
                    // Boundaries are the start-end currently displayed pages
                    var boundaries = CalculateBoundaries(PageNo, TotalPages, MaxDisplayedPages.Value);

                    string gapStr = "<li class=\"page-item border-0\">&nbsp;...&nbsp;</li>";

                    if (ShowGap == true && boundaries.End > MaxDisplayedPages)
                    {
                        // add page no 1
                        var num1Tag = CreatePagingLink(1, null, null, ClassActivePage);
                        pagingControl.InnerHtml.AppendHtml(num1Tag);

                        // Add gap after first page
                        pagingControl.InnerHtml.AppendHtml(gapStr);
                    }

                    for (int i = boundaries.Start; i <= boundaries.End; i++)
                    {
                        var numTag = CreatePagingLink(i, null, null, ClassActivePage);
                        pagingControl.InnerHtml.AppendHtml(numTag);
                    }

                    if (ShowGap == true && boundaries.End < TotalPages)
                    {
                        // Add gap before last page
                        pagingControl.InnerHtml.AppendHtml(gapStr);

                        // add last page
                        var numLastTag = CreatePagingLink(TotalPages, null, null, ClassActivePage);
                        pagingControl.InnerHtml.AppendHtml(numLastTag);
                    }
                }

                if (ShowPrevNext == true)
                {
                    var nextPage = PageNo + 1 > TotalPages ? TotalPages : PageNo + 1;
                    var next = CreatePagingLink(nextPage, TextNext, SrTextNext, ClassDisabledJumpingButton);
                    pagingControl.InnerHtml.AppendHtml(next);
                }

                if (ShowFirstLast == true)
                {
                    var last = CreatePagingLink(TotalPages, TextLast, SrTextLast, ClassDisabledJumpingButton);
                    pagingControl.InnerHtml.AppendHtml(last);
                }

                var pagingControlDiv = new TagBuilder("div");
                pagingControlDiv.AddCssClass($"{ClassPagingControlDiv}");
                pagingControlDiv.InnerHtml.AppendHtml(pagingControl);

                output.TagName = "div";
                output.Attributes.SetAttribute("class", $"{Class}");
                output.Content.AppendHtml(pagingControlDiv);

                if (ShowPageSizeNav == true)
                {
                    var psDropdown = CreatePageSizeControl();

                    var psDiv = new TagBuilder("div");
                    psDiv.AddCssClass($"{ClassPageSizeDiv}");
                    psDiv.InnerHtml.AppendHtml(psDropdown);

                    output.Content.AppendHtml(psDiv);
                }

                if (ShowTotalPages == true || ShowTotalRecords == true)
                {
                    var infoDiv = AddDisplayInfo();

                    output.Content.AppendHtml(infoDiv);
                }

            }
        }

        private AttributeDictionary SetupAjaxAttributes()
        {
            if (string.IsNullOrWhiteSpace(AjaxUpdate))
                throw new ArgumentNullException(nameof(AjaxUpdate));

            if (string.IsNullOrWhiteSpace(AjaxUrl))
                throw new ArgumentNullException(nameof(AjaxUrl));

            var ajaxAttributes = new AttributeDictionary
                    {
                        { "data-ajax", "true" },
                        { "data-ajax-mode", $"{AjaxMode}" },
                        { "data-ajax-update", AjaxUpdate }
                    };

            if (!string.IsNullOrWhiteSpace(AjaxBegin))
                ajaxAttributes.Add("data-ajax-begin", AjaxBegin);

            if (!string.IsNullOrWhiteSpace(AjaxComplete))
                ajaxAttributes.Add("data-ajax-complete", AjaxComplete);

            if (!string.IsNullOrWhiteSpace(AjaxConfirm))
                ajaxAttributes.Add("data-ajax-confirm", AjaxConfirm);

            if (!string.IsNullOrWhiteSpace(AjaxFailure))
                ajaxAttributes.Add("data-ajax-failure", AjaxFailure);

            if (!string.IsNullOrWhiteSpace(AjaxLoading))
                ajaxAttributes.Add("data-ajax-loading", AjaxLoading);

            if (AjaxLoadingDuration > 0)
                ajaxAttributes.Add("data-ajax-loading-duration", $"{AjaxLoadingDuration}");

            if (!string.IsNullOrWhiteSpace(AjaxSuccess))
                ajaxAttributes.Add("data-ajax-success", AjaxSuccess);

            return ajaxAttributes;
        }

        /// <summary>
        /// This method will assign the values by checking three places
        /// 1- Property value if set from HTML code
        /// 2- Default values in appSettings.json
        /// 3- Hard coded default value in code
        /// </summary>
        private void SetDefaults()
        {
            var _settingsJson = SettingsJson ?? "default";

            _logger.LogInformation($"----> PagingTagHelper SettingsJson: {SettingsJson} - {_settingsJson}");

            MaxDisplayedPages = MaxDisplayedPages == null ? int.TryParse(Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:max-displayed-pages"], out int _dp) ? _dp : 10 : MaxDisplayedPages;

            PageSizeDropdownItems = PageSizeDropdownItems ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:page-size-dropdown-items"] ?? "10-25-50";

            QueryStringKeyPageNo = QueryStringKeyPageNo ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:query-string-key-page-no"] ?? "p";

            QueryStringKeyPageSize = QueryStringKeyPageSize ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:query-string-key-page-size"] ?? "s";

            ShowGap = ShowGap == null ?
                bool.TryParse(Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:show-gap"], out bool _sg) ? _sg : true : ShowGap;

            ShowFirstLast = ShowFirstLast == null ?
                bool.TryParse(Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:show-first-last"], out bool _sfl) ? _sfl : true : ShowFirstLast;

            ShowPrevNext = ShowPrevNext == null ? bool.TryParse(Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:show-prev-next"], out bool _sprn) ? _sprn : true : ShowPrevNext;

            ShowPageSizeNav = ShowPageSizeNav == null ? bool.TryParse(Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:show-page-size-nav"], out bool _spsn) ? _spsn : true : ShowPageSizeNav;

            ShowTotalPages = ShowTotalPages == null ? bool.TryParse(Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:show-total-pages"], out bool _stp) ? _stp : true : ShowTotalPages;

            ShowTotalRecords = ShowTotalRecords == null ? bool.TryParse(Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:show-total-records"], out bool _str) ? _str : true : ShowTotalRecords;

            NumberFormat = NumberFormat ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:number-format"] ?? NumberFormats.Default;

            TextPageSize = TextPageSize ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:text-page-size"];

            TextFirst = TextFirst ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:text-first"] ?? "&laquo;";

            TextLast = TextLast ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:text-last"] ?? "&raquo;";

            TextPrevious = TextPrevious ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:text-previous"] ?? "&lsaquo;";

            TextNext = TextNext ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:text-next"] ?? "&rsaquo;";

            TextTotalPages = TextTotalPages ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:text-total-pages"] ?? "pages";

            TextTotalRecords = TextTotalRecords ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:text-total-records"] ?? "records";

            SrTextFirst = SrTextFirst ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:sr-text-first"] ?? "First";

            SrTextLast = SrTextLast ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:sr-text-last"] ?? "Last";

            SrTextPrevious = SrTextPrevious ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:sr-text-previous"] ?? "Previous";

            SrTextNext = SrTextNext ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:sr-text-next"] ?? "Next";

            Class = Class ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:class"] ?? "row";

            ClassActivePage = ClassActivePage ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:class-active-page"] ?? "active";

            ClassDisabledJumpingButton = ClassDisabledJumpingButton ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:class-disabled-jumping-button"] ?? "disabled";

            ClassInfoDiv = ClassInfoDiv ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:class-info-div"] ?? "col-2";

            ClassPageSizeDiv = ClassPageSizeDiv ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:class-page-size-div"] ?? "col-1";

            ClassPagingControlDiv = ClassPagingControlDiv ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:class-paging-control-div"] ?? "col";

            ClassPagingControl = ClassPagingControl ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:class-paging-control"] ?? "pagination";

            ClassTotalPages = ClassTotalPages ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:class-total-pages"] ?? "badge badge-light";

            ClassTotalRecords = ClassTotalRecords ?? Configuration[$"lazziya:pagingTagHelper:{_settingsJson}:class-total-records"] ?? "badge badge-dark";

            _logger.LogInformation($"----> PagingTagHelper - " +
                $"{nameof(PageNo)}: {PageNo}, " +
                $"{nameof(PageSize)}: {PageSize}, " +
                $"{nameof(TotalRecords)}: {TotalRecords}, " +
                $"{nameof(TotalPages)}: {TotalPages}, " +
                $"{nameof(QueryStringKeyPageNo)}: {QueryStringKeyPageNo}, " +
                $"{nameof(QueryStringKeyPageSize)}: {QueryStringKeyPageSize}, " +
                $"");
        }

        private TagBuilder AddDisplayInfo()
        {
            var infoDiv = new TagBuilder("div");
            infoDiv.AddCssClass($"{ClassInfoDiv}");

            var txt = string.Empty;
            if (ShowTotalPages == true)
            {
                infoDiv.InnerHtml.AppendHtml($"<span class=\"{ClassTotalPages}\">{TotalPages.ToNumberFormat(NumberFormat)} {TextTotalPages}</span>");
            }

            if (ShowTotalRecords == true)
            {
                infoDiv.InnerHtml.AppendHtml($"<span class=\"{ClassTotalRecords}\">{TotalRecords.ToNumberFormat(NumberFormat)} {TextTotalRecords}</span>");
            }

            return infoDiv;
        }

        /// <summary>
        /// Calculate the boundaries of the currently rendered page numbers
        /// </summary>
        /// <param name="currentPageNo"></param>
        /// <param name="totalPages"></param>
        /// <param name="maxDisplayedPages"></param>
        /// <returns></returns>
        private Boundaries CalculateBoundaries(int currentPageNo, int totalPages, int maxDisplayedPages)
        {
            int _start, _end;

            int _gap = (int)Math.Ceiling(maxDisplayedPages / 2.0);

            if (maxDisplayedPages > totalPages)
                maxDisplayedPages = totalPages;

            if(totalPages == 1)
            {
                _start = _end = 1;
            }
            // << < 1 2 (3) 4 5 6 7 8 9 10 > >>
            else if (currentPageNo < maxDisplayedPages)
            {
                _start = 1;
                _end = maxDisplayedPages;
            }
            // << < 91 92 93 94 95 96 97 (98) 99 100 > >>
            else if (currentPageNo + maxDisplayedPages == totalPages)
            {
                _start = totalPages - maxDisplayedPages > 0 ? totalPages - maxDisplayedPages - 1 : 1;
                _end = totalPages - 2;
            }
            // << < 91 92 93 94 95 96 97 (98) 99 100 > >>
            else if (currentPageNo + maxDisplayedPages == totalPages + 1)
            {
                _start = totalPages - maxDisplayedPages > 0 ? totalPages - maxDisplayedPages : 1;
                _end = totalPages - 1;
            }
            // << < 91 92 93 94 95 96 97 (98) 99 100 > >>
            else if (currentPageNo + maxDisplayedPages > totalPages + 1)
            {
                _start = totalPages - maxDisplayedPages > 0 ? totalPages - maxDisplayedPages + 1 : 1;
                _end = totalPages;
            }

            // << < 21 22 23 34 (25) 26 27 28 29 30 > >>
            else
            {
                _start = currentPageNo - _gap > 0 ? currentPageNo - _gap + 1 : 1;
                _end = _start + maxDisplayedPages - 1;
            }

            return new Boundaries { Start = _start, End = _end };
        }

        private TagBuilder CreatePagingLink(int targetPageNo, string text, string textSr, string pClass)
        {
            var liTag = new TagBuilder("li");
            liTag.AddCssClass("page-item");

            var pageUrl = CreateUrlTemplate(targetPageNo, PageSize);

            var aTag = new TagBuilder("a");
            aTag.AddCssClass("page-link");
            aTag.Attributes.Add("href", pageUrl);

            // If no text provided for screen readers
            // use the actual page number
            if (string.IsNullOrWhiteSpace(textSr))
            {
                var pageNoText = targetPageNo.ToNumberFormat(NumberFormat);

                aTag.InnerHtml.Append($"{pageNoText}");
            }
            else
            {
                liTag.MergeAttribute("area-label", textSr);
                aTag.InnerHtml.AppendHtml($"<span area-hidden=\"true\">{text}</span>");
                aTag.InnerHtml.AppendHtml($"<span class=\"sr-only\">{textSr}</span>");
            }

            if (PageNo == targetPageNo)
            {
                liTag.AddCssClass($"{pClass}");
                aTag.Attributes.Add("tabindex", "-1");
                aTag.Attributes.Remove("href");
            }

            if (Ajax)
            {
                var ajaxUrl = pageUrl.Replace("?", $"{AjaxUrl}&");
                aTag.Attributes.Add("data-ajax-url", ajaxUrl);

                foreach (var att in AjaxAttributes)
                {
                    aTag.Attributes.Add(att.Key, att.Value);
                }
            }

            liTag.InnerHtml.AppendHtml(aTag);
            return liTag;
        }

        /// <summary>
        /// dropdown list for changing page size (items per page)
        /// </summary>
        /// <returns></returns>
        private TagBuilder CreatePageSizeControl()
        {
            var dropDownDiv = new TagBuilder("div");
            dropDownDiv.AddCssClass("dropdown");

            var dropDownBtn = new TagBuilder("button");
            dropDownBtn.AddCssClass("btn btn-light dropdown-toggle");
            dropDownBtn.Attributes.Add("type", "button");
            dropDownBtn.Attributes.Add("id", "pagingDropDownMenuBtn");
            dropDownBtn.Attributes.Add("data-toggle", "dropdown");
            dropDownBtn.Attributes.Add("aria-haspopup", "true");
            dropDownBtn.Attributes.Add("aria-expanded", "false");
            dropDownBtn.InnerHtml.Append(TextPageSize ?? $"{PageSize.ToNumberFormat(NumberFormat)}");

            var dropDownMenu = new TagBuilder("div");
            dropDownMenu.AddCssClass("dropdown-menu dropdown-menu-right");
            dropDownMenu.Attributes.Add("aria-labelledby", "pagingDropDownMenuBtn");

            var pageSizeDropdownItems = PageSizeDropdownItems.Split("-", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < pageSizeDropdownItems.Length; i++)
            {
                var n = int.Parse(pageSizeDropdownItems[i]);

                var pageUrl = $"{CreateUrlTemplate(1, n)}";

                var option = new TagBuilder("a");
                option.AddCssClass("dropdown-item");
                option.Attributes.Add("href", pageUrl);

                option.InnerHtml.Append($"{n.ToNumberFormat(NumberFormat)}");

                if (n == PageSize)
                    option.AddCssClass("active");

                if (Ajax)
                {
                    var ajaxUrl = pageUrl.Replace("?", $"{AjaxUrl}&");
                    option.Attributes.Add("data-ajax-url", ajaxUrl);

                    foreach (var att in AjaxAttributes)
                    {
                        option.Attributes.Add(att.Key, att.Value);
                    }
                }

                dropDownMenu.InnerHtml.AppendHtml(option);
            }

            dropDownDiv.InnerHtml.AppendHtml(dropDownBtn);
            dropDownDiv.InnerHtml.AppendHtml(dropDownMenu);

            return dropDownDiv;
        }

        /// <summary>
        /// edit the url for each page, so it navigates to its target page number
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private string CreateUrlTemplate(int pageNo, int pageSize)
        {
            var urlPath = ViewContext.HttpContext.Request.QueryString.Value.Replace($"{AjaxUrl}&", "");

            _logger.LogDebug($"----> Page No '{pageNo}', Page Size '{pageSize}', URL Path '{urlPath}'");

            string p = $"{QueryStringKeyPageNo}={pageNo}"; // CurrentPageNo query string parameter, default: p
            string s = $"{QueryStringKeyPageSize}={pageSize}"; // PageSize query string parameter, default: s

            var urlTemplate = urlPath.TrimStart('?').Split('&').ToList();

            // Remove xml request parameters from url list
            urlTemplate.Remove(urlTemplate.FirstOrDefault(x => x.StartsWith("X-Requested-With=")));
            urlTemplate.Remove(urlTemplate.FirstOrDefault(x => x.StartsWith("_=")));

            for (int i = 0; i < urlTemplate.Count; i++)
            {
                var q = urlTemplate[i];
                urlTemplate[i] =
                    q.StartsWith($"{QueryStringKeyPageNo}=", StringComparison.OrdinalIgnoreCase) ? p :
                    q.StartsWith($"{QueryStringKeyPageSize}=", StringComparison.OrdinalIgnoreCase) ? s :
                    q;
            }

            if (!urlTemplate.Any(x => x == p))
                urlTemplate.Add(p);

            if (!urlTemplate.Any(x => x == s))
                urlTemplate.Add(s);

            return "?" + string.Join("&", urlTemplate);
        }
    }
}