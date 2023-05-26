using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using TestDataGeneration.DemoDomain;

namespace TestDataGeneration.Benchmarks.Published;

[Config(typeof(PackageVersionsConfig))]
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
