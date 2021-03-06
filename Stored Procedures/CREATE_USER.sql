USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[CREATE_USER]    Script Date: 5/21/2018 10:21:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[CREATE_USER]

@Username varchar(30),
@Password varchar(30),
@FirstName varchar(50),
@LastName varchar(50),
@EmailAddress varchar(150) = '',
@RoleID int

AS
BEGIN

	INSERT	INTO	Users
					(Username,
					Password,
					FirstName,
					LastName,
					EmailAddress,
					RoleID)
	VALUES			(@Username,
					@Password,
					@FirstName,
					@LastName,
					@EmailAddress,
					@RoleID)

END