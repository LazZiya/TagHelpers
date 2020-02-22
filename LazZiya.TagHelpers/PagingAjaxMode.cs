namespace LazZiya.TagHelpers
{
    /// <summary>
    /// ajax update modes
    /// </summary>
    public enum PagingAjaxMode
    {
        /// <summary>
        /// update before target content
        /// </summary>
        before,

        /// <summary>
        /// update after target content
        /// </summary>
        after,

        /// <summary>
        /// replace target content
        /// </summary>
        replace
    }

    /// <summary>
    /// The http request method
    /// </summary>
    public enum AjaxMethod
    {
        /// <summary>
        /// get request
        /// </summary>
        get,

        /// <summary>
        /// post request
        /// </summary>
        post
    }
}
