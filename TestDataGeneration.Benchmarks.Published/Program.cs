using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Bogus.Platform;
using TestDataGeneration.Benchmarks.Published;

Console.WriteLine("Hello, Published World!");
var summary = BenchmarkRunner.Run<IntGenerationBenchmarks>();
//var summary = BenchmarkRunner.Run(typeof(Program).GetAssembly());
Console.WriteLine("Bye, Published World!");


//Config class to run benchmarks for different nuget package versions
public class PackageVersionsConfig : ManualConfig
{
    public PackageVersionsConfig()
    {
        AddJob(Job.Default.WithNuGet("Some.TestDataGeneration", "0.1.0").WithId("v0.1.0"));
        AddJob(Job.Default.WithNuGet("Some.TestDataGeneration", "0.2.0").WithId("v0.2.0"));
    }
}   