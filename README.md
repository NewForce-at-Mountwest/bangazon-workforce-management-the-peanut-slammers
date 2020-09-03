# Bangazon Workforce Management Site

Your team is reponsible for building an internal application for our human resources and IT departments. The application will allow them to create, list, and view Employees, Training Programs, Departments, and Computers.

You will be building an ASP<span>.NET</span> MVC Web Application using Visual Studio on Windows, using SQL Server as the database engine. You will be learning the Razor templating syntax. You will be learning how to use view models for defining the data to be used in a Razor template.

## Setup

Follow the general procedure from your previous sprint by having one team member create the initial project in Visual Studio and push it to github. Remember to setup the connection string in the `appsettings.json` file

This project will use the same [Bangazon database structure](https://github.com/nashville-software-school/bangazon-inc/blob/master/book-2-platform-api/chapters/sql/bangazon.sql) as the previous project, however, it is recommended that you make a new database in SQL Server, `BangazonWorkforce`, to help ensure you and your new teammates are working with the same data.

## Seed Data:

-- USE MASTER
-- GO
-- IF NOT EXISTS (
--     SELECT [name]
--     FROM sys.databases
--     WHERE [name] = N'BangazonWorkforce'
-- )
-- CREATE DATABASE BangazonWorkforce
-- GO
-- USE BangazonWorkforce
-- GO
-- DELETE FROM OrderProduct;
-- DELETE FROM ComputerEmployee;
-- DELETE FROM EmployeeTraining;
-- DELETE FROM Employee;
-- DELETE FROM TrainingProgram;
-- DELETE FROM Computer;
-- DELETE FROM Department;
-- DELETE FROM [Order];
-- DELETE FROM PaymentType;
-- DELETE FROM Product;
-- DELETE FROM ProductType;
-- DELETE FROM Customer;
-- ALTER TABLE Employee DROP CONSTRAINT [FK_EmployeeDepartment];
-- ALTER TABLE ComputerEmployee DROP CONSTRAINT [FK_ComputerEmployee_Employee];
-- ALTER TABLE ComputerEmployee DROP CONSTRAINT [FK_ComputerEmployee_Computer];
-- ALTER TABLE EmployeeTraining DROP CONSTRAINT [FK_EmployeeTraining_Employee];
-- ALTER TABLE EmployeeTraining DROP CONSTRAINT [FK_EmployeeTraining_Training];
-- ALTER TABLE Product DROP CONSTRAINT [FK_Product_ProductType];
-- ALTER TABLE Product DROP CONSTRAINT [FK_Product_Customer];
-- ALTER TABLE PaymentType DROP CONSTRAINT [FK_PaymentType_Customer];
-- ALTER TABLE [Order] DROP CONSTRAINT [FK_Order_Customer];
-- ALTER TABLE [Order] DROP CONSTRAINT [FK_Order_Payment];
-- ALTER TABLE OrderProduct DROP CONSTRAINT [FK_OrderProduct_Product];
-- ALTER TABLE OrderProduct DROP CONSTRAINT [FK_OrderProduct_Order];
-- DROP TABLE IF EXISTS OrderProduct;
-- DROP TABLE IF EXISTS ComputerEmployee;
-- DROP TABLE IF EXISTS EmployeeTraining;
-- DROP TABLE IF EXISTS Employee;
-- DROP TABLE IF EXISTS TrainingProgram;
-- DROP TABLE IF EXISTS Computer;
-- DROP TABLE IF EXISTS Department;
-- DROP TABLE IF EXISTS [Order];
-- DROP TABLE IF EXISTS PaymentType;
-- DROP TABLE IF EXISTS Product;
-- DROP TABLE IF EXISTS ProductType;
-- DROP TABLE IF EXISTS Customer;
CREATE TABLE Department (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    [Name] VARCHAR(55) NOT NULL,
    Budget  INTEGER NOT NULL
);
CREATE TABLE Employee (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    FirstName VARCHAR(55) NOT NULL,
    LastName VARCHAR(55) NOT NULL,
    DepartmentId INTEGER NOT NULL,
    IsSuperVisor BIT NOT NULL DEFAULT(0),
    CONSTRAINT FK_EmployeeDepartment FOREIGN KEY(DepartmentId) REFERENCES Department(Id)
);
CREATE TABLE Computer (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    PurchaseDate DATETIME NOT NULL,
    DecomissionDate DATETIME,
    Make VARCHAR(55) NOT NULL,
    Manufacturer VARCHAR(55) NOT NULL
);
CREATE TABLE ComputerEmployee (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    EmployeeId INTEGER NOT NULL,
    ComputerId INTEGER NOT NULL,
    AssignDate DATETIME NOT NULL,
    UnassignDate DATETIME,
    CONSTRAINT FK_ComputerEmployee_Employee FOREIGN KEY(EmployeeId) REFERENCES Employee(Id),
    CONSTRAINT FK_ComputerEmployee_Computer FOREIGN KEY(ComputerId) REFERENCES Computer(Id)
);
CREATE TABLE TrainingProgram (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    [Name] VARCHAR(255) NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    MaxAttendees INTEGER NOT NULL
);
CREATE TABLE EmployeeTraining (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    EmployeeId INTEGER NOT NULL,
    TrainingProgramId INTEGER NOT NULL,
    CONSTRAINT FK_EmployeeTraining_Employee FOREIGN KEY(EmployeeId) REFERENCES Employee(Id),
    CONSTRAINT FK_EmployeeTraining_Training FOREIGN KEY(TrainingProgramId) REFERENCES TrainingProgram(Id)
);
CREATE TABLE ProductType (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    [Name] VARCHAR(55) NOT NULL
);
CREATE TABLE Customer (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    FirstName VARCHAR(55) NOT NULL,
    LastName VARCHAR(55) NOT NULL,
    CreationDate DATETIME NOT NULL,
    LastActiveDate DATETIME NOT NULL
);
CREATE TABLE Product (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    ProductTypeId INTEGER NOT NULL,
    CustomerId INTEGER NOT NULL,
    Price MONEY NOT NULL,
    Title VARCHAR(255) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    Quantity INTEGER NOT NULL,
    CONSTRAINT FK_Product_ProductType FOREIGN KEY(ProductTypeId) REFERENCES ProductType(Id),
    CONSTRAINT FK_Product_Customer FOREIGN KEY(CustomerId) REFERENCES Customer(Id)
);
CREATE TABLE PaymentType (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    AcctNumber VARCHAR(55) NOT NULL,
    [Name] VARCHAR(55) NOT NULL,
    CustomerId INTEGER NOT NULL,
    CONSTRAINT FK_PaymentType_Customer FOREIGN KEY(CustomerId) REFERENCES Customer(Id)
);
CREATE TABLE [Order] (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    CustomerId INTEGER NOT NULL,
    PaymentTypeId INTEGER,
    CONSTRAINT FK_Order_Customer FOREIGN KEY(CustomerId) REFERENCES Customer(Id),
    CONSTRAINT FK_Order_Payment FOREIGN KEY(PaymentTypeId) REFERENCES PaymentType(Id)
);
CREATE TABLE OrderProduct (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    OrderId INTEGER NOT NULL,
    ProductId INTEGER NOT NULL,
    CONSTRAINT FK_OrderProduct_Product FOREIGN KEY(ProductId) REFERENCES Product(Id),
    CONSTRAINT FK_OrderProduct_Order FOREIGN KEY(OrderId) REFERENCES [Order](Id)
);
INSERT INTO Department (Name, Budget) VALUES ('Clothing', 50000);
INSERT INTO Department (Name, Budget) VALUES ('Hardware', 100000);
INSERT INTO Department (Name, Budget) VALUES ('Pet Care', 1000000);

INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSupervisor) VALUES ('Derek', 'Mayse', 1, 'False');
INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSupervisor) VALUES ('Mike', 'Hotchkiss', 2, 'False');
INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSupervisor) VALUES ('Lindsey', 'Crittendon', 3, 'False');

INSERT INTO Computer (PurchaseDate, Make, Manufacturer) VALUES ('1983-09-01', 'Apple II Plus', 'Apple');
INSERT INTO Computer (PurchaseDate, Make, Manufacturer) VALUES ('1985-09-01', 'BBC Master', 'Acorn Computers');
INSERT INTO Computer (PurchaseDate, Make, Manufacturer) VALUES ('1984-09-01', '464 Plus', 'Amstrad');

INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) VALUES (1, 1, '2020-09-01');
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) VALUES (2, 2, '2020-09-01');
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) VALUES (3, 3, '2020-09-01');
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (1, 3, '2010-09-01', '2015-09-01');

INSERT INTO Customer (FirstName, LastName, CreationDate, LastActiveDate) VALUES ('Scary', 'Barry', '2020-09-01', '2020-09-01');
INSERT INTO Customer (FirstName, LastName, CreationDate, LastActiveDate) VALUES ('Orby', 'Hotchkiss', '2020-09-01', '2020-09-01');
INSERT INTO Customer (FirstName, LastName, CreationDate, LastActiveDate) VALUES ('Jordan', 'Castelloe', '2020-09-01', '2020-09-01');


INSERT INTO TrainingProgram (Name, StartDate, EndDate, MaxAttendees) VALUES ('Orientation', '2020-09-01', '2020-09-30', 30);
INSERT INTO TrainingProgram (Name, StartDate, EndDate, MaxAttendees) VALUES ('Sexual Harassment', '2020-09-01', '2020-09-30', 60);
INSERT INTO TrainingProgram (Name, StartDate, EndDate, MaxAttendees) VALUES ('Equal Opportunity Employement', '2020-09-01', '2020-09-30', 45);

INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (1, 1);
INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (2, 2);
INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (3, 3);
INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (1, 2);


INSERT INTO PaymentType (AcctNumber, Name, CustomerId) VALUES ('adfkhs', 'Visa', 1);
INSERT INTO PaymentType (AcctNumber, Name, CustomerId) VALUES ('SDHJSFDS', 'Mastercard', 2);
INSERT INTO PaymentType (AcctNumber, Name, CustomerId) VALUES ('21344145', 'American Express', 3);

INSERT INTO [Order] (CustomerId, PaymentTypeId) VALUES (1, 1);
INSERT INTO [Order] (CustomerId, PaymentTypeId) VALUES (2, 2);
INSERT INTO [Order] (CustomerId, PaymentTypeId) VALUES (3, 3);

INSERT INTO ProductType (Name) VALUES ('Clothing');
INSERT INTO ProductType (Name) VALUES ('Tool');
INSERT INTO ProductType (Name) VALUES ('Medication');

INSERT INTO Product (ProductTypeId, CustomerId, Price, Title, Description, Quantity) VALUES (1, 1, 200.00, 'Clothing', 'Stuff and things', 1);
INSERT INTO Product (ProductTypeId, CustomerId, Price, Title, Description, Quantity) VALUES (2, 2, 100.00, 'A thing', 'Does stuff', 2);
INSERT INTO Product (ProductTypeId, CustomerId, Price, Title, Description, Quantity) VALUES (3, 3, 100.00, 'Meds', 'For fleas', 3);

INSERT INTO OrderProduct (OrderId, ProductId) VALUES (1, 1);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (2, 2);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (3, 3);