USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[READ_CARDS_AT_DECK_ID]    Script Date: 5/21/2018 10:26:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[READ_CARDS_AT_DECK_ID]

@DeckID bigint

AS
BEGIN

	SELECT	Cards.CardID,
			CardName,
			ManaCost,
			CardType,
			ColorIdentity,
			Abilities,
			CardStats
	FROM	Cards
	INNER JOIN CardsInDecks ON Cards.CardID = CardsInDecks.CardID
	WHERE DeckID = @DeckID	
				
END