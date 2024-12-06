using Infrastructure.Models;

namespace Infrastructure.Interface;

public interface IOrder
{
    List<Order> GetOrders();
    bool AddOrder(Order order);
    bool UpdateOrder(Order order);
    bool DeleteOrder(int id);
    Order GetOrderById(int orderId);
}