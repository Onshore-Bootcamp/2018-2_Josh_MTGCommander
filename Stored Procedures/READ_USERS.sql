USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[READ_USERS]    Script Date: 5/21/2018 10:34:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[READ_USERS]

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

END