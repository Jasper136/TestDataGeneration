using AutoBogus;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using Bogus;
using TestDataGeneration.DemoDomain;

namespace TestDataGeneration.Benchmarks;

//class Config : ManualConfig
//{
//    public Config()
//    {
//        AddExporter(MarkdownExporter.GitHub);
//        AddJob(Job.MediumRun);
//    }
//}

//[Config(typeof(Config))]
//[GenericTypeArguments(typeof(Article))]
//[GenericTypeArguments(typeof(Customer))]
//[GenericTypeArguments(typeof(Order))]
//public class CustomTypesGenerationBenchmarks<T> where T : class
//{
//    private readonly Faker<T> _orderFaker = new Faker<T>();
//    [Benchmark]
//    public T ReusedFakerGenerate()
//    {
//        return _orderFaker.Generate();
//    }

//    [Benchmark(Baseline = true)]
//    public Order NewFakerGenerate()
//    {
//        return new Faker<Order>().Generate();
//    }

//    private readonly AutoFaker<T> _orderAutoFaker = new AutoFaker<T>();
//    [Benchmark]
//    public T ReusedAutoFakerGenerate()
//    {
//        return _orderAutoFaker.Generate();
//    }

//    [Benchmark]
//    public T NewAutoFakerGenerate()
//    {
//        return new AutoFaker<T>().Generate();
//    }

//    [Benchmark]
//    public T AutoFakerGenerate()
//    {
//        return AutoFaker.Generate<T>();
//    }

//    [Benchmark]
//    public T SomeGenerated()
//    {
//        return Some.Generated<T>();
//    }

//}

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class CustomTypesGenerationBenchmarks
{
    [BenchmarkCategory(nameof(Article)), Benchmark]
    public Article SomeGeneratedArticle()
    {
        return Some.Generated<Article>();
    }

    [BenchmarkCategory(nameof(Customer)), Benchmark]
    public Customer SomeGeneratedCustomer()
    {
        return Some.Generated<Customer>();
    }

    [BenchmarkCategory(nameof(Order)), Benchmark]
    public Order SomeGeneratedOrder()
    {
        return Some.Generated<Order>();
    }
}
