USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[UPDATE_DECK]    Script Date: 5/21/2018 10:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[UPDATE_DECK]

@DeckID bigint,
@UserID bigint,
@DeckName varchar(50),
@CommanderName varchar(30),
@DeckColors varchar(20) = '',
@DeckArchetype varchar(100) = ''

AS
BEGIN

	UPDATE	Decks
	SET		DeckName = @DeckName,
			CommanderName = @CommanderName,
			DeckColors = @DeckColors,
			DeckArchetype = @DeckArchetype
	WHERE	DeckID = @DeckID 	
				
END