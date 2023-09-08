namespace TestDataGeneration.Benchmarks.TestRunSimulations.UsingReusedAutoFaker;

public class GenerateTenTimes : IClassFixture<ReusedAutoFakerFixture>
{
    private readonly ReusedAutoFakerFixture _classFixture;

    public GenerateTenTimes(ReusedAutoFakerFixture classFixture)
    {
        _classFixture = classFixture;
    }

    [Fact]
    public void Article()
    {
        for (var i = 0; i < 10; i++)
        {
            var _ = _classFixture.ArticleFaker.Generate();
        }
    }

    [Fact]
    public void Customer()
    {
        for (var i = 0; i < 10; i++)
        {
            var _ = _classFixture.CustomerFaker.Generate();
        }
    }

    [Fact]
    public void Order()
    {
        for (var i = 0; i < 10; i++)
        {
            var _ = _classFixture.OrderFaker.Generate();
        }
    }
}