using CarConnect.Exceptions;
using CarConnect.Model;
using CarConnect.Service;
using System;

namespace CarConnect.CarConnectApp
{
    internal class CarConnectApplication
    {
        private CustomerService customerService = new CustomerService();
        private AdminService AdminService = new AdminService();
        private ReservationService reservationService = new ReservationService();
        private VehicleService vehicleService = new VehicleService();
        private Admin admin=new Admin();

        public static void MainMenu()
        {
            AdminService adminService = new AdminService();

            while (true)
            {
                Console.WriteLine("\n\n1.Admin");
                Console.WriteLine("2.Customer");
                Console.WriteLine("3.Exit\n");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("1.Admin Service");
                        Console.WriteLine("2.Reservation Service");
                        Console.WriteLine("0.Main Menu\n");
                        string option = Console.ReadLine();
                        switch (option)
                        {
                            case "1":
                                Console.WriteLine("Login to use Admin Services:");
                                Console.Write("Enter Admin ID: ");
                                int adminId = int.Parse(Console.ReadLine());
                                Console.Write("Enter Password: ");
                                string password = Console.ReadLine();
                                try
                                {
                                    adminService.ValidateAdmin(adminId, password);
                                    AdminServiceMenu();
                                    break;
                                }
                                catch (AdminNotFoundException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                break; 
                            case "2":
                                ReservationServiceMenu();
                                break;
                            case "0":
                                return;
                            default:
                                Console.WriteLine("Invalid choice. Press any key to continue...");
                                Console.ReadKey();
                                break;
                        }

                        break;

                    case "2":
                        Console.WriteLine("1.Customer Service");
                        Console.WriteLine("2.Vehicle Service");
                        Console.WriteLine("0.Main Menu\n");
                        string choose = Console.ReadLine();
                        switch (choose)
                        {
                            case "1":
                                CustomerServiceMenu();
                                break;
                            case "2":
                                VehicleServiceMenu();
                                break;
                            case "0":
                                return;
                            default:
                                Console.WriteLine("Invalid choice. Press any key to continue...");
                                Console.ReadKey();
                                break;
                        }
                        break;


                    case "3":
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue...");
                        Console.ReadKey();
                        break;


                }
            }
            
            static void CustomerServiceMenu()
            {
                CarConnectApplication carConnectApp = new CarConnectApplication();
                while (true)
                {
                    
                    Console.WriteLine("\n\nCustomer Service Menu");
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

            static void VehicleServiceMenu()
            {
                VehicleService vehicleService = new VehicleService();
                CarConnectApplication carConnectApp = new CarConnectApplication();
                while (true)
                {
                    
                    Console.WriteLine("\n\nVehicle Service Menu");
                    Console.WriteLine("1. Get Vehicle by ID");
                    Console.WriteLine("2. Get Available Vehicles");
                    Console.WriteLine("3. Add Vehicle");
                    Console.WriteLine("4. Update Vehicle");
                    Console.WriteLine("5. Remove Vehicle");
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

                        case "4":
                            try
                            {
                                Console.Write("Enter Vehicle ID to update: ");
                                int updateVehicleId = int.Parse(Console.ReadLine());

                                Vehicle existingVehicle = carConnectApp.vehicleService.GetVehicleById(updateVehicleId);
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

                                Vehicle updatedVehicle = new Vehicle
                                {
                                    VEHICLE_ID = updateVehicleId,
                                    MODEL = string.IsNullOrEmpty(newModel) ? existingVehicle.MODEL : newModel,
                                    MAKER = string.IsNullOrEmpty(newMaker) ? existingVehicle.MAKER : newMaker,
                                    YEAR_COL = string.IsNullOrEmpty(newYearCol) ? existingVehicle.YEAR_COL : DateTime.Parse(newYearCol),
                                    COLOR = string.IsNullOrEmpty(newColor) ? existingVehicle.COLOR : newColor,
                                    REGISTRATION_NUM = string.IsNullOrEmpty(newRegNum) ? existingVehicle.REGISTRATION_NUM : newRegNum,
                                    AVAILABILITY = string.IsNullOrEmpty(newAvailability) ? existingVehicle.AVAILABILITY : bool.Parse(newAvailability),
                                    DIALY_RATE = string.IsNullOrEmpty(newDailyRate) ? existingVehicle.DIALY_RATE : decimal.Parse(newDailyRate)
                                };

                                carConnectApp.vehicleService.UpdateVehicle(updatedVehicle);

                                Console.WriteLine($"Vehicle details updated successfully.");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input format. Please enter valid data.");
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
                                Console.Write("Enter Vehicle ID to remove: ");
                                int removeVehicleId = int.Parse(Console.ReadLine());
                                vehicleService.RemoveVehicle(removeVehicleId);
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
                            return;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            Console.WriteLine("Press any key to continue...");
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
                                Reservation reservationById = carConnectApp.reservationService.GetReservationById(reservationId);
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
                                List<Reservation> reservationsByCustomerId = carConnectApp.reservationService.GetReservationsByCustomerId(customerId);
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
                            Reservation reservations = new Reservation();

                            try
                            {
                                Console.Write("Enter Customer ID: ");
                                int customerId = int.Parse(Console.ReadLine());
                                Console.Write("Enter Vehicle ID:");
                                Console.WriteLine("\n\nAvailable Vehicle IDs:");
                                var availableVehicles = carConnectApp.vehicleService.GetAvailableVehicles();
                                foreach (var vehicle in availableVehicles)
                                {
                                    Console.WriteLine(vehicle.VEHICLE_ID);
                                }
                                int vehicleId = int.Parse(Console.ReadLine());
                                Console.Write("Enter Start Date (yyyy-MM-dd HH:mm:ss): ");
                                DateTime startDate = DateTime.Parse(Console.ReadLine());
                                Console.Write("Enter End Date (yyyy-MM-dd HH:mm:ss): ");
                                DateTime endDate = DateTime.Parse(Console.ReadLine());
                                Console.WriteLine("Enter Daily Rate:");
                                int dailyRate = int.Parse(Console.ReadLine());
                                int totalDays = (int)(endDate - startDate).TotalDays;
                                decimal totalCost = reservations.CalculateTotalCost(dailyRate, totalDays);

                                bool isReserved = carConnectApp.reservationService.IsVehicleReserved(vehicleId, startDate, endDate);
                                string status = isReserved ? "reserved" : "confirmed";

                                if (isReserved)
                                {
                                    throw new ReservationException("The vehicle is already reserved for the selected dates.");
                                }

                                Reservation newReservation = new Reservation
                                {
                                    CUSTOMER_ID = customerId,
                                    VEHICLE_ID = vehicleId,
                                    START_DATE = startDate,
                                    END_DATE = endDate,
                                    STATUS = status,
                                };
                                newReservation.TOTAL_COST = totalCost;
                                carConnectApp.reservationService.CreateReservation(newReservation);
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

                                Reservation existingReservation = carConnectApp.reservationService.GetReservationById(reservationId);
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

                                carConnectApp.reservationService.UpdateReservation(updatedReservation);
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
                                carConnectApp.reservationService.CancelReservation(cancelReservationId);
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
                    Console.Clear();
                    Console.WriteLine("Admin Service Menu");
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
                                Admin adminById = carConnectApp.AdminService.GetAdminById(adminId);
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
                            Admin adminByUsername = carConnectApp.AdminService.GetAdminByUsername(username);
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

                            carConnectApp.AdminService.RegisterAdmin(newAdmin);
                            Console.WriteLine("Admin registered successfully.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;

                        case "4":
                            try
                            {
                                Console.Write("Enter Admin ID to update: ");
                                int updateAdminId = int.Parse(Console.ReadLine());

                                Admin existingAdmin = carConnectApp.AdminService.GetAdminById(updateAdminId);
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

                                carConnectApp.AdminService.UpdateAdmin(updatedAdmin);
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
                                carConnectApp.AdminService.DeleteAdmin(deleteAdminId);
                                Console.WriteLine($"Admin with Admin Id {deleteAdminId} is successfully deleted...");

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
}
