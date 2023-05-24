using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Bogus;
using TestDataGeneration;
using TestDataGeneration.DemoDomain;
using TestDataGeneration.Wrapper;

Console.WriteLine("Hello, World!");
var summary = BenchmarkRunner.Run<PublishedSomeVsLocalSome>();
//var summary = BenchmarkRunner.Run(typeof(Program).GetAssembly());
Console.WriteLine("Bye, World!");

//Benchmark comparing published Some.Int() vs local Some.Int()
public class PublishedSomeVsLocalSome
{
    [Benchmark]
    public int PublishedSomeInt() => PublishedSome.Int();

    [Benchmark]
    public int LocalSomeInt() => Some.Int();
}

//Benchmark comparing Some.Int() vs Faker.Random.Int() vs reused Faker.Random.Int()
public class SomeVsFakerRandomVsReusedFakerRandom
{
    private readonly Faker _faker = new Faker();

    [Benchmark]
    public int SomeInt() => Some.Int();

    [Benchmark]
    public int FakerRandomInt() => new Faker().Random.Int();

    [Benchmark]
    public int ReusedFakerRandomInt() => _faker.Random.Int();
}

//Benchmark comparing Some.Generated<Article>() vs Faker<Article>().Generate() vs reused Faker<Article>().Generate()
public class SomeGeneratedVsFakerGenerateVsReusedFakerGenerate
{
    private readonly Faker<Article> _faker = new Faker<Article>();

    [Benchmark]
    public Article SomeGeneratedArticle() => Some.Generated<Article>();

    [Benchmark]
    public Article FakerGenerateArticle() => new Faker<Article>().Generate();

    [Benchmark]
    public Article ReusedFakerGenerateArticle() => _faker.Generate();
}

//Benchmark comparing Some.Generated<Article>(count) vs Faker<Article>().Generate(count) vs reused Faker<Article>().Generate(count)
public class SomeGeneratedCountVsFakerGenerateCountVsReusedFakerGenerateCount
{
    private readonly Faker<Article> _faker = new Faker<Article>();

    [Benchmark]
    public List<Article> SomeGeneratedArticles() => Some.Generated<Article>(10);

    [Benchmark]
    public List<Article> FakerGenerateArticles() => new Faker<Article>().Generate(10);

    [Benchmark]
    public List<Article> ReusedFakerGenerateArticles() => _faker.Generate(10);
}

//Benchmark comparing Some.Int() vs Faker.Random.Int() vs reused Faker.Random.Int() vs Some.Generated<int>()
public class SomeIntVsFakerRandomIntVsReusedFakerRandomIntVsSomeGeneratedIntVsFakerGenerateIntVsReusedFakerGenerateInt
{
    private readonly Faker _faker = new Faker();

    [Benchmark]
    public int SomeInt() => Some.Int();

    [Benchmark]
    public int FakerRandomInt() => new Faker().Random.Int();

    [Benchmark]
    public int ReusedFakerRandomInt() => _faker.Random.Int();

    [Benchmark]
    public int SomeGeneratedInt() => Some.Generated<int>();
}