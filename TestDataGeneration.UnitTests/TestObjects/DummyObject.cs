using System;

namespace TestDataGeneration.UnitTests.TestObjects;

public class DummyObject
{
    public string? StringProp { get; set; }

    internal static Guid GuidValue = new Guid("290FBE44-7B47-4096-B6E3-3E83906605F3");
    public Guid GuidWithRuleFor { get; set; }
}
