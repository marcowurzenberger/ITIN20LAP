USE innovations4austria;
GO

-- Trigger nach Update von Firma,
--  wenn Firma aktiv = false, dann werden die dazugehörigen Benutzer auch inaktiv gesetzt
CREATE TRIGGER tr_set_user_inactive
	ON portalusers
AFTER UPDATE
AS
BEGIN
	DECLARE @id INT;
	DECLARE @active BIT;
	DECLARE active_cursor CURSOR FOR 
			SELECT id, active FROM inserted;

	BEGIN TRY
			OPEN active_cursor; 

			FETCH NEXT FROM active_cursor   
			INTO @id, @active; 
  
			WHILE @@FETCH_STATUS = 0  
			BEGIN   

				UPDATE portalusers SET active = @active WHERE id = @id;

				FETCH NEXT FROM active_cursor   
				INTO @id, @active; 

			END

	END TRY
	BEGIN CATCH

	END CATCH

	CLOSE active_cursor;  
	DEALLOCATE active_cursor;  

END;
GO