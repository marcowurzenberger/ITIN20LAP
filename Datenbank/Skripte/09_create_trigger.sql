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

-- Trigger beim Einfügen eines neuen Benutzers
-- setze das Passwort auf Standard
--CREATE TRIGGER tr_set_default_pwd_new_user
--	ON portalusers
--FOR INSERT
--AS
--BEGIN
--	DECLARE @pwd VARBINARY(1000) = HASHBYTES('sha2_256', '123user!');
--	DECLARE @id INT;
--	DECLARE pwd_cursor CURSOR FOR
--		SELECT id FROM inserted;

--	BEGIN TRY
--		BEGIN TRANSACTION trans_set_pwd
			
--			OPEN pwd_cursor;

--			FETCH NEXT FROM pwd_cursor
--			INTO @id;

--			WHILE @@FETCH_STATUS = 0
--			BEGIN

--				UPDATE portalusers SET [password] = @pwd WHERE id = @id;

--				FETCH NEXT FROM pwd_cursor
--				INTO @id;

--			END

--		COMMIT TRANSACTION trans_set_pwd
--	END TRY
--	BEGIN CATCH
--		ROLLBACK TRANSACTION trans_set_pwd
--	END CATCH

--	CLOSE pwd_cursor;
--	DEALLOCATE pwd_cursor;
--END;
--GO