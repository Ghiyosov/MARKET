using Dapper;
using Infrastructure.DataContext;
using Infrastructure.Interface;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class CustomerServices: ICustomer
{
    DapperContext _context;

    public CustomerServices()
    {
        _context = new DapperContext();
    }

    public List<Customer> GetCustomers()
    {
        try
        {
            string sql = "select * from customers";
            var customers = _context.GetConnection().Query<Customer>(sql).ToList();
            return customers;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool AddCustomer(Customer customer)
    {
        try
        {
            var sql = "insert into customers(name,email,phone,budget) values (@name,@email,@phone,@budget)";
            var customers = _context.GetConnection().Execute(sql,customer);
            return customers > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool DeleteCustomer(int customerId)
    {
        try
        {
            string sql = "delete from customers where customerId=@customerId";
            var customer = _context.GetConnection().Execute(sql,new { CustomerId = customerId });
            return customer > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool UpdateCustomer(Customer customer)
    {
        try
        {
            var sql = "update customers set name=@name,email=@email,phone=@phone,budget=@budget where customerid=@customerid";
            var customers = _context.GetConnection().Execute(sql,customer);
            return customers > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Customer GetCustomerByID(int customerID)
    {
        try
        {
            var sql = "select * from customers where customerid=@customerid";
            var customer = _context.GetConnection().QueryFirstOrDefault<Customer>(sql,new { CustomerId = customerID });
            return customer;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}