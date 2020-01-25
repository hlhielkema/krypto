USE [BluefoxData]
GO

/****** Object:  Table [dbo].[Account]    Script Date: 25-1-2020 15:41:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](70) NOT NULL,
	[EmailAddress] [nvarchar](200) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Role] [int] NOT NULL,
	[LastLogon] [datetime] NULL,
 CONSTRAINT [PK_AccountTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

