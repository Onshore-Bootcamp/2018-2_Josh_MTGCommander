USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[READ_DECK_AT_DECK_ID]    Script Date: 5/21/2018 10:28:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[READ_DECK_AT_DECK_ID]

@DeckID bigint

AS
BEGIN

	SELECT	DeckID,
			UserID,
			DeckName,
			CommanderName,
			DeckColors,
			DeckArchetype
	FROM	Decks
	WHERE	DeckID = @DeckID 	
				
END