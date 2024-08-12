USE master
GO

DECLARE @sql nvarchar(1000);

IF EXISTS (SELECT 1 FROM sys.databases WHERE name = N'JwtAuth')

BEGIN
    SET @sql = N'USE JwtAuth;

                 ALTER DATABASE JwtAuth SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                 USE master;

                 DROP DATABASE JwtAuth;';
    EXEC (@sql);
END;

CREATE DATABASE JwtAuth
GO

USE JwtAuth
GO

CREATE TABLE [User](
	UserId INT IDENTITY(1,1) NOT NULL,
	Username VARCHAR(50) NOT NULL,
	PasswordHash VARCHAR(200) NOT NULL,
	Salt VARCHAR(200) NOT NULL,
	FirstName VARCHAR(255) NOT NULL,
	LastName VARCHAR(255) NOT NULL,
	Email VARCHAR(255) NOT NULL,
	Phone CHAR(10) NOT NULL,
	Roles VARCHAR(255) NOT NULL,
	CreateDate DATETIME NOT NULL,
	UpdateDate DATETIME NULL,
	CONSTRAINT PK_User PRIMARY KEY(UserId),
	CONSTRAINT UC_User_Username UNIQUE(Username),
	CONSTRAINT UC_User_Email UNIQUE(Email),
	CONSTRAINT UC_User_Phone UNIQUE(Phone),
)
GO
