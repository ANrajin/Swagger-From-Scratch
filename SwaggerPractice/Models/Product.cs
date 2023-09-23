namespace SwaggerPractice.Models;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string ProductName { get; set; } = string.Empty;

    public int Price { get; set; } = default;
}