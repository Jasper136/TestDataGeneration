using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDataGeneration.DemoDomain;
using TestDataGeneration.UnitTests.Wiring;
using Xunit.Runners;

namespace TestDataGeneration.Benchmarks;

public class UnitTestRunBenchmarks
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