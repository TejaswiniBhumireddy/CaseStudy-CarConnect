using System.Runtime.Serialization;

namespace CarConnect.Exceptions
{
    [Serializable]
    internal class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException()
        {
        }

        public DatabaseConnectionException(string? message) : base(message)
        {
        }

        public DatabaseConnectionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}