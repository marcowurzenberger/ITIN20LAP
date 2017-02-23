USE innovations4austria;
GO

INSERT INTO roles([description]) VALUES('startups');
INSERT INTO roles([description]) VALUES('innovations4austria');
GO

INSERT INTO facilities(name, zip, city, street, number) VALUES('Cyberport 69', '1110', 'Wien', 'Simmeringer Hauptstrasse', '30');
GO

INSERT INTO companies(name, zip, city, street, number, active) VALUES('innovations4austria', '1030', 'Wien', 'Rennweg', '95', 1);
INSERT INTO companies(name, zip, city, street, number, active) VALUES('ITfox', '1110', 'Wien', 'Simmeringer Hauptstrasse', '47', 1);
GO

INSERT INTO portalusers(email, firstname, lastname, [password], role_id, company_id, active)
VALUES('max.muster@i4a.at', 'Max', 'Muster', HASHBYTES('sha2_256', '123user!'), 2, 1, 1);
INSERT INTO portalusers(email, firstname, lastname, [password], role_id, company_id, active)
VALUES('claudia@itfox.at', 'Claudia', 'Stiglmayer', HASHBYTES('sha2_256', '123user!'), 1, 2, 1);
INSERT INTO portalusers(email, firstname, lastname, [password], role_id, company_id, active)
VALUES('marco@itfox.at', 'Marco', 'Wurzenberger', HASHBYTES('sha2_256', '123user!'), 1, 2, 1);
GO

INSERT INTO furnishments([description]) VALUES('Beamer');
INSERT INTO furnishments([description]) VALUES('Kaffeemaschine');
INSERT INTO furnishments([description]) VALUES('Stuhl');
INSERT INTO furnishments([description]) VALUES('Tisch');
INSERT INTO furnishments([description]) VALUES('Whiteboard');
INSERT INTO furnishments([description]) VALUES('Flipchart');
GO

INSERT INTO rooms([description], facility_id, price) VALUES('EG04', 1, 6.90);
INSERT INTO rooms([description], facility_id, price) VALUES('EG02', 1, 4.90);
INSERT INTO rooms([description], facility_id, price) VALUES('1001', 1, 5.90);
INSERT INTO rooms([description], facility_id, price) VALUES('2001', 1, 7.90);
INSERT INTO rooms([description], facility_id, price) VALUES('3001', 1, 4.50);
INSERT INTO rooms([description], facility_id, price) VALUES('3010', 1, 5.00);
INSERT INTO rooms([description], facility_id, price) VALUES('4001', 1, 4.00);
GO

INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(1, 4);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(1, 3);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(2, 1);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(3, 5);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(3, 6);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(4, 2);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(4, 3);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(4, 4);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(4, 3);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(5, 6);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(5, 5);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(5, 3);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(5, 4);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(6, 2);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(7, 1);
GO

INSERT INTO bills(billdate) VALUES('2016-05-01');
GO

INSERT INTO bookings(company_id, room_id) VALUES(2, 1);
INSERT INTO bookings(company_id, room_id) VALUES(2, 2);
INSERT INTO bookings(company_id, room_id) VALUES(2, 4);
GO

INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(1, '2016-15-12', 1, 6.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(1, '2016-16-12', 1, 6.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(1, '2016-17-12', 1, 6.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(2, '2016-02-12', 1, 4.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(2, '2016-03-12', 1, 4.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(2, '2016-04-12', 1, 4.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(2, '2016-05-12', 1, 4.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(2, '2016-06-12', 1, 4.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(2, '2016-07-12', 1, 4.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(2, '2016-08-12', 1, 4.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(2, '2016-09-12', 1, 4.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(3, '2017-25-01', NULL, 7.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(3, '2017-26-01', NULL, 7.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(3, '2017-27-01', NULL, 7.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(3, '2017-28-01', NULL, 7.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(3, '2017-29-01', NULL, 7.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(3, '2017-30-01', NULL, 7.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(3, '2017-31-01', NULL, 7.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(3, '2017-01-02', NULL, 7.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(3, '2017-02-02', NULL, 7.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(3, '2017-03-02', NULL, 7.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(3, '2017-04-02', NULL, 7.90);
INSERT INTO bookingdetails(booking_id, booking_date, bill_id, price) VALUES(3, '2017-05-02', NULL, 7.90);
GO