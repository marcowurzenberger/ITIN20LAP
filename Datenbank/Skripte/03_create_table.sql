USE innovations4austria;
GO

CREATE TABLE portaluser(
	id INT IDENTITY NOT NULL,
	firstname NVARCHAR(50) NOT NULL,
	lastname NVARCHAR(50) NOT NULL,
	email NVARCHAR(250) NOT NULL,
	[password] VARBINARY(1000) NOT NULL,
	role_id INT NOT NULL,
	company_id INT NOT NULL
);
GO

CREATE TABLE roles(
	id INT IDENTITY NOT NULL,
	description NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE companies(
	id INT IDENTITY NOT NULL,
	name NVARCHAR(50) NOT NULL,
	zip NVARCHAR(10) NOT NULL,
	city NVARCHAR(50) NOT NULL,
	street NVARCHAR(50) NOT NULL,
	number NVARCHAR(10) NOT NULL
);
GO

CREATE TABLE rooms(
	id INT IDENTITY NOT NULL,
	[description] NVARCHAR(50) NOT NULL,
	facility_id INT NOT NULL
);
GO

CREATE TABLE facilities(
	id INT IDENTITY NOT NULL,
	name NVARCHAR(50) NOT NULL,
	zip NVARCHAR(10) NOT NULL,
	city NVARCHAR(50) NOT NULL,
	street NVARCHAR(50) NOT NULL,
	number NVARCHAR(10) NOT NULL
);
GO

CREATE TABLE furnishments(
	id INT IDENTITY NOT NULL,
	[description] NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE roomfurnishments(
	id INT IDENTITY NOT NULL,
	furnishment_id INT NOT NULL,
	room_id INT NOT NULL
);
GO

CREATE TABLE prices(
	id INT IDENTITY NOT NULL,
	[date] DATETIME NOT NULL,
	value DECIMAL(5,2) NOT NULL
);
GO

CREATE TABLE bookings(
	id INT IDENTITY NOT NULL,
	portaluser_id INT NOT NULL,
	room_id INT NOT NULL,
	company_id INT NOT NULL,
	bookingdate DATE NOT NULL
);
GO

CREATE TABLE bills(
	id INT IDENTITY NOT NULL,
	company_id INT NOT NULL,
	billdate DATETIME NOT NULL,
	portaluser_id INT NOT NULL,
	createdate DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE billed_bookings(
	id INT IDENTITY NOT NULL,
	booking_id INT NOT NULL,
	bill_id INT NOT NULL,
	createdate DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE canceled_bookings(
	id INT IDENTITY NOT NULL,
	booking_id INT NOT NULL,
	portaluser_id INT NOT NULL,
	createdate DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE canceled_bills(
	id INT IDENTITY NOT NULL,
	bill_id INT NOT NULL,
	portaluser_id INT NOT NULL,
	createdate DATETIME DEFAULT GETDATE()
);
GO