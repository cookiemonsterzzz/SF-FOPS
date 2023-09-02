USE [FOPS]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 2/9/2023 7:50:19 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [uniqueidentifier] NOT NULL,
	[CustomerName] [varchar](100) NOT NULL,
	[CustomerEmail] [varchar](100) NULL,
	[CustomerContact] [varchar](100) NOT NULL,
	[CustomerLastCreated] [datetime] NULL,
	[CustomerLastUpdated] [datetime] NULL,
	[CustomerIsDeleted] [bit] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerEnquiries]    Script Date: 2/9/2023 7:50:19 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerEnquiries](
	[EnquiriesId] [uniqueidentifier] NOT NULL,
	[EnquiriesSubject] [varchar](1000) NOT NULL,
	[EnquiriesDescription] [varchar](5000) NOT NULL,
	[EnquiriesLastCreated] [datetime] NULL,
	[EnquiriesLastUpdated] [datetime] NULL,
	[EnquiriesIsDeleted] [bit] NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_CustomerEnquiries] PRIMARY KEY CLUSTERED 
(
	[EnquiriesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Foods]    Script Date: 2/9/2023 7:50:19 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Foods](
	[FoodId] [uniqueidentifier] NOT NULL,
	[FoodName] [varchar](100) NOT NULL,
	[FoodDescription] [varchar](5000) NULL,
	[FoodPrice] [decimal](11, 2) NOT NULL,
	[FoodLastCreated] [datetime] NULL,
	[FoodLastUpdated] [datetime] NULL,
	[FoodIsDeleted] [bit] NULL,
 CONSTRAINT [PK_Foods] PRIMARY KEY CLUSTERED 
(
	[FoodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodsCustomization]    Script Date: 2/9/2023 7:50:19 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodsCustomization](
	[FoodCustomizationId] [uniqueidentifier] NOT NULL,
	[FoodCustomizationName] [varchar](100) NOT NULL,
	[FoodCustomizationPrice] [decimal](11, 2) NOT NULL,
	[FoodCustomizationLastCreated] [datetime] NULL,
	[FoodCustomizationLastUpdated] [datetime] NULL,
	[FoodCustomizationIsDeleted] [bit] NULL,
	[FoodId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_FoodsCustomization] PRIMARY KEY CLUSTERED 
(
	[FoodCustomizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomerEnquiries]  WITH CHECK ADD  CONSTRAINT [FK_Customer_CustomerEnquiries] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[CustomerEnquiries] CHECK CONSTRAINT [FK_Customer_CustomerEnquiries]
GO
ALTER TABLE [dbo].[FoodsCustomization]  WITH CHECK ADD  CONSTRAINT [FK_Foods_FoodsCustomization] FOREIGN KEY([FoodId])
REFERENCES [dbo].[Foods] ([FoodId])
GO
ALTER TABLE [dbo].[FoodsCustomization] CHECK CONSTRAINT [FK_Foods_FoodsCustomization]
GO
