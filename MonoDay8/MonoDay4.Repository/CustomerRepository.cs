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
    public class CustomerRepository : ICustomerRepository
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
        
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
                    if((int) await commandCheckId.ExecuteScalarAsync()>0)
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

        public async Task<List<Customer>> FindCustomerAsync(Paging paging, Sorting sorting, FilterCustomer filterCustomer)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("", connection);
                List<Customer> customers = new List<Customer>();
                int offset = (paging.PageNumber - 1) * paging.RecordsByPage;
                StringBuilder stringBuilder = new StringBuilder("SELECT * FROM Customer WHERE 1=1 ");
                if (filterCustomer.FirstName != null)
                {
                    stringBuilder.Append("and FirstName=@firstName ");
                    command.Parameters.AddWithValue("@firstName", filterCustomer.FirstName);
                }
                if (filterCustomer.LastName != null)
                {
                    stringBuilder.Append("and LastName=@lastName ");
                    command.Parameters.AddWithValue("@lastName", filterCustomer.LastName);
                }
                stringBuilder.Append(string.Format("ORDER BY {0} {1} ", sorting.OrderBy, sorting.SortOrder));
                stringBuilder.Append("offset @offset rows fetch next @recordsByPage rows only;");

                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@recordsByPage", paging.RecordsByPage);

                command.CommandText = stringBuilder.ToString();
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
            if (await CustomerIdExistsAsync(customerId) == false)
            {
                throw new Exception("No customer with given Id!");
            }
            Customer customer;
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand(
                "SELECT * FROM Customer WHERE CustomerID=@customerId", connection);
                command.Parameters.AddWithValue("@customerId", customerId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        customer = new Customer(reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
                        return customer;
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

        public async Task <Customer> PostAsync(Customer customer)
        {
            if (await CustomerIdExistsAsync(customer.CustomerId) == true)
            {
                throw new Exception("Customer already exists!");
            }
            Guid customerNewId = Guid.NewGuid();
            customer.CustomerId = customerNewId;
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                "INSERT INTO Customer (CustomerID, FirstName, LastName) VALUES(@customerId, @firstName, @lastName)", connection);
                command.Parameters.AddWithValue("@customerId", customer.CustomerId);
                command.Parameters.AddWithValue("@firstName", customer.FirstName);
                command.Parameters.AddWithValue("@lastName", customer.LastName);
                try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch(SqlException)
                {
                    throw new Exception("Incorrect SQL command!");
                }          
            }
            return customer;
        }

        public async Task<Customer> PutAsync(Guid customerId, Customer customer)
        {
            if (await CustomerIdExistsAsync(customerId) == false)
            {
                throw new Exception("No customer with given Id!");
            }
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                "UPDATE Customer SET FirstName = @firstName, LastName = @lastName  WHERE CustomerID = @customerId", connection);
                command.Parameters.AddWithValue("@customerId", customerId);
                command.Parameters.AddWithValue("@firstName", customer.FirstName);
                command.Parameters.AddWithValue("@lastName", customer.LastName);
                try
                {
                    await command.ExecuteNonQueryAsync();
                    return new Customer(customerId, customer.FirstName, customer.LastName);
                }
                catch (SqlException)
                {
                    throw new Exception("Incorrect SQL command!");
                }
            }
            
        }

        public async Task<bool> DeleteAsync(Guid customerId)
        {
            if (await CustomerIdExistsAsync(customerId) == false)
            {
                throw new Exception("No customer with given Id!");
            }
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                "DELETE FROM Customer WHERE CustomerID = @customerId", connection);
                command.Parameters.AddWithValue("@customerId", customerId);
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
