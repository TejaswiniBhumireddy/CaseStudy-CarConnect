using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Service
{
    internal class AuthenticationService
    {
        private readonly CustomerService _customerService;
        private readonly AdminService _adminService;

        public AuthenticationService(CustomerService customerService, AdminService adminService)
        {
            _customerService = customerService;
            _adminService = adminService;
        }

        public bool AuthenticateCustomer(string username, string password)
        {
            var customer = _customerService.GetCustomerByUsername(username);
            if (customer == null || !customer.Authenticate(password))
            {
                throw new AuthenticationException("Incorrect username or password.");
            }
            return true;
        }

        public bool AuthenticateAdmin(string username, string password)
        {
            var admin = _adminService.GetAdminByUsername(username);
            if (admin == null || !admin.Authenticate(password))
            {
                throw new AuthenticationException("Incorrect username or password.");
            }
            return true;
        }

    }
}
