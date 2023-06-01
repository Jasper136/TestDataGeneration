using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace TestDataGeneration.SourceGenerators.Tests.Wiring;

public class SomeWiringGeneratorTests
{
    [Fact(Skip = "Not yet supported by ci-pipeline")]
    public async Task WiringMarkerAttribute_TemplateComparison()
    {
        var sourceFilesDirectory = new DirectoryInfo("Wiring\\Templates\\Written\\");
        var sourceFiles = new List<FileInfo>();

        var expectedFilesDirectory = new DirectoryInfo("Wiring\\Templates\\Generated\\");
        var expectedFiles = new[]
        {
            expectedFilesDirectory.GetFiles("WireSomeGeneratorAttribute.cs").Single(),
        };

        var test = new CSharpSourceGeneratorVerifier<SomeWiringGenerator>.Test();
        test.TestState.Sources.AddRange(sourceFiles.Select(x => (x.Name, SourceText.From(File.ReadAllText(x.FullName)))));
        foreach (var expectedFile in expectedFiles)
        {
            var sourceText = SourceText.From(await File.ReadAllTextAsync(expectedFile.FullName), Encoding.UTF8);
            test.TestState.GeneratedSources.Add((typeof(SomeWiringGenerator), expectedFile.Name.Replace(".cs", ".g.cs"), sourceText));
        }

        await test.RunAsync();
    }

}