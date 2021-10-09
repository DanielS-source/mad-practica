/* 
 * SQL Server Script
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */



/* 
 * Drop tables.                                                             
 * NOTE: before dropping a table (when re-executing the script), the tables 
 * having columns acting as foreign keys of the table to be dropped must be 
 * dropped first (otherwise, the corresponding checks on those tables could 
 * not be done).                                                            
 */

USE minibank

/* Drop Table Comments if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comments]') 
AND type in ('U')) DROP TABLE [Comments]
GO

/* Drop Table Likes if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Likes]') 
AND type in ('U')) DROP TABLE [Likes]
GO

/* Drop Table Follow if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Follow]') 
AND type in ('U')) DROP TABLE [Follow]
GO

/* Drop Table Image if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Image]') 
AND type in ('U')) DROP TABLE [Image]
GO

/* Drop Table User if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile]') 
AND type in ('U')) DROP TABLE [UserProfile]
GO

/*  UserProfile */

CREATE TABLE UserProfile (
	usrId bigint IDENTITY(1,1) NOT NULL,
	loginName varchar(30) NOT NULL,
	enPassword varchar(50) NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(40) NOT NULL,
	email varchar(60) NOT NULL,
	language varchar(2) NOT NULL,

	CONSTRAINT [PK_UserProfile] PRIMARY KEY (usrId),
	CONSTRAINT [UniqueKey_Login] UNIQUE (loginName)
)


CREATE NONCLUSTERED INDEX [IX_UserProfileIndexByLoginName]
ON [UserProfile] ([loginName] ASC)

PRINT N'Table UserProfile created.'
GO

PRINT N'Table User created.'
GO

/* Image */

CREATE TABLE Image(
    imgId bigint IDENTITY(1,1) NOT NULL,
    usrId bigint NOT NULL,
    pathImg varchar(150) NOT NULL,
    title varchar(150) NOT NULL,
    description varchar(150) NOT NULL,
    dateImg varchar(150) NOT NULL,
    category varchar(150) NOT NULL,
    f varchar(150) NOT NULL,
    t varchar(150) NOT NULL,
    ISO varchar(150) NOT NULL,
    wb varchar(150) NOT NULL,
    likes bigint NOT NULL,

    CONSTRAINT [PK_Image] PRIMARY KEY (imgId),
    CONSTRAINT [FK_User] FOREIGN KEY (usrId) REFERENCES UserProfile(usrId)
) 

CREATE NONCLUSTERED INDEX [IX_AccountIndexByImageId] 
ON Image (imgId ASC, usrId ASC)


PRINT N'Table Image created.'
GO

/* Comments */

CREATE TABLE Comments(
    comId bigint IDENTITY(1,1) NOT NULL,
    imgId bigint NOT NULL,
    usrId bigint NOT NULL,
    message varchar(150) NOT NULL,

    CONSTRAINT [PK_Comments] PRIMARY KEY (comId),
    CONSTRAINT [FK_User] FOREIGN KEY (usrId) REFERENCES UserProfile(usrId),
    CONSTRAINT [FK_Image] FOREIGN KEY (imgId) REFERENCES Image(imgId)
) 

CREATE NONCLUSTERED INDEX [IX_AccountIndexByCommentsId] 
ON Comments (comId ASC, usrId ASC)


PRINT N'Table Comments created.'
GO

/* Likes */

CREATE TABLE Likes(
    likeId bigint IDENTITY(1,1) NOT NULL,
    imgId bigint NOT NULL,
    usrId bigint NOT NULL,

    CONSTRAINT [PK_Likes] PRIMARY KEY (likeId),
    CONSTRAINT [FK_User] FOREIGN KEY (usrId) REFERENCES UserProfile(usrId),
    CONSTRAINT [FK_Image] FOREIGN KEY (imgId) REFERENCES Image(imgId)
) 

CREATE NONCLUSTERED INDEX [IX_AccountIndexByLikesId] 
ON Likes (likeId ASC, usrId ASC)


PRINT N'Table Likes created.'
GO

/* Follow */

CREATE TABLE Follow(
    folwId bigint IDENTITY(1,1) NOT NULL,
    usrId bigint NOT NULL,
    followerId bigint NOT NULL,

    CONSTRAINT [PK_Follow] PRIMARY KEY (folwId),
    CONSTRAINT [FK_User_1] FOREIGN KEY (usrId) REFERENCES UserProfile(usrId),
    CONSTRAINT [FK_User_2] FOREIGN KEY (followerId) REFERENCES UserProfile(usrId),
) 

CREATE NONCLUSTERED INDEX [IX_AccountIndexByFollowId] 
ON Follow (folwId ASC, usrId ASC)


PRINT N'Table Follow created.'
GO

PRINT N'Done'