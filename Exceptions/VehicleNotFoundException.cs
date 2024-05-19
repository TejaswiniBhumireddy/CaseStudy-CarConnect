using System.Runtime.Serialization;

namespace CarConnect.Exceptions
{
    internal class VehicleNotFoundException : Exception
    {
        public VehicleNotFoundException()
        {
        }

        public VehicleNotFoundException(string? message) : base(message)
        {
        }

        public VehicleNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}