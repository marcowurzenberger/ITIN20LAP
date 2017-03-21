USE innovations4austria;
GO

INSERT INTO roles([description]) VALUES('startups');
INSERT INTO roles([description]) VALUES('innovations4austria');
GO

INSERT INTO facilities(name, zip, city, street, number) VALUES('Cyberport 69', '1110', 'Wien', 'Simmeringer Hauptstrasse', '30');
-- LAP-Simulation
INSERT INTO facilities(name, zip, city, street, number) VALUES('Millennium Tower', '1040', 'Wien', 'Donaustraße', '21');
GO

INSERT INTO companies(name, zip, city, street, number, active) VALUES('innovations4austria', '1030', 'Wien', 'Rennweg', '95', 1);
INSERT INTO companies(name, zip, city, street, number, active) VALUES('ITfox', '1110', 'Wien', 'Simmeringer Hauptstrasse', '47', 1);
-- LAP-Simulation
INSERT INTO companies(name, zip, city, street, number, active) VALUES('Millennium Information Technologies', '3100', 'St. Pölten', 'Julius-Raab-Strasse', '23', 1);
GO

INSERT INTO portalusers(email, firstname, lastname, [password], role_id, company_id, active)
VALUES('max.muster@i4a.at', 'Max', 'Muster', HASHBYTES('sha2_256', '123user!'), 2, 1, 1);
INSERT INTO portalusers(email, firstname, lastname, [password], role_id, company_id, active)
VALUES('claudia@itfox.at', 'Claudia', 'Stiglmayer', HASHBYTES('sha2_256', '123user!'), 1, 2, 1);
INSERT INTO portalusers(email, firstname, lastname, [password], role_id, company_id, active)
VALUES('marcowurzenberger@live.at', 'Marco', 'Wurzenberger', HASHBYTES('sha2_256', '123user!'), 1, 2, 1);
INSERT INTO portalusers(email, firstname, lastname, [password], role_id, company_id, active)
VALUES('john.doe@mit.at', 'John', 'Doe', HASHBYTES('sha2_256', '123user!'), 1, 3, 1);
GO

INSERT INTO furnishments([description]) VALUES('Beamer');
INSERT INTO furnishments([description]) VALUES('Kaffeemaschine');
INSERT INTO furnishments([description]) VALUES('Stuhl');
INSERT INTO furnishments([description]) VALUES('Tisch');
INSERT INTO furnishments([description]) VALUES('Whiteboard');
INSERT INTO furnishments([description]) VALUES('Flipchart');
GO

-- Cyberport
INSERT INTO rooms([description], facility_id, price) VALUES('EG04', 1, 6.90);
INSERT INTO rooms([description], facility_id, price) VALUES('EG02', 1, 4.90);
INSERT INTO rooms([description], facility_id, price) VALUES('1001', 1, 5.90);
INSERT INTO rooms([description], facility_id, price) VALUES('2001', 1, 7.90);
INSERT INTO rooms([description], facility_id, price) VALUES('3001', 1, 4.50);
INSERT INTO rooms([description], facility_id, price) VALUES('3010', 1, 5.00);
INSERT INTO rooms([description], facility_id, price) VALUES('4001', 1, 4.00);
-- Millennium Tower
INSERT INTO rooms([description], facility_id, price) VALUES('Top 1', 2, 9.90);
INSERT INTO rooms([description], facility_id, price) VALUES('Top 2', 2, 6.90);
INSERT INTO rooms([description], facility_id, price) VALUES('Top 3', 2, 8.90);
INSERT INTO rooms([description], facility_id, price) VALUES('Top 4', 2, 11.90);
INSERT INTO rooms([description], facility_id, price) VALUES('Top 5', 2, 7.50);
INSERT INTO rooms([description], facility_id, price) VALUES('Top 6', 2, 8.00);
INSERT INTO rooms([description], facility_id, price) VALUES('Top 7', 2, 6.00);
GO

-- Cyberport
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
-- Millennium Tower
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(8, 4);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(8, 3);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(9, 1);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(10, 5);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(10, 6);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(11, 2);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(11, 3);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(11, 4);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(11, 3);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(12, 6);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(12, 5);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(12, 3);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(12, 4);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(13, 2);
INSERT INTO roomfurnishments(room_id, furnishment_id) VALUES(14, 1);
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

INSERT INTO images (data, name, furnishment_id)
SELECT BulkColumn, 'Video Projector', 1
FROM OPENROWSET( 
BULK 'C:\img\Video Projector-50.png', Single_Blob) AS import;

INSERT INTO images (data, name, furnishment_id)
SELECT BulkColumn, 'Video Projector Checked', 1
FROM OPENROWSET( 
BULK 'C:\img\Video Projector Filled-50.png', Single_Blob) AS import;

INSERT INTO images (data, name, furnishment_id)
SELECT BulkColumn, 'Coffee Maker', 2
FROM OPENROWSET( 
BULK 'C:\img\Coffee Maker-50.png', Single_Blob) AS import;

INSERT INTO images (data, name, furnishment_id)
SELECT BulkColumn, 'Coffee Maker Checked', 2
FROM OPENROWSET( 
BULK 'C:\img\Coffee Maker Filled-50.png', Single_Blob) AS import;

INSERT INTO images (data, name, furnishment_id)
SELECT BulkColumn, 'Chair', 3
FROM OPENROWSET(
BULK 'C:\img\Chair-50.png', SINGLE_BLOB) AS import;

INSERT INTO images (data, name, furnishment_id)
SELECT BulkColumn, 'Chair Checked', 3
FROM OPENROWSET(
BULK 'C:\img\Chair Filled-50.png', SINGLE_BLOB) AS import;

INSERT INTO images (data, name, furnishment_id)
SELECT BulkColumn, 'Table', 4
FROM OPENROWSET(
BULK 'C:\img\Table-50.png', SINGLE_BLOB) AS import;

INSERT INTO images (data, name, furnishment_id)
SELECT BulkColumn, 'Table Checked', 4
FROM OPENROWSET(
BULK 'C:\img\Table Filled-50.png', SINGLE_BLOB) AS import;

INSERT INTO images (data, name, furnishment_id)
SELECT BulkColumn, 'Whiteboard', 5
FROM OPENROWSET(
BULK 'C:\img\Whiteboard-50.png', SINGLE_BLOB) AS import;

INSERT INTO images (data, name, furnishment_id)
SELECT BulkColumn, 'Whiteboard Checked', 5
FROM OPENROWSET(
BULK 'C:\img\Whiteboard Filled-50.png', SINGLE_BLOB) AS import;

INSERT INTO images (data, name, furnishment_id)
SELECT BulkColumn, 'Flipchart', 6
FROM OPENROWSET(
BULK 'C:\img\Flipchart-50.png', SINGLE_BLOB) AS import;

INSERT INTO images (data, name, furnishment_id)
SELECT BulkColumn, 'Flipchart Checked', 6
FROM OPENROWSET(
BULK 'C:\img\Flipchart Filled-50.png', SINGLE_BLOB) AS import;

GO

-- LAP-Simulation

INSERT INTO discounts(facility_id, company_id, percentage) VALUES(2, 3, 5);
INSERT INTO discounts(facility_id, company_id, percentage) VALUES(1, 2, 3);
GO