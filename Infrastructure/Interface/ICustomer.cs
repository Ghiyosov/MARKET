using Infrastructure.Models;

namespace Infrastructure.Interface;

public interface ICustomer
{
    List<Customer> GetCustomers();
    bool AddCustomer(Customer customer);
    bool DeleteCustomer(int customerId);
    bool UpdateCustomer(Customer customer);
    Customer GetCustomerByID(int customerID);

}