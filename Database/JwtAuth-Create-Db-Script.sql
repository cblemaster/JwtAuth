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
	-- User data that is not related to authentication
	ProfileId INT IDENTITY(1,1) NOT NULL,
	FirstName VARCHAR(255) NOT NULL,		-- the user's first name, required and has max length of 255
	LastName VARCHAR(255) NOT NULL,			-- the user's last name, required and has max length of 255
	Email VARCHAR(255) NOT NULL,			-- the user's email address, required and has max length of 255, must be unique
	Phone CHAR(10) NOT NULL,				-- the user's phone number, required and has max length of 10, must be unique
	CreateDate DATETIME NOT NULL,			-- set when record created, then never updated
	UpdateDate DATETIME NULL,				-- null when record created, then updated when record is updated
	CONSTRAINT PK_Profile PRIMARY KEY(ProfileId),
	CONSTRAINT UC_Profile_Email UNIQUE(Email),
	CONSTRAINT UC_Profile_Phone UNIQUE(Phone),
)
GO

CREATE TABLE [Role](
	-- User roles to use in authorization schemes
	RoleId INT IDENTITY(1,1) NOT NULL,
	Rolename VARCHAR(50) NOT NULL,			-- the role's name, required and has max length of 50, must be unique
	CreateDate DATETIME NOT NULL,			-- set when record created, then never updated
	UpdateDate DATETIME NULL,				-- null when record created, then updated when record is updated
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
	-- User data related to authentication
	UserId INT IDENTITY(1,1) NOT NULL,
	Username VARCHAR(50) NOT NULL,						-- the user's username, required and has max length of 50, must be unique
	PasswordHash VARCHAR(200) NOT NULL,
	Salt VARCHAR(200) NOT NULL,
	CreateDate DATETIME NOT NULL,						-- set when record created, then never updated
	UpdateDate DATETIME NULL,							-- null when record created, then updated when record is updated
	ProfileId INT NOT NULL,
	CONSTRAINT PK_User PRIMARY KEY(UserId),
	CONSTRAINT UC_User_Username UNIQUE(Username),
	CONSTRAINT UC_User_ProfileId UNIQUE(ProfileId),		-- user-to-profile relationship is one-to-one
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
