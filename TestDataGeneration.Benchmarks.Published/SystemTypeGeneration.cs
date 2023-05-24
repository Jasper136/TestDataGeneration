using BenchmarkDotNet.Attributes;
using Bogus;

namespace TestDataGeneration.Benchmarks.Published;

//duplicated to avoid having to add a reference to TestDataGeneration directly
//todo: figure out how to reuse benchmark code from TestDataGeneration.Benchmarks instead of duplicating it here

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

public class StringGenerationBenchmarks
{
    private readonly Faker _faker = new Faker();

    [Benchmark]
    public string ReusedFakerRandomString()
    {
        return _faker.Random.String();
    }

    [Benchmark(Baseline = true)]
    public string NewFakerRandomString()
    {
        return new Faker().Random.String();
    }

    [Benchmark]
    public string SomeString()
    {
        return Some.String();
    }

    [Benchmark]
    public string SomeGeneratedString()
    {
        return Some.Generated<string>();
    }
}

public class BoolGenerationBenchmarks
{
    private readonly Faker _faker = new Faker();

    [Benchmark]
    public bool ReusedFakerRandomBool()
    {
        return _faker.Random.Bool();
    }

    [Benchmark(Baseline = true)]
    public bool NewFakerRandomBool()
    {
        return new Faker().Random.Bool();
    }

    [Benchmark]
    public bool SomeBool()
    {
        return Some.Bool();
    }

    [Benchmark]
    public bool SomeGeneratedBool()
    {
        return Some.Generated<bool>();
    }
}

public class DateTimeGenerationBenchmarks
{
    private readonly Faker _faker = new Faker();

    [Benchmark]
    public DateTime ReusedFakerRandomDateTime()
    {
        //todo: figure out best Faker.Date method to use
        return _faker.Date.Soon();
    }

    [Benchmark(Baseline = true)]
    public DateTime NewFakerRandomDateTime()
    {
        //todo: figure out best Faker.Date method to use
        return new Faker().Date.Soon();
    }

    [Benchmark]
    public DateTime SomeDateTime()
    {
        return Some.DateTime();
    }

    [Benchmark]
    public DateTime SomeGeneratedDateTime()
    {
        return Some.Generated<DateTime>();
    }
}

public class GuidGenerationBenchmarks
{
    private readonly Faker _faker = new Faker();

    [Benchmark]
    public Guid ReusedFakerRandomGuid()
    {
        return _faker.Random.Guid();
    }

    [Benchmark(Baseline = true)]
    public Guid NewFakerRandomGuid()
    {
        return new Faker().Random.Guid();
    }

    [Benchmark]
    public Guid SomeGuid()
    {
        return Some.Guid();
    }

    [Benchmark]
    public Guid SomeGeneratedGuid()
    {
        return Some.Generated<Guid>();
    }
}