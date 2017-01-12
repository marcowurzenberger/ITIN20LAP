USE master;
GO

IF NOT DB_ID('innovations4austria') IS NULL ALTER DATABASE innovations4austria SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
IF NOT DB_ID('innovations4austria') IS NULL DROP DATABASE innovations4austria;
GO