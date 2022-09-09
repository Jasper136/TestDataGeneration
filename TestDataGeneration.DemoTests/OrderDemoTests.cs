using System;
using AutoBogus;
using TestDataGeneration.DemoDomain;
using Xunit;

namespace TestDataGeneration.DemoTests;

public class OrderDemoTests :IDisposable
{
    [Fact]
    public void GenerateOrder_AutoFaker()
    {
        var order = new AutoFaker<Order>().Generate();
        Assert.NotNull(order);
        Assert.NotNull(order.Customer);
        Assert.NotEqual(order.Customer.Id, order.CustomerId);
    }

    [Fact]
    public void GenerateOrder_SomeBinderDefinesFinalRule()
    {
        Some.CustomConfigApplied(new DemoBinder());
        var order = Some.Generated<Order>();
        Assert.NotNull(order);
        Assert.NotNull(order.Customer);
        Assert.Equal(order.Customer.Id, order.CustomerId);
    }

    //reset binding configuration after each test, for demo purposes
    public void Dispose()
    {
        Some.CustomConfigApplied();
    }
}