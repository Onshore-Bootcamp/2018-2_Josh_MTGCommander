USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[DELETE_ROLE]    Script Date: 5/21/2018 10:24:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[DELETE_ROLE]

@RoleID int

AS
BEGIN

	DELETE	FROM Roles
		   WHERE RoleID = @RoleID

END