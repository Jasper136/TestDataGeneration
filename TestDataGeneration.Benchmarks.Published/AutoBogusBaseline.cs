using AutoBogus;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Bogus;
using TestDataGeneration.DemoDomain;

namespace TestDataGeneration.Benchmarks.Published;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class SystemTypeAutoFakerBaselineBenchmarks
{
    private readonly Faker _faker = new Faker();

    [BenchmarkCategory(nameof(Int32)), Benchmark]
    public int ReusedFakerRandomInt()
    {
        return _faker.Random.Int();
    }

    [BenchmarkCategory(nameof(Int32)), Benchmark(Baseline = true)]
    public int NewFakerRandomInt()
    {
        return new Faker().Random.Int();
    }

    [BenchmarkCategory(nameof(Int32)), Benchmark]
    public int AutoFakerGenerateInt()
    {
        return AutoFaker.Generate<int>();
    }


    [BenchmarkCategory(nameof(String)), Benchmark]
    public string ReusedFakerRandomString()
    {
        return _faker.Random.String();
    }

    [BenchmarkCategory(nameof(String)), Benchmark(Baseline = true)]
    public string NewFakerRandomString()
    {
        return new Faker().Random.String();
    }

    [BenchmarkCategory(nameof(String)), Benchmark]
    public string AutoFakerGenerateString()
    {
        return AutoFaker.Generate<string>();
    }


    [BenchmarkCategory(nameof(Guid)), Benchmark]
    public Guid ReusedFakerRandomGuid()
    {
        return _faker.Random.Guid();
    }

    [BenchmarkCategory(nameof(Guid)), Benchmark(Baseline = true)]
    public Guid NewFakerRandomGuid()
    {
        return new Faker().Random.Guid();
    }

    [BenchmarkCategory(nameof(Guid)), Benchmark]
    public Guid NewAutoFakeGenerateGuid()
    {
        return AutoFaker.Generate<Guid>();
    }


    [BenchmarkCategory(nameof(DateTime)), Benchmark]
    public DateTime ReusedFakerRandomDateTime()
    {
        //todo: figure out best Faker.Date method to use
        return _faker.Date.Soon();
    }

    [BenchmarkCategory(nameof(DateTime)), Benchmark(Baseline = true)]
    public DateTime NewFakerRandomDateTime()
    {
        return new Faker().Date.Soon();
    }

    [BenchmarkCategory(nameof(DateTime)), Benchmark]
    public DateTime AutoFakerGenerateDateTime()
    {
        return AutoFaker.Generate<DateTime>();
    }


    [BenchmarkCategory(nameof(Boolean)), Benchmark]
    public bool ReusedFakerRandomBool()
    {
        return _faker.Random.Bool();
    }

    [BenchmarkCategory(nameof(Boolean)), Benchmark(Baseline = true)]
    public bool NewFakerRandomBool()
    {
        return new Faker().Random.Bool();
    }

    [BenchmarkCategory(nameof(Boolean)), Benchmark]
    public bool AutoFakerGenerateBool()
    {
        return AutoFaker.Generate<bool>();
    }
}

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class CustomTypeAutoFakerBaselineBenchmarks
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

    private readonly AutoFaker<Article> _articleAutoFaker = new AutoFaker<Article>();
    [BenchmarkCategory(nameof(Article)), Benchmark]
    public Article ReusedAutoFakerGenerateArticle()
    {
        return _articleAutoFaker.Generate();
    }

    [BenchmarkCategory(nameof(Article)), Benchmark]
    public Article NewAutoFakerGenerateArticle()
    {
        return new AutoFaker<Article>().Generate();
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

    private readonly AutoFaker<Customer> _customerAutoFaker = new AutoFaker<Customer>();
    [BenchmarkCategory(nameof(Customer)), Benchmark]
    public Customer ReusedAutoFakerGenerateCustomer()
    {
        return _customerAutoFaker.Generate();
    }

    [BenchmarkCategory(nameof(Customer)), Benchmark]
    public Customer NewAutoFakerGenerateCustomer()
    {
        return new AutoFaker<Customer>().Generate();
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

    private readonly AutoFaker<Order> _orderAutoFaker = new AutoFaker<Order>();
    [BenchmarkCategory(nameof(Order)), Benchmark]
    public Order ReusedAutoFakerGenerateOrder()
    {
        return _orderAutoFaker.Generate();
    }

    [BenchmarkCategory(nameof(Order)), Benchmark]
    public Order NewAutoFakerGenerateOrder()
    {
        return new AutoFaker<Order>().Generate();
    }
}