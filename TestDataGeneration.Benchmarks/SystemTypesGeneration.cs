using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace TestDataGeneration.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class SystemTypesGenerationBenchmarks
{
    [BenchmarkCategory(nameof(Int32)), Benchmark]
    public int SomeInt()
    {
        return Some.Int();
    }

    [BenchmarkCategory(nameof(Int32)), Benchmark]
    public int SomeGeneratedInt()
    {
        return Some.Generated<int>();
    }


    [BenchmarkCategory(nameof(String)), Benchmark]
    public string SomeString()
    {
        return Some.String();
    }

    [BenchmarkCategory(nameof(String)), Benchmark]
    public string SomeGeneratedString()
    {
        return Some.Generated<string>();
    }
    
    
    [BenchmarkCategory(nameof(Boolean)), Benchmark]
    public bool SomeBool()
    {
        return Some.Bool();
    }

    [BenchmarkCategory(nameof(Boolean)), Benchmark]
    public bool SomeGeneratedBool()
    {
        return Some.Generated<bool>();
    }


    [BenchmarkCategory(nameof(DateTime)), Benchmark]
    public DateTime SomeDateTime()
    {
        return Some.DateTime();
    }

    [BenchmarkCategory(nameof(DateTime)), Benchmark]
    public DateTime SomeGeneratedDateTime()
    {
        return Some.Generated<DateTime>();
    }


    [BenchmarkCategory(nameof(Guid)), Benchmark]
    public Guid SomeGuid()
    {
        return Some.Guid();
    }

    [BenchmarkCategory(nameof(Guid)), Benchmark]
    public Guid SomeGeneratedGuid()
    {
        return Some.Generated<Guid>();
    }
}