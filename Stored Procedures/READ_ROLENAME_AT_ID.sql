USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[READ_ROLENAME_AT_ID]    Script Date: 5/21/2018 10:31:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[READ_ROLENAME_AT_ID]

@RoleID int

AS
BEGIN

SELECT	RoleName
FROM	Roles 
WHERE	RoleID = @RoleID					

END