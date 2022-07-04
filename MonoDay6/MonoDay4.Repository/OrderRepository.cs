using MonoDay4.Model;
using MonoDay4.Repository.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoDay4.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                List<Order> orders = new List<Order>();
                SqlCommand command = new SqlCommand(
                "SELECT * FROM Orders", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        orders.Add(new Order(reader.GetGuid(0), reader.GetGuid(1), reader.GetString(2)));
                    }
                }
                reader.Close();
                return orders;
            }
        }

        public async Task<Order> GetOrderAsync(Guid orderId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                Order order = new Order();
                SqlCommand commandCount = new SqlCommand(
                "SELECT Count (*) FROM Orders WHERE OrderID=@orderId", connection);
                connection.Open();
                commandCount.Parameters.AddWithValue("@orderId", orderId);
                int count = (int)await commandCount.ExecuteScalarAsync();
                if (count == 0) throw new Exception("No order with given Id!");
                SqlCommand command = new SqlCommand(
                "SELECT * FROM Orders WHERE OrderID=@orderId", connection);
                command.Parameters.AddWithValue("@orderId", orderId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (await reader.ReadAsync())
                    {
                        order.OrderId = reader.GetGuid(0);
                        order.CustomerId = reader.GetGuid(1);
                        order.OrderName = reader.GetString(2);
                    }
                }
                reader.Close();
                return order;
            }
        }

        public async Task<Order> PostOrderAsync(Guid customerId, Order order)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                Guid orderNewId = Guid.NewGuid();
                order.OrderId = orderNewId;
                SqlCommand commandCount = new SqlCommand(
                "SELECT Count (*) FROM Customer WHERE CustomerID=@customerId", connection);
                connection.Open();
                commandCount.Parameters.AddWithValue("@customerId", customerId);
                int count = (int) await commandCount.ExecuteScalarAsync();
                if (count == 0) throw new Exception("No customer with given Id!");

                SqlCommand command = new SqlCommand(
                "INSERT INTO Orders (OrderID, CustomerID, OrderName) VALUES(@orderId, @customerId, @orderName)", connection);
                command.Parameters.AddWithValue("@orderId", order.OrderId);
                command.Parameters.AddWithValue("@customerId", customerId);
                command.Parameters.AddWithValue("@orderName", order.OrderName);
                await command.ExecuteNonQueryAsync();
            }
            return order;
        }

        public async Task<Order> PutOrderAsync(Guid orderId, Order order)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand commandCount = new SqlCommand(
                "SELECT Count (*) FROM Orders WHERE OrderID=@orderId", connection);
                connection.Open();
                commandCount.Parameters.AddWithValue("@orderId", orderId);
                int count = (int) await commandCount.ExecuteScalarAsync();
                if (count == 0) throw new Exception("No order with given Id!");
                SqlCommand command = new SqlCommand(
                "UPDATE Orders SET OrderName = @orderName WHERE OrderID = @orderId", connection);
                command.Parameters.AddWithValue("@orderId", orderId);
                command.Parameters.AddWithValue("@orderName", order.OrderName);
                await command.ExecuteNonQueryAsync();
            }
            return order;
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                SqlCommand commandCount = new SqlCommand(
                "SELECT Count(*) FROM Orders WHERE OrderID = @orderId", connection);
                commandCount.Parameters.AddWithValue("@orderId", orderId);
                int count = (int) await commandCount.ExecuteScalarAsync();
                if (count == 0) throw new Exception("No order with given Id!");
                SqlCommand command = new SqlCommand(
                "DELETE FROM Orders WHERE orderID = @orderId", connection);
                command.Parameters.AddWithValue("@orderId", orderId);
                await command.ExecuteNonQueryAsync ();
                return true;
            }
        }
    }
}
