using System;
using Bogus;
using TestDataGeneration.UnitTests.TestObjects;

namespace TestDataGeneration.UnitTests.Wiring;

public class TestBinder : Some.DefaultBinder
{
    public TestBinder()
    {
        TypeRules[typeof(DummyObject)] = DummyObjectRules;

        //todo: avoid this, when no rules are needed, you should not have to set them (source generator??)
        //TypeRules[typeof(DummyObjectWithoutDefaultRules)] = (Func<Faker<DummyObjectWithoutDefaultRules>, Faker<DummyObjectWithoutDefaultRules>>)(f => f);
    }

    private static readonly Func<Faker<DummyObject>, Faker<DummyObject>> DummyObjectRules =
        f => f.RuleFor(x => x.GuidWithRuleFor, DummyObject.GuidValue);
}