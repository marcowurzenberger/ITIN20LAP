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