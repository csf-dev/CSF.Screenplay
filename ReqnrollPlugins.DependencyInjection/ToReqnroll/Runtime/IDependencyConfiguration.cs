using Reqnroll.BoDi;

namespace Reqnroll;

public interface IDependencyConfiguration
{
    /// <summary>
    /// Tries to resolve the target type from the container.
    /// </summary>
    /// <param name="targetType">The type that is about to be resolved</param>
    /// <param name="lifetime">The lifetime scope</param>
    /// <param name="reqnrollContainer">The Reqnroll container of the requested lifetime scope</param>
    /// <param name="instance">The resolved instance in case of success</param>
    /// <returns><see langword="true" /> if the instance can be resolved; otherwise, <see langword="false" />.</returns>
    bool TryResolve(Type targetType, DependencyLifetime lifetime, IObjectContainer reqnrollContainer, out object instance);

    /// <summary>
    /// Returns an exception that describes why the resolution of the target type failed.
    /// The implementations can include concrete suggestions of how to complete the dependency configuration.
    /// </summary>
    /// <param name="targetType">The type that is about to be resolved</param>
    /// <param name="missingTypeName"></param>
    /// <param name="lifetime">The lifetime scope</param>
    /// <param name="reqnrollContainer">The Reqnroll container of the requested lifetime scope</param>
    /// <param name="innerException">The inner exception, if any</param>
    /// <returns>An exception to be thrown by the dependency management infrastructure</returns>
    Exception GetResolveError(Type targetType, string? missingTypeName, DependencyLifetime lifetime, IObjectContainer reqnrollContainer, Exception? innerException);
}