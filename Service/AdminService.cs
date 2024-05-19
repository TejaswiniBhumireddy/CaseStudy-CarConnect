using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CarConnect.Exceptions;
using CarConnect.Model;
using CarConnect.Util;

namespace CarConnect.Service
{
    internal class AdminService : IAdminService
    {
        public Admin GetAdminById(int adminId)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    string query = "SELECT * FROM ADMIN WHERE ADMIN_ID = @AdminId";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AdminId", adminId);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            Admin admin = new Admin
                            {
                                ADMIN_ID = (int)reader["ADMIN_ID"],
                                FIRST_NAME = reader["FIRST_NAME"].ToString(),
                                LAST_NAME = reader["LAST_NAME"].ToString(),
                                EMAIL = reader["EMAIL"].ToString(),
                                PHONE_NUMBER = reader["PHONE_NUMBER"].ToString(),
                                USERNAME = reader["USERNAME"].ToString(),
                                PASSWORDHASH = reader["PASSWORDHASH"].ToString(),
                                ROLE = reader["ROLE"].ToString(),
                                JOIN_DATE = Convert.ToDateTime(reader["JOIN_DATE"])
                            };
                            return admin;
                        }
                        else
                        {
                            return null; // Admin not found
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving admin data: {ex.Message}");
                return null;
            }
        }

        public Admin GetAdminByUsername(string username)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    string query = "SELECT * FROM ADMIN WHERE USERNAME = @Username";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            Admin admin = new Admin
                            {
                                ADMIN_ID = (int)reader["ADMIN_ID"],
                                FIRST_NAME = reader["FIRST_NAME"].ToString(),
                                LAST_NAME = reader["LAST_NAME"].ToString(),
                                EMAIL = reader["EMAIL"].ToString(),
                                PHONE_NUMBER = reader["PHONE_NUMBER"].ToString(),
                                USERNAME = reader["USERNAME"].ToString(),
                                PASSWORDHASH = reader["PASSWORDHASH"].ToString(),
                                ROLE = reader["ROLE"].ToString(),
                                JOIN_DATE = Convert.ToDateTime(reader["JOIN_DATE"])
                            };
                            return admin;
                        }
                        else
                        {
                            return null; // Admin not found
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving admin data: {ex.Message}");
                return null;
            }
        }

        public void RegisterAdmin(Admin adminData)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    string query = @"
                        INSERT INTO ADMIN (FIRST_NAME, LAST_NAME, EMAIL, PHONE_NUMBER, USERNAME, PASSWORDHASH, ROLE, JOIN_DATE)
                        VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Username, @PasswordHash, @Role, @JoinDate)";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", adminData.FIRST_NAME);
                        command.Parameters.AddWithValue("@LastName", adminData.LAST_NAME);
                        command.Parameters.AddWithValue("@Email", adminData.EMAIL);
                        command.Parameters.AddWithValue("@PhoneNumber", adminData.PHONE_NUMBER);
                        command.Parameters.AddWithValue("@Username", adminData.USERNAME);
                        command.Parameters.AddWithValue("@PasswordHash", adminData.PASSWORDHASH);
                        command.Parameters.AddWithValue("@Role", adminData.ROLE);
                        command.Parameters.AddWithValue("@JoinDate", adminData.JOIN_DATE);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while registering admin: {ex.Message}");
            }
        }

        public void UpdateAdmin(Admin adminData)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    string query = @"
                        UPDATE ADMIN
                        SET FIRST_NAME = @FirstName,
                            LAST_NAME = @LastName,
                            EMAIL = @Email,
                            PHONE_NUMBER = @PhoneNumber,
                            USERNAME = @Username,
                            PASSWORDHASH = @PasswordHash,
                            ROLE = @Role
                        WHERE ADMIN_ID = @AdminId";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", adminData.FIRST_NAME);
                        command.Parameters.AddWithValue("@LastName", adminData.LAST_NAME);
                        command.Parameters.AddWithValue("@Email", adminData.EMAIL);
                        command.Parameters.AddWithValue("@PhoneNumber", adminData.PHONE_NUMBER);
                        command.Parameters.AddWithValue("@Username", adminData.USERNAME);
                        command.Parameters.AddWithValue("@PasswordHash", adminData.PASSWORDHASH);
                        command.Parameters.AddWithValue("@Role", adminData.ROLE);
                        command.Parameters.AddWithValue("@AdminId", adminData.ADMIN_ID);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating admin: {ex.Message}");
            }
        }

        public void DeleteAdmin(int adminId)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    string query = "DELETE FROM ADMIN WHERE ADMIN_ID = @AdminId";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AdminId", adminId);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting admin: {ex.Message}");
            }
        }


        public void ValidateAdmin(int adminId, string password)
        {
            // Retrieve the admin from the database
            Admin admin = GetAdminById(adminId);

            if (admin == null || !admin.Authenticate(password))
            {
                throw new AdminNotFoundException("Admin ID or password is incorrect.");
            }
        }
    }
}
