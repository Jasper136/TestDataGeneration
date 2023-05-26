using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Bogus.Platform;
using TestDataGeneration.Benchmarks.Published;

Console.WriteLine("Hello, Published World!");
//var summary = BenchmarkRunner.Run<CustomTypesGenerationBenchmarks>();
var summary = BenchmarkRunner.Run(
    typeof(Program).GetAssembly(),
    ManualConfig.Create(DefaultConfig.Instance)
        .WithOptions(ConfigOptions.JoinSummary | ConfigOptions.DisableLogFile)
        .AddExporter(MarkdownExporter.GitHub)
        .AddJob(Job.ShortRun));
Console.WriteLine("Bye, Published World!");

//Config class to run benchmarks for different nuget package versions
public class PackageVersionsConfig : ManualConfig
{
    public PackageVersionsConfig()
    {
        AddJob(Job.ShortRun.WithNuGet("Some.TestDataGeneration", "0.1.0").WithId("v0.1.0").AsBaseline());
        AddJob(Job.ShortRun.WithNuGet("Some.TestDataGeneration", "0.2.0").WithId("v0.2.0"));
        AddJob(Job.ShortRun.WithNuGet("Some.TestDataGeneration", "0.3.0").WithId("v0.3.0"));
    }
}

