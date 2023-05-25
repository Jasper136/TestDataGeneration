using System;
using Bogus;
using TestDataGeneration.UnitTests.TestObjects;

namespace TestDataGeneration.UnitTests.Wiring;

public class TestBinder : Some.DefaultBinder
{
    public TestBinder()
    {
        TypeRules[typeof(DummyObject)] = ArticleRules;
    }

    private static readonly Func<Faker<DummyObject>, Faker<DummyObject>> ArticleRules =
        f => f.RuleFor(x => x.GuidWithRuleFor, DummyObject.GuidValue);
}