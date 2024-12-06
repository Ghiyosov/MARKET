using Infrastructure.Models;
using Infrastructure.Services;

class Program
{
    static void Main(string[] args)
    {
        CustomerServices customerService = new CustomerServices();
        ProductServices productService = new ProductServices();
        OrderServices orderService = new OrderServices();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Welcome to the Management System ===");
            Console.WriteLine("1. Manage Customers");
            Console.WriteLine("2. Manage Products");
            Console.WriteLine("3. Manage Orders");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ManageCustomers(customerService);
                    break;
                case "2":
                    ManageProducts(productService);
                    break;
                case "3":
                    ManageOrders(orderService);
                    break;
                case "4":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    static void ManageCustomers(CustomerServices service)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Customer Management ===");
            Console.WriteLine("1. List Customers");
            Console.WriteLine("2. Add Customer");
            Console.WriteLine("3. Update Customer");
            Console.WriteLine("4. Delete Customer");
            Console.WriteLine("5. Back");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var customers = service.GetCustomers();
                    Console.WriteLine("\n=== Customer List ===");
                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"{customer.CustomerId}. {customer.Name} - {customer.Email}, Budget: {customer.Budget}");
                    }
                    Console.ReadLine();
                    break;

                case "2":
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Email: ");
                    string email = Console.ReadLine();
                    Console.Write("Enter Phone: ");
                    string phone = Console.ReadLine();
                    Console.Write("Enter Budget: ");
                    decimal budget = decimal.Parse(Console.ReadLine());
                    var newCustomer = new Customer { Name = name, Email = email, Phone = phone, Budget = budget };
                    service.AddCustomer(newCustomer);
                    Console.WriteLine("Customer added successfully.");
                    Console.ReadLine();
                    break;

                case "3":
                    Console.Write("Enter Customer ID to Update: ");
                    int id = int.Parse(Console.ReadLine());
                    var customerToUpdate = service.GetCustomerByID(id);
                    if (customerToUpdate == null)
                    {
                        Console.WriteLine("Customer not found.");
                        Console.ReadLine();
                        break;
                    }

                    Console.Write("Enter New Name (Leave empty to keep current): ");
                    name = Console.ReadLine();
                    Console.Write("Enter New Email (Leave empty to keep current): ");
                    email = Console.ReadLine();
                    Console.Write("Enter New Phone (Leave empty to keep current): ");
                    phone = Console.ReadLine();
                    Console.Write("Enter New Budget (Leave empty to keep current): ");
                    string budgetInput = Console.ReadLine();

                    if (!string.IsNullOrEmpty(name)) customerToUpdate.Name = name;
                    if (!string.IsNullOrEmpty(email)) customerToUpdate.Email = email;
                    if (!string.IsNullOrEmpty(phone)) customerToUpdate.Phone = phone;
                    if (!string.IsNullOrEmpty(budgetInput)) customerToUpdate.Budget = decimal.Parse(budgetInput);

                    service.UpdateCustomer(customerToUpdate);
                    Console.WriteLine("Customer updated successfully.");
                    Console.ReadLine();
                    break;

                case "4":
                    Console.Write("Enter Customer ID to Delete: ");
                    id = int.Parse(Console.ReadLine());
                    service.DeleteCustomer(id);
                    Console.WriteLine("Customer deleted successfully.");
                    Console.ReadLine();
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    static void ManageProducts(ProductServices service)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== Product Management ===");
        Console.WriteLine("1. List Products");
        Console.WriteLine("2. Add Product");
        Console.WriteLine("3. Update Product");
        Console.WriteLine("4. Delete Product");
        Console.WriteLine("5. Back");
        Console.Write("Choose an option: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                var products = service.GetProducts();
                Console.WriteLine("\n=== Product List ===");
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.ProductId}. {product.Name} - Price: {product.Price}");
                }
                Console.ReadLine();
                break;

            case "2":
                Console.Write("Enter Product Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Product Price: ");
                decimal price = decimal.Parse(Console.ReadLine());
                var newProduct = new Product { Name = name, Price = price };
                service.AddProduct(newProduct);
                Console.WriteLine("Product added successfully.");
                Console.ReadLine();
                break;

            case "3":
                Console.Write("Enter Product ID to Update: ");
                int id = int.Parse(Console.ReadLine());
                var productToUpdate = service.GetProductById(id);
                if (productToUpdate == null)
                {
                    Console.WriteLine("Product not found.");
                    Console.ReadLine();
                    break;
                }

                Console.Write("Enter New Name (Leave empty to keep current): ");
                name = Console.ReadLine();
                Console.Write("Enter New Price (Leave empty to keep current): ");
                string priceInput = Console.ReadLine();

                if (!string.IsNullOrEmpty(name)) productToUpdate.Name = name;
                if (!string.IsNullOrEmpty(priceInput)) productToUpdate.Price = decimal.Parse(priceInput);

                service.UpdateProduct(productToUpdate);
                Console.WriteLine("Product updated successfully.");
                Console.ReadLine();
                break;

            case "4":
                Console.Write("Enter Product ID to Delete: ");
                id = int.Parse(Console.ReadLine());
                service.DeleteProduct(id);
                Console.WriteLine("Product deleted successfully.");
                Console.ReadLine();
                break;

            case "5":
                return;

            default:
                Console.WriteLine("Invalid option. Try again.");
                break;
        }
    }
}


    static void ManageOrders(OrderServices service)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== Order Management ===");
        Console.WriteLine("1. List Orders");
        Console.WriteLine("2. Add Order");
        Console.WriteLine("3. Update Order");
        Console.WriteLine("4. Delete Order");
        Console.WriteLine("5. Back");
        Console.Write("Choose an option: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                var orders = service.GetOrders();
                Console.WriteLine("\n=== Order List ===");
                foreach (var order in orders)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}, Customer ID: {order.CustomerId}, Product ID: {order.ProductId}, Quantity: {order.Quantity}, Total Price: {order.TotalPrice}, Order Date: {order.OrderDate}");
                }
                Console.ReadLine();
                break;

            case "2":
                Console.Write("Enter Customer ID: ");
                int customerId = int.Parse(Console.ReadLine());
                Console.Write("Enter Product ID: ");
                int productId = int.Parse(Console.ReadLine());
                Console.Write("Enter Quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                var newOrder = new Order
                {
                    CustomerId = customerId,
                    ProductId = productId,
                    Quantity = quantity,
                    OrderDate = DateTime.Now
                };

                if (service.AddOrder(newOrder))
                {
                    Console.WriteLine("Order added successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to add order. Check customer budget or other constraints.");
                }
                Console.ReadLine();
                break;

            case "3":
                Console.Write("Enter Order ID to Update: ");
                int orderId = int.Parse(Console.ReadLine());
                var orderToUpdate = service.GetOrderById(orderId);
                if (orderToUpdate == null)
                {
                    Console.WriteLine("Order not found.");
                    Console.ReadLine();
                    break;
                }

                Console.Write("Enter New Customer ID: ");
                customerId = int.Parse(Console.ReadLine());
                Console.Write("Enter New Product ID: ");
                productId = int.Parse(Console.ReadLine());
                Console.Write("Enter New Quantity: ");
                quantity = int.Parse(Console.ReadLine());

                orderToUpdate.CustomerId = customerId;
                orderToUpdate.ProductId = productId;
                orderToUpdate.Quantity = quantity;

                if (service.UpdateOrder(orderToUpdate))
                {
                    Console.WriteLine("Order updated successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to update order.");
                }
                Console.ReadLine();
                break;

            case "4":
                Console.Write("Enter Order ID to Delete: ");
                orderId = int.Parse(Console.ReadLine());
                if (service.DeleteOrder(orderId))
                {
                    Console.WriteLine("Order deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to delete order.");
                }
                Console.ReadLine();
                break;

            case "5":
                return;

            default:
                Console.WriteLine("Invalid option. Try again.");
                break;
        }
    }
}

}
