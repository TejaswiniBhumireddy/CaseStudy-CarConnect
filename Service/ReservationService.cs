using System;
using System.Collections.Generic;
using System.Linq;
using CarConnect.Model;
using CarConnect.Exceptions;
using CarConnect.Util;
using System.Data.SqlClient;

namespace CarConnect.Service
{
    internal class ReservationService : IReservationService
    {
        private readonly List<Reservation> _reservations;

        public ReservationService()
        {
            _reservations = new List<Reservation>();
        }

        public Reservation GetReservationById(int reservationId)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    string query = "SELECT * FROM RESERVATION WHERE RESERVATION_ID = @ReservationId";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReservationId", reservationId);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            Reservation reservation = new Reservation
                            {
                                RESERVATION_ID = (int)reader["RESERVATION_ID"],
                                CUSTOMER_ID = (int)reader["CUSTOMER_ID"],
                                VEHICLE_ID = (int)reader["VEHICLE_ID"],
                                START_DATE = Convert.ToDateTime(reader["START_DATE"]),
                                END_DATE = Convert.ToDateTime(reader["END_DATE"]),
                                TOTAL_COST = (decimal)reader["TOTAL_COST"],
                                STATUS = reader["STATUS"].ToString()
                            };
                            return reservation;
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
                Console.WriteLine($"An error occurred while retrieving reservation data: {ex.Message}");
                return null;
            }
        }



        public List<Reservation> GetReservationsByCustomerId(int customerId)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM RESERVATION WHERE CUSTOMER_ID = @CustomerId";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerId", customerId);
                        SqlDataReader reader = command.ExecuteReader();

                        List<Reservation> reservations = new List<Reservation>();
                        while (reader.Read())
                        {
                            Reservation reservation = new Reservation
                            {
                                RESERVATION_ID = (int)reader["RESERVATION_ID"],
                                CUSTOMER_ID = (int)reader["CUSTOMER_ID"],
                                VEHICLE_ID = (int)reader["VEHICLE_ID"],
                                START_DATE = Convert.ToDateTime(reader["START_DATE"]),
                                END_DATE = Convert.ToDateTime(reader["END_DATE"]),
                                TOTAL_COST = (decimal)reader["TOTAL_COST"],
                                STATUS = reader["STATUS"].ToString()
                            };
                            reservations.Add(reservation);
                        }
                        return reservations;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving reservation data: {ex.Message}");
                return new List<Reservation>();
            }
        }


        public void CreateReservation(Reservation reservationData)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    connection.Open();

                    string checkQuery = @"
                SELECT COUNT(*) FROM RESERVATION 
                WHERE VEHICLE_ID = @VehicleId 
                AND (
                    @StartDate BETWEEN START_DATE AND END_DATE 
                    OR @EndDate BETWEEN START_DATE AND END_DATE
                )";

                    using (var checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@VehicleId", reservationData.VEHICLE_ID);
                        checkCommand.Parameters.AddWithValue("@StartDate", reservationData.START_DATE);
                        checkCommand.Parameters.AddWithValue("@EndDate", reservationData.END_DATE);

                        int count = (int)checkCommand.ExecuteScalar();
                        if (count > 0)
                        {
                            throw new ReservationException("Vehicle is already reserved for the given dates.");
                        }
                    }

                    
                    string insertQuery = @"
                INSERT INTO RESERVATION (CUSTOMER_ID, VEHICLE_ID, START_DATE, END_DATE, TOTAL_COST, STATUS) 
                VALUES (@CustomerId, @VehicleId, @StartDate, @EndDate, @TotalCost, @Status)";

                    using (var insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@CustomerId", reservationData.CUSTOMER_ID);
                        insertCommand.Parameters.AddWithValue("@VehicleId", reservationData.VEHICLE_ID);
                        insertCommand.Parameters.AddWithValue("@StartDate", reservationData.START_DATE);
                        insertCommand.Parameters.AddWithValue("@EndDate", reservationData.END_DATE);
                        insertCommand.Parameters.AddWithValue("@TotalCost", reservationData.TOTAL_COST);
                        insertCommand.Parameters.AddWithValue("@Status", reservationData.STATUS);

                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the reservation: {ex.Message}");
            }
        }


        public void UpdateReservation(Reservation reservationData)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    connection.Open();

                    var existingReservation = GetReservationById(reservationData.RESERVATION_ID);
                    if (existingReservation != null)
                    {
                        string query = @"
                UPDATE RESERVATION
                SET 
                    CUSTOMER_ID = @CustomerId,
                    VEHICLE_ID = @VehicleId,
                    START_DATE = @StartDate,
                    END_DATE = @EndDate,
                    TOTAL_COST = @TotalCost,
                    STATUS = @Status
                WHERE RESERVATION_ID = @ReservationId";

                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@CustomerId", reservationData.CUSTOMER_ID);
                            command.Parameters.AddWithValue("@VehicleId", reservationData.VEHICLE_ID);
                            command.Parameters.AddWithValue("@StartDate", reservationData.START_DATE);
                            command.Parameters.AddWithValue("@EndDate", reservationData.END_DATE);
                            command.Parameters.AddWithValue("@TotalCost", reservationData.TOTAL_COST);
                            command.Parameters.AddWithValue("@Status", reservationData.STATUS);
                            command.Parameters.AddWithValue("@ReservationId", reservationData.RESERVATION_ID);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Reservation updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Reservation update failed.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Reservation with ID {reservationData.RESERVATION_ID} does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating reservation data: {ex.Message}");
            }
        }



        public void CancelReservation(int reservationId)
        {
            try
            {
                using (var connection = DBConnUtil.GetSqlConnection())
                {
                    string query = "DELETE FROM RESERVATION WHERE RESERVATION_ID = @ReservationId";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReservationId", reservationId);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Reservation with ID {reservationId} has been cancelled successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Reservation with ID {reservationId} does not exist.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while cancelling reservation: {ex.Message}");
            }
        }

        public bool IsVehicleReserved(int vehicleId, DateTime startDate, DateTime endDate)
        {
            foreach (var reservation in _reservations)
            {
                if (reservation.VEHICLE_ID == vehicleId &&
                    !(reservation.END_DATE <= startDate || reservation.START_DATE >= endDate))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
