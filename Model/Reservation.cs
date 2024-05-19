using System;

namespace CarConnect.Model
{
    internal class Reservation
    {
         public int RESERVATION_ID { get; set; }
        public int CUSTOMER_ID { get;set; }
        public int VEHICLE_ID { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public decimal TOTAL_COST { get; set; }
        public string STATUS { get; set; }

        public Reservation() { }

        public Reservation(int reservationId, int customerId, int vehicleId, DateTime startDate, DateTime endDate, decimal totalCost, string status)
        {
            RESERVATION_ID = reservationId;
            CUSTOMER_ID = customerId;
            VEHICLE_ID = vehicleId;
            START_DATE = startDate;
            END_DATE = endDate;
            TOTAL_COST = totalCost;
            STATUS = status;
        }


        public decimal CalculateTotalCost(decimal dailyRate, int numberOfDays)
        {
            TOTAL_COST = dailyRate * numberOfDays;
            return TOTAL_COST;
        }
    }
}
