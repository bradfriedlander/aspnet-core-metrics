using Microsoft.AspNetCore.Razor.TagHelpers;

namespace demoWebApp.TagHelpers
{
    /// <summary>
    ///     This class implements the "asp-if" tag helper.
    /// </summary>
    /// <seealso cref="TagHelper" />
    [HtmlTargetElement(Attributes = "asp-if")]
    public class IfTagHelper : TagHelper
    {
        /// <summary>
        ///     Gets or sets a value indicating whether the content should be rendered.
        /// </summary>
        /// <value><c>true</c> if the content should be rendered; otherwise, <c>false</c>.</value>
        [HtmlAttributeName("asp-if")]
        public bool ContentShouldBeRendered { get; set; } = true;

        /// <summary>
        ///     When a set of <see cref="ITagHelper" /> s are executed, their <see cref="TagHelper.Init(TagHelperContext)" />'s are first invoked in
        ///     the specified <see cref="TagHelper.Order" />; then their <see cref="TagHelper.ProcessAsync(TagHelperContext,TagHelperOutput)" />'s are
        ///     invoked in the specified <see cref="TagHelper.Order" />. Lower values are executed first.
        /// </summary>
        /// <remarks>Default order is <c>0</c>.</remarks>
        public override int Order => int.MinValue;

        /// <summary>
        ///     Synchronously executes the <see cref="TagHelper" /> with the given <paramref name="context" /> and <paramref name="output" />.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!ContentShouldBeRendered)
            {
                output.TagName = null;
                output.SuppressOutput();
            }
            output.Attributes.RemoveAll("asp-if");
        }
    }
}
