using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// A builder type to create a performable action which types text (sends keys) to a target element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Like all builders in this namespace, this class is not to be instantiated directly.
    /// Instead, use the <see cref="PerformableBuilder.EnterTheText(string[])"/> method.
    /// </para>
    /// </remarks>
    public class SendKeysBuilder
    {
        readonly string keysToSend;

        /// <summary>
        /// Gets a performable action which sends/types the already-specified text to the specified target element.
        /// </summary>
        /// <param name="target">The target element to which the keys will be sent.</param>
        /// <returns>A performable action</returns>
        public IPerformable Into(ITarget target) => SingleElementPerformableAdapter.From(new SendKeys(keysToSend), target);

        /// <summary>
        /// Initializes a new instance of the <see cref="SendKeysBuilder"/> class.
        /// </summary>
        /// <param name="keysToSend">The text/keys to send.</param>
        internal SendKeysBuilder(string keysToSend)
        {
            this.keysToSend = keysToSend ?? throw new System.ArgumentNullException(nameof(keysToSend));
        }
    }
}