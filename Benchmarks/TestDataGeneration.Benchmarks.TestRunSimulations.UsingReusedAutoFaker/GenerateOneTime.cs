namespace TestDataGeneration.Benchmarks.TestRunSimulations.UsingReusedAutoFaker;

public class GenerateOneTime : IClassFixture<ReusedAutoFakerFixture>
{
    private readonly ReusedAutoFakerFixture _classFixture;

    public GenerateOneTime(ReusedAutoFakerFixture classFixture)
    {
        _classFixture = classFixture;
    }

    [Fact]
    public void Article()
    {
        var _ = _classFixture.ArticleFaker.Generate();
    }

    [Fact]
    public void Customer()
    {
        var _ = _classFixture.CustomerFaker.Generate();
    }

    [Fact]
    public void Order()
    {
        var _ = _classFixture.OrderFaker.Generate();
    }
}