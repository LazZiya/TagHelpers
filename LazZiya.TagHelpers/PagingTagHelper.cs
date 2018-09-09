/*******************************************************
 * Copyright © 2018  Ziya Mollamahmut
 * http://www.ziya.info/en/21-Pagination_TagHelper_ASP_NET_Core_21
 * 
 * License: no restriction, just keep the credits note in place :)
 * 
 * *****************************************************/
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Linq;

namespace LazZiya.TagHelpers
{
    public class PagingTagHelper : TagHelper
    {
        /// <summary>
        /// current page number.
        /// <para>default: 1</para>
        /// <para>example: p=1</para>
        /// </summary>
        public int CurrentPageNo { get; set; } = 1;

        /// <summary>
        /// Query string paramter name for current page.
        /// <para>default: p</para>
        /// <para>exmaple: p=1</para>
        /// </summary>
        public string CurrentPageParam { get; set; } = "p";

        /// <summary>
        /// if count of pages is too much, restrict shown pages count to specific number
        /// <para>default: 10</para>
        /// </summary>
        public int MaxDisplayedPages { get; set; } = 10;

        /// <summary>
        /// how many items to get from db per page per request
        /// <para>default: 25</para>
        /// <para>example: pageSize=25</para>
        /// </summary>
        public int PageSize { get; set; } = 2;

        /// <summary>
        /// Query string parameter name for page size
        /// <para>default: s</para>
        /// <para>example: s=25</para>
        /// </summary>
        public string PageSizeParam { get; set; } = "s";

        /// <summary>
        /// Show drop down list for different page size options
        /// <para>default: false</para>
        /// <para>options: true, false</para>
        /// </summary>
        public bool ShowPageSizeNav { get; set; } = false;

        /// <summary>
        /// The text to display at page size dropdown list label
        /// <para>default: Show </para>
        /// </summary>
        public string PageSizeNavText { get; set; } = "Show ";

        /// <summary>
        /// Form submit method when selecting different page size option
        /// <para>default: get</param>
        /// <para>options: get, post</para>
        /// </summary>
        public string PageSizeNavFormMethod { get; set; } = "get";

        /// <summary>
        /// The minimum block size to populate all possible page sizes for dropdown list
        /// <para>default: 25</para>
        /// </summary>
        public int PageSizeNavBlock { get; set; } = 25;

        /// <summary>
        /// maximum nmber of items to show in the page size navigation menu
        /// <para>default: 5</para>
        /// </summary>
        public int PageSizeNavMaxItems { get; set; } = 5;

        /// <summary>
        /// action to take when page size dropdown changes
        /// <para>default: this.form.submit();</para>
        /// </summary>
        public string PageSizeNavOnChange { get; set; } = "this.form.submit();";

        /// <summary>
        /// styling class for page size div
        /// </summary>
        public string PageSizeDivClass { get; set; } = "col";

        /// <summary>
        /// css class for pagination div
        /// </summary>
        public string PagingControlDivClass { get; set; } = "col";

        /// <summary>
        /// css class for page count/record count div
        /// </summary>
        public string InfoDivClass { get; set; } = "col";

        /// <summary>
        /// Total count of records in the db
        /// <para>default: 0</para>
        /// </summary>
        public int TotalRecords { get; set; } = 0;

        /// <summary>
        /// Url path section starting from the ? including all next query string parameters 
        /// to consider for next pages links.
        /// <para>default: string.Empty</para>
        /// <para>example: ?p=1&s=20&filter=xyz</para>
        /// </summary>
        public string UrlPath { get; set; } = string.Empty;

        /// <summary>
        /// Text to show on the "Go To First" Page button
        /// <para>default: &laquo;</para>
        /// </summary>
        public string FirstText { get; set; } = "&laquo;";

        /// <summary>
        /// Text for screen readers only
        /// </summary>
        public string FirstTextSr { get; set; } = "First";

        /// <summary>
        /// Text to show on "Go to last page" button
        /// <para>default: &raquo;</para>
        /// </summary>
        public string LastText { get; set; } = "&raquo;";

        /// <summary>
        /// text for screen readers only
        /// </summary>
        public string LastTextSr { get; set; } = "Last";

        /// <summary>
        /// previous button text
        /// <para>default: &lsaquo;</para>
        /// </summary>
        public string PreviousText { get; set; } = "&lsaquo;";

        /// <summary>
        /// text for screen readers only
        /// </summary>
        public string PreviousTextSr { get; set; } = "Previous";

        /// <summary>
        /// Next button text
        /// <para>default: &rsaquo;</para>
        /// </summary>
        public string NextText { get; set; } = "&rsaquo;";

        /// <summary>
        /// text for screenreaders only
        /// </summary>
        public string NextTextSc { get; set; } = "Next";

        /// <summary>
        /// Show/hide First-Last buttons
        /// <para>default: true</para>
        /// </summary>
        public bool ShowFirstLast { get; set; } = true;

        /// <summary>
        /// Show/hide Previous-Next buttons
        /// <para>default: false</para>
        /// </summary>
        public bool ShowPrevNext { get; set; } = false;

        /// <summary>
        /// add custom class to content div
        /// </summary>
        public string Class { get; set; } = "row";

        /// <summary>
        /// pagination control class
        /// <para>default: pagination</para>
        /// </summary>
        public string PagingControlClass { get; set; } = "pagination";

        /// <summary>
        /// class name for the active page
        /// <para>default: active</para>
        /// <para>examples: disabled, active, ...</para>
        /// </summary>
        public string ActivePageClass { get; set; } = "active";

        /// <summary>
        /// name of the class when jumping button is disabled.
        /// jumping buttons are prev-next and first-last buttons
        /// <param>default: disabled</param>
        /// <para>example: disabled, d-hidden</para>
        /// </summary>
        public string DisabledJumpingButtonClass { get; set; } = "disabled";

        /// <summary>
        /// Show or hide total pages count
        /// <para>default: false</para>
        /// </summary>
        public bool ShowTotalPagesInfo { get; set; } = false;

        /// <summary>
        /// Display text for total pages label
        /// <para>default: page</para>
        /// </summary>
        public string TotalPagesInfoText { get; set; } = "page";

        /// <summary>
        /// css class for total pages info
        /// <para>default: badge badge-light</para>
        /// </summary>
        public string TotalPagesInfoClass { get; set; } = "badge badge-light";

        /// <summary>
        /// Show or hide total records count
        /// <para>default: false</para>
        /// </summary>
        public bool ShowTotalRecordsInfo { get; set; } = false;

        /// <summary>
        /// Display text for total records label
        /// <para>default: records</para>
        /// </summary>
        public string TotalRecordsInfoText { get; set; } = "record";

        /// <summary>
        /// css class for total records info
        /// <para>default: badge badge-light</para>
        /// </summary>
        public string TotalRecordsInfoClass { get; set; } = "badge badge-light";

        /// <summary>
        /// Show last numbered page when total pages count is larger than max displayed pages
        /// <para>default: false</para>
        /// </summary>
        public bool ShowLastNumberedPage { get; set; } = false;

        /// <summary>
        /// Show first numbered page when total pages count is larger than max displayed pages
        /// <para>default: false</para>
        /// </summary>
        public bool ShowFirstNumberedPage { get; set; } = false;

        /// <summary>
        /// Gap size to start show first/last numbered page
        /// <para>default: 5</para>
        /// </summary>
        public int GapSize { get; set; } = 3;

        private int TotalPages => (int)Math.Ceiling(TotalRecords / (double)PageSize);

        public string DisabledJumpingButtonClass1 => DisabledJumpingButtonClass;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (TotalPages > 0)
            {
                var pagingControl = new TagBuilder("ul");
                pagingControl.AddCssClass($"{PagingControlClass}");

                if (ShowFirstLast)
                {
                    var first = CreatePagingLink(1, FirstText, FirstTextSr, DisabledJumpingButtonClass);
                    pagingControl.InnerHtml.AppendHtml(first);
                }

                if (ShowPrevNext)
                {
                    var prevPage = CurrentPageNo - 1 <= 1 ? 1 : CurrentPageNo - 1;
                    var prev = CreatePagingLink(prevPage, PreviousText, PreviousTextSr, DisabledJumpingButtonClass);
                    pagingControl.InnerHtml.AppendHtml(prev);
                }

                int start = 1;
                int end = MaxDisplayedPages;

                (start, end) = CalculateBoundaries(CurrentPageNo, TotalPages, MaxDisplayedPages);

                if (ShowFirstNumberedPage
                    && start > GapSize
                    && TotalPages > MaxDisplayedPages
                    && CurrentPageNo >= MaxDisplayedPages)
                {
                    var numTag = CreatePagingLink(1, null, null, ActivePageClass);
                    pagingControl.InnerHtml.AppendHtml(numTag);

                    var gap = new TagBuilder("li");
                    gap.AddCssClass("page-item border-0");
                    gap.InnerHtml.AppendHtml("&nbsp;...&nbsp;");
                    pagingControl.InnerHtml.AppendHtml(gap);
                }


                for (int i = start; i <= end; i++)
                {
                    var numTag = CreatePagingLink(i, null, null, ActivePageClass);
                    pagingControl.InnerHtml.AppendHtml(numTag);
                }

                if (ShowLastNumberedPage
                    && TotalPages - end >= GapSize
                    && CurrentPageNo - GapSize <= TotalPages - MaxDisplayedPages)
                {
                    var gap = new TagBuilder("li");
                    gap.AddCssClass("page-item border-0");
                    gap.InnerHtml.AppendHtml("&nbsp;...&nbsp;");
                    pagingControl.InnerHtml.AppendHtml(gap);

                    var numTag = CreatePagingLink(TotalPages, null, null, ActivePageClass);
                    pagingControl.InnerHtml.AppendHtml(numTag);
                }

                if (ShowPrevNext)
                {
                    var nextPage = CurrentPageNo + 1 > TotalPages ? TotalPages : CurrentPageNo + 1;
                    var next = CreatePagingLink(nextPage, NextText, NextTextSc, DisabledJumpingButtonClass);
                    pagingControl.InnerHtml.AppendHtml(next);
                }

                if (ShowFirstLast)
                {
                    var last = CreatePagingLink(TotalPages, LastText, LastTextSr, DisabledJumpingButtonClass);
                    pagingControl.InnerHtml.AppendHtml(last);
                }

                var pagingControlDiv = new TagBuilder("div");
                pagingControlDiv.AddCssClass($"{PagingControlDivClass}");
                pagingControlDiv.InnerHtml.AppendHtml(pagingControl);

                output.TagName = "div";
                output.Attributes.SetAttribute("class", $"{Class}");
                output.Content.AppendHtml(pagingControlDiv);

                if (ShowTotalPagesInfo || ShowTotalRecordsInfo)
                {
                    var infoDiv = new TagBuilder("div");
                    infoDiv.AddCssClass($"{InfoDivClass}");

                    if (ShowTotalPagesInfo)
                    {
                        var totalPagesInfo = AddDisplayInfo(TotalPages, TotalPagesInfoText, TotalPagesInfoClass);
                        infoDiv.InnerHtml.AppendHtml(totalPagesInfo);
                    }

                    if (ShowTotalRecordsInfo)
                    {
                        var totalRecordsInfo = AddDisplayInfo(TotalRecords, TotalRecordsInfoText, TotalRecordsInfoClass);
                        infoDiv.InnerHtml.AppendHtml(totalRecordsInfo);
                    }

                    output.Content.AppendHtml(infoDiv);
                }

                if (ShowPageSizeNav)
                {
                    var psDropdown = CreatePageSizeControl();

                    var psDiv = new TagBuilder("div");
                    psDiv.AddCssClass($"{PageSizeDivClass}");
                    psDiv.InnerHtml.AppendHtml(psDropdown);

                    output.Content.AppendHtml(psDiv);
                }
            }
        }

        private TagBuilder AddDisplayInfo(int count, string itemName, string cssClassName)
        {
            var span = new TagBuilder("span");
            span.AddCssClass($"{cssClassName}");
            span.InnerHtml.AppendHtml($"{count.ToString("N0")} {itemName}");

            return span;
        }

        private (int start, int end) CalculateBoundaries(int currentPageNo, int totalPages, int maxDisplayedPages)
        {
            var _start = 1;
            var _end = maxDisplayedPages;
            var _gap = (int)Math.Ceiling(maxDisplayedPages / 2.0);

            if (maxDisplayedPages > totalPages)
                maxDisplayedPages = totalPages;

            // << < 1 2 (3) 4 5 6 7 8 9 10 > >>
            if (currentPageNo < maxDisplayedPages)
            {
                _start = 1;
                _end = maxDisplayedPages;
            }

            // << < 91 92 93 94 95 96 97 (98) 99 100 > >>
            else if (currentPageNo + maxDisplayedPages > totalPages)
            {
                _start = totalPages - maxDisplayedPages > 0 ? totalPages - maxDisplayedPages : 1;
                _end = totalPages;
            }

            // << < 21 22 23 34 (25) 26 27 28 29 30 > >>
            else
            {
                _start = currentPageNo - _gap > 0 ? currentPageNo - _gap : 1;
                _end = _start + maxDisplayedPages;
            }

            return (_start, _end);
        }

        private TagBuilder CreatePagingLink(int targetPageNo, string text, string textSr, string pClass)
        {
            var liTag = new TagBuilder("li");
            liTag.AddCssClass("page-item");

            var aTag = new TagBuilder("a");
            aTag.AddCssClass("page-link");
            aTag.Attributes.Add("href", CreateUrlTemplate(targetPageNo, PageSize, UrlPath));

            if (string.IsNullOrWhiteSpace(textSr))
            {
                aTag.InnerHtml.Append($"{targetPageNo}");
            }
            else
            {
                liTag.MergeAttribute("area-label", textSr);
                aTag.InnerHtml.AppendHtml($"<span area-hidden=\"true\">{text}</span>");
                aTag.InnerHtml.AppendHtml($"<span class=\"sr-only\">{textSr}</span>");
            }

            if (CurrentPageNo == targetPageNo)
            {
                liTag.AddCssClass($"{pClass}");
                aTag.Attributes.Add("tabindex", "-1");
                aTag.Attributes.Remove("href");
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
            var dropDown = new TagBuilder("select");
            dropDown.AddCssClass($"form-control");
            dropDown.Attributes.Add("name", PageSizeParam);
            dropDown.Attributes.Add("onchange", $"{PageSizeNavOnChange}");

            for (int i = 1; i <= PageSizeNavMaxItems; i++)
            {
                var option = new TagBuilder("option");
                option.InnerHtml.AppendHtml($"{i * PageSizeNavBlock}");

                if ((i * PageSizeNavBlock) == PageSize)
                    option.Attributes.Add("selected", "selected");

                dropDown.InnerHtml.AppendHtml(option);
            }


            var fGroup = new TagBuilder("div");
            fGroup.AddCssClass("form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", "pageSizeControl");
            label.InnerHtml.AppendHtml($"{PageSizeNavText}&nbsp;");
            fGroup.InnerHtml.AppendHtml(label);
            fGroup.InnerHtml.AppendHtml(dropDown);

            var form = new TagBuilder("form");
            form.AddCssClass("form-inline");
            form.Attributes.Add("method", PageSizeNavFormMethod);
            form.InnerHtml.AppendHtml(fGroup);

            return form;
        }

        /// <summary>
        /// edit the url for each page, so it navigates to its target page number
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="urlPath"></param>
        /// <returns></returns>
        private string CreateUrlTemplate(int pageNo, int pageSize, string urlPath)
        {
            string p = $"{CurrentPageParam}={pageNo}"; // CurrentPageNo query string parameter, default: p
            string s = $"{PageSizeParam}={pageSize}"; // PageSize query string parameter, default: s

            var urlTemplate = urlPath.TrimStart('?').Split('&').ToList();

            for (int i = 0; i < urlTemplate.Count; i++)
            {
                var q = urlTemplate[i];
                urlTemplate[i] =
                    q.StartsWith($"{CurrentPageParam}=") ? p :
                    q.StartsWith($"{PageSizeParam}=") ? s :
                    q;
            }

            if (!urlTemplate.Any(x => x == p))
                urlTemplate.Add(p);

            if (!urlTemplate.Any(x => x == s))
                urlTemplate.Add(s);

            return "?" + string.Join('&', urlTemplate);
        }
    }
}