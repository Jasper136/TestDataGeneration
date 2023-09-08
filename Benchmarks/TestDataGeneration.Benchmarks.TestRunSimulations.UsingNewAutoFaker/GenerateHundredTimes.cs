using AutoBogus;
using TestDataGeneration.DemoDomain;

namespace TestDataGeneration.Benchmarks.TestRunSimulations.UsingNewAutoFaker;

public class GenerateHundredTimes
{
    [Fact]
    public void Article()
    {
        for (var i = 0; i < 100; i++)
        {
            var _ = new AutoFaker<Article>().Generate();
        }
    }

    [Fact]
    public void Customer()
    {
        for (var i = 0; i < 100; i++)
        {
            var _ = new AutoFaker<Customer>().Generate();
        }
    }

    [Fact]
    public void Order()
    {
        for (var i = 0; i < 100; i++)
        {
            var _ = new AutoFaker<Order>().Generate();
        }
    }
}