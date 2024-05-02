
USE CARCONNECT
--1. Customer Table:
--• CustomerID (Primary Key): Unique identifier for each customer.
--• FirstName: First name of the customer.
--• LastName: Last name of the customer.
--• Email: Email address of the customer for communication.
--• PhoneNumber: Contact number of the customer.
--• Address: Customer's residential address.
--• Username: Unique username for customer login.
--• Password: Securely hashed password for customer authentication.
--• RegistrationDate: Date when the customer registered.

CREATE TABLE CUSTOMER(
CUSTOMER_ID INT IDENTITY(1,1) PRIMARY KEY,
FIRST_NAME VARCHAR(50) NOT NULL,
LAST_NAME VARCHAR(50) NOT NULL,
EMAIL VARCHAR(50) NOT NULL,
PHONE_NUMBER NUMERIC NOT NULL constraint CK_PhoneNumber 
check (PHONE_NUMBER like '[1-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
ADDRESS VARCHAR(50) NOT NULL,
USERNAME VARCHAR(50) NOT NULL UNIQUE,
PASSWORD VARCHAR(50) NOT NULL,
REGISTRATION_DATE DATETIME NOT NULL)

SELECT * FROM CUSTOMER

--2. Vehicle Table:
--• VehicleID (Primary Key): Unique identifier for each vehicle.
--• Model: Model of the vehicle.
--• Make: Manufacturer or brand of the vehicle.
--• Year: Manufacturing year of the vehicle.
--• Color: Color of the vehicle.
--• RegistrationNumber: Unique registration number for each vehicle.
--• Availability: Boolean indicating whether the vehicle is available for rent.
--• DailyRate: Daily rental rate for the vehicle.

CREATE TABLE VEHICLE(
VEHICLE_ID INT PRIMARY KEY IDENTITY(1,1),
MODEL VARCHAR(255) NOT NULL,
MAKER VARCHAR(255) NOT NULL,
YEAR_COL DATE NOT NULL,
COLOR VARCHAR(50) NOT NULL,
REGISTRATION_NUM VARCHAR(255) UNIQUE NOT NULL,
AVAILABILITY BIT NOT NULL DEFAULT 0,
DIALY_RATE DECIMAL(10,3))

SELECT * FROM VEHICLE

--3. Reservation Table:
--• ReservationID (Primary Key): Unique identifier for each reservation.
--• CustomerID (Foreign Key): Foreign key referencing the Customer table.
--• VehicleID (Foreign Key): Foreign key referencing the Vehicle table.
--• StartDate: Date and time of the reservation start.
--• EndDate: Date and time of the reservation end.
--• TotalCost: Total cost of the reservation.
--• Status: Current status of the reservation (e.g., pending, confirmed, completed)

CREATE TABLE RESERVATION(
RESERVATION_ID INT PRIMARY KEY NOT NULL IDENTITY(100,2),
CUSRTOMER_ID INT FOREIGN KEY REFERENCES CUSTOMER(CUSTOMER_ID),
VEHICLE_ID INT FOREIGN KEY REFERENCES VEHICLE(VEHICLE_ID),
START_DATE DATETIME NOT NULL,
END_DATE DATETIME NOT NULL,
TOTAL_COST DECIMAL NOT NULL,
STATUS VARCHAR(50) NOT NULL)

alter table reservation add constraint status_ck check (status in ('pending','confirmed','completed'))

SELECT * FROM RESERVATION

--4. Admin Table:
--• AdminID (Primary Key): Unique identifier for each admin.
--• FirstName: First name of the admin.
--• LastName: Last name of the admin.
--• Email: Email address of the admin for communication.
--• PhoneNumber: Contact number of the admin.
--• Username: Unique username for admin login.
--• Password: Securely hashed password for admin authentication.
--• Role: Role of the admin within the system (e.g., super admin, fleet manager).
--• JoinDate: Date when the admin joined the system.

CREATE TABLE ADMIN(
ADMIN_ID INT PRIMARY KEY NOT NULL IDENTITY(1,5),
FIRST_NAME VARCHAR(50) NOT NULL,
LAST_NAME VARCHAR(50) NOT NULL,
EMAIL VARCHAR(50) NOT NULL,
PHONE_NUMBER NUMERIC NOT NULL constraint CK_Number 
check (PHONE_NUMBER like '[1-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
USERNAME VARCHAR(50) NOT NULL UNIQUE,
PASSWORDHASH VARCHAR(50) NOT NULL,
ROLE VARCHAR(255) NOT NULL,
JOIN_DATE DATETIME NOT NULL)

-- Inserting records into the Customer table
INSERT INTO CUSTOMER (FIRST_NAME, LAST_NAME, EMAIL, PHONE_NUMBER, ADDRESS, USERNAME, PASSWORD, REGISTRATION_DATE)
VALUES
('John', 'Doe', 'john.doe@example.com', 1234567890, '123 Main St, Cityville', 'johndoe', 'password123', '2023-03-15'),
('Alice', 'Smith', 'alice.smith@example.com', 9876543210, '456 Elm St, Townsville', 'alicesmith', 'securepass', '2022-07-20'),
('Bob', 'Johnson', 'bob.johnson@example.com', 5551234567, '789 Oak St, Villageton', 'bobjohnson', 'p@ssw0rd', '2023-11-10'),
('Emily', 'Brown', 'emily.brown@example.com', 6669876543, '321 Maple St, Hamletville', 'emilybrown', '123456', '2024-01-05'),
('Michael', 'Wilson', 'michael.wilson@example.com', 3334567890, '987 Pine St, Countryside', 'michaelwilson', 'abc123', '2022-12-30');


select * from customer
-- Inserting records into the Vehicle table
INSERT INTO VEHICLE (MODEL, MAKER, YEAR_COL, COLOR, REGISTRATION_NUM, AVAILABILITY, DIALY_RATE)
VALUES
('Civic', 'Honda', '2020-01-01', 'Blue', 'ABC123', 1, 50.00),
('Corolla', 'Toyota', '2019-01-01', 'Red', 'DEF456', 1, 55.00),
('Accord', 'Honda', '2021-01-01', 'Black', 'GHI789', 1, 60.00),
('Camry', 'Toyota', '2020-01-01', 'White', 'JKL012', 1, 65.00),
('Sentra', 'Nissan', '2020-01-01', 'Silver', 'MNO345', 1, 70.00);

select * from vehicle
-- Inserting records into the Reservation table
INSERT INTO RESERVATION (CUSRTOMER_ID, VEHICLE_ID, START_DATE, END_DATE, TOTAL_COST, STATUS)
VALUES
(1, 1, '2024-05-10 09:00:00', '2024-05-12 18:00:00', 150.00, 'Confirmed'),
(2, 2, '2024-05-15 10:00:00', '2024-05-18 15:00:00', 165.00, 'Confirmed'),
(3, 3, '2024-05-20 11:00:00', '2024-05-22 12:00:00', 120.00, 'Pending'),
(4, 4, '2024-05-25 12:00:00', '2024-05-27 12:00:00', 130.00, 'Completed'),
(5, 5, '2024-05-30 14:00:00', '2024-06-02 16:00:00', 210.00, 'Confirmed');


-- Inserting records into the Admin table 
INSERT INTO ADMIN (FIRST_NAME, LAST_NAME, EMAIL, PHONE_NUMBER, USERNAME, PASSWORDHASH, ROLE, JOIN_DATE)
VALUES
('Admin', 'One', 'admin.one@example.com', 9998887770, 'admin1', 'adminpass', 'Super Admin', '2023-05-10'),
('Admin', 'Two', 'admin.two@example.com', 8887776660, 'admin2', 'adminpass', 'Fleet Manager', '2022-08-25'),
('Admin', 'Three', 'admin.three@example.com', 7776665550, 'admin3', 'adminpass', 'Customer Support', '2024-02-15'),
('Admin', 'Four', 'admin.four@example.com', 6665554440, 'admin4', 'adminpass', 'Finance Manager', '2023-10-20'),
('Admin', 'Five', 'admin.five@example.com', 5554443330, 'admin5', 'adminpass', 'Operations Manager', '2022-11-05');
