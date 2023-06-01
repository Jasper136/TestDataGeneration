namespace TestDataGeneration.SourceGenerators.Templates;

internal class WireSomeGeneratorAttribute
{
    //todo: create syntax tree instead of using template-string
    //todo: adjust namespace based on target project
    public const string Template = @$"namespace TestDataGeneration.SourceGenerators.Templates.Wiring.Generated;

[AttributeUsage(AttributeTargets.Class)]
public class WireSomeGeneratorAttribute : Attribute
{{

}}";
}