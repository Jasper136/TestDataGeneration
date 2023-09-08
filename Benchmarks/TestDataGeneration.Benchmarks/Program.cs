using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Bogus.Platform;
using TestDataGeneration.Benchmarks;

Console.WriteLine("Hello, World!");
var summary = BenchmarkRunner.Run<UnitTestRunBenchmarks>();
//var summary = BenchmarkRunner.Run<SanityCheck>();
//var summary = BenchmarkRunner.Run(typeof(Program).GetAssembly());
//var summary = BenchmarkRunner.Run(
//    typeof(Program).GetAssembly(),
//    ManualConfig.Create(DefaultConfig.Instance)
//        .WithOptions(ConfigOptions.JoinSummary | ConfigOptions.DisableLogFile)
//        .AddExporter(MarkdownExporter.GitHub)
//        .AddJob(Job.MediumRun));
Console.WriteLine("Bye, World!");