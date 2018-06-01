USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[UPDATE_ROLE]    Script Date: 5/21/2018 10:36:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[UPDATE_ROLE]

@RoleID int,
@RoleName varchar(50),
@RoleDescription varchar(200)

AS
BEGIN

	UPDATE	Roles
	SET		RoleName = @RoleName,
			RoleDescription = @RoleDescription
	WHERE	RoleID = @RoleID 	
				
END