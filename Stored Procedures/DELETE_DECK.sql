USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[DELETE_DECK]    Script Date: 5/21/2018 10:24:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[DELETE_DECK]

@DeckID bigint

AS
BEGIN

	DELETE	FROM Decks
		   WHERE DeckID = @DeckID

END