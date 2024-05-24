using NUnit.Framework;

using CarConnect.Model;
using CarConnect.Service;
using System;
using NUnit.Framework.Legacy;


namespace CarConnect.CarConnectUnitTests
{
    public class Tests
    {
     
        [Test]
        public void TestCustomerAuthentication_InvalidCredentials()
        {
            string username = "alicesmith";
            string password = "securepass";
            var customerService = new CarConnect.Service.CustomerService();
            var adminService = new AdminService();
            CarConnect.Service.AuthenticationService authenticationService = new CarConnect.Service.AuthenticationService(customerService, adminService);

            bool result = authenticationService.AuthenticateCustomer(username, password);

            Assert.That(true, Is.EqualTo(result));
        }



        [Test]
        public void TestUpdatingCustomerInformation()
        {
            CarConnect.Service.CustomerService customerService = new CarConnect.Service.CustomerService();
            Model.Customer customer = new Model.Customer
            {
                CUSTOMER_ID = 2,
                FIRST_NAME = "Alice",
                LAST_NAME = "Smith",
                EMAIL = "alice.smith@example.com",
                PHONE_NUMBER = "9876543210",
                ADDRESS = "456 Elm St, Townsville",
                USERNAME = "alicesmith",
                PASSWORD = "securepass",
                registration_date = DateTime.Now
            };

            customerService.UpdateCustomer(customer);

            // Re-fetch the customer to verify the update
            Model.Customer updatedCustomer = customerService.GetCustomerById(2);

            // Assert that the customer was updated with the expected values
            ClassicAssert.IsNotNull(updatedCustomer, "The updated customer was not found in the database.");
            Assert.That(updatedCustomer.CUSTOMER_ID, Is.EqualTo(2));
            Assert.That(updatedCustomer.FIRST_NAME, Is.EqualTo("Alice"));
            Assert.That(updatedCustomer.LAST_NAME, Is.EqualTo("Smith"));
            Assert.That(updatedCustomer.EMAIL, Is.EqualTo("alice.smith@example.com"));
            Assert.That(updatedCustomer.PHONE_NUMBER, Is.EqualTo("9876543210"));
            Assert.That(updatedCustomer.ADDRESS, Is.EqualTo("456 Elm St, Townsville"));
            Assert.That(updatedCustomer.USERNAME, Is.EqualTo("alicesmith"));
            Assert.That(updatedCustomer.PASSWORD, Is.EqualTo("securepass"));

        }


       
        [Test]
        public void TestAddingNewVehicle()
        {
            VehicleService vehicleService = new VehicleService();
            int addVehicleStatus = vehicleService.AddVehicle(new Vehicle
            {
                VEHICLE_ID = 1,
                MODEL = "nexon",
                MAKER = "Tata",
                YEAR_COL = DateTime.Now,
                COLOR = "Blue",
                REGISTRATION_NUM = "AP39AQ4956",
                AVAILABILITY = false,
                DIALY_RATE = 1000.0m 
            });

            Assert.That(0, Is.EqualTo(addVehicleStatus));
        }



        [Test]
        public void TestUpdatingVehicleDetails()
        {
            VehicleService vehicleService = new VehicleService();
            int vehicleId = 1;
            var vehicle = vehicleService.GetVehicleById(vehicleId);
            string originalModel = vehicle.MODEL;

            vehicle.MODEL = "Camry";
            vehicleService.UpdateVehicle(vehicle);

            var updatedVehicle = vehicleService.GetVehicleById(vehicleId);
            ClassicAssert.AreNotEqual(originalModel, updatedVehicle.MODEL);
        }

        [Test]
        public void TestGettingListOfAvailableVehicles()
        {
            VehicleService vehicleService = new VehicleService();
            var availableVehicles = vehicleService.GetAvailableVehicles();

            ClassicAssert.IsNotNull(availableVehicles);
            ClassicAssert.Greater(availableVehicles.Count, 0);
        }

        [Test]
        public void TestGettingListOfAllVehicles()
        {
            VehicleService vehicleService = new VehicleService();
            var allVehicles = vehicleService.GetAvailableVehicles();

            ClassicAssert.IsNotNull(allVehicles);
            ClassicAssert.Greater(allVehicles.Count, 0);
        }
    }
}
