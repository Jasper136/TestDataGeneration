namespace TestDataGeneration.Wrapper;

public class PublishedSome
{
    public static int Int() => Some.Int();

    public static T Generated<T>() => Some.Generated<T>();
}