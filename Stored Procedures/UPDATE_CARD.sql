USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[UPDATE_CARD]    Script Date: 5/21/2018 10:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[UPDATE_CARD]

@CardID bigint,
@CardName varchar(50),
@ManaCost smallint = 0,
@CardType varchar(50),
@ColorIdentity varchar(20) = '',
@Abilities varchar(700) = '',
@CardStats varchar(10) = ''

AS
BEGIN

	UPDATE	Cards
	SET		CardName = @CardName,
			ManaCost = @ManaCost,
			CardType = @CardType,
			ColorIdentity = @ColorIdentity,
			Abilities = @Abilities,
			CardStats = @CardStats
	WHERE	CardID = @CardID 	
				
END