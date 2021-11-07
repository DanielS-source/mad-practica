/* 
 * SQL Server Script
 * 
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */

 /******************************************************************************/
 /*** PATH to store the db files. This path must exists in the local system. ***/
 /******************************************************************************/
 DECLARE @Default_DB_Path as VARCHAR(64)  
 SET @Default_DB_Path = N'C:\Software\DataBase'
 

USE [master]

/****** Drop database if already exists  ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'photogram_test')
DROP DATABASE [photogram_test]



/* DataBase Creation */
                                  
DECLARE @sql nvarchar(500)

SET @sql = 
  N'CREATE DATABASE [photogram_test] 
    ON PRIMARY ( NAME = photogram_test, FILENAME = "' + @Default_DB_Path + N'photogram_test.mdf")
    LOG ON ( NAME = photogram_test_log, FILENAME = "' + @Default_DB_Path + N'photogram_test_log.ldf")'

EXEC(@sql)
PRINT N'Test database created.'
PRINT N'Done'
GO


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

USE photogram_test

/* Drop Table Label if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Image_Tag]') 
AND type in ('U')) DROP TABLE Image_Tag
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Tag]') 
AND type in ('U')) DROP TABLE Tag
GO

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

/* Drop Table Category if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') 
AND type in ('U')) DROP TABLE Category
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

/* Category */

CREATE TABLE Category(
    catId bigint IDENTITY(1,1) NOT NULL,
    name varchar(25) NOT NULL,

    CONSTRAINT [PK_Category] PRIMARY KEY (catId),
) 

CREATE NONCLUSTERED INDEX [IX_AccountIndexByCatId] 
ON Category (catId)


PRINT N'Table Category created.'
GO

/* Tag */
CREATE TABLE Tag(
	tagId	bigint IDENTITY(1,1) NOT NULL,
	name	varchar(12)	NOT NULL,
	uses	bigint NOT NULL,
	CONSTRAINT [PK_Tag]		PRIMARY KEY (tagId),
	CONSTRAINT [UK_Tag_Name]	UNIQUE		(name)
);
PRINT N'Table Tag created.'

/* Image */

CREATE TABLE Image(
    imgId bigint IDENTITY(1,1) NOT NULL,
    usrId bigint NOT NULL,
    pathImg varchar(255) NOT NULL,
    title varchar(25) NOT NULL,
    description varchar(255) NOT NULL,
    dateImg date NOT NULL,
    catId bigint NOT NULL,
    f varchar(150),
    t varchar(150),
    ISO varchar(150),
    wb varchar(150),
    likes bigint NOT NULL,

    CONSTRAINT [PK_Image] PRIMARY KEY (imgId),
    CONSTRAINT [FK_User_Img] FOREIGN KEY (usrId) REFERENCES UserProfile(usrId),
    CONSTRAINT [FK_User_Cat] FOREIGN KEY (catId) REFERENCES Category(catId)
) 

CREATE NONCLUSTERED INDEX [IX_AccountIndexByImageId] 
ON Image (imgId ASC, usrId ASC)


PRINT N'Table Image created.'
GO

/* Image_Tag */
CREATE TABLE Image_Tag (
	imgId	BIGINT	NOT NULL,
	tagId	BIGINT	NOT NULL,

	CONSTRAINT [PK_Image_Tag]			PRIMARY KEY (imgId, tagId),
	CONSTRAINT [FK_Image_Tag_imgId]	FOREIGN KEY (imgId)	REFERENCES Image(imgId),
	CONSTRAINT [FK_Image_Tag_tagId]	FOREIGN KEY (tagId)		REFERENCES Tag(tagId)
);
PRINT N'Table Image_Tag created.'

/* Comments */

CREATE TABLE Comments(
    comId bigint IDENTITY(1,1) NOT NULL,
    imgId bigint NOT NULL,
    usrId bigint NOT NULL,
    message varchar(255) NOT NULL,
    postDate date NOT NULL,

    CONSTRAINT [PK_Comments] PRIMARY KEY (comId),
    CONSTRAINT [FK_User_Comm] FOREIGN KEY (usrId) REFERENCES UserProfile(usrId),
    CONSTRAINT [FK_Image_Comm] FOREIGN KEY (imgId) REFERENCES Image(imgId)
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
    CONSTRAINT [FK_User_Like] FOREIGN KEY (usrId) REFERENCES UserProfile(usrId),
    CONSTRAINT [FK_Image_Like] FOREIGN KEY (imgId) REFERENCES Image(imgId)
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
    CONSTRAINT [FK_User_Foll] FOREIGN KEY (usrId) REFERENCES UserProfile(usrId),
    CONSTRAINT [FK_User_Foll_2] FOREIGN KEY (followerId) REFERENCES UserProfile(usrId),
) 

CREATE NONCLUSTERED INDEX [IX_AccountIndexByFollowId] 
ON Follow (folwId ASC, usrId ASC)


PRINT N'Table Follow created.'
GO

PRINT N'Done'