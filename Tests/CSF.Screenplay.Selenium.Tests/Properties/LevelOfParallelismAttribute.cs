/* If we are running with the BrowserStack SDK enabled then we must switch off parallelism in NUnit,
 * or else the two mechanisms 'trample over' one another and we end up receiving very confusing
 * test runs, with many duplicate tests.
 */

#if NO_NUNIT_PARALLELISM
[assembly:LevelOfParallelism(1)]
#endif
