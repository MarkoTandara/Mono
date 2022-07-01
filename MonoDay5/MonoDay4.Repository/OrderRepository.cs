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
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;

        public List<Order> GetAllOrders()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            using (connection)
            {
                List<Order> orders = new List<Order>();
                SqlCommand command = new SqlCommand(
                "SELECT * FROM Orders", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order(reader.GetGuid(0), reader.GetGuid(1), reader.GetString(2)));
                    }
                }
                reader.Close();
                return orders;
            }
        }

        public Order GetOrder(Guid orderId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            using (connection)
            {
                Order order = new Order();
                SqlCommand command = new SqlCommand(
                "SELECT * FROM Orders WHERE OrderID=@orderId", connection);
                command.Parameters.AddWithValue("@orderId", orderId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        order.OrderID = reader.GetGuid(0);
                        order.CustomerID = reader.GetGuid(1);
                        order.OrderName = reader.GetString(2);
                    }
                }
                reader.Close();
                return order;
            }
        }

        public Order PostOrder(Guid customerId, Order order)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            using (connection)
            {
                Guid orderNewId = Guid.NewGuid();
                order.OrderID = orderNewId;
                SqlCommand command = new SqlCommand(
                "INSERT INTO Orders (OrderID, CustomerID, OrderName) VALUES(@orderId, @customerId, @orderName)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@orderId", order.OrderID);
                command.Parameters.AddWithValue("@customerId", customerId);
                command.Parameters.AddWithValue("@orderName", order.OrderName);
                command.ExecuteNonQuery();
            }
            return order;
        }

        public Order PutOrder(Guid orderId, Order order)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand(
                    "UPDATE Orders SET OrderName = @orderName WHERE OrderID = @orderId", connection);
                connection.Open();
                command.Parameters.AddWithValue("@orderId", orderId);
                command.Parameters.AddWithValue("@orderName", order.OrderName);
                command.ExecuteNonQuery();
            }
            return order;
        }

        public bool DeleteOrder(Guid orderId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            using (connection)
            {
                connection.Open();
                SqlCommand commandCount = new SqlCommand(
                "SELECT Count(*) FROM Orders WHERE OrderID = @orderID", connection);
                commandCount.Parameters.AddWithValue("@orderID", orderId);
                int count = (int)commandCount.ExecuteScalar();
                if (count == 0)
                {
                    return false;
                }
                SqlCommand command = new SqlCommand(
                "DELETE FROM Orders WHERE orderID = @orderID", connection);
                command.Parameters.AddWithValue("@orderID", orderId);
                command.ExecuteNonQuery();
                return true;
            }
        }
    }
}
