USE [BluefoxData]
GO

/****** Object:  Table [dbo].[Currency.ValueRates]    Script Date: 25-1-2020 15:41:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Currency.ValueRates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Currency] [int] NOT NULL,
	[Rank] [int] NOT NULL,
	[PriceUsd] [decimal](18, 0) NOT NULL,
	[VolumeUsd24h] [decimal](18, 0) NOT NULL,
	[MarketCapUsd] [decimal](18, 0) NOT NULL,
	[AvailableSupply] [decimal](18, 0) NOT NULL,
	[TotalSupply] [decimal](18, 0) NOT NULL,
	[MaxSupply] [decimal](18, 0) NULL,
	[PercentChange1h] [float] NULL,
	[PercentChange24h] [float] NULL,
	[LastUpdated] [datetime] NOT NULL,
	[PriceEur] [decimal](18, 0) NOT NULL,
	[VolumeEur24h] [decimal](18, 0) NOT NULL,
	[MarketCapEur] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Currency.ValueRates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

