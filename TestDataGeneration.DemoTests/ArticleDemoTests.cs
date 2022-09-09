using System;
using AutoBogus;
using TestDataGeneration.DemoDomain;
using Xunit;

namespace TestDataGeneration.DemoTests;

public class ArticleDemoTests : IDisposable
{
    /// <summary>
    /// Under the cover, AutoFaker is used
    /// </summary>
    [Fact]
    public void GenerateArticle_AutoFaker()
    {
        var articleFaker = new AutoFaker<Article>();
        var article = articleFaker.Generate();
        Assert.NotNull(article);
        //Assert.True(article.Price >= 0);
    }

    /// <summary>
    /// Using the Some-wrapper, syntax can be cleaner
    /// </summary>
    [Fact]
    public void GenerateArticle_SomeWithoutBinderConfigured()
    {
        var article = Some.Generated<Article>();
        Assert.NotNull(article);
        //Assert.True(article.Price >= 0);
    }

    /// <summary>
    /// AutoFaker can be configured with specific property-rules
    /// </summary>
    [Fact]
    public void GenerateArticle_AutoFakerWithPriceRule()
    {
        var articleFaker = new AutoFaker<Article>()
            .RuleFor(x => x.Price, f => f.Random.Int(0));
        var article = articleFaker.Generate();
        Assert.NotNull(article);
        Assert.True(article.Price >= 0);
    }

    /// <summary>
    /// Some data-generator can be customized with specific rules for specific types
    /// </summary>
    [Fact]
    public void GenerateArticle_SomeBinderDefinesPriceRule()
    {
        Some.CustomConfigApplied(new DemoBinder());
        var article = Some.Generated<Article>();
        Assert.NotNull(article);
        Assert.True(article.Price >= 0);
    }

    /// <summary>
    /// AutoFaker can be configured with specific property-rules
    /// Property-rules are remembered by faker-instance
    /// </summary>
    [Fact]
    public void GenerateArticle_AutoFakerWithDifferentPriceRules()
    {
        var articleFaker = new AutoFaker<Article>()
            .RuleFor(x => x.Price, f => f.Random.Int(0));
        var article1 = articleFaker.Generate();
        var article2 = articleFaker.RuleFor(x => x.Price, Some.Int(int.MinValue, 0)).Generate();
        var article3 = articleFaker.Generate();
        var article4 = articleFaker.RuleFor(x => x.Price, Some.Int(0)).Generate();
        Assert.True(article1.Price >= 0);
        Assert.True(article2.Price <= 0);
        Assert.True(article3.Price <= 0);
        Assert.True(article4.Price >= 0);
    }

    /// <summary>
    /// Specific rules for specific types can be overriden for one specific generated object
    /// Syntax is similar to (Auto)Bogus syntax
    /// Generating new object, starts with default rules
    /// </summary>
    [Fact]
    public void GenerateArticle_SomeBinderPriceRuleOverridden()
    {
        Some.CustomConfigApplied(new DemoBinder());
        var article1 = Some.Generated<Article>();
        var article2 = Some.InstanceOf<Article>().RuleFor(x => x.Price, Some.Int(int.MinValue, 0)).Generate();
        var article3 = Some.Generated<Article>();
        Assert.True(article1.Price >= 0);
        Assert.True(article2.Price <= 0);
        Assert.True(article3.Price >= 0);
    }

    //reset binding configuration after each test, for demo purposes
    public void Dispose()
    {
        Some.CustomConfigApplied();
    }
}