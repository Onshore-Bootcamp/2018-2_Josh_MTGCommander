USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[READ_USER_AT_USERNAME]    Script Date: 5/21/2018 10:33:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[READ_USER_AT_USERNAME]

@Username varchar(30)

AS
BEGIN

	SELECT	UserID,
			Username,
			Password,
			FirstName,
			LastName,
			EmailAddress,
			RoleID
	FROM	Users
	WHERE	Username = @Username	
				
END