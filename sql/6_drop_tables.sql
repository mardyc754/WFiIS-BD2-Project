USE [Project]
GO

DROP PROCEDURE dbo.getProjectXML;
GO

DROP PROCEDURE dbo.categoriesTable;
GO

DROP PROCEDURE dbo.productsTable;
GO

DROP PROCEDURE dbo.getProductXMLByID;
GO

/****** Object:  Table [dbo].[ProjectXML]    Script Date: 03.05.2022 20:53:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectXML]') AND type in (N'U'))
DROP TABLE [dbo].[ProjectXML]
GO

/****** Object:  Table [dbo].[Product]    Script Date: 01.05.2022 23:57:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
DROP TABLE [dbo].[Product]
GO

/****** Object:  Table [dbo].[ProductCategory]    Script Date: 01.05.2022 23:57:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductCategory]') AND type in (N'U'))
DROP TABLE [dbo].[ProductCategory]
GO

