namespace CSF.Screenplay
{
    /// <summary>
    /// An object which can fully configure and get a <see cref="Screenplay"/> instance.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface is particularly important when using Screenplay as a testing tool.
    /// Some test integrations do not have any inherent extension points for the placement of 'configuration' or
    /// startup logic which affects the entire test run. In those cases, a developer will need to implement this
    /// interface with a class of their own, in order to configure and get the <see cref="Screenplay"/> instance.
    /// </para>
    /// <para>
    /// Types which implement this interface need only implement the <see cref="GetScreenplay"/> method, which should build
    /// and return a Screenplay instance. In almost all cases, developers are advised to use <see cref="ScreenplayBuilder"/>
    /// to build and return the Screenplay.
    /// </para>
    /// <para>
    /// Note that implementations of this type must have a public parameterless constructor, because they will be instantiated
    /// via <see cref="System.Activator.CreateInstance(System.Type)"/> and not resolved from dependency injection.
    /// </para>
    /// </remarks>
    public interface IGetsScreenplay
    {
        /// <summary>
        /// Gets the configured Screenplay instance provided by the current type.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Implementors should create and return a new <see cref="Screenplay"/> instance from this method; they are strongly urged
        /// to consider the use of <see cref="ScreenplayBuilder"/> for this purpose.
        /// As well as the creation of the Screenplay instance itself, they should also add to the service collection any
        /// services which relate to <xref href="AbilityGlossaryItem?text=abilities+which+could+be+used+in+the+Screenplay"/>.
        /// It is recommended to use <see cref="ScreenplayBuilder.ConfigureServices(System.Action{Microsoft.Extensions.DependencyInjection.IServiceCollection})"/>
        /// to accomplish this.
        /// </para>
        /// </remarks>
        /// <returns>A Screenplay instance</returns>
        Screenplay GetScreenplay();
    }
}

