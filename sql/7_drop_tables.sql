USE [Project]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectXML]') AND type in (N'U'))
DROP TABLE [dbo].[ProjectXML]
GO


