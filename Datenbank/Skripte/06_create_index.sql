USE innovations4austria;
GO

CREATE UNIQUE INDEX ui_portalusers_email ON portalusers (email);
CREATE UNIQUE INDEX ui_companies_company ON companies (name, zip, city, street, number);
CREATE UNIQUE INDEX ui_billdetails_bookingdetail_id ON billdetails (bookingdetail_id);
CREATE UNIQUE INDEX ui_facilities_facility ON facilities (name, zip, city, street, number);
GO