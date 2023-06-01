using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using TestDataGeneration.SourceGenerators.Templates;

namespace TestDataGeneration.SourceGenerators
{
    [Generator]
    public class SomeWiringGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            //add the WireSomeGenerator marker-attribute to the assembly
            const string wiringMarkerAttributeTemplate = WireSomeGeneratorAttribute.Template;
            context.RegisterPostInitializationOutput(c => c.AddSource("WireSomeGeneratorAttribute.g.cs",
                SourceText.From(wiringMarkerAttributeTemplate, Encoding.UTF8)));
        }
    }
}
