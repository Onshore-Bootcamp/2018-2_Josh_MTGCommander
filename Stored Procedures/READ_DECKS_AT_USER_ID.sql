USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[READ_DECKS_AT_USER_ID]    Script Date: 5/21/2018 10:30:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[READ_DECKS_AT_USER_ID]

@UserID bigint

AS
BEGIN

	SELECT	DeckID,
			UserID,
			DeckName,
			CommanderName,
			DeckColors,
			DeckArchetype
	FROM	Decks
	WHERE	UserID = @UserID 	
				
END