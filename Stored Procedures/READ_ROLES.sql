USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[READ_ROLES]    Script Date: 5/21/2018 10:32:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[READ_ROLES]

AS
BEGIN

	SELECT	RoleID,
			RoleName,
			RoleDescription
	FROM	Roles 					

END