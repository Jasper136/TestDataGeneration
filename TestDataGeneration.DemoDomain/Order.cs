namespace TestDataGeneration.DemoDomain;

public class Order
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; }//todo => avoid compilation errors/warnings
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }//todo => avoid compilation errors/warnings
    public List<Article> Articles { get; set; }=new();

}