using System;

namespace TestDataGeneration.UnitTests.TestObjects;

public class DummyObject
{
    public string? StringProp { get; set; }

    internal static Guid GuidValue = new Guid("290FBE44-7B47-4096-B6E3-3E83906605F3");
    public Guid GuidWithRuleFor { get; set; }
    public TimeSpan TimeSpan { get; set; }
    public ChildObject ChildObject { get; set; }
}

public class ChildObject
{
    public string? StringProp { get; set; }
    
    public Guid Guid { get; set; }

    public TimeSpan TimeSpan { get; set; }
}

/// <summary>
/// Should not have default rules set by custom binder
/// </summary>
public class DummyObjectWithoutDefaultRules
{
    public string? StringProp { get; set; }
    
    public Guid Guid { get; set; }

    public TimeSpan TimeSpan { get; set; }
}
