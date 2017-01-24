USE innovations4austria;
GO

INSERT INTO roles([description]) VALUES('startups');
INSERT INTO roles([description]) VALUES('innovations4austria');
GO

INSERT INTO facilities(name, zip, city, street, number) VALUES('Cyberport 69', '1110', 'Wien', 'Simmeringer Hauptstrasse', '30');
GO

INSERT INTO companies(name, zip, city, street, number) VALUES('innovations4austria', '1030', 'Wien', 'Rennweg', '95');
INSERT INTO companies(name, zip, city, street, number) VALUES('ITfox', '1110', 'Wien', 'Simmeringer Hauptstrasse', '47');
GO

INSERT INTO portalusers(email, firstname, lastname, [password], role_id, company_id, active)
VALUES('max.muster@i4.at', 'Max', 'Muster', HASHBYTES('sha2_256', '123user!'), 2, 1, 1)
INSERT INTO portalusers(email, firstname, lastname, [password], role_id, company_id, active)
VALUES('claudia@itfox.at', 'Claudia', 'Stiglmayer', HASHBYTES('sha2_256', '123user!'), 1, 2, 1)
INSERT INTO portalusers(email, firstname, lastname, [password], role_id, company_id, active)
VALUES('marco@itfox.at', 'Marco', 'Wurzenberger', HASHBYTES('sha2_256', '123user!'), 1, 2, 1)
GO

INSERT INTO furnishments([description]) VALUES('Büro');
INSERT INTO furnishments([description]) VALUES('Seminarraum');
INSERT INTO furnishments([description]) VALUES('Meetingraum');
INSERT INTO furnishments([description]) VALUES('Präsentationsraum');
GO

INSERT INTO rooms([description], facility_id, price) VALUES('EG04', 1, 6.90);
INSERT INTO rooms([description], facility_id, price) VALUES('EG02', 1, 4.90);
INSERT INTO rooms([description], facility_id, price) VALUES('2001', 1, 5.90);
INSERT INTO rooms([description], facility_id, price) VALUES('4001', 1, 7.90);
INSERT INTO rooms([description], facility_id, price) VALUES('1001', 1, 4.50);
GO

INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(1,4);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(2,4);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(2,3);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(3,2);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(4,1);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(5,1);
GO

INSERT INTO bills(billdate) VALUES('2016-31-12');
GO

INSERT INTO bookings(company_id, room_id) VALUES(2, 1);
INSERT INTO bookings(company_id, room_id) VALUES(2, 5);
GO

INSERT INTO bookingdetails(booking_id, bookingdate, price) VALUES(1, '2016-15-12', 6.90);
INSERT INTO bookingdetails(booking_id, bookingdate, price) VALUES(1, '2016-16-12', 6.90);
INSERT INTO bookingdetails(booking_id, bookingdate, price) VALUES(1, '2016-17-12', 6.90);
INSERT INTO bookingdetails(booking_id, bookingdate, price) VALUES(2, '2016-02-12', 4.50);
INSERT INTO bookingdetails(booking_id, bookingdate, price) VALUES(2, '2016-03-12', 4.50);
INSERT INTO bookingdetails(booking_id, bookingdate, price) VALUES(2, '2016-04-12', 4.50);
INSERT INTO bookingdetails(booking_id, bookingdate, price) VALUES(2, '2016-05-12', 4.50);
INSERT INTO bookingdetails(booking_id, bookingdate, price) VALUES(2, '2016-10-12', 4.50);
INSERT INTO bookingdetails(booking_id, bookingdate, price) VALUES(2, '2016-11-12', 4.50);
INSERT INTO bookingdetails(booking_id, bookingdate, price) VALUES(2, '2016-12-12', 4.50);
INSERT INTO bookingdetails(booking_id, bookingdate, price) VALUES(2, '2016-13-12', 4.50);
GO

INSERT INTO billdetails(bookingdetail_id, bill_id) VALUES(1, 1);
INSERT INTO billdetails(bookingdetail_id, bill_id) VALUES(2, 1);
GO