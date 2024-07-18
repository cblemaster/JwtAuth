USE master
GO

DECLARE @SQL nvarchar(1000);
IF EXISTS (SELECT 1 FROM sys.databases WHERE name = N'JwtAuth')
BEGIN
    SET @SQL = N'USE JwtAuth;

                 ALTER DATABASE JwtAuth SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                 USE master;

                 DROP DATABASE JwtAuth;';
    EXEC (@SQL);
END;

CREATE DATABASE JwtAuth
GO

USE JwtAuth
GO

CREATE TABLE [Profile](
	ProfileId INT IDENTITY(1,1) NOT NULL,
	FirstName VARCHAR(255) NOT NULL,
	LastName VARCHAR(255) NOT NULL,
	Email VARCHAR(255) NOT NULL,
	Phone CHAR(10) NOT NULL,
	CreateDate DATETIME NOT NULL,
	UpdateDate DATETIME NULL,
	CONSTRAINT PK_Profile PRIMARY KEY(ProfileId),
	CONSTRAINT UC_Profile_Email UNIQUE(Email),
	CONSTRAINT UC_Profile_Phone UNIQUE(Phone),
)
GO

CREATE TABLE [Role](
	RoleId INT IDENTITY(1,1) NOT NULL,
	Rolename VARCHAR(50) NOT NULL,
	CreateDate DATETIME NOT NULL,
	UpdateDate DATETIME NULL,
	CONSTRAINT PK_Role PRIMARY KEY(RoleId),
	CONSTRAINT UC_Role_Rolename UNIQUE(Rolename),
)
GO

-- optional sample data
INSERT INTO [Role](Rolename, CreateDate, UpdateDate) VALUES ('Admin', GETDATE(), NULL);
INSERT INTO [Role](Rolename, CreateDate, UpdateDate) VALUES ('Customer', GETDATE(), NULL);
INSERT INTO [Role](Rolename, CreateDate, UpdateDate) VALUES ('Vendor', GETDATE(), NULL);
INSERT INTO [Role](Rolename, CreateDate, UpdateDate) VALUES ('Sales', GETDATE(), NULL);
INSERT INTO [Role](Rolename, CreateDate, UpdateDate) VALUES ('Marketing', GETDATE(), NULL);
INSERT INTO [Role](Rolename, CreateDate, UpdateDate) VALUES ('Reporting', GETDATE(), NULL);
INSERT INTO [Role](Rolename, CreateDate, UpdateDate) VALUES ('Support', GETDATE(), NULL);

CREATE TABLE [User](
	UserId INT IDENTITY(1,1) NOT NULL,
	ProfileId INT NOT NULL,
	Username VARCHAR(50) NOT NULL,
	PasswordHash VARCHAR(200) NOT NULL,
	Salt VARCHAR(200) NOT NULL,
	CreateDate DATETIME NOT NULL,
	UpdateDate DATETIME NULL,
	CONSTRAINT PK_User PRIMARY KEY(UserId),
	CONSTRAINT UC_User_Username UNIQUE(Username),
	CONSTRAINT UC_User_ProfileId UNIQUE(ProfileId),
	CONSTRAINT FK_User_Profile FOREIGN KEY(ProfileId) REFERENCES [Profile](ProfileId)
)
GO

CREATE TABLE RoleUser (
	UserId INT NOT NULL,
	RoleId INT NOT NULL,
	CONSTRAINT PK_RoleUser PRIMARY KEY (UserId, RoleId),
	CONSTRAINT FK_RoleUser_User FOREIGN KEY(UserId) REFERENCES [User](UserId),
	CONSTRAINT FK_RoleUser_Role FOREIGN KEY(RoleId) REFERENCES Role(RoleId),
)
GO
