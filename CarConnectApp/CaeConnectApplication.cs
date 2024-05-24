using CarConnect.Exceptions;
using System;
using System.Collections.Generic;
using CarConnect.Model;
using CarConnect.Service;
using CarConnect.Util;

namespace CarConnect.CarConnectApp
{
    internal class CarConnectApplication
    {
       

        private CustomerService customerService = new CustomerService();
        private AdminService adminService = new AdminService();
        private static ReservationService reservationService = new ReservationService();
        private static VehicleService vehicleService = new VehicleService();
        private Admin admin = new Admin();
        private static ReportGenerator reportGenerator = new ReportGenerator(reservationService, vehicleService);

        public static void MainMenu()
        {
            AdminService adminService = new AdminService();
            AuthenticationService authService = new AuthenticationService(new CustomerService(), new AdminService());
            CustomerService customerService = new CustomerService(); // Instantiate CustomerService here

            bool exitProgram = false;

            while (!exitProgram)
            {
                Console.WriteLine("\n\n1.Admin");
                Console.WriteLine("2.Customer");
                Console.WriteLine("3.Report Generator");
                Console.WriteLine("4.Exit\n");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\n\nLogin to use Admin Services:");
                        Console.Write("Enter Admin Username: ");
                        string adminUsername = Console.ReadLine();
                        Console.Write("Enter Password: ");
                        string adminPassword = Console.ReadLine();

                        try
                        {
                            if (authService.AuthenticateAdmin(adminUsername, adminPassword))
                            {
                                bool backToMainMenu = false;
                                while (!backToMainMenu)
                                {
                                    Console.WriteLine("\n\nChoose the service required:");
                                    Console.WriteLine("1.Admin Service");
                                    Console.WriteLine("2.Reservation Service");
                                    Console.WriteLine("3.Vehicle Service");
                                    Console.WriteLine("4.Customer Service");
                                    Console.WriteLine("0.Main Menu\n");
                                    string option = Console.ReadLine();
                                    switch (option)
                                    {
                                        case "1":
                                            AdminServiceMenu();
                                            break;
                                        case "2":
                                            ReservationServiceMenu();
                                            break;
                                        case "3":
                                            VehicleServiceMenu(true); // Admin has full access
                                            break;
                                        case "4":
                                            AdminCustomerServiceMenu();
                                            break;
                                        case "0":
                                            backToMainMenu = true;
                                            break;
                                        default:
                                            Console.WriteLine("Invalid choice. Press any key to continue...");
                                            Console.ReadKey();
                                            break;
                                    }
                                }
                            }
                        }
                        catch (AuthenticationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "2":
                        Console.WriteLine("\n\nLogin to use Customer Services:");
                        Console.Write("Enter Customer Username: ");
                        string customerUsername = Console.ReadLine();
                        Console.Write("Enter Password: ");
                        string customerPassword = Console.ReadLine();

                        try
                        {
                            if (authService.AuthenticateCustomer(customerUsername, customerPassword))
                            {
                                bool backToMainMenu = false;
                                while (!backToMainMenu)
                                {
                                    Console.WriteLine("\nChoose the service required:");
                                    Console.WriteLine("1.Customer Service");
                                    Console.WriteLine("2.Vehicle Service");
                                    Console.WriteLine("3.Register Customer");
                                    Console.WriteLine("0.Main Menu\n");
                                    string option = Console.ReadLine();
                                    switch (option)
                                    {
                                        case "1":
                                            CustomerServiceMenu(customerUsername);
                                            break;
                                        case "2":
                                            VehicleServiceMenu(false); // Customer has limited access
                                            break;
                                        case "3":
                                            try
                                            {
                                                Console.Write("Enter First Name: ");
                                                string firstName = Console.ReadLine();
                                                Console.Write("Enter Last Name: ");
                                                string lastName = Console.ReadLine();
                                                Console.Write("Enter Email: ");
                                                string email = Console.ReadLine();
                                                Console.Write("Enter Phone Number: ");
                                                string phoneNumber = Console.ReadLine();
                                                Console.Write("Enter Address: ");
                                                string address = Console.ReadLine();
                                                Console.Write("Enter Username: ");
                                                string username = Console.ReadLine();
                                                Console.Write("Enter Password: ");
                                                string password = Console.ReadLine();

                                                Customer newCustomer = new Customer
                                                {
                                                    FIRST_NAME = firstName,
                                                    LAST_NAME = lastName,
                                                    EMAIL = email,
                                                    PHONE_NUMBER = phoneNumber,
                                                    ADDRESS = address,
                                                    USERNAME = username,
                                                    PASSWORD = password,
                                                    registration_date = DateTime.Now
                                                };

                                                customerService.RegisterCustomer(newCustomer);
                                                Console.WriteLine("Customer added successfully.");
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Invalid input format.");
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine($"An error occurred: {ex.Message}");
                                            }
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                            break;
                                            
                                        case "0":
                                            backToMainMenu = true;
                                            break;
                                        default:
                                            Console.WriteLine("Invalid choice. Press any key to continue...");
                                            Console.ReadKey();
                                            break;
                                    }
                                }
                            }
                        }
                        catch (AuthenticationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "3":
                        ReportGeneratorMenu();
                        break;

                    case "0":
                        exitProgram = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }



        static void AdminCustomerServiceMenu()
        {
            CarConnectApplication carConnectApp = new CarConnectApplication();
            while (true)
            {
                Console.WriteLine("\n\nAdmin Customer Service Menu");
                Console.WriteLine("1. Get Customer by ID");
                Console.WriteLine("2. Get Customer by Username");
                Console.WriteLine("3. Register Customer");
                Console.WriteLine("4. Update Customer");
                Console.WriteLine("5. Delete Customer");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        try
                        {
                            Console.Write("Enter Customer ID: ");
                            int customerId = int.Parse(Console.ReadLine());
                            Customer customerById = carConnectApp.customerService.GetCustomerById(customerId);
                            if (customerById != null)
                            {
                                Console.WriteLine($"Customer Details:");
                                Console.WriteLine($"Customer ID: {customerById.CUSTOMER_ID}");
                                Console.WriteLine($"First Name: {customerById.FIRST_NAME}");
                                Console.WriteLine($"Last Name: {customerById.LAST_NAME}");
                                Console.WriteLine($"Email: {customerById.EMAIL}");
                                Console.WriteLine($"Phone Number: {customerById.PHONE_NUMBER}");
                                Console.WriteLine($"Address: {customerById.ADDRESS}");
                                Console.WriteLine($"Username: {customerById.USERNAME}");
                                Console.WriteLine($"Registration Date: {customerById.registration_date}");
                            }
                            else
                            {
                                Console.WriteLine("Customer not found.");
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input. Please enter a valid Customer ID.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Write("Enter Username: ");
                        string username = Console.ReadLine();
                        Customer customerByUsername = carConnectApp.customerService.GetCustomerByUsername(username);
                        if (customerByUsername != null)
                        {
                            Console.WriteLine($"Customer Details:");
                            Console.WriteLine($"Customer ID: {customerByUsername.CUSTOMER_ID}");
                            Console.WriteLine($"First Name: {customerByUsername.FIRST_NAME}");
                            Console.WriteLine($"Last Name: {customerByUsername.LAST_NAME}");
                            Console.WriteLine($"Email: {customerByUsername.EMAIL}");
                            Console.WriteLine($"Phone Number: {customerByUsername.PHONE_NUMBER}");
                            Console.WriteLine($"Address: {customerByUsername.ADDRESS}");
                            Console.WriteLine($"Username: {customerByUsername.USERNAME}");
                            Console.WriteLine($"Registration Date: {customerByUsername.registration_date}");
                        }
                        else
                        {
                            Console.WriteLine("Customer not found.");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.Write("Enter First Name: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Enter Last Name: ");
                        string lastName = Console.ReadLine();
                        Console.Write("Enter Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Enter Phone Number: ");
                        string phoneNumber = Console.ReadLine();
                        Console.Write("Enter Address: ");
                        string address = Console.ReadLine();
                        Console.Write("Enter Username: ");
                        string uname = Console.ReadLine();
                        Console.Write("Enter Password: ");
                        string password = Console.ReadLine();

                        Customer newCustomer = new Customer
                        {
                            FIRST_NAME = firstName,
                            LAST_NAME = lastName,
                            EMAIL = email,
                            PHONE_NUMBER = phoneNumber,
                            ADDRESS = address,
                            USERNAME = uname,
                            PASSWORD = password,
                            registration_date = DateTime.Now
                        };

                        carConnectApp.customerService.RegisterCustomer(newCustomer);
                        Console.WriteLine("Customer registered successfully.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "4":
                        try
                        {
                            Console.Write("Enter Customer ID to update: ");
                            int updateCustomerId = int.Parse(Console.ReadLine());

                            Customer existingCustomer = carConnectApp.customerService.GetCustomerById(updateCustomerId);
                            if (existingCustomer == null)
                            {
                                Console.WriteLine("Customer not found.");
                                break;
                            }

                            Console.WriteLine($"Updating details for Customer: {existingCustomer.FIRST_NAME} {existingCustomer.LAST_NAME}");

                            Console.Write($"Enter First Name ({existingCustomer.FIRST_NAME}): ");
                            string newFirstName = Console.ReadLine();
                            Console.Write($"Enter Last Name ({existingCustomer.LAST_NAME}): ");
                            string newLastName = Console.ReadLine();
                            Console.Write($"Enter Email ({existingCustomer.EMAIL}): ");
                            string newEmail = Console.ReadLine();
                            Console.Write($"Enter Phone Number ({existingCustomer.PHONE_NUMBER}): ");
                            string newPhoneNumber = Console.ReadLine();
                            Console.Write($"Enter Address ({existingCustomer.ADDRESS}): ");
                            string newAddress = Console.ReadLine();
                            Console.Write($"Enter Username ({existingCustomer.USERNAME}): ");
                            string newUsername = Console.ReadLine();
                            Console.Write("Enter New Password: ");
                            string newPassword = Console.ReadLine();

                            Customer updatedCustomer = new Customer
                            {
                                CUSTOMER_ID = updateCustomerId,
                                FIRST_NAME = string.IsNullOrEmpty(newFirstName) ? existingCustomer.FIRST_NAME : newFirstName,
                                LAST_NAME = string.IsNullOrEmpty(newLastName) ? existingCustomer.LAST_NAME : newLastName,
                                EMAIL = string.IsNullOrEmpty(newEmail) ? existingCustomer.EMAIL : newEmail,
                                PHONE_NUMBER = string.IsNullOrEmpty(newPhoneNumber) ? existingCustomer.PHONE_NUMBER : newPhoneNumber,
                                ADDRESS = string.IsNullOrEmpty(newAddress) ? existingCustomer.ADDRESS : newAddress,
                                USERNAME = string.IsNullOrEmpty(newUsername) ? existingCustomer.USERNAME : newUsername,
                                PASSWORD = string.IsNullOrEmpty(newPassword) ? existingCustomer.PASSWORD : newPassword,
                                registration_date = existingCustomer.registration_date
                            };

                            carConnectApp.customerService.UpdateCustomer(updatedCustomer);
                            Console.WriteLine("Customer details updated successfully.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input. Please enter a valid Customer ID.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "5":
                        try
                        {
                            Console.Write("Enter Customer ID to delete: ");
                            int deleteCustomerId = int.Parse(Console.ReadLine());
                            carConnectApp.customerService.DeleteCustomer(deleteCustomerId);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input. Please enter a valid Customer ID.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }




        static void ReportGeneratorMenu()
            {
                while (true)
                {
                    Console.WriteLine("\n\nReport Generator:");
                    Console.WriteLine("1. Generate Reservation History Report");
                    Console.WriteLine("2. Generate Vehicle Utilization Report");
                    Console.WriteLine("3. Generate Revenue Report");
                    Console.WriteLine("0. Main Menu\n");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter start date (yyyy-mm-dd): ");
                            DateTime startDate1 = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter end date (yyyy-mm-dd): ");
                            DateTime endDate1 = DateTime.Parse(Console.ReadLine());
                            reportGenerator.GenerateReservationHistoryReport(startDate1, endDate1);
                            break;
                        case "2":
                            Console.Write("Enter start date (yyyy-mm-dd): ");
                            DateTime startDate2 = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter end date (yyyy-mm-dd): ");
                            DateTime endDate2 = DateTime.Parse(Console.ReadLine());
                            reportGenerator.GenerateVehicleUtilizationReport(startDate2, endDate2);
                            break;
                        case "3":
                            Console.Write("Enter start date (yyyy-mm-dd): ");
                            DateTime startDate3 = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter end date (yyyy-mm-dd): ");
                            DateTime endDate3 = DateTime.Parse(Console.ReadLine());
                            reportGenerator.GenerateRevenueReport(startDate3, endDate3);
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
            }

            static void VehicleServiceMenu(bool isAdmin)
            {
                VehicleService vehicleService = new VehicleService();
                CarConnectApplication carConnectApp = new CarConnectApplication();

                while (true)
                {
                    Console.WriteLine("\n\nVehicle Service Menu");
                    Console.WriteLine("1. Get Vehicle by ID");
                    Console.WriteLine("2. Get Available Vehicles");

                    // Admin-only options
                    if (isAdmin)
                    {
                        Console.WriteLine("3. Add Vehicle");
                        Console.WriteLine("4. Update Vehicle");
                        Console.WriteLine("5. Remove Vehicle");
                    }

                    Console.WriteLine("0. Back to Main Menu");
                    Console.Write("Select an option: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            try
                            {
                                Console.Write("Enter Vehicle ID: ");
                                int vehicleId = int.Parse(Console.ReadLine());
                                var vehicle = vehicleService.GetVehicleById(vehicleId);
                                if (vehicle != null)
                                {
                                    Console.WriteLine($"Vehicle Details:");
                                    Console.WriteLine($"Vehicle ID: {vehicle.VEHICLE_ID}");
                                    Console.WriteLine($"Model: {vehicle.MODEL}");
                                    Console.WriteLine($"Maker: {vehicle.MAKER}");
                                    Console.WriteLine($"Year: {vehicle.YEAR_COL}");
                                    Console.WriteLine($"Color: {vehicle.COLOR}");
                                    Console.WriteLine($"Registration Number: {vehicle.REGISTRATION_NUM}");
                                    Console.WriteLine($"Availability: {vehicle.AVAILABILITY}");
                                    Console.WriteLine($"Daily Rate: {vehicle.DIALY_RATE}");
                                }
                                else
                                {
                                    Console.WriteLine("Vehicle not found.");
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input. Please enter a valid Vehicle ID.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"An error occurred: {ex.Message}");
                            }
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;

                        case "2":
                            try
                            {
                                var vehicles = vehicleService.GetAvailableVehicles();
                                if (vehicles.Any())
                                {
                                    Console.WriteLine("Available Vehicles:");
                                    foreach (var vehicle in vehicles)
                                    {
                                        Console.WriteLine($"Vehicle ID: {vehicle.VEHICLE_ID}, Model: {vehicle.MODEL}, Maker: {vehicle.MAKER}, Year: {vehicle.YEAR_COL}, Color: {vehicle.COLOR}, Registration Number: {vehicle.REGISTRATION_NUM}, Daily Rate: {vehicle.DIALY_RATE}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No vehicles available.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"An error occurred: {ex.Message}");
                            }
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;

                        case "3":
                            if (isAdmin)
                            {
                                try
                                {
                                    Console.Write("Enter Model: ");
                                    string model = Console.ReadLine();
                                    Console.Write("Enter Maker: ");
                                    string maker = Console.ReadLine();
                                    Console.Write("Enter Year of Collection (YYYY-MM-DD): ");
                                    DateTime yearCol = DateTime.Parse(Console.ReadLine());
                                    Console.Write("Enter Color: ");
                                    string color = Console.ReadLine();
                                    Console.Write("Enter Registration Number: ");
                                    string regNum = Console.ReadLine();
                                    Console.Write("Enter Availability (true/false): ");
                                    bool availability = bool.Parse(Console.ReadLine());
                                    Console.Write("Enter Daily Rate: ");
                                    decimal dailyRate = decimal.Parse(Console.ReadLine());

                                    Vehicle newVehicle = new Vehicle
                                    {
                                        MODEL = model,
                                        MAKER = maker,
                                        YEAR_COL = yearCol,
                                        COLOR = color,
                                        REGISTRATION_NUM = regNum,
                                        AVAILABILITY = availability,
                                        DIALY_RATE = dailyRate
                                    };

                                    vehicleService.AddVehicle(newVehicle);
                                    Console.WriteLine("Vehicle added successfully.");
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Invalid input format.");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"An error occurred: {ex.Message}");
                                }
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("You do not have permission to perform this action.");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            break;

                        case "4":
                            if (isAdmin)
                            {
                                try
                                {
                                    Console.Write("Enter Vehicle ID to update: ");
                                    int updateVehicleId = int.Parse(Console.ReadLine());

                                    Vehicle existingVehicle = vehicleService.GetVehicleById(updateVehicleId);
                                    if (existingVehicle == null)
                                    {
                                        Console.WriteLine("Vehicle not found.");
                                        break;
                                    }

                                    Console.WriteLine($"Updating details for Vehicle: {existingVehicle.MODEL} {existingVehicle.MAKER}");

                                    Console.Write($"Enter Model ({existingVehicle.MODEL}): ");
                                    string newModel = Console.ReadLine();
                                    Console.Write($"Enter Maker ({existingVehicle.MAKER}): ");
                                    string newMaker = Console.ReadLine();
                                    Console.Write($"Enter Year of Collection ({existingVehicle.YEAR_COL}): ");
                                    string newYearCol = Console.ReadLine();
                                    Console.Write($"Enter Color ({existingVehicle.COLOR}): ");
                                    string newColor = Console.ReadLine();
                                    Console.Write($"Enter Registration Number ({existingVehicle.REGISTRATION_NUM}): ");
                                    string newRegNum = Console.ReadLine();
                                    Console.Write($"Enter Availability ({existingVehicle.AVAILABILITY}): ");
                                    string newAvailability = Console.ReadLine();
                                    Console.Write($"Enter Daily Rate ({existingVehicle.DIALY_RATE}): ");
                                    string newDailyRate = Console.ReadLine();

                                    if (!string.IsNullOrEmpty(newModel)) existingVehicle.MODEL = newModel;
                                    if (!string.IsNullOrEmpty(newMaker)) existingVehicle.MAKER = newMaker;
                                    if (!string.IsNullOrEmpty(newYearCol)) existingVehicle.YEAR_COL = DateTime.Parse(newYearCol);
                                    if (!string.IsNullOrEmpty(newColor)) existingVehicle.COLOR = newColor;
                                    if (!string.IsNullOrEmpty(newRegNum)) existingVehicle.REGISTRATION_NUM = newRegNum;
                                    if (!string.IsNullOrEmpty(newAvailability)) existingVehicle.AVAILABILITY = bool.Parse(newAvailability);
                                    if (!string.IsNullOrEmpty(newDailyRate)) existingVehicle.DIALY_RATE = decimal.Parse(newDailyRate);

                                    vehicleService.UpdateVehicle(existingVehicle);
                                    Console.WriteLine("Vehicle details updated successfully.");
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Invalid input format.");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"An error occurred: {ex.Message}");
                                }
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("You do not have permission to perform this action.");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            break;

                        case "5":
                            if (isAdmin)
                            {
                                try
                                {
                                    Console.Write("Enter Vehicle ID to remove: ");
                                    int removeVehicleId = int.Parse(Console.ReadLine());
                                    vehicleService.RemoveVehicle(removeVehicleId);
                                    Console.WriteLine("Vehicle removed successfully.");
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Invalid input format.");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"An error occurred: {ex.Message}");
                                }
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("You do not have permission to perform this action.");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            break;

                        case "0":
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
            }






            static void CustomerServiceMenu(string authenticatedUsername)
            {
            CarConnectApplication carConnectApp = new CarConnectApplication();
            Customer authenticatedCustomer = carConnectApp.customerService.GetCustomerByUsername(authenticatedUsername);

            if (authenticatedCustomer == null)
            {
                Console.WriteLine("Authenticated customer not found.");
                return;
            }

            bool exitCustomerMenu = false;
            while (!exitCustomerMenu)
            {
                Console.WriteLine("\n\nCustomer Service Menu");
                Console.WriteLine("1. View My Details");
                Console.WriteLine("2. Update My Details");
                Console.WriteLine("3. Delete My Account");
                Console.WriteLine("4. Logout");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine($"Customer Details:");
                        Console.WriteLine($"Customer ID: {authenticatedCustomer.CUSTOMER_ID}");
                        Console.WriteLine($"First Name: {authenticatedCustomer.FIRST_NAME}");
                        Console.WriteLine($"Last Name: {authenticatedCustomer.LAST_NAME}");
                        Console.WriteLine($"Email: {authenticatedCustomer.EMAIL}");
                        Console.WriteLine($"Phone Number: {authenticatedCustomer.PHONE_NUMBER}");
                        Console.WriteLine($"Address: {authenticatedCustomer.ADDRESS}");
                        Console.WriteLine($"Username: {authenticatedCustomer.USERNAME}");
                        Console.WriteLine($"Registration Date: {authenticatedCustomer.registration_date}");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.WriteLine($"Updating details for Customer: {authenticatedCustomer.FIRST_NAME} {authenticatedCustomer.LAST_NAME}");

                        Console.Write($"Enter First Name ({authenticatedCustomer.FIRST_NAME}): ");
                        string newFirstName = Console.ReadLine();
                        Console.Write($"Enter Last Name ({authenticatedCustomer.LAST_NAME}): ");
                        string newLastName = Console.ReadLine();
                        Console.Write($"Enter Email ({authenticatedCustomer.EMAIL}): ");
                        string newEmail = Console.ReadLine();
                        Console.Write($"Enter Phone Number ({authenticatedCustomer.PHONE_NUMBER}): ");
                        string newPhoneNumber = Console.ReadLine();
                        Console.Write($"Enter Address ({authenticatedCustomer.ADDRESS}): ");
                        string newAddress = Console.ReadLine();
                        Console.Write($"Enter Username ({authenticatedCustomer.USERNAME}): ");
                        string newUsername = Console.ReadLine();
                        Console.Write("Enter New Password: ");
                        string newPassword = Console.ReadLine();

                        Customer updatedCustomer = new Customer
                        {
                            CUSTOMER_ID = authenticatedCustomer.CUSTOMER_ID,
                            FIRST_NAME = string.IsNullOrEmpty(newFirstName) ? authenticatedCustomer.FIRST_NAME : newFirstName,
                            LAST_NAME = string.IsNullOrEmpty(newLastName) ? authenticatedCustomer.LAST_NAME : newLastName,
                            EMAIL = string.IsNullOrEmpty(newEmail) ? authenticatedCustomer.EMAIL : newEmail,
                            PHONE_NUMBER = string.IsNullOrEmpty(newPhoneNumber) ? authenticatedCustomer.PHONE_NUMBER : newPhoneNumber,
                            ADDRESS = string.IsNullOrEmpty(newAddress) ? authenticatedCustomer.ADDRESS : newAddress,
                            USERNAME = string.IsNullOrEmpty(newUsername) ? authenticatedCustomer.USERNAME : newUsername,
                            PASSWORD = string.IsNullOrEmpty(newPassword) ? authenticatedCustomer.PASSWORD : newPassword,
                            registration_date = authenticatedCustomer.registration_date
                        };

                        carConnectApp.customerService.UpdateCustomer(updatedCustomer);
                        Console.WriteLine("Customer details updated successfully.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "3":
                        try
                        {
                            Console.Write("Are you sure you want to delete your account? (yes/no): ");
                            string confirmation = Console.ReadLine();
                            if (confirmation.ToLower() == "yes")
                            {
                                carConnectApp.customerService.DeleteCustomer(authenticatedCustomer.CUSTOMER_ID);
                                Console.WriteLine("Your account has been deleted successfully.");
                                exitCustomerMenu = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "4":

                    case "0":
                        exitCustomerMenu = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }





        static void ReservationServiceMenu()
            {
                CarConnectApplication carConnectApp = new CarConnectApplication();

                while (true)
                {
                    
                    Console.WriteLine("\n\nReservation Service Menu");
                    Console.WriteLine("1. Get Reservation by ID");
                    Console.WriteLine("2. Get Reservations by Customer ID");
                    Console.WriteLine("3. Create Reservation");
                    Console.WriteLine("4. Update Reservation");
                    Console.WriteLine("5. Cancel Reservation");
                    Console.WriteLine("0. Back to Main Menu");
                    Console.Write("Select an option: ");
                    string choice = Console.ReadLine();

                   
                switch (choice)
                    {
                    case "1":
                        try
                        {
                            Console.Write("Enter Reservation ID: ");
                            int reservationId = int.Parse(Console.ReadLine());
                            Reservation reservationById = reservationService.GetReservationById(reservationId);
                            if (reservationById != null)
                            {
                                Console.WriteLine("Reservation Details:");
                                Console.WriteLine($"Reservation ID: {reservationById.RESERVATION_ID}");
                                Console.WriteLine($"Customer ID: {reservationById.CUSTOMER_ID}");
                                Console.WriteLine($"Vehicle ID: {reservationById.VEHICLE_ID}");
                                Console.WriteLine($"Start Date: {reservationById.START_DATE}");
                                Console.WriteLine($"End Date: {reservationById.END_DATE}");
                                Console.WriteLine($"Total Cost: {reservationById.TOTAL_COST}");
                                Console.WriteLine($"Status: {reservationById.STATUS}");
                            }
                            else
                            {
                                Console.WriteLine("Reservation not found.");
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input. Please enter a valid Reservation ID.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "2":
                            try
                            {
                                Console.Write("Enter Customer ID: ");
                                int customerId = int.Parse(Console.ReadLine());
                                List<Reservation> reservationsByCustomerId = reservationService.GetReservationsByCustomerId(customerId);
                                if (reservationsByCustomerId.Count > 0)
                                {
                                    Console.WriteLine("Reservations:");
                                    foreach (var reservation in reservationsByCustomerId)
                                    {
                                        Console.WriteLine($"Reservation ID: {reservation.RESERVATION_ID}, Vehicle ID: {reservation.VEHICLE_ID}, Start Date: {reservation.START_DATE}, End Date: {reservation.END_DATE}, Total Cost: {reservation.TOTAL_COST}, Status: {reservation.STATUS}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No reservations found for this customer.");
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input. Please enter a valid Customer ID.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"An error occurred: {ex.Message}");
                            }
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;

                    case "3":
                        try
                        {
                            Console.Write("Enter Customer ID: ");
                            int customerId = int.Parse(Console.ReadLine());

                            Console.WriteLine("\n\nAvailable Vehicle IDs:");
                            var availableVehicles = vehicleService.GetAvailableVehicles();
                            foreach (var vehicle in availableVehicles)
                            {
                                Console.WriteLine(vehicle.VEHICLE_ID);
                            }

                            Console.Write("Enter Vehicle ID: ");
                            int vehicleId = int.Parse(Console.ReadLine());

                            Console.Write("Enter Start Date (yyyy-MM-dd HH:mm:ss): ");
                            DateTime startDate = DateTime.Parse(Console.ReadLine());

                            Console.Write("Enter End Date (yyyy-MM-dd HH:mm:ss): ");
                            DateTime endDate = DateTime.Parse(Console.ReadLine());

                            Console.Write("Enter Daily Rate: ");
                            decimal dailyRate = decimal.Parse(Console.ReadLine());

                            int totalDays = (int)(endDate - startDate).TotalDays;
                            if (totalDays <= 0)
                            {
                                throw new Exception("End date must be later than start date.");
                            }

                            decimal totalCost = dailyRate * totalDays;

                            Reservation newReservation = new Reservation
                            {
                                CUSTOMER_ID = customerId,
                                VEHICLE_ID = vehicleId,
                                START_DATE = startDate,
                                END_DATE = endDate,
                                TOTAL_COST = totalCost,
                                STATUS = "confirmed"
                            };

                            reservationService.CreateReservation(newReservation);
                            Console.WriteLine($"Reservation created successfully. Total cost: {totalCost}");
                        }
                        catch (ReservationException ex)
                        {
                            Console.WriteLine($"Reservation error: {ex.Message}");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input. Please enter valid data.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;



                    case "4":
                            try
                            {
                                Console.Write("Enter Reservation ID to update: ");
                                int reservationId = int.Parse(Console.ReadLine());

                                Reservation existingReservation = reservationService.GetReservationById(reservationId);
                                if (existingReservation == null)
                                {
                                    Console.WriteLine("Reservation not found.");
                                    break;
                                }

                                Console.WriteLine($"Updating details for Reservation ID: {existingReservation.RESERVATION_ID}");

                                Console.Write($"Enter Customer ID ({existingReservation.CUSTOMER_ID}): ");
                                string newCustomerId = Console.ReadLine();
                                Console.Write($"Enter Vehicle ID ({existingReservation.VEHICLE_ID}): ");
                                string newVehicleId = Console.ReadLine();
                                Console.Write($"Enter Start Date ({existingReservation.START_DATE:yyyy-MM-dd HH:mm:ss}): ");
                                string newStartDate = Console.ReadLine();
                                Console.Write($"Enter End Date ({existingReservation.END_DATE:yyyy-MM-dd HH:mm:ss}): ");
                                string newEndDate = Console.ReadLine();
                                Console.Write($"Enter Total Cost ({existingReservation.TOTAL_COST}): ");
                                string newTotalCost = Console.ReadLine();
                                Console.Write($"Enter Status ({existingReservation.STATUS}): ");
                                string newStatus = Console.ReadLine();

                                Reservation updatedReservation = new Reservation
                                {
                                    RESERVATION_ID = reservationId,
                                    CUSTOMER_ID = string.IsNullOrEmpty(newCustomerId) ? existingReservation.CUSTOMER_ID : int.Parse(newCustomerId),
                                    VEHICLE_ID = string.IsNullOrEmpty(newVehicleId) ? existingReservation.VEHICLE_ID : int.Parse(newVehicleId),
                                    START_DATE = string.IsNullOrEmpty(newStartDate) ? existingReservation.START_DATE : DateTime.Parse(newStartDate),
                                    END_DATE = string.IsNullOrEmpty(newEndDate) ? existingReservation.END_DATE : DateTime.Parse(newEndDate),
                                    TOTAL_COST = string.IsNullOrEmpty(newTotalCost) ? existingReservation.TOTAL_COST : decimal.Parse(newTotalCost),
                                    STATUS = string.IsNullOrEmpty(newStatus) ? existingReservation.STATUS : newStatus
                                };

                                reservationService.UpdateReservation(updatedReservation);
                                Console.WriteLine("Reservation details updated successfully.");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input. Please enter valid data.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"An error occurred: {ex.Message}");
                            }
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;

                        case "5":
                            try
                            {
                                Console.Write("Enter Reservation ID to cancel: ");
                                int cancelReservationId = int.Parse(Console.ReadLine());
                                reservationService.CancelReservation(cancelReservationId);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input. Please enter a valid Reservation ID.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"An error occurred: {ex.Message}");
                            }
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;

                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
            }



            static void AdminServiceMenu()
            {
                CarConnectApplication carConnectApp = new CarConnectApplication();
                while (true)
                {
                    
                    Console.WriteLine("\n\nAdmin Service Menu");
                    Console.WriteLine("1. Get Admin by ID");
                    Console.WriteLine("2. Get Admin by Username");
                    Console.WriteLine("3. Register Admin");
                    Console.WriteLine("4. Update Admin");
                    Console.WriteLine("5. Delete Admin");
                    Console.WriteLine("0. Back to Main Menu");
                    Console.Write("Select an option: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            try
                            {
                                Console.Write("Enter Admin ID: ");
                                int adminId = int.Parse(Console.ReadLine());
                                Admin adminById = carConnectApp.adminService.GetAdminById(adminId);
                                if (adminById != null)
                                {
                                    Console.WriteLine($"Admin Details:");
                                    Console.WriteLine($"Admin ID: {adminById.ADMIN_ID}");
                                    Console.WriteLine($"First Name: {adminById.FIRST_NAME}");
                                    Console.WriteLine($"Last Name: {adminById.LAST_NAME}");
                                    Console.WriteLine($"Email: {adminById.EMAIL}");
                                    Console.WriteLine($"Phone Number: {adminById.PHONE_NUMBER}");
                                    Console.WriteLine($"Username: {adminById.USERNAME}");
                                    Console.WriteLine($"Join Date: {adminById.JOIN_DATE}");
                                }
                                else
                                {
                                    Console.WriteLine("Admin not found.");
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input. Please enter a valid Admin ID.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"An error occurred: {ex.Message}");
                            }
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;

                        case "2":
                            Console.Write("Enter Username: ");
                            string username = Console.ReadLine();
                            Admin adminByUsername = carConnectApp.adminService.GetAdminByUsername(username);
                            if (adminByUsername != null)
                            {
                                Console.WriteLine($"Admin Details:");
                                Console.WriteLine($"Admin ID: {adminByUsername.ADMIN_ID}");
                                Console.WriteLine($"First Name: {adminByUsername.FIRST_NAME}");
                                Console.WriteLine($"Last Name: {adminByUsername.LAST_NAME}");
                                Console.WriteLine($"Email: {adminByUsername.EMAIL}");
                                Console.WriteLine($"Phone Number: {adminByUsername.PHONE_NUMBER}");
                                Console.WriteLine($"Join Date: {adminByUsername.JOIN_DATE}");
                            }
                            else
                            {
                                Console.WriteLine("Admin not found.");
                            }
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;

                        case "3":
                            Console.Write("Enter First Name: ");
                            string firstName = Console.ReadLine();
                            Console.Write("Enter Last Name: ");
                            string lastName = Console.ReadLine();
                            Console.Write("Enter Email: ");
                            string email = Console.ReadLine();
                            Console.Write("Enter Phone Number: ");
                            string phoneNumber = Console.ReadLine();
                            Console.Write("Enter Username: ");
                            string uname = Console.ReadLine();
                            Console.Write("Enter Password: ");
                            string password = Console.ReadLine();
                            Console.Write("Enter Role: ");
                            string role = Console.ReadLine();

                            Admin newAdmin = new Admin
                            {
                                FIRST_NAME = firstName,
                                LAST_NAME = lastName,
                                EMAIL = email,
                                PHONE_NUMBER = phoneNumber,
                                USERNAME = uname,
                                PASSWORDHASH = password,
                                ROLE = role,
                                JOIN_DATE = DateTime.Now
                            };

                            carConnectApp.adminService.RegisterAdmin(newAdmin);
                            Console.WriteLine("Admin registered successfully.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;

                        case "4":
                            try
                            {
                                Console.Write("Enter Admin ID to update: ");
                                int updateAdminId = int.Parse(Console.ReadLine());

                                Admin existingAdmin = carConnectApp.adminService.GetAdminById(updateAdminId);
                                if (existingAdmin == null)
                                {
                                    Console.WriteLine("Admin not found.");
                                    break;
                                }

                                Console.WriteLine($"Updating details for Admin: {existingAdmin.FIRST_NAME} {existingAdmin.LAST_NAME}");

                                Console.Write($"Enter First Name ({existingAdmin.FIRST_NAME}): ");
                                string newFirstName = Console.ReadLine();
                                Console.Write($"Enter Last Name ({existingAdmin.LAST_NAME}): ");
                                string newLastName = Console.ReadLine();
                                Console.Write($"Enter Email ({existingAdmin.EMAIL}): ");
                                string newEmail = Console.ReadLine();
                                Console.Write($"Enter Phone Number ({existingAdmin.PHONE_NUMBER}): ");
                                string newPhoneNumber = Console.ReadLine();
                                Console.Write($"Enter Username ({existingAdmin.USERNAME}): ");
                                string newUsername = Console.ReadLine();
                                Console.Write("Enter New Password: ");
                                string newPassword = Console.ReadLine();
                                Console.Write($"Enter Role ({existingAdmin.ROLE}): ");
                                string newRole = Console.ReadLine();

                                Admin updatedAdmin = new Admin
                                {
                                    ADMIN_ID = updateAdminId,
                                    FIRST_NAME = string.IsNullOrEmpty(newFirstName) ? existingAdmin.FIRST_NAME : newFirstName,
                                    LAST_NAME = string.IsNullOrEmpty(newLastName) ? existingAdmin.LAST_NAME : newLastName,
                                    EMAIL = string.IsNullOrEmpty(newEmail) ? existingAdmin.EMAIL : newEmail,
                                    PHONE_NUMBER = string.IsNullOrEmpty(newPhoneNumber) ? existingAdmin.PHONE_NUMBER : newPhoneNumber,
                                    USERNAME = string.IsNullOrEmpty(newUsername) ? existingAdmin.USERNAME : newUsername,
                                    PASSWORDHASH = string.IsNullOrEmpty(newPassword) ? existingAdmin.PASSWORDHASH : newPassword,
                                    ROLE = string.IsNullOrEmpty(newRole) ? existingAdmin.ROLE : newRole,
                                    JOIN_DATE = existingAdmin.JOIN_DATE
                                };

                                carConnectApp.adminService.UpdateAdmin(updatedAdmin);
                                Console.WriteLine("Admin details updated successfully.");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input. Please enter a valid Admin ID.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"An error occurred: {ex.Message}");
                            }
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;

                    case "5":
                        try
                        {
                            Console.Write("Enter Admin ID to delete: ");
                            int deleteAdminId = int.Parse(Console.ReadLine());
                            bool isDeleted = carConnectApp.adminService.DeleteAdmin(deleteAdminId);

                            if (isDeleted)
                            {
                                Console.WriteLine($"Admin with Admin Id {deleteAdminId} is successfully deleted.");
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input. Please enter a valid Admin ID.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
            }

        }
    }
    


