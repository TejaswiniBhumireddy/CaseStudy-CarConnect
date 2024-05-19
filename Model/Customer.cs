using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Model
{
    internal class Customer
    {
        public int CUSTOMER_ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string EMAIL { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string ADDRESS { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public DateTime registration_date { get; set; }

        public Customer() { }

        public Customer(int customerId, string firstName, string lastName, string email, string phoneNumber, string address, string username, string password, DateTime registrationDate)
        {
            CUSTOMER_ID = customerId;
            FIRST_NAME = firstName;
            LAST_NAME = lastName;
            EMAIL = email;
            PHONE_NUMBER = phoneNumber;
            ADDRESS = address;
            USERNAME = username;
            PASSWORD = password;
            registration_date = registrationDate;
        }

       

        public bool Authenticate(string password)
        {
            return PASSWORD == password;
        }
    }
}
