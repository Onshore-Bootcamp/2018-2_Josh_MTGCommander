USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[UPDATE_USER]    Script Date: 5/21/2018 10:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[UPDATE_USER]

@UserID bigint,
@Username varchar(30),
@Password varchar(30),
@FirstName varchar(50),
@LastName varchar(50),
@EmailAddress varchar(150) = '',
@RoleId int

AS
BEGIN

	UPDATE	Users
	SET		Username= @Username,
			Password = @Password,
			FirstName = @FirstName,
			LastName = @LastName,
			EmailAddress = @EmailAddress,
			RoleID = @RoleId
	WHERE	UserID = @UserID 	
				
END