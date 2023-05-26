using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Bogus;
using Bogus.Platform;
using TestDataGeneration;
using TestDataGeneration.Benchmarks;
using TestDataGeneration.DemoDomain;

Console.WriteLine("Hello, World!");
//var summary = BenchmarkRunner.Run<CustomTypeGenerationBenchmarks>();
//var summary = BenchmarkRunner.Run(typeof(Program).GetAssembly());
var summary = BenchmarkRunner.Run(
    typeof(Program).GetAssembly(),
    ManualConfig.Create(DefaultConfig.Instance)
        .WithOptions(ConfigOptions.JoinSummary | ConfigOptions.DisableLogFile)
        .AddExporter(MarkdownExporter.GitHub)
        .AddJob(Job.ShortRun));
Console.WriteLine("Bye, World!");

////Benchmark comparing Some.Generated<Article>(count) vs Faker<Article>().Generate(count) vs reused Faker<Article>().Generate(count)
//public class SomeGeneratedCountVsFakerGenerateCountVsReusedFakerGenerateCount
//{
//    private readonly Faker<Article> _faker = new Faker<Article>();

//    [Benchmark]
//    public List<Article> SomeGeneratedArticles() => Some.Generated<Article>(10);

//    [Benchmark]
//    public List<Article> FakerGenerateArticles() => new Faker<Article>().Generate(10);

//    [Benchmark]
//    public List<Article> ReusedFakerGenerateArticles() => _faker.Generate(10);
//}