using System.Text.RegularExpressions;
using Reqnroll.BoDi;
using Reqnroll.Infrastructure;

namespace ReqnrollPlugins.ReqnrollDi.ToReqnroll.Runtime.BoDi;

public abstract class ReqnrollDiDependencyConfiguration(ReqnrollDiDependencyConfigurationOptions options) : IDependencyConfiguration
{
    protected ReqnrollDiDependencyConfiguration() : this(new ReqnrollDiDependencyConfigurationOptions())
    {
    }

    public ReqnrollDiDependencyConfigurationOptions Options { get; } = options;

    #region Customization methods for end users

    /// <summary>
    /// Allows to customize the container for the scenario scope.
    /// </summary>
    /// <param name="scenarioContainer">The Reqnroll scenario container</param>
    protected abstract void SetupScenarioScope(IObjectContainer scenarioContainer);

    /// <summary>
    /// Allows to customize the container for the feature scope.
    /// </summary>
    /// <param name="featureContainer">The Reqnroll feature container</param>
    protected virtual void SetupFeatureScope(IObjectContainer featureContainer)
    {
        //nop
    }

    /// <summary>
    /// Allows to customize the container for the worker scope.
    /// </summary>
    /// <param name="workerContainer">The Reqnroll worker container</param>
    protected virtual void SetupWorkerScope(IObjectContainer workerContainer)
    {
        //nop
    }

    /// <summary>
    /// Allows to customize the container for the test run scope.
    /// </summary>
    /// <param name="testRunContainer">The Reqnroll test-run container</param>
    protected virtual void SetupTestRunScope(IObjectContainer testRunContainer)
    {
        //nop
    }

    #endregion

    public bool TryResolve(Type targetType, DependencyLifetime lifetime, IObjectContainer reqnrollContainer, out object instance)
    {
        var lifetimeScope = GetLifetimeScope(lifetime, reqnrollContainer);
        return lifetimeScope.TryResolve(targetType, out instance);
    }

    public Exception GetResolveError(Type targetType, string? missingTypeName, DependencyLifetime lifetime, IObjectContainer reqnrollContainer, Exception? innerException)
    {
        string? GetMissingTypeNameFromException(Exception? exception)
        {
            if (exception is not ObjectContainerException || string.IsNullOrEmpty(exception.Message))
                return null;
            var match = Regex.Match(exception.Message, @"Interface cannot be resolved: (?<typeName>\S*)");
            if (!match.Success)
                return null;
            return match.Groups["typeName"].Value;
        }

        missingTypeName ??= GetMissingTypeNameFromException(innerException) ?? "interface-type";
        var instructions = $"To configure this service for {lifetime} scope, please " +
                           $"add 'container.RegisterTypeAs<your-implementation-type, {missingTypeName}>()'" +
                           $" to the 'Setup{lifetime}Scope' method of your dependency configuration.";
        return new InvalidOperationException($"Could not resolve {targetType} for {lifetime} with the Reqnroll built-in dependency framework. {instructions}", innerException);
    }

    private IObjectContainer GetLifetimeScope(DependencyLifetime lifetime, IObjectContainer reqnrollContainer)
    {
        if (reqnrollContainer.TryResolveIfExplicitlyRegistered<ScopeSetupMarker>(out _, lifetime.GetName()))
            return reqnrollContainer;

        lock (reqnrollContainer)
        {
            if (reqnrollContainer.TryResolveIfExplicitlyRegistered<ScopeSetupMarker>(out _, lifetime.GetName()))
                return reqnrollContainer;

            if (lifetime != DependencyLifetime.TestRun)
            {
                // making sure that the parent container is set up
                GetLifetimeScope(lifetime - 1, reqnrollContainer.GetBaseContainer());
            }

            SetupScopeContainer(lifetime, reqnrollContainer);

            // We register a marker so that we know that the user's setup has been done.
            reqnrollContainer.RegisterInstanceAs(new ScopeSetupMarker(), lifetime.GetName());
            return reqnrollContainer;
        }
    }

    private void SetupScopeContainer(DependencyLifetime lifetime, IObjectContainer reqnrollContainer)
    {
        switch (lifetime)
        {
            case DependencyLifetime.Scenario:
                SetupScenarioScope(reqnrollContainer);
                break;
            case DependencyLifetime.Feature:
                SetupFeatureScope(reqnrollContainer);
                break;
            case DependencyLifetime.Worker:
                SetupWorkerScope(reqnrollContainer);
                break;
            case DependencyLifetime.TestRun:
                SetupTestRunScope(reqnrollContainer);
                break;
        }
    }

    #region Helper classes
    /// <summary>
    /// Tracks the setup of scopes
    /// </summary>
    class ScopeSetupMarker;
    #endregion
}