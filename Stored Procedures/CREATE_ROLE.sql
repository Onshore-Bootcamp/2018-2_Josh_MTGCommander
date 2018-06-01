USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[CREATE_ROLE]    Script Date: 5/21/2018 10:20:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[CREATE_ROLE]

@RoleName varchar(50),
@RoleDescription varchar(200)

AS
BEGIN

	INSERT	INTO	Roles
					(RoleName,
					RoleDescription)
	VALUES			(@RoleName,
					@RoleDescription)

END