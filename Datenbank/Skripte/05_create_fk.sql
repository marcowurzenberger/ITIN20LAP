USE innovations4austria;
GO

ALTER TABLE billed_bookings
ADD
CONSTRAINT fk_billed_bookings_bills
FOREIGN KEY (bill_id)
REFERENCES bills(id);
GO

ALTER TABLE billed_bookings
ADD
CONSTRAINT fk_billed_bookings_bookings
FOREIGN KEY (booking_id)
REFERENCES bookings(id);
GO

ALTER TABLE bills
ADD
CONSTRAINT fk_bills_companies
FOREIGN KEY (company_id)
REFERENCES companies(id);
GO

ALTER TABLE bills
ADD
CONSTRAINT fk_bills_portaluser
FOREIGN KEY (portaluser_id)
REFERENCES portaluser(id);
GO

ALTER TABLE bookings
ADD
CONSTRAINT fk_bookings_portaluser
FOREIGN KEY (portaluser_id)
REFERENCES portaluser(id);
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

ALTER TABLE canceled_bills
ADD
CONSTRAINT fk_canceled_bills_bills
FOREIGN KEY (bill_id)
REFERENCES bills(id);
GO

ALTER TABLE canceled_bills
ADD
CONSTRAINT fk_canceled_bills_portaluser
FOREIGN KEY (portaluser_id)
REFERENCES portaluser(id);
GO

ALTER TABLE canceled_bookings
ADD
CONSTRAINT fk_canceled_bookings_bookings
FOREIGN KEY (booking_id)
REFERENCES bookings(id);
GO

ALTER TABLE canceled_bookings
ADD
CONSTRAINT fk_canceled_bookings_portaluser
FOREIGN KEY (portaluser_id)
REFERENCES portaluser(id);
GO

ALTER TABLE portaluser
ADD
CONSTRAINT fk_portaluser_roles
FOREIGN KEY (role_id)
REFERENCES roles(id);
GO

ALTER TABLE portaluser
ADD
CONSTRAINT fk_portaluser_companies
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