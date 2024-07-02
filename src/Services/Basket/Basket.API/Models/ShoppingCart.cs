namespace Basket.API.Models;

public class ShoppingCart
{
    public string Username { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);

    public ShoppingCart(string userName)
    {
        Username = userName;
    }

    //Required for Mapping
    public ShoppingCart()
    {
        
    }
}
