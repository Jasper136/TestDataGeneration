using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using TestDataGeneration.Benchmarks.Configs;
using TestDataGeneration.UnitTests.Wiring;
using Xunit.Runners;

namespace TestDataGeneration.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
//[Config(typeof(DevConfig))]
[Config(typeof(TestRunSimulationConfig))]
public class UnitTestRunBenchmarks
{
    [BenchmarkCategory("GenerateOneTime"), Benchmark(Baseline = true)]
    public void NewAutoFaker_GenerateOneTime()
    {
        RunTestSimulations(typeof(TestRunSimulations.UsingNewAutoFaker.GenerateOneTime));
    }

    [BenchmarkCategory("GenerateOneTime"), Benchmark]
    public void ReusedAutoFaker_GenerateOneTime()
    {
        RunTestSimulations(typeof(TestRunSimulations.UsingReusedAutoFaker.GenerateOneTime));
    }

    [BenchmarkCategory("GenerateOneTime"), Benchmark]
    public void StaticAutoFaker_GenerateOneTime()
    {
        RunTestSimulations(typeof(TestRunSimulations.UsingStaticAutoFaker.GenerateOneTime));
    }

    [BenchmarkCategory("GenerateOneTime"), Benchmark]
    public void Some_GenerateOneTime()
    {
        RunTestSimulations(typeof(TestRunSimulations.UsingSome.GenerateOneTime));
    }


    [BenchmarkCategory("GenerateTenTimes"), Benchmark(Baseline = true)]
    public void NewAutoFaker_GenerateTenTimes()
    {
        RunTestSimulations(typeof(TestRunSimulations.UsingNewAutoFaker.GenerateTenTimes));
    }

    [BenchmarkCategory("GenerateTenTimes"), Benchmark]
    public void ReusedAutoFaker_GenerateTenTimes()
    {
        RunTestSimulations(typeof(TestRunSimulations.UsingReusedAutoFaker.GenerateTenTimes));
    }

    [BenchmarkCategory("GenerateTenTimes"), Benchmark]
    public void StaticAutoFaker_GenerateTenTimes()
    {
        RunTestSimulations(typeof(TestRunSimulations.UsingStaticAutoFaker.GenerateTenTimes));
    }

    [BenchmarkCategory("GenerateTenTimes"), Benchmark]
    public void Some_GenerateTenTimes()
    {
        RunTestSimulations(typeof(TestRunSimulations.UsingSome.GenerateTenTimes));
    }


    [BenchmarkCategory("GenerateHundredTimes"), Benchmark(Baseline = true)]
    public void NewAutoFaker_GenerateHundredTimes()
    {
        RunTestSimulations(typeof(TestRunSimulations.UsingNewAutoFaker.GenerateHundredTimes));
    }

    [BenchmarkCategory("GenerateHundredTimes"), Benchmark]
    public void ReusedAutoFaker_GenerateHundredTimes()
    {
        RunTestSimulations(typeof(TestRunSimulations.UsingReusedAutoFaker.GenerateHundredTimes));
    }

    [BenchmarkCategory("GenerateHundredTimes"), Benchmark]
    public void StaticAutoFaker_GenerateHundredTimes()
    {
        RunTestSimulations(typeof(TestRunSimulations.UsingStaticAutoFaker.GenerateHundredTimes));
    }

    [BenchmarkCategory("GenerateHundredTimes"), Benchmark]
    public void Some_GenerateHundredTimes()
    {
        RunTestSimulations(typeof(TestRunSimulations.UsingSome.GenerateHundredTimes));
    }

    private static void RunTestSimulations(Type assemblyMarkerClass)
    {
        //https://github.com/xunit/samples.xunit/blob/main/TestRunner/Program.cs

        var testAssembly = assemblyMarkerClass.Assembly;

        // Use an event to know when we're done
        ManualResetEvent finished = new ManualResetEvent(false);

        using var runner = AssemblyRunner.WithoutAppDomain(testAssembly.Location);
        runner.OnExecutionComplete = _ => finished.Set();
        runner.Start();
        //wait for the tests to complete
        finished.WaitOne();
    }
}

[Config(typeof(DevConfig))]
public class SanityCheck
{
    [Benchmark]
    public void UnitTestRun()
    {
        //https://github.com/xunit/samples.xunit/blob/main/TestRunner/Program.cs

        var testAssembly = typeof(TestBinder).Assembly;

        // Use an event to know when we're done
        ManualResetEvent finished = new ManualResetEvent(false);

        //run xunit tests with assembly name TestDataGeneration.UnitTests
        using (var runner = AssemblyRunner.WithoutAppDomain(testAssembly.Location))
        {
            runner.OnExecutionComplete = _ => finished.Set();
            runner.Start();
            //wait for the tests to complete
            //runner.OnDiscoveryComplete = info => runner.OnExecutionComplete = null;
            finished.WaitOne();


        }
    }
}