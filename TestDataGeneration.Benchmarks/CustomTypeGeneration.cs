using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Bogus;
using TestDataGeneration.DemoDomain;

namespace TestDataGeneration.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class CustomTypeGenerationBenchmarks
{
    private readonly Faker<Article> _articleFaker = new Faker<Article>();
    [BenchmarkCategory(nameof(Article)), Benchmark]
    public Article ReusedFakerGenerateArticle()
    {
        return _articleFaker.Generate();
    }

    [BenchmarkCategory(nameof(Article)), Benchmark(Baseline = true)]
    public Article NewFakerGenerateArticle()
    {
        return new Faker<Article>().Generate();
    }

    [BenchmarkCategory(nameof(Article)), Benchmark]
    public Article SomeGeneratedArticle()
    {
        return Some.Generated<Article>();
    }


    private readonly Faker<Customer> _customerFaker = new Faker<Customer>();
    [BenchmarkCategory(nameof(Customer)), Benchmark]
    public Customer ReusedFakerGenerateCustomer()
    {
        return _customerFaker.Generate();
    }

    [BenchmarkCategory(nameof(Customer)), Benchmark(Baseline = true)]
    public Customer NewFakerGenerateCustomer()
    {
        return new Faker<Customer>().Generate();
    }

    [BenchmarkCategory(nameof(Customer)), Benchmark]
    public Customer SomeGeneratedCustomer()
    {
        return Some.Generated<Customer>();
    }


    private readonly Faker<Order> _orderFaker = new Faker<Order>();
    [BenchmarkCategory(nameof(Order)), Benchmark]
    public Order ReusedFakerGenerateOrder()
    {
        return _orderFaker.Generate();
    }

    [BenchmarkCategory(nameof(Order)), Benchmark(Baseline = true)]
    public Order NewFakerGenerateOrder()
    {
        return new Faker<Order>().Generate();
    }

    [BenchmarkCategory(nameof(Order)), Benchmark]
    public Order SomeGeneratedOrder()
    {
        return Some.Generated<Order>();
    }
}

public class ArticleGenerationBenchmarks
{
    private readonly Faker<Article> _faker = new Faker<Article>();
    [Benchmark]
    public Article ReusedFakerGenerateArticle()
    {
        return _faker.Generate();
    }

    [Benchmark(Baseline = true)]
    public Article NewFakerGenerateArticle()
    {
        return new Faker<Article>().Generate();
    }

    [Benchmark]
    public Article SomeGeneratedArticle()
    {
        return Some.Generated<Article>();
    }
}

public class CustomerGenerationBenchmarks
{
    private readonly Faker<Customer> _faker = new Faker<Customer>();
    [Benchmark]
    public Customer ReusedFakerGenerateCustomer()
    {
        return _faker.Generate();
    }

    [Benchmark(Baseline = true)]
    public Customer NewFakerGenerateCustomer()
    {
        return new Faker<Customer>().Generate();
    }

    [Benchmark]
    public Customer SomeGeneratedCustomer()
    {
        return Some.Generated<Customer>();
    }
}

public class OrderGenerationBenchmarks
{
    private readonly Faker<Order> _faker = new Faker<Order>();
    [Benchmark]
    public Order ReusedFakerGenerateOrder()
    {
        return _faker.Generate();
    }

    [Benchmark(Baseline = true)]
    public Order NewFakerGenerateOrder()
    {
        return new Faker<Order>().Generate();
    }

    [Benchmark]
    public Order SomeGeneratedOrder()
    {
        return Some.Generated<Order>();
    }
}
