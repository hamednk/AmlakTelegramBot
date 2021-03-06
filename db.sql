USE [master]
GO
/****** Object:  Database [AmlakSazBot]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
CREATE DATABASE [AmlakSazBot] ON  PRIMARY 
( NAME = N'AmlakSazBot', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\AmlakSazBot.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AmlakSazBot_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\AmlakSazBot_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AmlakSazBot].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AmlakSazBot] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AmlakSazBot] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AmlakSazBot] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AmlakSazBot] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AmlakSazBot] SET ARITHABORT OFF 
GO
ALTER DATABASE [AmlakSazBot] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AmlakSazBot] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AmlakSazBot] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AmlakSazBot] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AmlakSazBot] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AmlakSazBot] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AmlakSazBot] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AmlakSazBot] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AmlakSazBot] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AmlakSazBot] SET  ENABLE_BROKER 
GO
ALTER DATABASE [AmlakSazBot] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AmlakSazBot] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AmlakSazBot] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AmlakSazBot] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AmlakSazBot] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AmlakSazBot] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AmlakSazBot] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AmlakSazBot] SET RECOVERY FULL 
GO
ALTER DATABASE [AmlakSazBot] SET  MULTI_USER 
GO
ALTER DATABASE [AmlakSazBot] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AmlakSazBot] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'AmlakSazBot', N'ON'
GO
USE [AmlakSazBot]
GO
/****** Object:  Table [dbo].[Bongah]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bongah](
	[BongahId] [int] IDENTITY(1,1) NOT NULL,
	[BongahName] [nvarchar](80) NULL,
	[CertImg] [nvarchar](50) NULL,
 CONSTRAINT [PK_Bongah] PRIMARY KEY CLUSTERED 
(
	[BongahId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CommandState]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommandState](
	[CommandStateId] [int] IDENTITY(1,1) NOT NULL,
	[TelegramId] [float] NULL,
	[PrimaryCommand] [nvarchar](100) NULL,
	[SubCommand] [nvarchar](100) NULL,
	[State] [int] NULL,
	[PM] [nvarchar](max) NULL,
 CONSTRAINT [PK_CommandState] PRIMARY KEY CLUSTERED 
(
	[CommandStateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EstateCats]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstateCats](
	[EstateCatId] [int] IDENTITY(1,1) NOT NULL,
	[CatName] [nvarchar](50) NULL,
	[Sale] [bit] NULL,
	[Mortgage-Rent] [bit] NULL,
 CONSTRAINT [PK_EstateCat] PRIMARY KEY CLUSTERED 
(
	[EstateCatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Estates]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estates](
	[EstateId] [int] IDENTITY(1,1) NOT NULL,
	[EstateCatId] [int] NOT NULL,
	[DateTimeCreated] [datetime] NULL,
	[UserId] [int] NOT NULL,
	[FullnameOwner] [nvarchar](80) NULL,
	[MobileNo] [nvarchar](13) NULL,
	[Region] [nvarchar](50) NULL,
	[Area] [int] NULL,
	[Price1] [int] NULL,
	[Price2] [int] NULL,
	[StatusDocumentId] [int] NOT NULL,
	[Descr] [nvarchar](max) NULL,
	[EstateImg] [nvarchar](max) NULL,
	[Spec1] [nvarchar](50) NULL,
	[Spec2] [nvarchar](50) NULL,
	[Spec3] [nvarchar](50) NULL,
	[Spec4] [nvarchar](50) NULL,
	[Spec5] [nvarchar](50) NULL,
	[Spec6] [nvarchar](50) NULL,
	[State] [int] NULL,
	[TOT] [nvarchar](50) NULL,
 CONSTRAINT [PK_Estate] PRIMARY KEY CLUSTERED 
(
	[EstateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StatusDocuments]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusDocuments](
	[StatusDocumentId] [int] NOT NULL,
	[StatusDocument] [nvarchar](80) NULL,
 CONSTRAINT [PK_StatusDocuments] PRIMARY KEY CLUSTERED 
(
	[StatusDocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[TelegramUserId] [numeric](18, 0) NULL,
	[Fullname] [nvarchar](90) NULL,
	[Password] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[DateTimeCreated] [datetime] NULL,
	[Active] [bit] NULL,
	[UserTypeId] [int] NOT NULL,
	[Address] [nvarchar](max) NULL,
	[MobileNo] [nvarchar](11) NULL,
	[BongahId] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserTypes]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTypes](
	[UserTypeId] [int] IDENTITY(1,1) NOT NULL,
	[UserType] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserTypes] PRIMARY KEY CLUSTERED 
(
	[UserTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Bongah] ON 

INSERT [dbo].[Bongah] ([BongahId], [BongahName], [CertImg]) VALUES (1, N'حامد', N'12.jpg')
SET IDENTITY_INSERT [dbo].[Bongah] OFF
SET IDENTITY_INSERT [dbo].[CommandState] ON 

INSERT [dbo].[CommandState] ([CommandStateId], [TelegramId], [PrimaryCommand], [SubCommand], [State], [PM]) VALUES (1085, 86357977, N'فروش', N'آپارتمان', 3, N'')
INSERT [dbo].[CommandState] ([CommandStateId], [TelegramId], [PrimaryCommand], [SubCommand], [State], [PM]) VALUES (1091, 87549952, N'فروش', N'آپارتمان', 2, N'')
SET IDENTITY_INSERT [dbo].[CommandState] OFF
SET IDENTITY_INSERT [dbo].[EstateCats] ON 

INSERT [dbo].[EstateCats] ([EstateCatId], [CatName], [Sale], [Mortgage-Rent]) VALUES (1, N'ویلایی', 1, 1)
INSERT [dbo].[EstateCats] ([EstateCatId], [CatName], [Sale], [Mortgage-Rent]) VALUES (2, N'آپارتمان', 1, 1)
INSERT [dbo].[EstateCats] ([EstateCatId], [CatName], [Sale], [Mortgage-Rent]) VALUES (3, N'پیش فروش', 1, 0)
INSERT [dbo].[EstateCats] ([EstateCatId], [CatName], [Sale], [Mortgage-Rent]) VALUES (4, N'زمین', 1, 1)
INSERT [dbo].[EstateCats] ([EstateCatId], [CatName], [Sale], [Mortgage-Rent]) VALUES (5, N'کارگاه/کارخانه/انبار', 1, 1)
INSERT [dbo].[EstateCats] ([EstateCatId], [CatName], [Sale], [Mortgage-Rent]) VALUES (6, N'تجاری/اداری', 1, 1)
INSERT [dbo].[EstateCats] ([EstateCatId], [CatName], [Sale], [Mortgage-Rent]) VALUES (7, N'باغ/کشاورزی', 1, 1)
SET IDENTITY_INSERT [dbo].[EstateCats] OFF
SET IDENTITY_INSERT [dbo].[Estates] ON 

INSERT [dbo].[Estates] ([EstateId], [EstateCatId], [DateTimeCreated], [UserId], [FullnameOwner], [MobileNo], [Region], [Area], [Price1], [Price2], [StatusDocumentId], [Descr], [EstateImg], [Spec1], [Spec2], [Spec3], [Spec4], [Spec5], [Spec6], [State], [TOT]) VALUES (7, 2, CAST(N'2017-07-15 02:41:04.393' AS DateTime), 2, N'یها', N'232', N'پردیسان', 200, 150, 11, 7, N'تازه رنگ شده', N'AgADAgADFKgxGxiASUuiD9Clc_4Eie0SSw0ABFBJtKZGhGofci0LAAEC', N'4', N'1', N'فلزی', N'2', N'13', N'-', 1, N'فروش')
INSERT [dbo].[Estates] ([EstateId], [EstateCatId], [DateTimeCreated], [UserId], [FullnameOwner], [MobileNo], [Region], [Area], [Price1], [Price2], [StatusDocumentId], [Descr], [EstateImg], [Spec1], [Spec2], [Spec3], [Spec4], [Spec5], [Spec6], [State], [TOT]) VALUES (8, 2, CAST(N'2017-07-15 21:06:21.130' AS DateTime), 4, N'این', N'45ه88', N'سلام', 104, 165, 25, 3, N'در مورد من', N'AgADBAADhKgxGwGyUVPb-dU9cHu6f9kevRkABMC-f_sto3aqdEMDAAEC', N'4', N'3', N'بتون', N'2', N'4', N'-', 1, N'فروش')
INSERT [dbo].[Estates] ([EstateId], [EstateCatId], [DateTimeCreated], [UserId], [FullnameOwner], [MobileNo], [Region], [Area], [Price1], [Price2], [StatusDocumentId], [Descr], [EstateImg], [Spec1], [Spec2], [Spec3], [Spec4], [Spec5], [Spec6], [State], [TOT]) VALUES (9, 2, CAST(N'2017-07-25 14:20:40.143' AS DateTime), 2, N'محمد رضا کریمی', N'0914265446', N'پردیسان', 130, 155, 20, 6, N'002', N'AgADAgAD0KcxG655uEshpSuMHS82k6oTSw0ABM81-w1Y9Ag2l9kLAAEC', N'9', N'1', N'بتون', N'3', N'10سال', N'-', 1, N'فروش')
INSERT [dbo].[Estates] ([EstateId], [EstateCatId], [DateTimeCreated], [UserId], [FullnameOwner], [MobileNo], [Region], [Area], [Price1], [Price2], [StatusDocumentId], [Descr], [EstateImg], [Spec1], [Spec2], [Spec3], [Spec4], [Spec5], [Spec6], [State], [TOT]) VALUES (10, 2, CAST(N'2017-08-16 15:06:30.570' AS DateTime), 2, N'رضا میری', N'091299999', N'پردیسان', 200, 35, 20, 3, N'ندارد', N'AgADAgADPagxGzKOoUjmDxnzlfYsl-AxSw0ABCHqpdvKw9Pu1p4NAAEC', N'4', N'2', N'فلزی', N'4', N'10سال', N'-', 1, N'فروش')
INSERT [dbo].[Estates] ([EstateId], [EstateCatId], [DateTimeCreated], [UserId], [FullnameOwner], [MobileNo], [Region], [Area], [Price1], [Price2], [StatusDocumentId], [Descr], [EstateImg], [Spec1], [Spec2], [Spec3], [Spec4], [Spec5], [Spec6], [State], [TOT]) VALUES (11, 2, CAST(N'2017-09-01 20:21:36.490' AS DateTime), 2, N'سعید اتابکی', N'09128888888', N'پردیسان', 200, 125, 20, 3, N'اسانسور دار', N'AgADAgADOagxGzEGUEny1wTAJKo9KHATSw0ABMdJUz6hAAETyboEDwABAg', N'5', N'2', N'فلزی', N'3', N'10', N'-', 1, N'فروش')
SET IDENTITY_INSERT [dbo].[Estates] OFF
INSERT [dbo].[StatusDocuments] ([StatusDocumentId], [StatusDocument]) VALUES (1, N'اوقافی')
INSERT [dbo].[StatusDocuments] ([StatusDocumentId], [StatusDocument]) VALUES (2, N'قرارداد واگذاری سازمان مسکن')
INSERT [dbo].[StatusDocuments] ([StatusDocumentId], [StatusDocument]) VALUES (3, N'شش دانگ')
INSERT [dbo].[StatusDocuments] ([StatusDocumentId], [StatusDocument]) VALUES (4, N'مشاع')
INSERT [dbo].[StatusDocuments] ([StatusDocumentId], [StatusDocument]) VALUES (5, N'شورایی')
INSERT [dbo].[StatusDocuments] ([StatusDocumentId], [StatusDocument]) VALUES (6, N'سرقفلی')
INSERT [dbo].[StatusDocuments] ([StatusDocumentId], [StatusDocument]) VALUES (7, N'قولنامه ای')
INSERT [dbo].[StatusDocuments] ([StatusDocumentId], [StatusDocument]) VALUES (8, N'وکالتی')
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [TelegramUserId], [Fullname], [Password], [Email], [DateTimeCreated], [Active], [UserTypeId], [Address], [MobileNo], [BongahId]) VALUES (2, CAST(87549952 AS Numeric(18, 0)), N'حامد نجفی', N'123', N'najafi.h2012@gmail.com', CAST(N'2017-09-12 00:00:00.000' AS DateTime), 1, 1, N'qom', N'09128535116', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
SET IDENTITY_INSERT [dbo].[UserTypes] ON 

INSERT [dbo].[UserTypes] ([UserTypeId], [UserType]) VALUES (1, N'مدیر سیستم')
INSERT [dbo].[UserTypes] ([UserTypeId], [UserType]) VALUES (2, N'کاربر بنگاه')
SET IDENTITY_INSERT [dbo].[UserTypes] OFF
ALTER TABLE [dbo].[Estates]  WITH CHECK ADD  CONSTRAINT [FK_Estates_EstateCats] FOREIGN KEY([EstateCatId])
REFERENCES [dbo].[EstateCats] ([EstateCatId])
GO
ALTER TABLE [dbo].[Estates] CHECK CONSTRAINT [FK_Estates_EstateCats]
GO
ALTER TABLE [dbo].[Estates]  WITH CHECK ADD  CONSTRAINT [FK_Estates_StatusDocuments] FOREIGN KEY([StatusDocumentId])
REFERENCES [dbo].[StatusDocuments] ([StatusDocumentId])
GO
ALTER TABLE [dbo].[Estates] CHECK CONSTRAINT [FK_Estates_StatusDocuments]
GO
ALTER TABLE [dbo].[Estates]  WITH CHECK ADD  CONSTRAINT [FK_Estates_Users1] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Estates] CHECK CONSTRAINT [FK_Estates_Users1]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserTypes] FOREIGN KEY([UserTypeId])
REFERENCES [dbo].[UserTypes] ([UserTypeId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserTypes]
GO
/****** Object:  StoredProcedure [dbo].[Delete_CommandState]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Delete_CommandState]
	@TelegramId bigint
AS
BEGIN
	SET NOCOUNT ON;
	Delete [dbo].[CommandState] Where
		[TelegramId]=@TelegramId 
END


GO
/****** Object:  StoredProcedure [dbo].[Delete_Estates]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Delete_Estates]
	@EstateId int
AS
BEGIN
	SET NOCOUNT ON;
	Delete [dbo].[Estates] Where
		[EstateId]=@EstateId 
END

GO
/****** Object:  StoredProcedure [dbo].[Insert_CommandState]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insert_CommandState]
	@TelegramId bigint
AS
BEGIN
	SET NOCOUNT ON;
	Insert Into [dbo].[CommandState]
		([TelegramId])
	Values
		(@TelegramId)
END


GO
/****** Object:  StoredProcedure [dbo].[Insert_Estates]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insert_Estates]
	@EstateCatId int,
	@DateTimeCreated datetime,
	@UserId int,
	@FullnameOwner nvarchar(80),
	@MobileNo nvarchar(13),
	@Region nvarchar(50),
	@Area int,
	@Price1 int,
	@Price2 int,
	@StatusDocumentId int,
	@Descr nvarchar(MAX),
	@EstateImg nvarchar(MAX),
	@Spec1 nvarchar(50),
	@Spec2 nvarchar(50),
	@Spec3 nvarchar(50),
	@Spec4 nvarchar(50),
	@Spec5 nvarchar(50),
	@Spec6 nvarchar(50),
	@State int,
	@TOT nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	Insert Into [dbo].[Estates]
		([EstateCatId],[DateTimeCreated],[UserId],[FullnameOwner],[MobileNo],[Region],[Area],[Price1],[Price2],[StatusDocumentId],[Descr],[EstateImg],[Spec1],[Spec2],[Spec3],[Spec4],[Spec5],[Spec6],[State],[TOT])
	Values
		(@EstateCatId,@DateTimeCreated,@UserId,@FullnameOwner,@MobileNo,@Region,@Area,@Price1,@Price2,@StatusDocumentId,@Descr,@EstateImg,@Spec1,@Spec2,@Spec3,@Spec4,@Spec5,@Spec6,@State,@TOT)
END

GO
/****** Object:  StoredProcedure [dbo].[Select_AnyFieldGetValue]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Select_AnyFieldGetValue]
(
	@TlgId numeric(18,0),
	@StateDoc nvarchar(80),
	@CatName nvarchar(50)
)
AS
BEGIN

 Declare @UserId int
 Declare @StateDocId int
 Declare @EstateCatId int


	set @UserId=(SELECT [UserId] FROM [dbo].[Users]	where  TelegramUserId=@TlgId )
	set @StateDocId=(SELECT [StatusDocumentId] FROM [dbo].[StatusDocuments] where [StatusDocument]=@StateDoc)
	set @EstateCatId=(SELECT [EstateCatId] FROM [dbo].[EstateCats] where [CatName]=@CatName)
			
	select (select top 1 @UserId from Users) as UserId, (select top 1 @StateDocId from StatusDocuments) as StatusDocumentsId ,(select top 1 @EstateCatId from EstateCats) as EstateCatsId

END



GO
/****** Object:  StoredProcedure [dbo].[Select_CommandState_GetTlgId]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Select_CommandState_GetTlgId]
@TelegramId bigint
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [CommandStateId],[TelegramId],[PrimaryCommand],[SubCommand],[State],[PM] From [dbo].[CommandState]
	where telegramid=@TelegramId
END


GO
/****** Object:  StoredProcedure [dbo].[Select_EstateCats]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[Select_EstateCats]
@Value bit
AS
BEGIN
	SET NOCOUNT ON;

	if(@Value = 1)
		begin
			SELECT [EstateCatId],[CatName],[Sale],[Mortgage-Rent] From [dbo].[EstateCats]
		end
	else if (@Value=0) 
		begin
			SELECT [EstateCatId],[CatName],[Sale],[Mortgage-Rent] From [dbo].[EstateCats]
				where [Mortgage-Rent]!='false'
		end


END

GO
/****** Object:  StoredProcedure [dbo].[Select_EstateCats_GetCat]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Select_EstateCats_GetCat]
@CatName nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	 
			SELECT [EstateCatId],[CatName],[Sale],[Mortgage-Rent] From [dbo].[EstateCats]
			where CatName like @CatName
END

GO
/****** Object:  StoredProcedure [dbo].[Select_Estates]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Select_Estates]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [EstateId],[EstateCatId],[DateTimeCreated],[UserId],[FullnameOwner],[MobileNo],[Region],[Area],[Price1],[Price2],[StatusDocumentId],[Descr],[EstateImg],[Spec1],[Spec2],[Spec3],[Spec4],[Spec5],[Spec6],[State],[TOT] From [dbo].[Estates]
END

GO
/****** Object:  StoredProcedure [dbo].[Select_Estates_GetUserId]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Select_Estates_GetUserId]
(
	@TelegramUserId numeric(18,0)
)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  top 1    dbo.Estates.EstateId, dbo.EstateCats.CatName, dbo.Estates.DateTimeCreated, dbo.Users.Fullname, dbo.Users.TelegramUserId, dbo.Estates.FullnameOwner, 
                         dbo.Estates.MobileNo, dbo.Estates.Region, dbo.Estates.Area, dbo.Estates.Price1, dbo.Estates.Price2, dbo.StatusDocuments.StatusDocument, 
                         dbo.Estates.Descr, dbo.Estates.EstateImg, dbo.Estates.Spec1, dbo.Estates.Spec2, dbo.Estates.Spec3, dbo.Estates.Spec4, dbo.Estates.Spec5, 
                         dbo.Estates.Spec6, dbo.Estates.TOT, dbo.Estates.State, dbo.Estates.EstateCatId, dbo.Users.MobileNo AS MobileBongah
FROM            dbo.Estates INNER JOIN
                         dbo.StatusDocuments ON dbo.Estates.StatusDocumentId = dbo.StatusDocuments.StatusDocumentId INNER JOIN
                         dbo.EstateCats ON dbo.Estates.EstateCatId = dbo.EstateCats.EstateCatId INNER JOIN
                         dbo.Users ON dbo.Estates.UserId = dbo.Users.UserId
	where TelegramUserId=@TelegramUserId order by [EstateId] desc
END
 
GO
/****** Object:  StoredProcedure [dbo].[Select_StatusDocument]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[Select_StatusDocument]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [StatusDocumentId]
      ,[StatusDocument]
  FROM [dbo].[StatusDocuments]
END

GO
/****** Object:  StoredProcedure [dbo].[Select_Users]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Select_Users]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [UserId],[TelegramUserId],[Fullname],[Password],[Email],[DateTimeCreated],[Active],[UserTypeId],[Address],[MobileNo],[BongahId] From [dbo].[Users]
END

GO
/****** Object:  StoredProcedure [dbo].[Select_Users_Login]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Select_Users_Login]
(
@TelegramUserId numeric(18, 0),
@Password nvarchar(50)
)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [UserId],[TelegramUserId],[Fullname],[Password],[Email],[DateTimeCreated],[Active],[UserTypeId],[Address],[MobileNo],[BongahId] From [dbo].[Users]
	where [TelegramUserId]=@TelegramUserId and [password]=@Password
END

GO
/****** Object:  StoredProcedure [dbo].[Update_CommandState]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Update_CommandState]
	@TelegramId bigint ,
	@PrimaryCommand nvarchar(100),
	@SubCommand nvarchar(100),
	@State int,
	@PM nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	Update [dbo].[CommandState] Set
		 [PrimaryCommand]=@PrimaryCommand, [SubCommand]=@SubCommand,[State]=@State,[PM]=@PM
	Where
		[TelegramId]=@TelegramId 
END


GO
/****** Object:  StoredProcedure [dbo].[Update_Estates]    Script Date: 28/09/2017 11:47:56 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Update_Estates]
	@EstateId int,
	@EstateCatId int,
	@UserId int,
	@FullnameOwner nvarchar(80),
	@MobileNo nvarchar(13),
	@Region nvarchar(50),
	@Area int,
	@Price1 money,
	@Price2 money,
	@StatusDocumentId int,
	@Descr nvarchar(MAX),
	@EstateImg nvarchar(50),
	@Spec1 nvarchar(50),
	@Spec2 nvarchar(50),
	@Spec3 nvarchar(50),
	@Spec4 nvarchar(50),
	@Spec5 nvarchar(50),
	@Spec6 nvarchar(50),
	@State int
AS
BEGIN
	SET NOCOUNT ON;
	Update [dbo].[Estates] Set
		[EstateCatId]=@EstateCatId,[UserId]=@UserId,[FullnameOwner]=@FullnameOwner,[MobileNo]=@MobileNo,[Region]=@Region,[Area]=@Area,[Price1]=@Price1,[Price2]=@Price2,[StatusDocumentId]=@StatusDocumentId,[Descr]=@Descr,[EstateImg]=@EstateImg,[Spec1]=@Spec1,[Spec2]=@Spec2,[Spec3]=@Spec3,[Spec4]=@Spec4,[Spec5]=@Spec5,[Spec6]=@Spec6,[State]=@State
	Where
		[EstateId]=@EstateId 

END


GO
USE [master]
GO
ALTER DATABASE [AmlakSazBot] SET  READ_WRITE 
GO
