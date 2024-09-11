using HPlusSport.API.Constants;
using HPlusSport.API.Data.Entity;

namespace HPlusSport.API.Extensions;

public static class ProductExtensions
{
    public static IQueryable<Product> FilterByMinPrice(
            this IQueryable<Product> products,
            decimal? minPrice)
    {
        if (minPrice is not null)
        {
            products = products.Where(p => p.Price >= minPrice);
        }

        return products;
    }

    public static IQueryable<Product> FilterByMaxPrice(
            this IQueryable<Product> products,
            decimal? maxPrice)
    {
        if (maxPrice is not null)
        {
            products = products.Where(p => p.Price <= maxPrice);
        }

        return products;
    }

    public static IQueryable<Product> SearchByProductName(
        this IQueryable<Product> products,
        string productName)
    {
        if (string.IsNullOrEmpty(productName) == false)
        {
            products = products.Where(p => p.Name.ToLower().Contains(productName.ToLower()));
        }

        return products;
    }

    public static IQueryable<Product> SearchByProductSku(
        this IQueryable<Product> products,
        string productSku)
    {
        if (string.IsNullOrEmpty(productSku) == false)
        {
            products = products.Where(p => p.Sku == productSku);
        }

        return products;
    }

    public static IQueryable<Product> SortBy(
        this IQueryable<Product> products,
        SortBy sortBy,
        SortOrder sortOrder)
    {
        if (typeof(Product).GetProperty(sortBy.ToString()) is not null)
        {
            bool ascending = sortOrder == SortOrder.Asc;

            products = products.OrderByCustom(sortBy.ToString(), ascending);
        }

        return products;
    }
}