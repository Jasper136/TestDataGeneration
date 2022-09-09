using System;
using Bogus;
using TestDataGeneration.DemoDomain;

namespace TestDataGeneration.DemoTests;

public class DemoBinder : Some.DefaultBinder
{
    public DemoBinder()
    {
        TypeRules[typeof(Article)] = ArticleRules;
        TypeRules[typeof(Order)] = OrderRules;
        TypeRules[typeof(Customer)] = CustomerRules;
    }

    private static readonly Func<Faker<Article>, Faker<Article>> ArticleRules =
        f => f.RuleFor(x=>x.Price, Some.Int(0));

    private static readonly Func<Faker<Order>, Faker<Order>> OrderRules =
        f => f.FinishWith((_, x) =>
        {
            x.CustomerId = x.Customer.Id;
        });

    private static readonly Func<Faker<Customer>, Faker<Customer>> CustomerRules = f => f
        .RuleFor(x=>x.FavoriteArticle, Some.Generated<Article>()) //todo: figure out a way to do this automagically
        .FinishWith((_, x) =>
        {
            x.FavoriteArticleId = x.FavoriteArticle?.Id;
        });
}