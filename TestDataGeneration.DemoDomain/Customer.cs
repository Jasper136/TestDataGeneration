namespace TestDataGeneration.DemoDomain;

public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; }//todo => avoid compilation errors/warnings
    public List<Order> Orders { get; set; } = new();
    public Guid? FavoriteArticleId { get; set; }
    public Article? FavoriteArticle{ get; set; }
}