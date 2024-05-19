using System;

namespace CarConnect.Model
{
    internal class Vehicle
    {
        public int VEHICLE_ID { get; set; }
        public string MODEL { get; set; }
        public string MAKER { get; set; }
        public DateTime YEAR_COL { get; set; }
        public string COLOR { get; set; }
        public string REGISTRATION_NUM { get; set; }
        public bool AVAILABILITY { get; set; }
        public decimal DIALY_RATE { get; set; }

        public Vehicle() { }

        public Vehicle(int vehicleId, string model, string maker, DateTime yearCol, string color, string registrationNum, bool availability, decimal dialyRate)
        {
            VEHICLE_ID = vehicleId;
            MODEL = model;
            MAKER = maker;
            YEAR_COL = yearCol;
            COLOR = color;
            REGISTRATION_NUM = registrationNum;
            AVAILABILITY = availability;
            DIALY_RATE = dialyRate;
        }

    }
}