using System.Runtime.CompilerServices;

namespace TestDataGeneration.UnitTests.Wiring;

internal class TestProjectInitializer
{
    [ModuleInitializer]
    internal static void ConfigureTestDataGeneration()
    {
        Some.CustomConfigApplied(new TestBinder());
    }
}