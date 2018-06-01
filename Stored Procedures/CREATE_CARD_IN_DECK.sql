USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[CREATE_CARD_IN_DECK]    Script Date: 5/21/2018 10:19:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[CREATE_CARD_IN_DECK]

@CardID bigint,
@DeckID bigint

AS
BEGIN

	INSERT	INTO	CardsInDecks
					(CardID,
					DeckID)
	VALUES			(@CardID,
					@DeckID)
END