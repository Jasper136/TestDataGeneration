using System;
using System.Linq;
using TestDataGeneration.UnitTests.TestObjects;

namespace TestDataGeneration.UnitTests;

using Xunit;

public class SomeTests
{
    [Fact]
    public void GeneratedObject_Type_ReturnsObjectOfType()
    {
        var type = typeof(DummyObject);

        var result = Some.Generated(type);

        Assert.IsType(type, result);
    }

    [Fact]
    public void GeneratedTyped_Invoke_ReturnsObjectOfType()
    {
        var result = Some.Generated<DummyObject>();

        Assert.IsType<DummyObject>(result);
    }

    [Fact]
    public void GeneratedTyped_LengthValue_ReturnsListOfType()
    {
        var count = Some.Int(1, 10);

        var result = Some.Generated<DummyObject>(count);

        Assert.Equal(count, result.Count);
    }

    [Fact]
    public void GeneratedTyped_MinMaxIntValue_ReturnsListOfType()
    {
        var min = Some.Int(1, 10);
        var max = Some.Int(min, 20);

        var result = Some.Generated<DummyObject>(min, max);

        Assert.True(min <= result.Count && result.Count <= max);
    }

    #region From

    [Fact]
    public void From_PossibleStringValues_ReturnsValueInPossibleValues()
    {
        var possibleValues = Some.Generated<string>(5);
        var result = Some.From(possibleValues.ToArray());
        Assert.Contains(result, possibleValues);
    }

    [Fact]
    public void From_PossibleIntValues_ReturnsValueInPossibleValues()
    {
        var possibleValues = Some.Generated<int>(5);
        var result = Some.From(possibleValues.ToArray());
        Assert.Contains(result, possibleValues);
    }

    [Fact]
    public void From_PossibleObjectValues_ReturnsValueInPossibleValues()
    {
        var possibleValues = Some.Generated<DummyObject>(5);
        var result = Some.From(possibleValues.ToArray());
        Assert.Contains(result, possibleValues);
    }

    [Fact]
    public void From_PossibleEnumValues_ReturnsValueInPossibleValues()
    {
        var possibleValues = new[] { DummyEnum.One, DummyEnum.Two };
        var result = Some.From(possibleValues);
        Assert.Contains(result, possibleValues);
    }

    [Fact]
    public void From_NoPossibleValues_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Some.From<DummyObject>());
        Assert.Contains("Possible values must be provided.", ex.Message);
    }

    #endregion

    #region Except

    [Fact]
    public void Except_ExcludedStringValues_ReturnsValueNotInExcludedValues()
    {
        var excludedValues = Some.Generated<string>(5);
        var result = Some.Except(excludedValues.ToArray());
        Assert.DoesNotContain(result, excludedValues);
    }

    [Fact]
    public void Except_ExcludedIntValues_ReturnsValueNotInExcludedValues()
    {
        var excludedValues = Some.Generated<int>(5);
        var result = Some.Except(excludedValues.ToArray());
        Assert.DoesNotContain(result, excludedValues);
    }

    [Fact]
    public void Except_ExcludedObjectValues_ReturnsValueNotInExcludedValues()
    {
        var excludedValues = Some.Generated<DummyObject>(5);
        var result = Some.Except(excludedValues.ToArray());
        Assert.DoesNotContain(result, excludedValues);
    }

    [Fact]
    public void Except_ExcludedEnumValues_ReturnsValueNotInExcludedValues()
    {
        var excludedValues = new[] { DummyEnum.One, DummyEnum.Two };
        var result = Some.Except(excludedValues);
        Assert.DoesNotContain(result, excludedValues);
    }

    [Fact]
    public void Except_NoExcludedValues_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Some.Except<DummyObject>());
        Assert.Contains("Excluded values must be provided.", ex.Message);
    }

    [Fact]
    public void Except_ExcludedAllValues_Throws()
    {
        var excludedValues = new[] { DummyEnum.One, DummyEnum.Two, DummyEnum.Three };
        var ex = Assert.Throws<ArgumentException>(() => Some.Except(excludedValues));
        Assert.Contains(
            "Impossible to generate a value, without using the excluded values, within a reasonable amount of tries.",
            ex.Message);
    }

    #endregion

    #region UniqueValues

    [Fact]
    public void UniqueValues_LengthValue_ReturnsUniqueValuesList()
    {
        var length = Some.Int(0, Enum.GetValues(typeof(DummyEnum)).Length);
        var enumValues = Some.UniqueValues<DummyEnum>(length);

        Assert.Equal(length, enumValues.Count);
        Assert.Equal(enumValues.Distinct().Count(), enumValues.Count);
    }

    [Fact]
    public void UniqueValues_LengthLargerThanPossibleValueCount_Throws()
    {
        var possibleValueCount = Enum.GetValues(typeof(DummyEnum)).Length;

        var ex = Assert.Throws<ArgumentException>(() => Some.UniqueValues<DummyEnum>(possibleValueCount + 1));
        Assert.Contains("Not enough possible values.", ex.Message);
    }

    [Fact]
    public void UniqueValues_MinMaxIntValue_ReturnsUniqueValuesList()
    {
        var min = 0;
        var max = Enum.GetValues(typeof(DummyEnum)).Length;
        var enumValues = Some.UniqueValues<DummyEnum>(min, max);

        Assert.InRange(enumValues.Count, min, max);
        Assert.Equal(enumValues.Distinct().Count(), enumValues.Count);
    }

    #endregion

    //test to verify default rules are used when no rules are specified
    //scenario:
    // - generate DummyObject with a rule set in default rules
    // - verify that the rule is used
    [Fact]
    public void GeneratedObject_WithRuleSetInDefaultRules_ReturnsObjectWithDefaultValue()
    {
        var result = Some.Generated<DummyObject>();
        Assert.Equal(DummyObject.GuidValue, result.GuidWithRuleFor);
    }

    //test to verify rules are used when specified
    //scenario:
    // - generate DummyObject with a rule set in default rules and override the rule
    // - verify that the new rule is used
    [Fact]
    public void GeneratedObject_WithRuleSetInDefaultRulesAndOverriden_ReturnsObjectWithOverridenValue()
    {
        var guid = Guid.NewGuid();
        var result = Some.InstanceOf<DummyObject>().RuleFor(x=>x.GuidWithRuleFor, _=>guid).Generate();
        Assert.Equal(guid, result.GuidWithRuleFor);
    }

    //test to verify that overriding a rule does not affect future generations
    //scenario:
    // - generate DummyObject with a rule set in default rules and override the rule
    // - generate DummyObject without overriding the rule
    // - verify that the default rule is used
    [Fact]
    public void GeneratedObject_WithRuleSetInDefaultRulesAndOverriden_ReturnsObjectWithDefaultValue()
    {
        var guid = Guid.NewGuid();
        var result = Some.InstanceOf<DummyObject>().RuleFor(x=>x.GuidWithRuleFor, _=>guid).Generate();
        Assert.Equal(guid, result.GuidWithRuleFor);

        result = Some.Generated<DummyObject>();
        Assert.Equal(DummyObject.GuidValue, result.GuidWithRuleFor);
    }
}
