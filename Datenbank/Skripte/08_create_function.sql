USE innovations4austria;
GO

CREATE PROCEDURE sp_getFilteredRoomIds
	@start DATETIME,
	@end DATETIME
AS
	SELECT DISTINCT r.id
	FROM rooms AS r
	WHERE r.id NOT IN (
		SELECT b.room_id
		FROM bookings AS b
			JOIN bookingdetails AS bd
				ON bd.booking_id = b.id
		WHERE (bd.booking_date >= @start AND bd.booking_date <= @end)
	);
GO

CREATE PROCEDURE sp_getRoomIdsBetweenDates
	@start DATETIME,
	@end DATETIME
AS
	SELECT DISTINCT r.id
	FROM rooms AS r
	WHERE r.id IN (
		SELECT b.room_id
		FROM bookings AS b
			JOIN bookingdetails AS bd
				ON bd.booking_id = b.id
		WHERE (bd.booking_date >= @start AND bd.booking_date <= @end)
	);
GO

CREATE PROCEDURE sp_UpdateRoom
	@id INT,
	@description NVARCHAR(50),
	@facility_id INT,
	@price DECIMAL(6,2)
AS
BEGIN

BEGIN TRY
	BEGIN TRANSACTION tr_UpdateRoom

		UPDATE rooms SET [description] = @description, facility_id = @facility_id, price = @price WHERE id = @id;

	COMMIT TRANSACTION tr_UpdateRoom
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION tr_UpdateRoom
END CATCH
END
GO

CREATE PROCEDURE sp_UpdateRoomfurnishments
	@id INT,
	@oldFurnishment INT,
	@newFurnishment INT
AS
BEGIN

BEGIN TRY
	BEGIN TRANSACTION tr_UpdateRoomfurnishments

	DECLARE @f_id INT = 0;

	SELECT @f_id = furnishment_id
	FROM roomfurnishments
	WHERE room_id = @id
	AND furnishment_id = @oldFurnishment

	IF(@f_id = 0)
	BEGIN
		INSERT INTO roomfurnishments(room_id, furnishment_id)
		VALUES (@id, @newFurnishment);
	END
	
	ELSE
	BEGIN
		UPDATE roomfurnishments 
		SET furnishment_id = @newFurnishment 
		WHERE room_id = @id 
		AND furnishment_id = @oldFurnishment
	END

	COMMIT TRANSACTION tr_UpdateRoomfurnishments
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION tr_UpdateRoomfurnishments
END CATCH
END
GO

CREATE PROCEDURE sp_GetExpendituresByMonthAndCompany
	@companyId INT,
	@month INT,
	@year INT
AS

SELECT	SUM(bd.price)
FROM companies AS c
	JOIN bookings AS b
		ON b.company_id = c.id
	JOIN bookingdetails AS bd
		ON bd.booking_id = b.id
WHERE c.id = @companyId
AND MONTH(bd.booking_date) = @month
AND YEAR(bd.booking_date) = @year;