using System;

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
    [Obsolete("This property is deprected. Use redirect-to-url instead.")]
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
    /// choose render mode style,
    /// <para>classic: regular dropdown select list</para>
    /// <para>Bootstrap4: HTML5 div with Bootstrap4 support</para>
    /// </summary>
    public enum RenderMode
    {
        /// <summary>
        /// regular dropdown list
        /// </summary>
        Classic,

        /// <summary>
        /// HTML5 div with Bootstrap 4 support
        /// </summary>
        Bootstrap
    }

    internal class LanguageItem
    {
        public string Name { get; set; }
        public string DisplayText { get; set; }
        public string Url { get; set; }
    }
}
