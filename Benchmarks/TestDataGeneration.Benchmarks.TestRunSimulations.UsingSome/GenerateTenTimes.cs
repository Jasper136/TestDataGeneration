using TestDataGeneration.DemoDomain;

namespace TestDataGeneration.Benchmarks.TestRunSimulations.UsingSome;

public class GenerateTenTimes
{
    [Fact]
    public void Article()
    {
        for (var i = 0; i < 10; i++)
        {
            var _ = Some.Generated<Article>();
        }
    }

    [Fact]
    public void Customer()
    {
        for (var i = 0; i < 10; i++)
        {
            var _ = Some.Generated<Customer>();
        }
    }

    [Fact]
    public void Order()
    {
        for (var i = 0; i < 10; i++)
        {
            var _ = Some.Generated<Order>();
        }
    }
}