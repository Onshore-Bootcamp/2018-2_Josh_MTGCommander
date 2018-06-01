USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[READ_CARD_AT_ID]    Script Date: 5/21/2018 10:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[READ_CARD_AT_ID]

@CardID bigint

AS
BEGIN

	SELECT	CardID,
			CardName,
			ManaCost,
			CardType,
			ColorIdentity,
			Abilities,
			CardStats
	FROM	Cards
	WHERE	CardID = @CardID 	
				
END