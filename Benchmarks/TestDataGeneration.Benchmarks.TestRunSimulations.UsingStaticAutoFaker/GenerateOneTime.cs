using AutoBogus;
using TestDataGeneration.DemoDomain;

namespace TestDataGeneration.Benchmarks.TestRunSimulations.UsingStaticAutoFaker;

public class GenerateOneTime
{
    [Fact]
    public void Article()
    {
        var _ = AutoFaker.Generate<Article>();
    }

    [Fact]
    public void Customer()
    {
        var _ = AutoFaker.Generate<Customer>();
    }

    [Fact]
    public void Order()
    {
        var _ = AutoFaker.Generate<Order>();
    }
}