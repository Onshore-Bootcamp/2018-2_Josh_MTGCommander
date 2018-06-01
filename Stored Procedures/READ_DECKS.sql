USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[READ_DECKS]    Script Date: 5/21/2018 10:29:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[READ_DECKS]

AS
BEGIN

	SELECT	DeckID,
			UserID,
			DeckName,
			CommanderName,
			DeckColors,
			DeckArchetype
	FROM	Decks					

END