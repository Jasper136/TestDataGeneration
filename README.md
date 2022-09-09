# TestDataGeneration

A C# library, wrapping the [AutoBogus](https://github.com/nickdodd79/AutoBogus) generator, providing reusable configuration and a more developer-friendly syntax.

## Usage

### Generate using provided default configuration

```c#
var x = Some.Generated<int>();
var y = Some.Generated<Article>();
```

### Define rules to be used when generating data

Create a binder that inherits from `Some.DefaultBinder`. This should contain rules that will be applied by default when generating data.

```c#
public class DemoBinder : Some.DefaultBinder
{
    public DemoBinder()
    {
        TypeRules[typeof(Article)] = ArticleRules;
        TypeRules[typeof(Order)] = OrderRules;
        TypeRules[typeof(Customer)] = CustomerRules;
    }

    private static readonly Func<Faker<Article>, Faker<Article>> ArticleRules =
        f => f.RuleFor(x=>x.Price, Some.Int(0)); //Price is positive

    private static readonly Func<Faker<Order>, Faker<Order>> OrderRules =
        f => f.FinishWith((_, x) =>
        {
            x.CustomerId = x.Customer.Id; //FK-property matches nav-prop.Id
        });

    private static readonly Func<Faker<Customer>, Faker<Customer>> CustomerRules = f => f
        .RuleFor(x=>x.FavoriteArticle, Some.Generated<Article>()) //Use "ArticleRules"
        .FinishWith((_, x) =>
        {
            x.FavoriteArticleId = x.FavoriteArticle?.Id;
        });
}
```

### Configure data-generation to use the defined rules

Add a initializer to your test project(s) to configure the data-generation with the defined rules.

```c#
internal class TestProjectInitializer
{
    [ModuleInitializer]
    internal static void ConfigureTestDataGeneration()
    {
        Some.CustomConfigApplied(new DemoBinder());
    }
}
```

### Override default rules for a single generation

```c#
var x = Some.InstanceOf<Article>()
            .RuleFor(x => x.Price, Some.Int(int.MinValue, 0))
            .Generate();
```