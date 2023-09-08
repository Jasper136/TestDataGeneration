using AutoBogus;
using TestDataGeneration.DemoDomain;

namespace TestDataGeneration.Benchmarks.TestRunSimulations.UsingStaticAutoFaker;

public class GenerateTenTimes
{
    [Fact]
    public void Article()
    {
        for (var i = 0; i < 10; i++)
        {
            var _ = AutoFaker.Generate<Article>();
        }
    }

    [Fact]
    public void Customer()
    {
        for (var i = 0; i < 10; i++)
        {
            var _ = AutoFaker.Generate<Customer>();
        }
    }

    [Fact]
    public void Order()
    {
        for (var i = 0; i < 10; i++)
        {
            var _ = AutoFaker.Generate<Order>();
        }
    }
}