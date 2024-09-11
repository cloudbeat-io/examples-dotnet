using NUnit.Framework;
[assembly: LevelOfParallelism(3)]
[assembly: FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[assembly: Parallelizable(ParallelScope.Fixtures)]  // ParallelScope.Children is currently not supported