using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Model;

namespace CarConnect.Service
{
    internal interface IVehicleservice
    {
        Vehicle GetVehicleById(int vehicleId);

        List<Vehicle> GetAvailableVehicles();
        void AddVehicle(Vehicle vehicleData);
        void UpdateVehicle(Vehicle vehicleData);
        void RemoveVehicle(int vehicleId);
    }
}
