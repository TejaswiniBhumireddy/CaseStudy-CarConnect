using System;

namespace CarConnect.Model
{
    internal class Admin
    {
        public int ADMIN_ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string EMAIL { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORDHASH { get; set; }
        public string ROLE { get; set; }
        public DateTime JOIN_DATE { get; set; }

        public Admin() { }

        public Admin(int adminId, string firstName, string lastName, string email, string phoneNumber, string username, string passwordHash, string role, DateTime joinDate)
        {
            ADMIN_ID = adminId;
            FIRST_NAME = firstName;
            LAST_NAME = lastName;
            EMAIL = email;
            PHONE_NUMBER = phoneNumber;
            USERNAME = username;
            PASSWORDHASH = passwordHash;
            ROLE = role;
            JOIN_DATE = joinDate;
        }

        

        public bool Authenticate(string password)
        {
            return PASSWORDHASH == password;
        }
    }
}
