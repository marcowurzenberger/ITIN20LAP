USE innovations4austria;
GO

CREATE UNIQUE INDEX ui_portaluser_email ON portaluser (email);
CREATE UNIQUE INDEX ui_canceled_bills_bill_id ON canceled_bills (bill_id);
CREATE UNIQUE INDEX ui_companies_company ON companies (name, zip, city, street, number);
CREATE UNIQUE INDEX ui_canceled_bookings_booking_id ON canceled_bookings (booking_id);
CREATE UNIQUE INDEX ui_billed_bookings_booking_id ON billed_bookings (booking_id);
CREATE UNIQUE INDEX ui_facilities_facility ON facilities (name, zip, city, street, number);
GO