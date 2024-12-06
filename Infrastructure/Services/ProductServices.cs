using Dapper;
using Infrastructure.DataContext;
using Infrastructure.Interface;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class ProductServices: IProduct
{
    private readonly DapperContext _context;

    public ProductServices()
    {
        _context = new DapperContext();
    }
    public List<Product> GetProducts()
    {
        try
        {
            var sql = @"select * from Products";
            var products = _context.GetConnection().Query<Product>(sql).ToList();
            return products;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool AddProduct(Product product)
    {
        try
        {
            var sql= @"insert into Products (Name,Price) values (@Name,@Price)";
            var products = _context.GetConnection().Execute(sql, product);
            return products > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public bool UpdateProduct(Product product)
    {
        try
        {
            var sql = @"update Products set Name = @Name, Price = @Price where productid = @ProductId";
            var products = _context.GetConnection().Execute(sql, product);
            return products > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool DeleteProduct(int productId)
    {
        try
        {
            string sql = @"delete from Products where prodactid = @ProductId";
            var products = _context.GetConnection().Execute(sql, new { ProductId = productId });
            return products > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Product GetProductById(int productId)
    {
        string sql = @"select * from Products where prodactid = @ProductId";
        Product products = _context.GetConnection().QueryFirstOrDefault<Product>(sql, new { ProductId = productId });
        return products;
    }
}