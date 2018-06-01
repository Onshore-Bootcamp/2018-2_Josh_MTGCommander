USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[DELETE_CARD]    Script Date: 5/21/2018 10:22:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[DELETE_CARD]

@CardID bigint

AS
BEGIN

	DELETE	FROM Cards
		   WHERE CardID = @CardID

END