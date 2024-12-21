using Refit;
using RestFull.Domain.Core.Commands;
using RestFull.Domain.Core.Entities;
using RestFull.Domain.Core.Queries;

namespace RestFullApi.Web.Services;

public interface IBFFApiService
{

    [Get("/Product")]
    Task<IEnumerable<Product>> GetProducts(ProductPaginatedQuery query);

    [Get("/Product/{id}")]
    Task<Product> GetProduct(Guid id);

    [Post("/admin/Product")]
    Task CreateProduct(AddProductCommand command);

    [Put("/admin/Product")]
    Task UpdateProduct(UpdateProductCommand command);

    [Delete("/admin/Product")]
    Task DeleteProduct(Guid id);
}
