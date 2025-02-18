using System.Globalization;
using Reqnroll.BoDi;

namespace ReqnrollPlugins.DependencyInjection.ToReqnroll.MsdiPlugin.Wrappers;

internal class FeatureContextWrapper(IFeatureContext originalContext): IFeatureContext
{
    public Exception TestError => originalContext.TestError;

    public FeatureInfo FeatureInfo => originalContext.FeatureInfo;

    public CultureInfo BindingCulture => originalContext.BindingCulture;

    public IObjectContainer FeatureContainer => originalContext.FeatureContainer;
}