using Infrastructure.Models;

namespace Infrastructure.Interface;

public interface IProduct
{
    List<Product> GetProducts();
    bool AddProduct(Product product);
    bool UpdateProduct(Product product);
    bool DeleteProduct(int productId);
    Product GetProductById(int productId);
}