USE [BluefoxData]
GO

/****** Object:  Table [dbo].[Currency.Comment]    Script Date: 6-1-2018 15:18:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Currency.Comment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExternalId] [uniqueidentifier] NOT NULL,
	[Currency] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Vote] [int] NOT NULL,
	[Message] [nvarchar](2000) NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Currency.Comment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

