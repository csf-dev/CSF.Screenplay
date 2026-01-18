using CSF.Screenplay;
#if SPECFLOW
using TechTalk.SpecFlow.Plugins;
#else
using Reqnroll.Plugins;
#endif

[assembly: RuntimePlugin(typeof(ScreenplayPlugin))]
