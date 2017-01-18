USE innovations4austria;
GO

ALTER TABLE billdetails
ADD
CONSTRAINT fk_billdetails_bills
FOREIGN KEY (bill_id)
REFERENCES bills(id);
GO

ALTER TABLE billdetails
ADD
CONSTRAINT fk_billdetails_bookingdetails
FOREIGN KEY (bookingdetail_id)
REFERENCES bookingdetails(id);
GO

ALTER TABLE bookings
ADD
CONSTRAINT fk_bookings_rooms
FOREIGN KEY (room_id)
REFERENCES rooms(id);
GO

ALTER TABLE bookings
ADD
CONSTRAINT fk_bookings_companies
FOREIGN KEY (company_id)
REFERENCES companies(id);
GO

ALTER TABLE bookingdetails
ADD
CONSTRAINT fk_bookingdetails_bookings
FOREIGN KEY (booking_id)
REFERENCES bookings(id);
GO

ALTER TABLE portalusers
ADD
CONSTRAINT fk_portalusers_roles
FOREIGN KEY (role_id)
REFERENCES roles(id);
GO

ALTER TABLE portalusers
ADD
CONSTRAINT fk_portalusers_companies
FOREIGN KEY (company_id)
REFERENCES companies(id);
GO

ALTER TABLE roomfurnishments
ADD
CONSTRAINT fk_roomfurnishments_furnishments
FOREIGN KEY (furnishment_id)
REFERENCES furnishments(id);
GO

ALTER TABLE roomfurnishments
ADD
CONSTRAINT fk_roomfurnishments_rooms
FOREIGN KEY (room_id)
REFERENCES rooms(id);
GO

ALTER TABLE rooms
ADD
CONSTRAINT fk_rooms_facilities
FOREIGN KEY (facility_id)
REFERENCES facilities(id);
GO