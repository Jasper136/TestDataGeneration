using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Bogus;
using TestDataGeneration;

Console.WriteLine("Hello, Published World!");
var summary = BenchmarkRunner.Run<IntGenerationBenchmarks>();
//var summary = BenchmarkRunner.Run(typeof(Program).GetAssembly());
Console.WriteLine("Bye, Published World!");

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