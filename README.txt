# Environment

- .net(asp.net) core 1.0
- entityframeworkcore 1.0(Code first)
- sqlexpress 11.0


# database schema

USE [master]
GO

/****** Object:  Database [Hudson]    Script Date: 2017-02-13 오후 5:05:28 ******/
CREATE DATABASE [Hudson]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Hudson', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Hudson.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Hudson_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Hudson_log.ldf' , SIZE = 1856KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO


USE [Hudson]
GO
/****** Object:  Table [dbo].[Audit]    Script Date: 2017-02-13 오후 5:05:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Audit](
	[AuditNo] [int] IDENTITY(1,1) NOT NULL,
	[MemberNo] [int] NULL,
	[MenuNo] [int] NULL,
	[Action] [tinyint] NULL,
	[Url] [nvarchar](500) NULL,
	[TableName] [nvarchar](100) NULL,
	[Message] [nvarchar](max) NULL,
	[UserIP] [nvarchar](100) NULL,
	[RegDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Audit] PRIMARY KEY CLUSTERED 
(
	[AuditNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Banner]    Script Date: 2017-02-13 오후 5:05:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banner](
	[BannerNo] [int] IDENTITY(1,1) NOT NULL,
	[ServiceNo] [int] NULL,
	[BannerType] [nvarchar](100) NULL,
	[TitleKey] [nvarchar](500) NULL,
	[ContentKey] [nvarchar](500) NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[LinkUrl] [nvarchar](max) NULL,
	[SortOrder] [tinyint] NULL,
	[IsPublic] [bit] NULL,
	[IsOpenNewTab] [bit] NULL,
	[IsEnable] [bit] NULL,
	[IsNew] [bit] NULL,
	[IsQRCode] [bit] NULL,
	[IsDownload] [bit] NULL,
	[HasToken] [bit] NULL,
	[RegDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Banner] PRIMARY KEY CLUSTERED 
(
	[BannerNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Department]    Script Date: 2017-02-13 오후 5:05:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentNo] [int] IDENTITY(1,1) NOT NULL,
	[ParentDepartmentNo] [int] NULL,
	[DepartmentName] [nvarchar](100) NOT NULL,
	[SortOrder] [tinyint] NOT NULL,
	[IsPublic] [bit] NULL,
	[IsDelete] [bit] NULL,
	[Description] [nvarchar](500) NULL,
	[RegDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Language]    Script Date: 2017-02-13 오후 5:05:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[LanguageNo] [int] IDENTITY(1,1) NOT NULL,
	[ServiceNo] [int] NULL,
	[Key] [nvarchar](500) NOT NULL,
	[en] [nvarchar](max) NULL,
	[zh-CN] [nvarchar](max) NULL,
	[zh-TW] [nvarchar](max) NULL,
	[ja] [nvarchar](max) NULL,
	[ru] [nvarchar](max) NULL,
	[RegDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Language] PRIMARY KEY CLUSTERED 
(
	[LanguageNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Member]    Script Date: 2017-02-13 오후 5:05:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[MemberNo] [int] IDENTITY(1,1) NOT NULL,
	[MemberId] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NULL,
	[MemberName] [nvarchar](100) NOT NULL,
	[DepartmentNo] [int] NOT NULL,
	[Email] [nvarchar](100) NULL,
	[LoginDate] [datetime] NULL,
	[ApproveDate] [datetime] NULL,
	[LeaveDate] [datetime] NULL,
	[UserIP] [nvarchar](100) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[RegDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Member] PRIMARY KEY CLUSTERED 
(
	[MemberNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MemberRole]    Script Date: 2017-02-13 오후 5:05:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberRole](
	[RoleNo] [int] IDENTITY(1,1) NOT NULL,
	[MemberNo] [int] NOT NULL,
	[MenuNo] [int] NOT NULL,
	[RegDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.MemberRole] PRIMARY KEY CLUSTERED 
(
	[RoleNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menu]    Script Date: 2017-02-13 오후 5:05:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[MenuNo] [int] IDENTITY(1,1) NOT NULL,
	[ServiceNo] [int] NOT NULL,
	[ParentMenuNo] [int] NULL,
	[MenuName] [nvarchar](100) NULL,
	[MenuUrl] [nvarchar](500) NULL,
	[SortOrder] [tinyint] NULL,
	[IsPublic] [bit] NULL,
	[IsDelete] [bit] NULL,
	[Description] [nvarchar](500) NULL,
	[RegDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Menu] PRIMARY KEY CLUSTERED 
(
	[MenuNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Service]    Script Date: 2017-02-13 오후 5:05:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[ServiceNo] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [nvarchar](100) NULL,
	[ServiceUrl] [nvarchar](500) NULL,
	[IsPublic] [bit] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[RegDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Service] PRIMARY KEY CLUSTERED 
(
	[ServiceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Audit]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Audit_dbo.Member_MemberNo] FOREIGN KEY([MemberNo])
REFERENCES [dbo].[Member] ([MemberNo])
GO
ALTER TABLE [dbo].[Audit] CHECK CONSTRAINT [FK_dbo.Audit_dbo.Member_MemberNo]
GO
ALTER TABLE [dbo].[Audit]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Audit_dbo.Menu_MenuNo] FOREIGN KEY([MenuNo])
REFERENCES [dbo].[Menu] ([MenuNo])
GO
ALTER TABLE [dbo].[Audit] CHECK CONSTRAINT [FK_dbo.Audit_dbo.Menu_MenuNo]
GO
ALTER TABLE [dbo].[Banner]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Banner_dbo.Service_ServiceNo] FOREIGN KEY([ServiceNo])
REFERENCES [dbo].[Service] ([ServiceNo])
GO
ALTER TABLE [dbo].[Banner] CHECK CONSTRAINT [FK_dbo.Banner_dbo.Service_ServiceNo]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Department_dbo.Department_ParentDepartmentNo] FOREIGN KEY([ParentDepartmentNo])
REFERENCES [dbo].[Department] ([DepartmentNo])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_dbo.Department_dbo.Department_ParentDepartmentNo]
GO
ALTER TABLE [dbo].[Language]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Language_dbo.Service_ServiceNo] FOREIGN KEY([ServiceNo])
REFERENCES [dbo].[Service] ([ServiceNo])
GO
ALTER TABLE [dbo].[Language] CHECK CONSTRAINT [FK_dbo.Language_dbo.Service_ServiceNo]
GO
ALTER TABLE [dbo].[Member]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Member_dbo.Department_DepartmentNo] FOREIGN KEY([DepartmentNo])
REFERENCES [dbo].[Department] ([DepartmentNo])
GO
ALTER TABLE [dbo].[Member] CHECK CONSTRAINT [FK_dbo.Member_dbo.Department_DepartmentNo]
GO
ALTER TABLE [dbo].[MemberRole]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MemberRole_dbo.Member_MemberNo] FOREIGN KEY([MemberNo])
REFERENCES [dbo].[Member] ([MemberNo])
GO
ALTER TABLE [dbo].[MemberRole] CHECK CONSTRAINT [FK_dbo.MemberRole_dbo.Member_MemberNo]
GO
ALTER TABLE [dbo].[MemberRole]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MemberRole_dbo.Menu_MenuNo] FOREIGN KEY([MenuNo])
REFERENCES [dbo].[Menu] ([MenuNo])
GO
ALTER TABLE [dbo].[MemberRole] CHECK CONSTRAINT [FK_dbo.MemberRole_dbo.Menu_MenuNo]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Menu_dbo.Menu_ParentMenuNo] FOREIGN KEY([ParentMenuNo])
REFERENCES [dbo].[Menu] ([MenuNo])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_dbo.Menu_dbo.Menu_ParentMenuNo]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Menu_dbo.Service_ServiceNo] FOREIGN KEY([ServiceNo])
REFERENCES [dbo].[Service] ([ServiceNo])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_dbo.Menu_dbo.Service_ServiceNo]
GO

