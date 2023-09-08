using AutoBogus;
using TestDataGeneration.DemoDomain;

namespace TestDataGeneration.Benchmarks.TestRunSimulations.UsingNewAutoFaker;

public class GenerateOneTime
{
    [Fact]
    public void Article()
    {
        var _ = new AutoFaker<Article>().Generate();
    }

    [Fact]
    public void Customer()
    {
        var _ = new AutoFaker<Customer>().Generate();
    }

    [Fact]
    public void Order()
    {
        var _ = new AutoFaker<Order>().Generate();
    }
}