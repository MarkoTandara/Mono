using MonoDay4.Common;
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

        private async Task<bool> OrderIdExistsAsync(Guid orderId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                await connection.OpenAsync();
                try
                {
                    SqlCommand commandCheckId = new SqlCommand(
                    "SELECT Count (*) FROM Orders WHERE OrderID=@orderId", connection);
                    commandCheckId.Parameters.AddWithValue("@orderId", orderId);
                    if ((int)await commandCheckId.ExecuteScalarAsync() > 0)
                    {
                        return true;
                    }
                    else return false;
                }
                catch (SqlException)
                {
                    throw new Exception("Incorrect SQL command!");
                }
            }
        }

        private async Task<bool> CustomerIdExistsAsync(Guid customerId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                await connection.OpenAsync();
                try
                {
                    SqlCommand commandCheckId = new SqlCommand(
                    "SELECT Count (*) FROM Customer WHERE CustomerID=@customerId", connection);
                    commandCheckId.Parameters.AddWithValue("@customerId", customerId);
                    if ((int)await commandCheckId.ExecuteScalarAsync() > 0)
                    {
                        return true;
                    }
                    else return false;
                }
                catch (SqlException)
                {
                    throw new Exception("Incorrect SQL command!");
                }
            }
        }

        public async Task<List<Order>> FindOrderAsync(Paging paging, Sorting sorting, FilterOrder filterOrder)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                List<Order> orders = new List<Order>();
                SqlCommand command = new SqlCommand("", connection);
                int offset = (paging.PageNumber - 1) * paging.RecordsByPage;
                StringBuilder stringBuilder = new StringBuilder("SELECT * FROM Orders WHERE 1=1 ");
                if (filterOrder.CustomerId != null)
                {
                    stringBuilder.Append("and CustomerID=@customerId ");
                    command.Parameters.AddWithValue("@customerId", filterOrder.CustomerId);
                }
                if (filterOrder.OrderName != null)
                {
                    stringBuilder.Append("and OrderName=@orderName ");
                    command.Parameters.AddWithValue("@orderName", filterOrder.OrderName);
                }
                stringBuilder.AppendFormat("ORDER BY {0} {1} ", sorting.OrderBy, sorting.SortOrder);
                stringBuilder.Append("offset @offset rows fetch next @recordsByPage rows only;");

                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@recordsByPage", paging.RecordsByPage);

                command.CommandText = stringBuilder.ToString();
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
            if (await OrderIdExistsAsync(orderId) == false)
            {
                throw new Exception("No orders with given Id!");
            }
            Order order;
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand(
                "SELECT * FROM Orders WHERE OrderID=@orderId", connection);
                command.Parameters.AddWithValue("@orderId", orderId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        order = new Order(reader.GetGuid(0), reader.GetGuid(1), reader.GetString(2));
                        return order;
                    }
                    else
                    {
                        throw new Exception("Object does not exists!");
                    }
                }
                catch (SqlException)
                {
                    throw new Exception("Incorrect SQL command!");
                }
            }
        }

        public async Task<Order> PostOrderAsync(Guid customerId, Order order)
        {
            if (await CustomerIdExistsAsync(customerId) == false)
            {
                throw new Exception("No customer with given Id!");
            }
            Guid orderNewId = Guid.NewGuid();
            order.OrderId = orderNewId;
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                "INSERT INTO Orders (OrderID, CustomerID, OrderName) VALUES(@orderId, @customerId, @orderName)", connection);
                command.Parameters.AddWithValue("@orderId", order.OrderId);
                command.Parameters.AddWithValue("@customerId", customerId);
                command.Parameters.AddWithValue("@orderName", order.OrderName);
                try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch (SqlException)
                {
                    throw new Exception("Incorrect SQL command!");
                }
            }
            return order;
        }

        public async Task<Order> PutOrderAsync(Guid orderId, Order order)
        {
            if (await OrderIdExistsAsync(orderId) == false)
            {
                throw new Exception("No orders with given Id!");
            }
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                "UPDATE Orders SET OrderName = @orderName WHERE OrderID = @orderId", connection);
                command.Parameters.AddWithValue("@orderId", orderId);
                command.Parameters.AddWithValue("@orderName", order.OrderName);
                try
                {
                    await command.ExecuteNonQueryAsync();
                    return order;
                }
                catch (SqlException)
                {
                    throw new Exception("Incorrect SQL command!");
                }
            }
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            if (await OrderIdExistsAsync(orderId) == false)
            {
                throw new Exception("No orders with given Id!");
            }
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                "DELETE FROM Orders WHERE orderID = @orderId", connection);
                command.Parameters.AddWithValue("@orderId", orderId);
                try
                {
                    await command.ExecuteNonQueryAsync();
                    return true;
                }
                catch (SqlException)
                {
                    throw new Exception("Incorrect SQL command!");
                }
            }
        }
    }
}
