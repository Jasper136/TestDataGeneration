using System;
using TestDataGeneration.DemoDomain;
using Xunit;

namespace TestDataGeneration.DemoTests;

/// <summary>
/// Configuration of Some needs to be done only once
/// For demo-purposes, a ClassFixture is used
/// Using a "ModuleInitializer" in your test project would be the cleanest solution
/// </summary>
public class ConfiguredSomeFixture : IDisposable
{
    public ConfiguredSomeFixture()
    {
        Some.CustomConfigApplied(new DemoBinder());
    }

    //reset binding configuration after all tests of test-class, for demo purposes
    public void Dispose()
    {
        Some.CustomConfigApplied();
    }
}

public class CustomerDemoTests : IClassFixture<ConfiguredSomeFixture>
{
    [Fact]
    public void GenerateCustomer_ArticleRuleAndFinalRuleDefined()
    {
        var customer = Some.Generated<Customer>();
        Assert.NotNull(customer.FavoriteArticle);
        Assert.Equal(customer.FavoriteArticle!.Id, customer.FavoriteArticleId);
        Assert.True(customer.FavoriteArticle.Price >= 0);
    }

    [Fact]
    public void GenerateCustomer_ArticleRuleOverriden()
    {
        var customer = Some.InstanceOf<Customer>().RuleFor(x=>x.FavoriteArticle, (Article?)null).Generate();
        Assert.Null(customer.FavoriteArticle);
        Assert.Null(customer.FavoriteArticleId);
    }
}