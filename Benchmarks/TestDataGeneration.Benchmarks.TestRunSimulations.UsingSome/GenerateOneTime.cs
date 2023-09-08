using TestDataGeneration.DemoDomain;

namespace TestDataGeneration.Benchmarks.TestRunSimulations.UsingSome;

public class GenerateOneTime
{
    [Fact]
    public void Article()
    {
        var _ = Some.Generated<Article>();
    }

    [Fact]
    public void Customer()
    {
        var _ = Some.Generated<Customer>();
    }

    [Fact]
    public void Order()
    {
        var _ = Some.Generated<Order>();
    }
}