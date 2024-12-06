using Dapper;
using Infrastructure.DataContext;
using Infrastructure.Interface;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class OrderServices: IOrder
{
    DapperContext _context;

    public OrderServices()
    {
        _context = new DapperContext();
    }
    
    public List<Order> GetOrders()
    {
        var sql = @"select * from Orders";
        var orders = _context.GetConnection().Query<Order>(sql).ToList();
        return orders;
    }

   public bool AddOrder(Order order)
{
    using (var connection = _context.GetConnection())
    {
        connection.Open();
        using (var transaction = connection.BeginTransaction())
        {
            try
            {
                // Получаем бюджет клиента
                var sql1 = "SELECT budget FROM customers WHERE customerid = @CustomerId";
                var customerBudget = connection.QueryFirstOrDefault<decimal>(sql1, new { order.CustomerId }, transaction);

                // Получаем цену продукта
                var sql2 = "SELECT price FROM products WHERE productid = @ProductId";
                var productPrice = connection.QueryFirstOrDefault<decimal>(sql2, new { order.ProductId }, transaction);

                // Рассчитываем общую стоимость
                order.TotalPrice = order.Quantity * productPrice;

                if (customerBudget < order.TotalPrice)
                {
                    transaction.Rollback();
                    return false;
                }

                // Вставляем заказ
                var sql3 = "INSERT INTO orders (customerid, productid, quantity, totalprice, orderdate) " +
                           "VALUES (@CustomerId, @ProductId, @Quantity, @TotalPrice, @OrderDate)";
                var result1 = connection.Execute(sql3, order, transaction);

                // Обновляем бюджет клиента
                var sql4 = "UPDATE customers SET budget = budget - @TotalPrice WHERE customerid = @CustomerId";
                var result2 = connection.Execute(sql4, new { order.TotalPrice, order.CustomerId }, transaction);

                if (result1 > 0 && result2 > 0)
                {
                    transaction.Commit();
                    return true;
                }
                transaction.Rollback();
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                transaction.Rollback();
                throw;
            }
        }
    }
}


    public bool UpdateOrder(Order order)
    {
        var sql = "update orders set customerId = @customerId,productId=@productId,quantity=@quantity,totalprice=@totalPrice,orderdate=@orderDate where orderId = @orderId";
        var udate = _context.GetConnection().Execute(sql,order);
        return udate>0;
    }

    public bool DeleteOrder(int id)
    {
        var sql = "delete from orders where orderId = @orderId";
        var udate = _context.GetConnection().Execute(sql,new { orderId = id });
        return udate>0;
    }

    public Order GetOrderById(int orderId)
    {
        var sql = "select * from orders where orderId = @orderId";
        var order = _context.GetConnection().QueryFirstOrDefault<Order>(sql,new { orderId = orderId });
        return order;
    }
}