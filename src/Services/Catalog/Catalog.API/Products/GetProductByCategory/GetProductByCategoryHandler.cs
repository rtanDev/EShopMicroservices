
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(IDocumentSession session)
: IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().Where(p => p.Category.Contains(query.Category)).ToListAsync();

        //null result is expected
        //if (products is null)
        //{
        //    throw new ProductNotFoundException(query.Category);
        //}

        return new GetProductByCategoryResult(products);
    }
}
