using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Bogus;
using Bogus.Platform;
using TestDataGeneration;
using TestDataGeneration.DemoDomain;
using TestDataGeneration.Wrapper;

Console.WriteLine("Hello, World!");
var summary = BenchmarkRunner.Run<PublishedSomeVsLocalSomeVsFakerRandomVsReusedFakerRandom>();
//var summary = BenchmarkRunner.Run(typeof(Program).GetAssembly());
Console.WriteLine("Bye, World!");

//Benchmark comparing local Some, published Some, Faker.Random and reused Faker.Random
public class PublishedSomeVsLocalSomeVsFakerRandomVsReusedFakerRandom
{
    private readonly Faker _faker = new Faker();
    [Benchmark]
    public int ReusedFakerRandomInt() => _faker.Random.Int();

    [Benchmark(Baseline = true)]
    public int FakerRandomInt() => new Faker().Random.Int();

    [Benchmark]
    public int PublishedSomeInt() => PublishedSome.Int();

    [Benchmark]
    public int LocalSomeInt() => Some.Int();



    [Benchmark]
    public int LocalSomeGeneratedInt() => Some.Generated<int>();

    [Benchmark]
    public int PublishedSomeGeneratedInt() => PublishedSome.Generated<int>();
}

//Benchmark comparing local Some.Generated<T>(), published Some.Generated<T>(), Faker<T>().Generate() and reused Faker<T>().Generate()
public class PublishedSomeGeneratedVsLocalSomeGeneratedVsFakerGenerateVsReusedFakerGenerate
{
    private readonly Faker<Article> _faker = new Faker<Article>();
    [Benchmark]
    public Article ReusedFakerGenerateArticle() => _faker.Generate();

    [Benchmark(Baseline = true)]
    public Article FakerGenerateArticle() => new Faker<Article>().Generate();

    [Benchmark]
    public Article PublishedSomeGeneratedArticle() => PublishedSome.Generated<Article>();

    [Benchmark]
    public Article LocalSomeGeneratedArticle() => Some.Generated<Article>();

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