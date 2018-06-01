USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[DELETE_CARD_FROM_DECK]    Script Date: 5/21/2018 10:23:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[DELETE_CARD_FROM_DECK]

@CardID bigint,
@DeckID bigint

AS
BEGIN

	DELETE	FROM	CardsInDecks
			WHERE	CardID = @CardID
			AND		DeckID = @DeckID

END