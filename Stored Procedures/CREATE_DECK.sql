USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[CREATE_DECK]    Script Date: 5/21/2018 10:20:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[CREATE_DECK]

@UserID bigint,
@DeckName varchar(50),
@CommanderName varchar(30),
@DeckColors varchar(20) = '',
@DeckArchetype varchar(100) = ''

AS
BEGIN

	INSERT	INTO	Decks 
					(UserID,
					DeckName,
					CommanderName,
					DeckColors,
					DeckArchetype)
	VALUES			(@UserID,
					@DeckName,
					@CommanderName,
					@DeckColors,
					@DeckArchetype)

END