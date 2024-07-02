namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default(CancellationToken));
    Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default(CancellationToken));  
    Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken= default(CancellationToken));
}
