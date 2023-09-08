using AutoBogus;
using TestDataGeneration.DemoDomain;

namespace TestDataGeneration.Benchmarks.TestRunSimulations.UsingReusedAutoFaker;

public class ReusedAutoFakerFixture
{
    public AutoFaker<Article> ArticleFaker { get; } = new();
    public AutoFaker<Customer> CustomerFaker { get; } = new();
    public AutoFaker<Order> OrderFaker { get; } = new();
}