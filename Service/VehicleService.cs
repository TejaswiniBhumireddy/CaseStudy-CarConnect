using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CarConnect.Model;
using CarConnect.Util;

namespace CarConnect.Service
{
    internal class VehicleService : IVehicleservice
    {
        public Vehicle GetVehicleById(int vehicleId)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    string query = "SELECT * FROM VEHICLE WHERE VEHICLE_ID = @VehicleId";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VehicleId", vehicleId);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            Vehicle vehicle = new Vehicle
                            {
                                VEHICLE_ID = (int)reader["VEHICLE_ID"],
                                MODEL = reader["MODEL"].ToString(),
                                MAKER = reader["MAKER"].ToString(),
                                YEAR_COL = Convert.ToDateTime(reader["YEAR_COL"]),
                                COLOR = reader["COLOR"].ToString(),
                                REGISTRATION_NUM = reader["REGISTRATION_NUM"].ToString(),
                                AVAILABILITY = (bool)reader["AVAILABILITY"],
                                DIALY_RATE = (decimal)reader["DIALY_RATE"]
                            };
                            return vehicle;
                        }
                        else
                        {
                            return null; // Vehicle not found
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving vehicle data: {ex.Message}");
                return null;
            }
        }

        public List<Vehicle> GetAvailableVehicles()
        {
            try
            {
                List<Vehicle> vehicles = new List<Vehicle>();
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    string query = "SELECT * FROM VEHICLE WHERE AVAILABILITY = 1";
                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Vehicle vehicle = new Vehicle
                            {
                                VEHICLE_ID = (int)reader["VEHICLE_ID"],
                                MODEL = reader["MODEL"].ToString(),
                                MAKER = reader["MAKER"].ToString(),
                                YEAR_COL = Convert.ToDateTime(reader["YEAR_COL"]),
                                COLOR = reader["COLOR"].ToString(),
                                REGISTRATION_NUM = reader["REGISTRATION_NUM"].ToString(),
                                AVAILABILITY = (bool)reader["AVAILABILITY"],
                                DIALY_RATE = (decimal)reader["DIALY_RATE"]
                            };
                            vehicles.Add(vehicle);
                        }
                    }
                }
                return vehicles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving available vehicles: {ex.Message}");
                return new List<Vehicle>();
            }
        }

        public void AddVehicle(Vehicle vehicle)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    string query = "INSERT INTO VEHICLE (MODEL, MAKER, YEAR_COL, COLOR, REGISTRATION_NUM, AVAILABILITY, DIALY_RATE) " +
                                   "VALUES (@Model, @Maker, @YearCol, @Color, @RegistrationNum, @Availability, @DialyRate)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Model", vehicle.MODEL);
                        command.Parameters.AddWithValue("@Maker", vehicle.MAKER);
                        command.Parameters.AddWithValue("@YearCol", vehicle.YEAR_COL);
                        command.Parameters.AddWithValue("@Color", vehicle.COLOR);
                        command.Parameters.AddWithValue("@RegistrationNum", vehicle.REGISTRATION_NUM);
                        command.Parameters.AddWithValue("@Availability", vehicle.AVAILABILITY);
                        command.Parameters.AddWithValue("@DialyRate", vehicle.DIALY_RATE);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Vehicle added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the vehicle: {ex.Message}");
            }
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    var existingVehicle = GetVehicleById(vehicle.VEHICLE_ID);
                    if (existingVehicle != null)
                    {
                        string query = @"
                        UPDATE VEHICLE
                        SET 
                            MODEL = @Model,
                            MAKER = @Maker,
                            YEAR_COL = @YearCol,
                            COLOR = @Color,
                            REGISTRATION_NUM = @RegistrationNum,
                            AVAILABILITY = @Availability,
                            DIALY_RATE = @DialyRate
                        WHERE VEHICLE_ID = @VehicleId";

                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Model", vehicle.MODEL);
                            command.Parameters.AddWithValue("@Maker", vehicle.MAKER);
                            command.Parameters.AddWithValue("@YearCol", vehicle.YEAR_COL);
                            command.Parameters.AddWithValue("@Color", vehicle.COLOR);
                            command.Parameters.AddWithValue("@RegistrationNum", vehicle.REGISTRATION_NUM);
                            command.Parameters.AddWithValue("@Availability", vehicle.AVAILABILITY);
                            command.Parameters.AddWithValue("@DialyRate", vehicle.DIALY_RATE);
                            command.Parameters.AddWithValue("@VehicleId", vehicle.VEHICLE_ID);

                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Vehicle updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Vehicle update failed.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Vehicle with ID {vehicle.VEHICLE_ID} does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating vehicle data: {ex.Message}");
            }
        }

        public void RemoveVehicle(int vehicleId)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    var existingVehicle = GetVehicleById(vehicleId);
                    if (existingVehicle != null)
                    {
                        string query = "DELETE FROM VEHICLE WHERE VEHICLE_ID = @VehicleId";

                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@VehicleId", vehicleId);
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Vehicle removed successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Vehicle removal failed.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Vehicle with ID {vehicleId} does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while removing the vehicle: {ex.Message}");
            }
        }
    }
}
