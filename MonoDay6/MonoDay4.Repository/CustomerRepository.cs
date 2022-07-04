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
    public class CustomerRepository : ICustomerRepository
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
        
        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                List<Customer> customers = new List<Customer>();

                SqlCommand commandCount = new SqlCommand(
                "SELECT Count(*) FROM Orders", connection);
                connection.Open();
                int count = (int)await commandCount.ExecuteScalarAsync();
                if (count == 0) throw new Exception("List is empty!");

                SqlCommand command = new SqlCommand(
                "SELECT * FROM Customer", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        customers.Add(new Customer(reader.GetGuid(0), reader.GetString(1), reader.GetString(2)));
                    }
                }
                reader.Close();
                return customers;
            }
        }

        public async Task<Customer> GetCustomerAsync(Guid customerId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                Customer customer = new Customer();

                SqlCommand commandCount = new SqlCommand(
                "SELECT Count (*) FROM Customer WHERE CustomerID=@customerId", connection);
                connection.Open();
                commandCount.Parameters.AddWithValue("@customerId", customerId);
                int count = (int)await commandCount.ExecuteScalarAsync();
                if (count == 0) throw new Exception("No customer with given Id!");

                SqlCommand command = new SqlCommand(
                "SELECT * FROM Customer WHERE CustomerID=@customerId", connection);
                command.Parameters.AddWithValue("@customerId", customerId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (await reader.ReadAsync())
                    {
                        customer.CustomerId = reader.GetGuid(0);
                        customer.FirstName = reader.GetString(1);
                        customer.LastName = reader.GetString(2);
                    }
                }
                reader.Close();
                return customer;
            }
        }

        public async Task <Customer> PostAsync(Customer customer)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                Guid customerNewId = Guid.NewGuid();
                customer.CustomerId = customerNewId;

                SqlCommand commandCount = new SqlCommand(
                "SELECT Count (*) FROM Customer WHERE CustomerID=@customerId", connection);
                connection.Open();
                commandCount.Parameters.AddWithValue("@customerId", customer.CustomerId);
                int count = (int)await commandCount.ExecuteScalarAsync();
                if (count > 0) throw new Exception("Customer already exists!");

                SqlCommand command = new SqlCommand(
                "INSERT INTO Customer (CustomerID, FirstName, LastName) VALUES(@customerId, @firstName, @lastName)", connection);
                command.Parameters.AddWithValue("@customerId", customer.CustomerId);
                command.Parameters.AddWithValue("@firstName", customer.FirstName);
                command.Parameters.AddWithValue("@lastName", customer.LastName);
                await command.ExecuteNonQueryAsync();                
            }
            return customer;
        }

        public async Task<Customer> PutAsync(Guid customerId, Customer customer)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand commandCount = new SqlCommand(
               "SELECT Count (*) FROM Customer WHERE CustomerID=@customerId", connection);
                connection.Open();
                commandCount.Parameters.AddWithValue("@customerId", customerId);
                int count = (int)await commandCount.ExecuteScalarAsync();
                if (count == 0) throw new Exception("Customer doesn't exists!");
                SqlCommand command = new SqlCommand(
                    "UPDATE Customer SET FirstName = @firstName, LastName = @lastName  WHERE CustomerID = @customerId", connection);
                command.Parameters.AddWithValue("@customerId", customerId);
                command.Parameters.AddWithValue("@firstName", customer.FirstName);
                command.Parameters.AddWithValue("@lastName", customer.LastName);
                await command.ExecuteNonQueryAsync();
            }
            return customer;
        }

        public async Task<bool> DeleteAsync(Guid customerId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                SqlCommand commandCount = new SqlCommand(
                "SELECT Count(*) FROM Customer WHERE CustomerID = @customerId", connection);
                commandCount.Parameters.AddWithValue("@customerId", customerId);
                int count = (int)commandCount.ExecuteScalar();
                if (count == 0) throw new Exception("Customer doesn't exists!");

                SqlCommand command = new SqlCommand(
                "DELETE FROM Customer WHERE CustomerID = @customerId", connection);
                command.Parameters.AddWithValue("@customerId", customerId);
                await command.ExecuteNonQueryAsync ();
                return true;
            }
        }
    }
}
