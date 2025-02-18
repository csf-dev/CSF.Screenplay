namespace ReqnrollPlugins.Autofac.StepDefinitions;

public interface IDependencyTester
{
    int Use();
}

public abstract class DependencyTester : IDisposable
{
    private static int _objectCounter = 0;

    private bool _disposed;
    private int _usageCounter;

    protected DependencyTester()
    {
        Interlocked.Increment(ref _objectCounter);
    }

    public int Use()
    {
        return Interlocked.Increment(ref _usageCounter);
    }

    public void Dispose()
    {
        lock (this)
        {
            if (_disposed)
                throw new InvalidOperationException($"The dependency {GetType().Name} has been disposed already");
            _disposed = true;
            Interlocked.Decrement(ref _objectCounter);
        }
    }

    public static int ObjectCounter => _objectCounter;
}

public interface IScenarioDependency : IDependencyTester;
public class ScenarioDependency : DependencyTester, IScenarioDependency;

public interface IFeatureDependency : IDependencyTester;
public class FeatureDependency : DependencyTester, IFeatureDependency;

public interface IWorkerDependency : IDependencyTester;
public class WorkerDependency : DependencyTester, IWorkerDependency;

public interface ITestRunDependency : IDependencyTester;
public class TestRunDependency : DependencyTester, ITestRunDependency;
