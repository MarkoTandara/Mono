using MonoDay4.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MonoDay4.WebApi.Controllers
{
    public class CustomerController : ApiController
    {

        public static string ConnectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll()
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
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No objects in list!");
                }
                reader.Close();
                return Request.CreateResponse(HttpStatusCode.OK, customers);
            }
        }

        [HttpGet]
        [Route("get")]
        public HttpResponseMessage Get([FromUri] Guid customerId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            using (connection)
            {
                Customer customer;
                SqlCommand command = new SqlCommand(
                "SELECT * FROM Customer WHERE CustomerID=@customerID", connection);
                command.Parameters.AddWithValue("@customerID", customerId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        customer = new Customer(reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
                        return Request.CreateResponse(HttpStatusCode.OK, customer);
                    }
                }
                reader.Close();
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given ID!");

            }
        }

        [HttpPost]
        [Route("post")]
        public HttpResponseMessage Post([FromBody] Customer customer)
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
                return Request.CreateResponse(HttpStatusCode.OK, customer);
            }
        }

        [HttpPut]
        [Route("put")]
        public void Put([FromUri] Guid customerId, [FromBody] Customer customer)
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
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete([FromUri] Guid customerId)
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
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given ID!");
                }
                SqlCommand command = new SqlCommand(
                "DELETE FROM Customer WHERE CustomerID = @customerID", connection);
                command.Parameters.AddWithValue("@customerID", customerId);
                command.ExecuteNonQuery();
                return Request.CreateResponse(HttpStatusCode.OK, "Customer deleted!");


            }
        }
    }
}
