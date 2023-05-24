using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Bogus;
using Bogus.Platform;
using TestDataGeneration;
using TestDataGeneration.DemoDomain;

Console.WriteLine("Hello, World!");
var summary = BenchmarkRunner.Run<IntGenerationBenchmarks>();
//var summary = BenchmarkRunner.Run(typeof(Program).GetAssembly());
Console.WriteLine("Bye, World!");

//Benchmark comparing new Faker.Random, reused Faker.Random, Some and Some.Generated<T>()
public class IntGenerationBenchmarks
{
    private readonly Faker _faker = new Faker();
    [Benchmark]
    public int ReusedFakerRandomInt()
    {
        return _faker.Random.Int();
    }

    [Benchmark(Baseline = true)]
    public int NewFakerRandomInt()
    {
        return new Faker().Random.Int();
    }

    [Benchmark]
    public int SomeInt()
    {
        return Some.Int();
    }

    [Benchmark]
    public int SomeGeneratedInt()
    {
        return Some.Generated<int>();
    }
}

//Benchmark comparing local Some.Generated<T>(), published Some.Generated<T>(), Faker<T>().Generate() and reused Faker<T>().Generate()
public class ArticleGenerationBenchmarks
{
    private readonly Faker<Article> _faker = new Faker<Article>();
    [Benchmark]
    public Article ReusedFakerGenerateArticle() => _faker.Generate();

    [Benchmark(Baseline = true)]
    public Article NewFakerGenerateArticle() => new Faker<Article>().Generate();

    [Benchmark]
    public Article SomeGeneratedArticle() => Some.Generated<Article>();
}

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