USE innovations4austria;
GO

CREATE TABLE portalusers(
	id INT IDENTITY NOT NULL,
	firstname NVARCHAR(50) NOT NULL,
	lastname NVARCHAR(50) NOT NULL,
	email NVARCHAR(250) NOT NULL,
	[password] VARBINARY(1000) NOT NULL,
	role_id INT NOT NULL,
	company_id INT NOT NULL,
	active BIT NOT NULL
);
GO

CREATE TABLE roles(
	id INT IDENTITY NOT NULL,
	[description] NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE companies(
	id INT IDENTITY NOT NULL,
	name NVARCHAR(50) NOT NULL,
	zip NVARCHAR(10) NOT NULL,
	city NVARCHAR(50) NOT NULL,
	street NVARCHAR(50) NOT NULL,
	number NVARCHAR(10) NOT NULL,
	active BIT NOT NULL
);
GO

CREATE TABLE rooms(
	id INT IDENTITY NOT NULL,
	[description] NVARCHAR(50) NOT NULL,
	facility_id INT NOT NULL,
	price DECIMAL(6,2) NOT NULL
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

CREATE TABLE bookings(
	id INT IDENTITY NOT NULL,
	room_id INT NOT NULL,
	company_id INT NOT NULL
);
GO

CREATE TABLE bookingdetails(
	id INT IDENTITY NOT NULL,
	booking_id INT NOT NULL,
	fromdate DATETIME NOT NULL,
	todate DATETIME NOT NULL,
	bill_id INT,
	price DECIMAL(6,2) NOT NULL
);

CREATE TABLE bills(
	id INT IDENTITY NOT NULL,
	billdate DATETIME NOT NULL
);
GO

CREATE TABLE logs(
	id INT IDENTITY NOT NULL,
	[date] DATETIME NOT NULL,
	thread NVARCHAR(255) NOT NULL,
	[level] NVARCHAR(50) NOT NULL,
	logger NVARCHAR(255) NOT NULL,
	[message] NVARCHAR(4000) NOT NULL,
	exception NVARCHAR(2000)
);
GO