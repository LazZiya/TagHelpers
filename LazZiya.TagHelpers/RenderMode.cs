namespace LazZiya.TagHelpers
{
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
        Classic = 0,

        /// <summary>
        /// HTML5 div with Bootstrap 4 support
        /// </summary>
        Bootstrap = 1,

        /// <summary>
        /// Render as form control
        /// </summary>
        FormControl = 2,
        /// <summary>
        /// HTML5 div with Bootstrap 5 support
        /// </summary>
        Bootstrap5 = 3
    }
}
