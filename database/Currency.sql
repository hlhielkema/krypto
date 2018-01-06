USE [BluefoxData]
GO

/****** Object:  Table [dbo].[Currency]    Script Date: 6-1-2018 15:18:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Currency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExternalId] [uniqueidentifier] NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
	[ShortCode] [nvarchar](5) NOT NULL,
	[Deleted] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

