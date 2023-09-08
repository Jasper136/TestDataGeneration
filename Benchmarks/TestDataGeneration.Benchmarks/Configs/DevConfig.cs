using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;

namespace TestDataGeneration.Benchmarks.Configs;

class DevConfig : ManualConfig
{
    public DevConfig()
    {
        AddExporter(MarkdownExporter.GitHub);
        AddJob(Job.MediumRun);
    }
}

class TestRunSimulationConfig : ManualConfig
{
    public TestRunSimulationConfig()
    {
        AddJob(Job.ShortRun);
    }
}