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
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
        
        public List<Customer> GetAllCustomer()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            using (connection)
            {
                List<Customer> customers = new List<Customer>();
                SqlCommand command = new SqlCommand(
                "SELECT * FROM Customer", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer(reader.GetGuid(0), reader.GetString(1), reader.GetString(2)));
                    }
                }
                reader.Close();
                return customers;
            }
        }

        public Customer GetCustomer(Guid customerId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            using (connection)
            {
                Customer customer = new Customer();
                SqlCommand command = new SqlCommand(
                "SELECT * FROM Customer WHERE CustomerID=@customerID", connection);
                command.Parameters.AddWithValue("@customerID", customerId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        customer.CustomerID = reader.GetGuid(0);
                        customer.FirstName = reader.GetString(1);
                        customer.LastName = reader.GetString(2);
                    }
                }
                reader.Close();
                return customer;
            }
        }

        public Customer Post(Customer customer)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            using (connection)
            {
                Guid customerNewId = Guid.NewGuid();
                customer.CustomerID = customerNewId;
                SqlCommand command = new SqlCommand(
                "INSERT INTO Customer (CustomerID, FirstName, LastName) VALUES(@customerID, @firstName, @lastName)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@customerID", customer.CustomerID);
                command.Parameters.AddWithValue("@firstName", customer.FirstName);
                command.Parameters.AddWithValue("@lastName", customer.LastName);
                command.ExecuteNonQuery();                
            }
            return customer;
        }

        public Customer Put(Guid customerId, Customer customer)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand(
                    "UPDATE Customer SET FirstName = @firstName, LastName = @lastName  WHERE CustomerID = @customerID", connection);
                connection.Open();
                command.Parameters.AddWithValue("@customerID", customerId);
                command.Parameters.AddWithValue("@firstName", customer.FirstName);
                command.Parameters.AddWithValue("@lastName", customer.LastName);
                command.ExecuteNonQuery();
            }
            return customer;
        }

        public bool Delete(Guid customerId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            using (connection)
            {
                connection.Open();
                SqlCommand commandCount = new SqlCommand(
                "SELECT Count(*) FROM Customer WHERE CustomerID = @customerID", connection);
                commandCount.Parameters.AddWithValue("@customerID", customerId);
                int count = (int)commandCount.ExecuteScalar();
                if (count == 0)
                {
                    return false;
                }
                SqlCommand command = new SqlCommand(
                "DELETE FROM Customer WHERE CustomerID = @customerID", connection);
                command.Parameters.AddWithValue("@customerID", customerId);
                command.ExecuteNonQuery();
                return true;
            }
        }
    }
}
