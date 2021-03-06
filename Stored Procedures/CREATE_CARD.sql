USE [MTG Commander Deck Builder]
GO
/****** Object:  StoredProcedure [dbo].[CREATE_CARD]    Script Date: 5/21/2018 10:17:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[CREATE_CARD]

@CardName varchar(50),
@ManaCost smallint = 0,
@CardType varchar(50),
@ColorIdentity varchar(20) = '',
@Abilities varchar(700) = '',
@CardStats varchar(10) = ''

AS
BEGIN

	INSERT	INTO	Cards
					(CardName,
					ManaCost,
					CardType,
					ColorIdentity,
					Abilities,
					CardStats)
	VALUES			(@CardName,
					@ManaCost,
					@CardType,
					@ColorIdentity,
					@Abilities,
					@CardStats)
END