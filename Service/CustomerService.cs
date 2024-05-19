
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Model;
using CarConnect.Util;

namespace CarConnect.Service
{
    internal class CustomerService : ICustomerService
    {
        private readonly List<Customer> _customers;
        public CustomerService()
        {
            _customers = new List<Customer>();
        }

        public Customer GetCustomerById(int customerId)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    string query = "SELECT * FROM CUSTOMER WHERE CUSTOMER_ID = @CustomerId";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerId", customerId);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            Customer customer = new Customer
                            {
                                CUSTOMER_ID = (int)reader["CUSTOMER_ID"],
                                FIRST_NAME = reader["FIRST_NAME"].ToString(),
                                LAST_NAME = reader["LAST_NAME"].ToString(),
                                EMAIL = reader["EMAIL"].ToString(),
                                PHONE_NUMBER = reader["PHONE_NUMBER"].ToString(),
                                ADDRESS = reader["ADDRESS"].ToString(),
                                USERNAME = reader["USERNAME"].ToString(),
                                PASSWORD = reader["PASSWORD"].ToString(),
                                registration_date = Convert.ToDateTime(reader["REGISTRATION_DATE"])
                            };
                            return customer;
                        }
                        else
                        {
                            return null; // Customer not found
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving customer data: {ex.Message}");
                return null;
            }
        }




        public Customer GetCustomerByUsername(string username)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM CUSTOMER WHERE USERNAME = @Username";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            Customer customer = new Customer
                            {
                                CUSTOMER_ID = (int)reader["CUSTOMER_ID"],
                                FIRST_NAME = reader["FIRST_NAME"].ToString(),
                                LAST_NAME = reader["LAST_NAME"].ToString(),
                                EMAIL = reader["EMAIL"].ToString(),
                                PHONE_NUMBER = reader["PHONE_NUMBER"].ToString(),
                                ADDRESS = reader["ADDRESS"].ToString(),
                                USERNAME = reader["USERNAME"].ToString(),
                                PASSWORD = reader["PASSWORD"].ToString(),
                                registration_date = Convert.ToDateTime(reader["registration_date"])
                            };
                            return customer;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving customer data: {ex.Message}");
                return null;
            }
        }

        public void RegisterCustomer(Customer customer)
        {
            using (SqlConnection connection = DBConnUtil.GetSqlConnection())
            {
                connection.Open();
                string query = "INSERT INTO CUSTOMER (FIRST_NAME, LAST_NAME, EMAIL, PHONE_NUMBER, ADDRESS, USERNAME, PASSWORD, registration_date) " +
                               "VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Address, @Username, @Password, @RegistrationDate)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", customer.FIRST_NAME);
                    command.Parameters.AddWithValue("@LastName", customer.LAST_NAME);
                    command.Parameters.AddWithValue("@Email", customer.EMAIL);
                    command.Parameters.AddWithValue("@PhoneNumber", customer.PHONE_NUMBER);
                    command.Parameters.AddWithValue("@Address", customer.ADDRESS);
                    command.Parameters.AddWithValue("@Username", customer.USERNAME);
                    command.Parameters.AddWithValue("@Password", customer.PASSWORD);
                    command.Parameters.AddWithValue("@RegistrationDate", customer.registration_date);

                    command.ExecuteNonQuery();
                }
            }
        }


        public void UpdateCustomer(Customer customerData)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    connection.Open();

                    var existingCustomer = GetCustomerById(customerData.CUSTOMER_ID);
                    if (existingCustomer != null)
                    {
                        string query = @"
                    UPDATE CUSTOMER
                    SET 
                        FIRST_NAME = @FirstName,
                        LAST_NAME = @LastName,
                        EMAIL = @Email,
                        PHONE_NUMBER = @PhoneNumber,
                        ADDRESS = @Address,
                        USERNAME = @Username,
                        PASSWORD = @Password,
                        registration_date = @RegistrationDate
                    WHERE CUSTOMER_ID = @CustomerId";

                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@FirstName", customerData.FIRST_NAME);
                            command.Parameters.AddWithValue("@LastName", customerData.LAST_NAME);
                            command.Parameters.AddWithValue("@Email", customerData.EMAIL);
                            command.Parameters.AddWithValue("@PhoneNumber", customerData.PHONE_NUMBER);
                            command.Parameters.AddWithValue("@Address", customerData.ADDRESS);
                            command.Parameters.AddWithValue("@Username", customerData.USERNAME);
                            command.Parameters.AddWithValue("@Password", customerData.PASSWORD);
                            command.Parameters.AddWithValue("@RegistrationDate", customerData.registration_date);
                            command.Parameters.AddWithValue("@CustomerId", customerData.CUSTOMER_ID);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Customer updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Customer update failed.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Customer with ID {customerData.CUSTOMER_ID} does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating customer data: {ex.Message}");
            }
        }


        public void DeleteCustomer(int customerId)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    string query = "DELETE FROM CUSTOMER WHERE CUSTOMER_ID = @CustomerId";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerId", customerId);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Customer with ID {customerId} has been deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Customer with ID {customerId} does not exist.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting customer data: {ex.Message}");
            }
        }


    }
}