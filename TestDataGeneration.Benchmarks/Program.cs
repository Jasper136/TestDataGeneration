using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Bogus;
using TestDataGeneration;

Console.WriteLine("Hello, World!");
var summary = BenchmarkRunner.Run<SomeVsFakerRandomVsReusedFakerRandom>();
Console.WriteLine("Bye, World!");

//Benchmark comparing Some.Int() vs Faker.Random.Int()
public class SomeVsFakerRandom
{
    [Benchmark]
    public int SomeInt() => Some.Int();

    [Benchmark]
    public int FakerRandomInt() => new Faker().Random.Int();
}

//Benchmark comparing Some.Int() vs reused Faker.Random.Int()
public class SomeVsReusedFakerRandom
{
    private readonly Faker _faker = new Faker();

    [Benchmark]
    public int SomeInt() => Some.Int();

    [Benchmark]
    public int ReusedFakerRandomInt() => _faker.Random.Int();
}

//Benchmark comparing Faker.Random.Int() vs reused Faker.Random.Int()
public class FakerRandomVsReusedFakerRandom
{
    private readonly Faker _faker = new Faker();

    [Benchmark]
    public int FakerRandomInt() => new Faker().Random.Int();

    [Benchmark]
    public int ReusedFakerRandomInt() => _faker.Random.Int();
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

