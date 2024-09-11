using HPlusSport.API.Data.Entity;

namespace HPlusSport.API.Models;

public static class ProductExtensions
{
    public static IQueryable<Product> FilterByPrice(
            this IQueryable<Product> products,
            RequestQueryParameters parameters)
    {
        if (parameters.MinPrice is not null)
        {
            products = products.Where(p => p.Price >= parameters.MinPrice);
        }

        if (parameters.MaxPrice is not null)
        {
            products = products.Where(p => p.Price <= parameters.MaxPrice);
        }

        return products;
    }

    public static IQueryable<Product> SearchByNameAndSKU(
        this IQueryable<Product> products,
        RequestQueryParameters parameters)
    {
        if (string.IsNullOrEmpty(parameters.Sku) == false)
        {
            products = products.Where(p => p.Sku == parameters.Sku);
        }

        if (string.IsNullOrEmpty(parameters.Name) == false)
        {
            products = products.Where(p => p.Name.ToLower().Contains(parameters.Name.ToLower()));
        }

        return products;
    }
}
