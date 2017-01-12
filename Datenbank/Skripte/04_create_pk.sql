USE innovations4austria;
GO

ALTER TABLE billed_bookings
ADD
CONSTRAINT pk_billed_bookings
PRIMARY KEY (id);
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

ALTER TABLE canceled_bills
ADD
CONSTRAINT pk_canceled_bills
PRIMARY KEY (id);
GO

ALTER TABLE canceled_bookings
ADD
CONSTRAINT pk_canceled_bookings
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

ALTER TABLE portaluser
ADD
CONSTRAINT pk_portaluser
PRIMARY KEY (id);
GO

ALTER TABLE prices
ADD
CONSTRAINT pk_prices
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