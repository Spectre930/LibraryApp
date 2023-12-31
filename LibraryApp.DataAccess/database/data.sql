USE [master]
GO
/****** Object:  Database [LibraryDB]    Script Date: 9/3/2023 10:24:21 PM ******/
CREATE DATABASE [LibraryDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LibraryDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\LibraryDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LibraryDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\LibraryDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [LibraryDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LibraryDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LibraryDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LibraryDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LibraryDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LibraryDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LibraryDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [LibraryDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LibraryDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LibraryDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LibraryDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LibraryDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LibraryDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LibraryDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LibraryDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LibraryDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LibraryDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [LibraryDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LibraryDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LibraryDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LibraryDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LibraryDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LibraryDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [LibraryDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LibraryDB] SET RECOVERY FULL 
GO
ALTER DATABASE [LibraryDB] SET  MULTI_USER 
GO
ALTER DATABASE [LibraryDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LibraryDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LibraryDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LibraryDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LibraryDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LibraryDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'LibraryDB', N'ON'
GO
ALTER DATABASE [LibraryDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [LibraryDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [LibraryDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/3/2023 10:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuthorBooks]    Script Date: 9/3/2023 10:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuthorBooks](
	[BookId] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
 CONSTRAINT [PK_AuthorBooks] PRIMARY KEY CLUSTERED 
(
	[AuthorId] ASC,
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 9/3/2023 10:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[F_Name] [nvarchar](max) NOT NULL,
	[L_Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[DOB] [datetime2](7) NOT NULL,
	[Age] [int] NOT NULL,
 CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 9/3/2023 10:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[GenreId] [int] NULL,
	[Copies] [int] NOT NULL,
	[AvailableCopies] [int] NOT NULL,
	[AuthPrice] [int] NOT NULL,
	[ListedPrice] [int] NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Borrows]    Script Date: 9/3/2023 10:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Borrows](
	[BookId] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
	[BorrowDate] [datetime2](7) NOT NULL,
	[ReturnDate] [datetime2](7) NOT NULL,
	[LateReturnFee] [real] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[returned] [bit] NOT NULL,
 CONSTRAINT [PK_Borrows] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 9/3/2023 10:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[F_Name] [nvarchar](max) NOT NULL,
	[L_Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](450) NOT NULL,
	[DOB] [datetime2](7) NOT NULL,
	[Age] [int] NOT NULL,
	[NumberOfBorrowed] [int] NOT NULL,
	[RolesId] [int] NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 9/3/2023 10:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[F_Name] [nvarchar](max) NOT NULL,
	[L_Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](450) NOT NULL,
	[DOB] [datetime2](7) NOT NULL,
	[Age] [int] NOT NULL,
	[PhoneNumber] [real] NOT NULL,
	[RoleId] [int] NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 9/3/2023 10:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genres](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Genres] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchases]    Script Date: 9/3/2023 10:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchases](
	[ClientId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
	[TotalPrice] [int] NOT NULL,
	[PurchaseDate] [datetime2](7) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Purchases] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 9/3/2023 10:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230801225907_AddToDb', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230802201523_fixBookAuthor', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230803002111_fixPurchases', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230810204317_FixBorrows', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230815222032_addPassword', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230817200438_passwordAndemployeeFix', N'7.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230827001534_fix-Borrows', N'7.0.10')
GO
INSERT [dbo].[AuthorBooks] ([BookId], [AuthorId]) VALUES (5, 1)
INSERT [dbo].[AuthorBooks] ([BookId], [AuthorId]) VALUES (5, 2)
INSERT [dbo].[AuthorBooks] ([BookId], [AuthorId]) VALUES (7, 1)
INSERT [dbo].[AuthorBooks] ([BookId], [AuthorId]) VALUES (10, 1)
INSERT [dbo].[AuthorBooks] ([BookId], [AuthorId]) VALUES (10, 2)
GO
SET IDENTITY_INSERT [dbo].[Authors] ON 

INSERT [dbo].[Authors] ([Id], [F_Name], [L_Name], [Email], [DOB], [Age]) VALUES (1, N'Mark', N'Jefferson', N'mark@gmail.com', CAST(N'1999-08-02T00:42:22.6490000' AS DateTime2), 24)
INSERT [dbo].[Authors] ([Id], [F_Name], [L_Name], [Email], [DOB], [Age]) VALUES (2, N'Thomas', N'Lincoln', N'thomas@gmail.com', CAST(N'2001-08-02T22:11:43.6340000' AS DateTime2), 22)
INSERT [dbo].[Authors] ([Id], [F_Name], [L_Name], [Email], [DOB], [Age]) VALUES (5, N'test', N'ahmad', N'tyugihop', CAST(N'2001-03-22T01:46:00.0000000' AS DateTime2), 22)
INSERT [dbo].[Authors] ([Id], [F_Name], [L_Name], [Email], [DOB], [Age]) VALUES (6, N'test22', N'asfgs', N'2345trfdshgfcv', CAST(N'1997-10-14T03:05:00.0000000' AS DateTime2), 26)
SET IDENTITY_INSERT [dbo].[Authors] OFF
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([Id], [Title], [Description], [GenreId], [Copies], [AvailableCopies], [AuthPrice], [ListedPrice]) VALUES (5, N'The Wizard of Oz', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 1, 17, 16, 100, 200)
INSERT [dbo].[Books] ([Id], [Title], [Description], [GenreId], [Copies], [AvailableCopies], [AuthPrice], [ListedPrice]) VALUES (7, N'WWII', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 2, 11, 11, 60, 125)
INSERT [dbo].[Books] ([Id], [Title], [Description], [GenreId], [Copies], [AvailableCopies], [AuthPrice], [ListedPrice]) VALUES (10, N'The Hobbit', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 1, 9, 8, 70, 95)
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[Borrows] ON 

INSERT [dbo].[Borrows] ([BookId], [ClientId], [BorrowDate], [ReturnDate], [LateReturnFee], [Id], [returned]) VALUES (5, 10, CAST(N'2023-08-24T22:43:25.3247701' AS DateTime2), CAST(N'2023-09-07T22:43:25.3247701' AS DateTime2), 0, 6, 1)
INSERT [dbo].[Borrows] ([BookId], [ClientId], [BorrowDate], [ReturnDate], [LateReturnFee], [Id], [returned]) VALUES (7, 10, CAST(N'2023-08-24T23:03:12.8301564' AS DateTime2), CAST(N'2023-09-07T23:03:12.8301564' AS DateTime2), 0, 7, 1)
INSERT [dbo].[Borrows] ([BookId], [ClientId], [BorrowDate], [ReturnDate], [LateReturnFee], [Id], [returned]) VALUES (10, 10, CAST(N'2023-08-24T23:04:17.7032258' AS DateTime2), CAST(N'2023-08-27T23:17:03.2126030' AS DateTime2), 0, 8, 1)
INSERT [dbo].[Borrows] ([BookId], [ClientId], [BorrowDate], [ReturnDate], [LateReturnFee], [Id], [returned]) VALUES (10, 15, CAST(N'2023-08-26T19:10:47.2437911' AS DateTime2), CAST(N'2023-09-09T19:10:47.2437911' AS DateTime2), 0, 9, 0)
INSERT [dbo].[Borrows] ([BookId], [ClientId], [BorrowDate], [ReturnDate], [LateReturnFee], [Id], [returned]) VALUES (5, 10, CAST(N'2023-08-27T22:25:29.6844247' AS DateTime2), CAST(N'2023-09-10T22:25:29.6844247' AS DateTime2), 0, 10, 0)
SET IDENTITY_INSERT [dbo].[Borrows] OFF
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([Id], [F_Name], [L_Name], [Email], [DOB], [Age], [NumberOfBorrowed], [RolesId], [PasswordHash], [PasswordSalt]) VALUES (10, N'jawad', N'ahmad', N'123@gmail.com', CAST(N'2000-08-17T20:09:25.1020000' AS DateTime2), 23, 1, 2, 0x638FA7D06D565C935F8CA82D6B75FD9074313CEBC64931BA25647111C4B341AA7BED34D185BB512B879F9EEDF25576FB35CF1E495464D6A187C82C12A001326E, 0x43EF6A0E68E83C7F79F49F20364C10D35249CE3AD3FEF2309AD9ECC5796C62F6B11679EA246ACD76F5A18E76B89AEE947114E07157C464F9EFEDD3CC01736B2B734FAC9E9F36DEA65457922FD7097012204D2FECF80524D2F322FE5E115616A231EB3CB248971625D1AAC439A8C56DD233C64F5E9DBCC88E54A2E3656065DD7E)
INSERT [dbo].[Clients] ([Id], [F_Name], [L_Name], [Email], [DOB], [Age], [NumberOfBorrowed], [RolesId], [PasswordHash], [PasswordSalt]) VALUES (11, N'ahmad', N'ahmad', N'ahmad@gmail.com', CAST(N'2001-02-15T22:31:00.0000000' AS DateTime2), 22, 0, 2, 0x5298FD8F5DE5577D628C00C098AF5A9557E7D3AF03CCCCC101DCDBB4F18CB6EE84FEBFF4DCF4EE53E0AEE3D1F099AEE7F7EFCCAFE0372E49DE9714A5547049AA, 0x07F5079F49F068506EEA868DB7FF32B4175841E403B362EA2D25E66B0BB1724E2912C10A394BAF75A3E70E2525B314452A8008F13CCDAEF8B31C3FAEA7DB0E4A930A4F4AB3CC5C92E23D42A7BF5937B8D30ADD5354E94CB9B73B7599A79A834D6BB310C2D609AEB542717C1BBD9AB25DD20587BEAA6E2EA1CE1EC528EA5712E8)
INSERT [dbo].[Clients] ([Id], [F_Name], [L_Name], [Email], [DOB], [Age], [NumberOfBorrowed], [RolesId], [PasswordHash], [PasswordSalt]) VALUES (12, N'user1', N'user1111111', N'user1@gmail.com', CAST(N'1999-06-07T02:38:00.0000000' AS DateTime2), 24, 0, 2, 0x7C7016AB836BB4CE0C96E5087D25BA209AC87D24533EC396BEE38F3BEFEE914F3C2B0EC5B5014A5F95C04784981BCC968FD6F34DB91F43E516DA654996167470, 0x0B016CC30B585CE4DBE25D53ED96F031CDFE674E56E78AE50B3A0F54FD42BC3324F73E1AB751CD918BA99E1AB73BFD795F163D2377807E1FB5AA7086D889E9710B2F7F0D9EEBBF4D3B6F5E3CA2E0FDBE2378B97154664A777EB1602DCD28A4FDA128F95DBD67592C7343F0E13FD01AEC10814D984306D52E74A103085803BA1A)
INSERT [dbo].[Clients] ([Id], [F_Name], [L_Name], [Email], [DOB], [Age], [NumberOfBorrowed], [RolesId], [PasswordHash], [PasswordSalt]) VALUES (13, N'sara', N'bravo', N'sara@gmail.com', CAST(N'1998-06-02T19:32:00.0000000' AS DateTime2), 25, 0, 2, 0x75311C18BC27EA530C88FABCD9C76550385EA0EC34ED0C57AD7AE6F1CBDD4140CF49C45EBAB5D47AB1F9022434427905F8CD64BF838836269B1C4571082DDDA4, 0x1490451CEA5273E9163A3C16A3268963BD281B1DE89B00A6BB2F11430149CE328E126D93CB0EFA557F63938727181FA0D3E7A3A952E6CB90AC86E214F85037AA076ACAD9F569A7E1445D1F721D10074C58AC5EBA637ABFFDADBA46550FA362D234E1C6909F7E5DF670B847669528AD3A73A9FA8BEF6CC720D756181767741B0E)
INSERT [dbo].[Clients] ([Id], [F_Name], [L_Name], [Email], [DOB], [Age], [NumberOfBorrowed], [RolesId], [PasswordHash], [PasswordSalt]) VALUES (14, N'jamil', N'khansa', N'jamil@gmail.com', CAST(N'1998-06-09T19:34:00.0000000' AS DateTime2), 25, 0, 2, 0x28C8B29BE492CF517BFF035E9B824260CAD1669866586D3558BD1F014F6B7F25AF21070186FADE4752407000F073923140DC62952A81360694656BA4F32956F8, 0x86793AAC5E1416348ABBE91F6195B19B6057574919C81E7403080FA94DD90FB39CF7AC6F16554DB7D42AC88FB8793BF99185611185C68B789554615179142456871DDA4F373B89EA4C69E5FBBAC92218A57AA664502534151635764BE8684A019D3E0C8BCF4ABC81AD02A1C3B35BD5B5BA20A8E151755F730CAA0BFFD9307D8C)
INSERT [dbo].[Clients] ([Id], [F_Name], [L_Name], [Email], [DOB], [Age], [NumberOfBorrowed], [RolesId], [PasswordHash], [PasswordSalt]) VALUES (15, N'Jack', N'Daniels', N'jack@gmail.com', CAST(N'1993-06-08T19:09:00.0000000' AS DateTime2), 30, 1, 2, 0x07BCD11EC0F13772D9FDE0C1142820F8746BA52A25E1A4688B97EFD685169C67B834ACE6004C1E501B766E82EF11358CD4CEE09C0728CCB8ED68809A4B60E6EB, 0x2704B818076921D8F0DBD9AA853CE10F60AA4F7EDFC174B4E1251E0B6DD8E99C0B0E71708709279D9241240C938182EB49C3D4C2FF8E77C8B70B4D7D2C48309CE16E45EAC36B5016ABB6E23620D47877B2912AAB5EEDAD280EF3E50FD465ED035F5A7B1EF8AD7D86F7B336FBDF6BC649CE93E7458ADB802C5240930CE008F406)
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([Id], [F_Name], [L_Name], [Email], [DOB], [Age], [PhoneNumber], [RoleId], [PasswordHash], [PasswordSalt]) VALUES (9, N'ahmad', N'samir', N'321@gmail.com', CAST(N'1998-08-17T20:14:09.8950000' AS DateTime2), 25, 13245364, 1, 0x1C4B399A4B9FB09CCA6E7830A64D1EA11F01EFA8FE3030229A63C97949FA93938C8EC7870862E6E4014A82B2D67AFEAE9DFA8251D86F5E25BCDC34E9B9E0E6D1, 0x9751D8E09025FD946121320274E1F927062A450308D1325E9952DA42D59FF02FCA0B322E67DF7F8E00BB22381D096D61987E731101501E30E32292A3B6CFFE31982052921E4B0B6F83F10AA64FEE915B7A3863E3B66A4AD8CB593114C3D5CCD833DB31FBE4A79D546782174BED207925F33FD84FE11CCF09744605EC3941199F)
INSERT [dbo].[Employees] ([Id], [F_Name], [L_Name], [Email], [DOB], [Age], [PhoneNumber], [RoleId], [PasswordHash], [PasswordSalt]) VALUES (10, N'ahmad', N'ahmad', N'333@gmail.com', CAST(N'1950-08-17T23:11:53.3610000' AS DateTime2), 73, 85255, 4, 0x9BD2A06FE263F158324C2CBFE6B60648DABF5744C99E5C2E36D3A3DE9DA7111272D9B21BE75F6E279013537F48B4F988D69246796FB1392128091BC0112080EB, 0x52F0310F34907ED7A31D29E674812EE4B13FBB4F1221133F60E04499B29355E77DBE79A10988F4A71D7C7C71FC07DF8DA7C95253ED7790D768940018D1366E8E8063EEC1DDBD8F9C2DA3A60867068476DD6986887E75CA9345AF030A493DE788D9FADB2B2861B230D9B90D6F27FE0B58C9052B2E225179732BB85FAFBD0B0288)
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Genres] ON 

INSERT [dbo].[Genres] ([Id], [Name]) VALUES (1, N'Fantasy')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (2, N'History')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (3, N'Biography')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (4, N'Science')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (5, N'Other')
SET IDENTITY_INSERT [dbo].[Genres] OFF
GO
SET IDENTITY_INSERT [dbo].[Purchases] ON 

INSERT [dbo].[Purchases] ([ClientId], [BookId], [TotalPrice], [PurchaseDate], [Quantity], [Id]) VALUES (10, 5, 400, CAST(N'2023-08-27T03:35:04.9156254' AS DateTime2), 2, 5)
INSERT [dbo].[Purchases] ([ClientId], [BookId], [TotalPrice], [PurchaseDate], [Quantity], [Id]) VALUES (10, 7, 125, CAST(N'2023-08-27T03:36:40.6150468' AS DateTime2), 1, 6)
INSERT [dbo].[Purchases] ([ClientId], [BookId], [TotalPrice], [PurchaseDate], [Quantity], [Id]) VALUES (10, 10, 190, CAST(N'2023-08-27T03:36:53.5756045' AS DateTime2), 2, 7)
INSERT [dbo].[Purchases] ([ClientId], [BookId], [TotalPrice], [PurchaseDate], [Quantity], [Id]) VALUES (10, 5, 200, CAST(N'2023-08-28T02:42:56.8268458' AS DateTime2), 1, 8)
SET IDENTITY_INSERT [dbo].[Purchases] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Role]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Id], [Role]) VALUES (2, N'User')
INSERT [dbo].[Roles] ([Id], [Role]) VALUES (4, N'Employee')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
/****** Object:  Index [IX_AuthorBooks_BookId]    Script Date: 9/3/2023 10:24:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_AuthorBooks_BookId] ON [dbo].[AuthorBooks]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Books_GenreId]    Script Date: 9/3/2023 10:24:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_Books_GenreId] ON [dbo].[Books]
(
	[GenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Borrows_BookId]    Script Date: 9/3/2023 10:24:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_Borrows_BookId] ON [dbo].[Borrows]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Borrows_ClientId]    Script Date: 9/3/2023 10:24:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Borrows_ClientId] ON [dbo].[Borrows]
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Clients_Email]    Script Date: 9/3/2023 10:24:22 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Clients_Email] ON [dbo].[Clients]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Clients_RolesId]    Script Date: 9/3/2023 10:24:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Clients_RolesId] ON [dbo].[Clients]
(
	[RolesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Employees_Email]    Script Date: 9/3/2023 10:24:22 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Employees_Email] ON [dbo].[Employees]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Employees_RoleId]    Script Date: 9/3/2023 10:24:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Employees_RoleId] ON [dbo].[Employees]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Purchases_BookId]    Script Date: 9/3/2023 10:24:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Purchases_BookId] ON [dbo].[Purchases]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Purchases_ClientId]    Script Date: 9/3/2023 10:24:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Purchases_ClientId] ON [dbo].[Purchases]
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Borrows] ADD  DEFAULT (CONVERT([bit],(0))) FOR [returned]
GO
ALTER TABLE [dbo].[Clients] ADD  DEFAULT (0x) FOR [PasswordHash]
GO
ALTER TABLE [dbo].[Clients] ADD  DEFAULT (0x) FOR [PasswordSalt]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT (0x) FOR [PasswordHash]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT (0x) FOR [PasswordSalt]
GO
ALTER TABLE [dbo].[AuthorBooks]  WITH CHECK ADD  CONSTRAINT [FK_AuthorBooks_Authors_AuthorId] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Authors] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AuthorBooks] CHECK CONSTRAINT [FK_AuthorBooks_Authors_AuthorId]
GO
ALTER TABLE [dbo].[AuthorBooks]  WITH CHECK ADD  CONSTRAINT [FK_AuthorBooks_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AuthorBooks] CHECK CONSTRAINT [FK_AuthorBooks_Books_BookId]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Genres_GenreId] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genres] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Genres_GenreId]
GO
ALTER TABLE [dbo].[Borrows]  WITH CHECK ADD  CONSTRAINT [FK_Borrows_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Borrows] CHECK CONSTRAINT [FK_Borrows_Books_BookId]
GO
ALTER TABLE [dbo].[Borrows]  WITH CHECK ADD  CONSTRAINT [FK_Borrows_Clients_ClientId] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Borrows] CHECK CONSTRAINT [FK_Borrows_Clients_ClientId]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Clients_Roles_RolesId] FOREIGN KEY([RolesId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_Clients_Roles_RolesId]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Roles_RoleId]
GO
ALTER TABLE [dbo].[Purchases]  WITH CHECK ADD  CONSTRAINT [FK_Purchases_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Purchases] CHECK CONSTRAINT [FK_Purchases_Books_BookId]
GO
ALTER TABLE [dbo].[Purchases]  WITH CHECK ADD  CONSTRAINT [FK_Purchases_Clients_ClientId] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Purchases] CHECK CONSTRAINT [FK_Purchases_Clients_ClientId]
GO
/****** Object:  StoredProcedure [dbo].[DeletAClient]    Script Date: 9/3/2023 10:24:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[DeletAClient] @id INT
AS
DELETE  
From Purchases where ClientId = @id
DELETE  
From Borrows where ClientId = @id;
GO
/****** Object:  StoredProcedure [dbo].[DeleteAuthor]    Script Date: 9/3/2023 10:24:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAuthor] @id INT
AS
DELETE from AuthorBooks where AuthorId = @id;
GO
/****** Object:  StoredProcedure [dbo].[GetBorrowsOfAClient]    Script Date: 9/3/2023 10:24:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetBorrowsOfAClient] @id INT
AS 
BEGIN
    SELECT B.*, C.*, BK.*
    FROM Borrows B
    INNER JOIN Clients C ON B.ClientId = C.Id
    INNER JOIN Books BK ON B.BookId = BK.Id
    WHERE B.ClientId = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[GetPurchasesOfAClient]    Script Date: 9/3/2023 10:24:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetPurchasesOfAClient] @id INT
AS
BEGIN
    SELECT P.*, C.*, BK.*
    FROM Purchases P
    INNER JOIN Clients C ON P.ClientId = C.Id
    INNER JOIN Books BK ON P.BookId = BK.Id
    WHERE P.ClientId = @id;
END
GO
USE [master]
GO
ALTER DATABASE [LibraryDB] SET  READ_WRITE 
GO
