using CarConnect.Model;
using CarConnect.Service;
using System;
using System.Collections.Generic;

namespace CarConnect.CarConnectApp
{
    public class ReportGenerator
    {
        private readonly ReservationService reservationService;
        private readonly VehicleService vehicleService;

        public ReportGenerator(ReservationService reservationService, VehicleService vehicleService)
        {
            this.reservationService = reservationService;
            this.vehicleService = vehicleService;
        }

        public void GenerateReservationHistoryReport(DateTime startDate, DateTime endDate)
        {
            List<Reservation> reservations = reservationService.GetReservationsByDateRange(startDate, endDate);

            if (reservations.Count == 0)
            {
                Console.WriteLine("No reservations found for the specified date range.");
                return;
            }

            Console.WriteLine("Reservation History Report:");
            foreach (var reservation in reservations)
            {
                Console.WriteLine($"Reservation ID: {reservation.RESERVATION_ID}, Customer ID: {reservation.CUSTOMER_ID}, Vehicle ID: {reservation.VEHICLE_ID}, Start Date: {reservation.START_DATE}, End Date: {reservation.END_DATE}, Status: {reservation.STATUS}, Total Cost: {reservation.TOTAL_COST}");
            }
        }

        public void GenerateVehicleUtilizationReport(DateTime startDate, DateTime endDate)
        {
            List<VehicleUtilizationData> utilizationData = reservationService.GetVehicleUtilizationData(startDate, endDate);

            if (utilizationData.Count == 0)
            {
                Console.WriteLine("No vehicle utilization data found for the specified date range.");
                return;
            }

            Console.WriteLine("Vehicle Utilization Report:");
            foreach (var data in utilizationData)
            {
                Console.WriteLine($"Vehicle ID: {data.VehicleId}, Total Reservations: {data.TotalReservations}, Total Days Rented: {data.TotalDaysRented}, Average Rental Duration: {data.AverageRentalDuration}, Average Revenue per Rental: {data.AverageRevenuePerRental}");
            }
        }

        public void GenerateRevenueReport(DateTime startDate, DateTime endDate)
        {
            List<RevenueData> revenueData = reservationService.GetRevenueData(startDate, endDate);

            if (revenueData.Count == 0)
            {
                Console.WriteLine("No revenue data found for the specified date range.");
                return;
            }

            Console.WriteLine("Revenue Report:");
            foreach (var data in revenueData)
            {
                Console.WriteLine($"Date: {data.Date}, Total Revenue: {data.TotalRevenue}, Revenue Breakdown: {data.RevenueBreakdown}");
            }
        }

    }
}
