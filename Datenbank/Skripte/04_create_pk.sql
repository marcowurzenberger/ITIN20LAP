USE innovations4austria;
GO

ALTER TABLE bills
ADD
CONSTRAINT pk_bills
PRIMARY KEY (id);
GO

ALTER TABLE bookings
ADD
CONSTRAINT pk_bookings
PRIMARY KEY (id);
GO

ALTER TABLE bookingdetails
ADD
CONSTRAINT pk_bookingdetails
PRIMARY KEY (id);
GO

ALTER TABLE companies
ADD
CONSTRAINT pk_companies
PRIMARY KEY (id);
GO

ALTER TABLE facilities
ADD
CONSTRAINT pk_facilities
PRIMARY KEY (id);
GO

ALTER TABLE furnishments
ADD
CONSTRAINT pk_furnishments
PRIMARY KEY (id);
GO

ALTER TABLE portalusers
ADD
CONSTRAINT pk_portalusers
PRIMARY KEY (id);
GO

ALTER TABLE logs
ADD
CONSTRAINT pk_logs
PRIMARY KEY (id);
GO

ALTER TABLE roles
ADD
CONSTRAINT pk_roles
PRIMARY KEY (id);
GO

ALTER TABLE roomfurnishments
ADD
CONSTRAINT pk_roomfurnishments
PRIMARY KEY (id);
GO

ALTER TABLE rooms
ADD
CONSTRAINT pk_rooms
PRIMARY KEY (id);
GO

ALTER TABLE images
ADD
CONSTRAINT pk_images
PRIMARY KEY (id);
GO

-- LAP-Simulation

ALTER TABLE discounts
ADD
CONSTRAINT pk_discounts
PRIMARY KEY (id);
GO