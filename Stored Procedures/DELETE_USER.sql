USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[DELETE_USER]    Script Date: 5/21/2018 10:25:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[DELETE_USER]

@UserID bigint

AS
BEGIN

	DELETE	FROM	Users
			WHERE	UserID = @UserID

END