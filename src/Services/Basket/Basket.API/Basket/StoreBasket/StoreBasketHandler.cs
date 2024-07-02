namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(sb => sb.Cart)
            .NotNull()
            .WithMessage("Cart can not be null");

        RuleFor(sb => sb.Cart.Username)
            .NotEmpty()
            .WithMessage("UserName is required");
    }
}

internal class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async  Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;
        var basket = await repository.StoreBasket(cart, cancellationToken);

        return new StoreBasketResult(command.Cart.Username);
    }
}
