USE [master]
GO
/****** Object:  Database [Pegasus]    Script Date: 3/23/2019 6:53:18 PM ******/
CREATE DATABASE [Pegasus]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Pegasus', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Pegasus.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Pegasus_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Pegasus_log.ldf' , SIZE = 7616KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Pegasus] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Pegasus].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Pegasus] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Pegasus] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Pegasus] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Pegasus] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Pegasus] SET ARITHABORT OFF 
GO
ALTER DATABASE [Pegasus] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Pegasus] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Pegasus] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Pegasus] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Pegasus] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Pegasus] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Pegasus] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Pegasus] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Pegasus] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Pegasus] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Pegasus] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Pegasus] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Pegasus] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Pegasus] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Pegasus] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Pegasus] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Pegasus] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Pegasus] SET RECOVERY FULL 
GO
ALTER DATABASE [Pegasus] SET  MULTI_USER 
GO
ALTER DATABASE [Pegasus] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Pegasus] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Pegasus] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Pegasus] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Pegasus] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Pegasus', N'ON'
GO
/****** Object:  Login [NT SERVICE\Winmgmt]    Script Date: 3/23/2019 6:53:19 PM ******/
CREATE LOGIN [NT SERVICE\Winmgmt] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\SQLWriter]    Script Date: 3/23/2019 6:53:19 PM ******/
CREATE LOGIN [NT SERVICE\SQLWriter] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\SQLSERVERAGENT]    Script Date: 3/23/2019 6:53:19 PM ******/
CREATE LOGIN [NT SERVICE\SQLSERVERAGENT] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT Service\MSSQLSERVER]    Script Date: 3/23/2019 6:53:19 PM ******/
CREATE LOGIN [NT Service\MSSQLSERVER] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT AUTHORITY\SYSTEM]    Script Date: 3/23/2019 6:53:19 PM ******/
CREATE LOGIN [NT AUTHORITY\SYSTEM] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [DESKTOP-2AOUIUT\Project1141]    Script Date: 3/23/2019 6:53:19 PM ******/
CREATE LOGIN [DESKTOP-2AOUIUT\Project1141] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [##MS_PolicyTsqlExecutionLogin##]    Script Date: 3/23/2019 6:53:19 PM ******/
CREATE LOGIN [##MS_PolicyTsqlExecutionLogin##] WITH PASSWORD=N'WLJLz21bGMEqaLjLOyV98sN4Q1NpO4m3nrQp+Q9jftk=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [##MS_PolicyTsqlExecutionLogin##] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [##MS_PolicyEventProcessingLogin##]    Script Date: 3/23/2019 6:53:19 PM ******/
CREATE LOGIN [##MS_PolicyEventProcessingLogin##] WITH PASSWORD=N'KCF6+LID5bH+HHhNpvub6ZKuScUFsARnfvSIfh0VmKk=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [##MS_PolicyEventProcessingLogin##] DISABLE
GO
ALTER AUTHORIZATION ON DATABASE::[Pegasus] TO [DESKTOP-2AOUIUT\Project1141]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\Winmgmt]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\SQLWriter]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\SQLSERVERAGENT]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT Service\MSSQLSERVER]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [DESKTOP-2AOUIUT\Project1141]
GO
USE [Pegasus]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 3/23/2019 6:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[__MigrationHistory] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Areas]    Script Date: 3/23/2019 6:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Areas](
	[aID] [int] IDENTITY(1,1) NOT NULL,
	[AreaDetails] [nvarchar](50) NULL,
 CONSTRAINT [PK_Areas] PRIMARY KEY CLUSTERED 
(
	[aID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[Areas] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Book_Discount]    Script Date: 3/23/2019 6:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book_Discount](
	[discno] [int] IDENTITY(1,1) NOT NULL,
	[trn_Id] [int] NOT NULL,
	[disc_Id] [int] NOT NULL,
	[userid] [nvarchar](50) NULL,
 CONSTRAINT [PK_Book_Discount] PRIMARY KEY CLUSTERED 
(
	[discno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[Book_Discount] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Book_Menus]    Script Date: 3/23/2019 6:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book_Menus](
	[No] [int] IDENTITY(1,1) NOT NULL,
	[trn_Id] [int] NULL,
	[menuid] [nvarchar](20) NULL,
 CONSTRAINT [PK_Book_Menus] PRIMARY KEY CLUSTERED 
(
	[No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[Book_Menus] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[BookingAddons]    Script Date: 3/23/2019 6:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingAddons](
	[No] [int] IDENTITY(1,1) NOT NULL,
	[trn_Id] [int] NULL,
	[Addondesc] [nvarchar](50) NULL,
	[Note] [nvarchar](50) NULL,
	[AddonAmount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_BookingAddons] PRIMARY KEY CLUSTERED 
(
	[No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[BookingAddons] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 3/23/2019 6:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[trn_Id] [int] IDENTITY(1,1) NOT NULL,
	[transdate] [datetime] NULL,
	[c_Id] [int] NULL,
	[noofperson] [int] NULL,
	[occasion] [nvarchar](50) NULL,
	[venue] [nvarchar](50) NULL,
	[apply_extendedAmount] [bit] NULL,
	[typeofservice] [int] NULL,
	[startdate] [datetime] NULL,
	[enddate] [datetime] NULL,
	[serve_stat] [bit] NULL CONSTRAINT [DF_Bookings_status]  DEFAULT ((0)),
	[eventcolor] [nchar](10) NULL,
	[p_id] [int] NULL,
	[reference] [nvarchar](50) NULL,
 CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED 
(
	[trn_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[Bookings] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[CourseCategory]    Script Date: 3/23/2019 6:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseCategory](
	[CourserId] [int] IDENTITY(1,1) NOT NULL,
	[Course] [nvarchar](50) NULL,
	[Note] [nvarchar](50) NULL,
	[Main_Bol] [bit] NULL,
 CONSTRAINT [PK_CourseCategory] PRIMARY KEY CLUSTERED 
(
	[CourserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[CourseCategory] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 3/23/2019 6:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[c_Id] [int] IDENTITY(1,1) NOT NULL,
	[lastname] [nvarchar](50) NULL,
	[firstname] [nvarchar](50) NULL,
	[middle] [nchar](10) NULL,
	[address] [nvarchar](50) NULL,
	[contact1] [nvarchar](30) NULL,
	[contact2] [nvarchar](30) NULL,
	[datereg] [datetime] NULL,
	[company] [nvarchar](50) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[c_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[Customer] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Department]    Script Date: 3/23/2019 6:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[deptId] [int] IDENTITY(1,1) NOT NULL,
	[deptName] [nvarchar](30) NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[deptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[Department] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Discount]    Script Date: 3/23/2019 6:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discount](
	[disc_Id] [int] IDENTITY(1,1) NOT NULL,
	[discCode] [nvarchar](50) NULL,
	[disctype] [nchar](10) NULL,
	[discount] [decimal](18, 2) NULL,
	[discStartdate] [datetime] NULL,
	[discEnddate] [datetime] NULL,
 CONSTRAINT [PK_Discount] PRIMARY KEY CLUSTERED 
(
	[disc_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[Discount] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 3/23/2019 6:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[menuid] [nvarchar](20) NOT NULL,
	[CourserId] [int] NULL,
	[menu_name] [nvarchar](50) NULL,
	[deptId] [int] NULL,
	[note] [nvarchar](max) NULL,
	[date_added] [datetime] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[menuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[Menu] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[PackageAreaCoverage]    Script Date: 3/23/2019 6:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PackageAreaCoverage](
	[p_areaNo] [int] IDENTITY(1,1) NOT NULL,
	[p_id] [int] NULL,
	[aID] [int] NULL,
	[is_extended] [bit] NULL,
	[ext_amount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_PackageAreaCoverage] PRIMARY KEY CLUSTERED 
(
	[p_areaNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[PackageAreaCoverage] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[PackageBody]    Script Date: 3/23/2019 6:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PackageBody](
	[No] [int] IDENTITY(1,1) NOT NULL,
	[p_id] [int] NULL,
	[mainCourse] [int] NULL,
	[sea_vegi] [int] NULL,
	[noodlepasta] [int] NULL,
	[salad] [int] NULL,
	[dessert] [int] NULL,
	[pineapple] [int] NULL,
	[drinks] [int] NULL,
	[rice] [int] NULL,
 CONSTRAINT [PK_PackageBody] PRIMARY KEY CLUSTERED 
(
	[No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[PackageBody] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Packages]    Script Date: 3/23/2019 6:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Packages](
	[p_id] [int] IDENTITY(1,1) NOT NULL,
	[p_descripton] [nvarchar](max) NULL,
	[p_amountPax] [decimal](18, 2) NULL,
	[p_min] [int] NULL,
 CONSTRAINT [PK_Packages] PRIMARY KEY CLUSTERED 
(
	[p_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[Packages] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[PackagesRangeBelowMin]    Script Date: 3/23/2019 6:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PackagesRangeBelowMin](
	[no] [int] IDENTITY(1,1) NOT NULL,
	[pMin] [int] NULL,
	[pMax] [int] NULL,
	[Amt_added] [decimal](18, 2) NULL
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[PackagesRangeBelowMin] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 3/23/2019 6:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[payNo] [int] IDENTITY(1,1) NOT NULL,
	[trn_Id] [int] NULL,
	[dateofPayment] [datetime] NULL,
	[particular] [nvarchar](50) NULL,
	[payType] [int] NULL,
	[amtPay] [decimal](18, 2) NULL,
	[pay_means] [nchar](10) NULL,
	[checkNo] [nvarchar](50) NULL,
	[notes] [nvarchar](50) NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[payNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[Payments] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Reservations]    Script Date: 3/23/2019 6:53:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservations](
	[resId] [int] IDENTITY(1,1) NOT NULL,
	[c_Id] [int] NOT NULL,
	[resDate] [datetime] NOT NULL,
	[noofPax] [int] NOT NULL,
	[occasion] [nvarchar](50) NULL,
	[reserveStat] [bit] NOT NULL CONSTRAINT [DF_Reservations_reserveStat]  DEFAULT ((1)),
 CONSTRAINT [PK_Reservations] PRIMARY KEY CLUSTERED 
(
	[resId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[Reservations] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 3/23/2019 6:53:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[Roles] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[ServiceType]    Script Date: 3/23/2019 6:53:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceType](
	[serviceId] [int] IDENTITY(1,1) NOT NULL,
	[servicetypedetails] [nvarchar](30) NULL,
 CONSTRAINT [PK_ServiceType] PRIMARY KEY CLUSTERED 
(
	[serviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[ServiceType] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 3/23/2019 6:53:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[UserClaims] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 3/23/2019 6:53:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.UserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[UserLogins] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 3/23/2019 6:53:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[UserRoles] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/23/2019 6:53:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER AUTHORIZATION ON [dbo].[Users] TO  SCHEMA OWNER 
GO
/****** Object:  View [dbo].[CourseCount]    Script Date: 3/23/2019 6:53:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CourseCount]
AS
SELECT        dbo.Book_Menus.trn_Id, dbo.CourseCategory.CourserId, COUNT(dbo.CourseCategory.CourserId) AS Coursecount
FROM            dbo.Book_Menus INNER JOIN
                         dbo.Menu ON dbo.Book_Menus.menuid = dbo.Menu.menuid INNER JOIN
                         dbo.CourseCategory ON dbo.Menu.CourserId = dbo.CourseCategory.CourserId
GROUP BY dbo.Book_Menus.trn_Id, dbo.CourseCategory.CourserId

GO
ALTER AUTHORIZATION ON [dbo].[CourseCount] TO  SCHEMA OWNER 
GO
/****** Object:  View [dbo].[View_1]    Script Date: 3/23/2019 6:53:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_1]
AS
SELECT        dbo.PackageAreaCoverage.aID, dbo.Areas.AreaDetails
FROM            dbo.PackageAreaCoverage INNER JOIN
                         dbo.Areas ON dbo.PackageAreaCoverage.aID = dbo.Areas.aID
GROUP BY dbo.PackageAreaCoverage.aID, dbo.Areas.AreaDetails

GO
ALTER AUTHORIZATION ON [dbo].[View_1] TO  SCHEMA OWNER 
GO
/****** Object:  View [dbo].[View_3]    Script Date: 3/23/2019 6:53:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_3]
AS
SELECT        dbo.Book_Menus.trn_Id, dbo.CourseCategory.CourserId
FROM            dbo.Book_Menus INNER JOIN
                         dbo.Menu ON dbo.Book_Menus.menuid = dbo.Menu.menuid INNER JOIN
                         dbo.CourseCategory ON dbo.Menu.CourserId = dbo.CourseCategory.CourserId
WHERE        (dbo.Book_Menus.trn_Id = 29)

GO
ALTER AUTHORIZATION ON [dbo].[View_3] TO  SCHEMA OWNER 
GO
SET IDENTITY_INSERT [dbo].[Areas] ON 

INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (1, N'ADAMS')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (2, N'SAN MIGUEL')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (3, N'SANTA FE')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (4, N'TABANGO')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (5, N'TABONTABON')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (6, N'TACLOBAN CITY (Capital)')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (7, N'TANAUAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (8, N'TOLOSA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (9, N'TUNGA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (10, N'VILLABA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (11, N'ALLEN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (12, N'BIRI')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (13, N'BOBON')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (14, N'CAPUL')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (15, N'CATARMAN (Capital)')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (16, N'CATUBIG')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (17, N'GAMAY')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (18, N'LAOANG')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (19, N'LAPINIG')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (20, N'LAS NAVAS')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (21, N'LAVEZARES')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (22, N'MAPANAS')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (23, N'MONDRAGON')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (24, N'PALAPAG')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (25, N'PAMBUJAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (26, N'ROSARIO')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (27, N'SAN ANTONIO')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (28, N'SAN ISIDRO')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (29, N'SAN JOSE')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (30, N'SAN ROQUE')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (31, N'SAN VICENTE')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (32, N'SILVINO LOBOS')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (33, N'VICTORIA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (34, N'LOPE DE VEGA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (35, N'ALMAGRO')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (36, N'BASEY')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (37, N'CALBAYOG CITY')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (38, N'CALBIGA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (39, N'CITY OF CATBALOGAN (Capital)')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (40, N'DARAM')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (41, N'GANDARA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (42, N'HINABANGAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (43, N'JIABONG')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (44, N'MARABUT')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (45, N'MATUGUINAO')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (46, N'MOTIONG')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (47, N'PINABACDAO')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (48, N'SAN JOSE DE BUAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (49, N'SAN SEBASTIAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (50, N'SANTA MARGARITA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (51, N'SANTA RITA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (52, N'SANTO NIÑO')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (53, N'TALALORA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (54, N'TARANGNAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (55, N'VILLAREAL')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (56, N'PARANAS (WRIGHT)')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (57, N'ZUMARRAGA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (58, N'TAGAPUL-AN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (59, N'SAN JORGE')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (60, N'PAGSANGHAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (61, N'ANAHAWAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (62, N'BONTOC')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (63, N'HINUNANGAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (64, N'HINUNDAYAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (65, N'LIBAGON')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (66, N'LILOAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (67, N'CITY OF MAASIN (Capital)')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (68, N'MACROHON')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (69, N'MALITBOG')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (70, N'PADRE BURGOS')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (71, N'PINTUYAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (72, N'SAINT BERNARD')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (73, N'SAN FRANCISCO')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (74, N'SAN JUAN (CABALIAN)')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (75, N'SAN RICARDO')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (76, N'SILAGO')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (77, N'SOGOD')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (78, N'TOMAS OPPUS')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (79, N'LIMASAWA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (80, N'ALMERIA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (81, N'BILIRAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (82, N'CABUCGAYAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (83, N'CAIBIRAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (84, N'CULABA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (85, N'KAWAYAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (86, N'MARIPIPI')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (87, N'NAVAL (Capital)')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (88, N'ARTECHE')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (89, N'BALANGIGA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (90, N'BALANGKAYAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (91, N'CITY OF BORONGAN (Capital)')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (92, N'CAN-AVID')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (93, N'DOLORES')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (94, N'GENERAL MACARTHUR')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (95, N'GIPORLOS')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (96, N'GUIUAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (97, N'HERNANI')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (98, N'JIPAPAD')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (99, N'LAWAAN')
GO
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (100, N'LLORENTE')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (101, N'MASLOG')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (102, N'MAYDOLONG')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (103, N'MERCEDES')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (104, N'ORAS')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (105, N'QUINAPONDAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (106, N'SALCEDO')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (107, N'SAN JULIAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (108, N'SAN POLICARPO')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (109, N'SULAT')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (110, N'TAFT')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (111, N'ABUYOG')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (112, N'ALANGALANG')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (113, N'ALBUERA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (114, N'BABATNGON')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (115, N'BARUGO')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (116, N'BATO')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (117, N'CITY OF BAYBAY')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (118, N'BURAUEN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (119, N'CALUBIAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (120, N'CAPOOCAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (121, N'CARIGARA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (122, N'DAGAMI')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (123, N'DULAG')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (124, N'HILONGOS')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (125, N'HINDANG')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (126, N'INOPACAN')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (127, N'ISABEL')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (128, N'JARO')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (129, N'JAVIER (BUGHO)')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (130, N'JULITA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (131, N'KANANGA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (132, N'LA PAZ')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (133, N'LEYTE')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (134, N'MACARTHUR')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (135, N'MAHAPLAG')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (136, N'MATAG-OB')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (137, N'MATALOM')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (138, N'MAYORGA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (139, N'MERIDA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (140, N'ORMOC CITY')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (141, N'PALO')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (142, N'PALOMPON')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (143, N'PASTRANA')
INSERT [dbo].[Areas] ([aID], [AreaDetails]) VALUES (144, N'SAN ISIDRO')
SET IDENTITY_INSERT [dbo].[Areas] OFF
SET IDENTITY_INSERT [dbo].[Book_Discount] ON 

INSERT [dbo].[Book_Discount] ([discno], [trn_Id], [disc_Id], [userid]) VALUES (5, 58, 1, N'541ebf94-d28d-4311-a3e8-7061ec1951cc')
INSERT [dbo].[Book_Discount] ([discno], [trn_Id], [disc_Id], [userid]) VALUES (6, 62, 5, N'541ebf94-d28d-4311-a3e8-7061ec1951cc')
SET IDENTITY_INSERT [dbo].[Book_Discount] OFF
SET IDENTITY_INSERT [dbo].[Book_Menus] ON 

INSERT [dbo].[Book_Menus] ([No], [trn_Id], [menuid]) VALUES (56, 62, N'0002')
INSERT [dbo].[Book_Menus] ([No], [trn_Id], [menuid]) VALUES (57, 62, N'0001')
INSERT [dbo].[Book_Menus] ([No], [trn_Id], [menuid]) VALUES (58, 59, N'0016')
INSERT [dbo].[Book_Menus] ([No], [trn_Id], [menuid]) VALUES (59, 59, N'0024')
INSERT [dbo].[Book_Menus] ([No], [trn_Id], [menuid]) VALUES (60, 59, N'0002')
INSERT [dbo].[Book_Menus] ([No], [trn_Id], [menuid]) VALUES (62, 60, N'0004')
INSERT [dbo].[Book_Menus] ([No], [trn_Id], [menuid]) VALUES (63, 60, N'0002')
INSERT [dbo].[Book_Menus] ([No], [trn_Id], [menuid]) VALUES (64, 57, N'0004')
INSERT [dbo].[Book_Menus] ([No], [trn_Id], [menuid]) VALUES (65, 57, N'0001')
INSERT [dbo].[Book_Menus] ([No], [trn_Id], [menuid]) VALUES (66, 57, N'0012')
SET IDENTITY_INSERT [dbo].[Book_Menus] OFF
SET IDENTITY_INSERT [dbo].[Bookings] ON 

INSERT [dbo].[Bookings] ([trn_Id], [transdate], [c_Id], [noofperson], [occasion], [venue], [apply_extendedAmount], [typeofservice], [startdate], [enddate], [serve_stat], [eventcolor], [p_id], [reference]) VALUES (54, CAST(N'2019-03-15 04:11:50.000' AS DateTime), 807, 100, N'BirthDay', N'Origami ', NULL, 1, CAST(N'2019-05-01 05:28:00.000' AS DateTime), CAST(N'2019-05-01 05:28:00.000' AS DateTime), 0, NULL, 98, NULL)
INSERT [dbo].[Bookings] ([trn_Id], [transdate], [c_Id], [noofperson], [occasion], [venue], [apply_extendedAmount], [typeofservice], [startdate], [enddate], [serve_stat], [eventcolor], [p_id], [reference]) VALUES (55, CAST(N'2019-03-15 04:36:27.000' AS DateTime), 601, 150, N'I dont Know Event', N'Ka', NULL, 1, CAST(N'2019-05-14 04:36:00.000' AS DateTime), CAST(N'2019-05-14 04:36:00.000' AS DateTime), 0, NULL, 98, NULL)
INSERT [dbo].[Bookings] ([trn_Id], [transdate], [c_Id], [noofperson], [occasion], [venue], [apply_extendedAmount], [typeofservice], [startdate], [enddate], [serve_stat], [eventcolor], [p_id], [reference]) VALUES (57, CAST(N'2019-03-15 05:29:22.000' AS DateTime), 677, 300, N'Party@802', N'Isabel,Leyte', 0, 1, CAST(N'2019-03-30 18:00:00.000' AS DateTime), CAST(N'2019-03-30 18:00:00.000' AS DateTime), 0, NULL, 98, NULL)
INSERT [dbo].[Bookings] ([trn_Id], [transdate], [c_Id], [noofperson], [occasion], [venue], [apply_extendedAmount], [typeofservice], [startdate], [enddate], [serve_stat], [eventcolor], [p_id], [reference]) VALUES (58, CAST(N'2019-03-15 06:00:31.000' AS DateTime), 808, 100, N'Bday', N'Ormoc City', 0, 2, CAST(N'2019-03-25 06:00:00.000' AS DateTime), CAST(N'2019-03-25 06:00:00.000' AS DateTime), 0, NULL, 98, NULL)
INSERT [dbo].[Bookings] ([trn_Id], [transdate], [c_Id], [noofperson], [occasion], [venue], [apply_extendedAmount], [typeofservice], [startdate], [enddate], [serve_stat], [eventcolor], [p_id], [reference]) VALUES (59, CAST(N'2019-03-16 09:24:39.000' AS DateTime), 809, 60, N'Bday Party', N'Sals Bar', 0, 2, CAST(N'2019-03-25 09:25:00.000' AS DateTime), CAST(N'2019-03-25 09:25:00.000' AS DateTime), 0, NULL, 105, NULL)
INSERT [dbo].[Bookings] ([trn_Id], [transdate], [c_Id], [noofperson], [occasion], [venue], [apply_extendedAmount], [typeofservice], [startdate], [enddate], [serve_stat], [eventcolor], [p_id], [reference]) VALUES (60, CAST(N'2019-03-16 19:11:31.000' AS DateTime), 810, 100, N'New Event', N'Ormoc', 0, 1, CAST(N'2019-03-28 19:11:00.000' AS DateTime), CAST(N'2019-03-28 19:11:00.000' AS DateTime), 0, NULL, 98, NULL)
INSERT [dbo].[Bookings] ([trn_Id], [transdate], [c_Id], [noofperson], [occasion], [venue], [apply_extendedAmount], [typeofservice], [startdate], [enddate], [serve_stat], [eventcolor], [p_id], [reference]) VALUES (61, CAST(N'2019-03-16 19:16:18.000' AS DateTime), 628, 100, N'Bday', N'Kananga', NULL, 1, CAST(N'2019-03-28 19:16:00.000' AS DateTime), CAST(N'2019-03-28 19:16:00.000' AS DateTime), 0, NULL, 98, NULL)
INSERT [dbo].[Bookings] ([trn_Id], [transdate], [c_Id], [noofperson], [occasion], [venue], [apply_extendedAmount], [typeofservice], [startdate], [enddate], [serve_stat], [eventcolor], [p_id], [reference]) VALUES (62, CAST(N'2019-03-17 00:15:52.000' AS DateTime), 808, 80, N'Bday', N'Pavi', 0, 1, CAST(N'2019-03-29 00:16:00.000' AS DateTime), CAST(N'2019-03-29 00:16:00.000' AS DateTime), 0, NULL, 120, NULL)
SET IDENTITY_INSERT [dbo].[Bookings] OFF
SET IDENTITY_INSERT [dbo].[CourseCategory] ON 

INSERT [dbo].[CourseCategory] ([CourserId], [Course], [Note], [Main_Bol]) VALUES (1, N'Beef', NULL, 1)
INSERT [dbo].[CourseCategory] ([CourserId], [Course], [Note], [Main_Bol]) VALUES (2, N'Soda', N'asdsdas', 0)
INSERT [dbo].[CourseCategory] ([CourserId], [Course], [Note], [Main_Bol]) VALUES (3, N'Seafood', NULL, 0)
INSERT [dbo].[CourseCategory] ([CourserId], [Course], [Note], [Main_Bol]) VALUES (4, N'Vegetable', NULL, 0)
INSERT [dbo].[CourseCategory] ([CourserId], [Course], [Note], [Main_Bol]) VALUES (5, N'Chicken', NULL, 1)
INSERT [dbo].[CourseCategory] ([CourserId], [Course], [Note], [Main_Bol]) VALUES (6, N'Noodle / Pasta', NULL, 0)
INSERT [dbo].[CourseCategory] ([CourserId], [Course], [Note], [Main_Bol]) VALUES (8, N'Rice', NULL, 0)
INSERT [dbo].[CourseCategory] ([CourserId], [Course], [Note], [Main_Bol]) VALUES (11, N'NewCourse', N'New Course', 0)
INSERT [dbo].[CourseCategory] ([CourserId], [Course], [Note], [Main_Bol]) VALUES (12, N'CourseNew', NULL, 0)
INSERT [dbo].[CourseCategory] ([CourserId], [Course], [Note], [Main_Bol]) VALUES (13, N'Dessert', NULL, 0)
SET IDENTITY_INSERT [dbo].[CourseCategory] OFF
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (601, N'Brgy. Tugbong', N'Kanangga', N'          ', N'Tugbong, Leyte, Kanangga', N'9491248257', N'          ', CAST(N'2016-02-26 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (602, N'Municipality Of Kanangga', N'Kanangga', N'          ', N'Uknown, Kanangga, Leyte', N'929912564 ', N'          ', CAST(N'2016-02-26 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (603, N'Gucela', N'Virgenia', N'S         ', N'Tugbong, Merida, Leyte', N'9397084465', N'          ', CAST(N'2016-02-26 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (604, N'Brgy. Tugbong', N'Tugbong', N'          ', N'Tugbong, Kanangga, Leyte', N'9491248257', N'          ', CAST(N'2016-02-26 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (605, N'Municipality Of Kanangga', N'Kanangga', N'          ', N'Tugbong, Kangga, Leyte', N'9399125641', N'          ', CAST(N'2016-02-26 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (606, N'Cuyag', N'Angelo', N'I         ', N'Ormoc, Leyte', N'9488025224', N'          ', CAST(N'2016-02-26 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (607, N'Meca', N'Jocelyn', N'S         ', N'Ormoc, Leyte', N'9985783086', N'          ', CAST(N'2016-02-26 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (608, N'Elliott', N'Hedeliza', N'V         ', N'Tugbong, Magbagacay St.bernard, Southern Leyte', N'9993619345', N'          ', CAST(N'2016-02-26 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (610, N'Fabular', N'Gloria', N'J         ', N'NULL, Hilongos, Leyte', N'9072385292', N'          ', CAST(N'2016-02-26 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (611, N'Tolentino', N'Michelle', N'A         ', N', Albuera, Leyte', N'9176371300', N'          ', CAST(N'2016-02-26 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (612, N'Ventula', N'Rhowel', N'N         ', N'Brgy.san Isidro, Baybay, Leyte', N'9265853986', N'          ', CAST(N'2016-02-26 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (614, N'Agudo', N'Razel', N'M         ', N'521 Alegria Village, Ormoc, Leyte', N'9178860950', N'          ', CAST(N'2016-02-26 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (615, N'Caritas Foundation Inc.', N'Caritas', N'          ', N', Caritas, Manila', N'917222217 ', N'          ', CAST(N'2016-02-26 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (616, N'Laurente', N'Esteban', N'V         ', N'Purok 11 Brgy. Dona Feliza Mejia, Ormoc, Leyte', N'9393979717', N'          ', CAST(N'2016-02-26 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (627, N'Tabudlong', N'Jusito', N'F         ', N', Baybay, Leyte', N'9397084465', N'          ', CAST(N'2016-02-26 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (628, N'Roble', N'Eduardo', N'P         ', N'256 Sss Vilage San Pablo, Ormoc, Leyte', N'-   -     ', N'          ', CAST(N'2016-02-26 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (641, N'Roble', N'Eduardo', N'p         ', N'Sss Village San Pablo, Ormoc, Leyte', N'-   -     ', N'          ', CAST(N'2016-04-02 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (663, N'Bandalan', N'Roberto', N'B         ', N'Bonifacio, Baybay, Leyte', N'9155980823', N'          ', CAST(N'2016-04-02 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (677, N'PASAR', N'PASAR', N'          ', NULL, NULL, NULL, NULL, N'Philippine Associated Smelting And Refining Corpor')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (679, N'Degelio', N'Jocelyn', N'L         ', N'Margen, Ormoc, Leyte', N'          ', N'          ', CAST(N'2016-04-04 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (683, N'Acuna', N'Eugenio', N'M         ', N'Brgy.canmarating, Abuyog, Leyte', N'          ', N'          ', CAST(N'2016-04-04 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (689, N'Leyte Provincial Hospital', N'', N'          ', N', Palo, Leyte', N'          ', N'          ', CAST(N'2016-04-04 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (700, N'Floreza', N'Clyde', N'C         ', N'Bagong Buhay, Ormoc, Leyte', N'-   -     ', N'          ', CAST(N'2016-04-04 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (709, N'Bereso', N'Eglesciano', N'S         ', N'1st Silver Hilld Subd. Brgy.luna, Ormoc, Leyte', N'          ', N'          ', CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (714, N'Rizarri', N'Ernesto', N'b         ', N'Cogon, Ormoc, Leyte', N'          ', N'          ', CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (718, N'Estipona', N'Osias', N'G         ', N'Benolho, Albuera, Leyte', N'          ', N'          ', CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (719, N'Achacoso', N'Jose Luna', N'Jr        ', N'116 Bolongeta St. Cogon, Ormoc, Leyte', N'9065773119', N'          ', CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (721, N'Kapili', N'Bethany', N'G         ', N'Canturing, Maasin, Leyte', N'          ', N'          ', CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (723, N'Ebr Marketing Corp.', N'', N'          ', N'Carlos Tan, Ormoc, Leyte', N'          ', N'          ', CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (725, N'Orwasa', N'', N'          ', N', Ormoc, Leyte', N'          ', N'          ', CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (726, N'Doyon', N'Geronimo', N'L         ', N'Fatima Heights Cogon, Ormoc, Leyte', N'          ', N'          ', CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (734, N'Mecaydor', N'Jecel Beth', N'M         ', N'Lo-ok, Almeria, Biliran', N'          ', N'          ', CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (739, N'Smith', N'Normand', N'D         ', N'Mary Anne Homes Brgy.san Pablo, Ormoc, Leyte', N'          ', N'          ', CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (740, N'Chua', N'Gerard', N'          ', N'Rincon Malinta, Valenzuela, Manila', N'          ', N'          ', CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (743, N'City Government Of Baybay', N'', N'          ', N', Baybay, Leyte', N'          ', N'          ', CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (748, N'Provincial Government Of Leyte', N'', N'          ', N', Tacloban, Leyte', N'          ', N'          ', CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (750, N'City Disaster Risk Reduction Management Council', N'', N'          ', N', Ormoc, Leyte', N'          ', N'          ', CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (754, N'Provincial Government Of Leyte', N'', N'          ', N', Tacloban, Leyte', N'          ', N'          ', CAST(N'2016-04-05 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (761, N'Caso', N'', N'          ', N', Ormoc, Leyte', N'          ', N'          ', CAST(N'2016-04-06 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (768, N'Therma Control Machinery Inc', N'', N'          ', N'1910 Me Reyes St, Navotas, Manila', N'          ', N'          ', CAST(N'2016-04-06 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (769, N'Sabino', N'Leizel', N'R         ', N'Brgy.boungaville Punta, Ormoc, Leyte', N'          ', N'          ', CAST(N'2016-04-06 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (770, N'Rusell', N'Cristabelle', N'M         ', N'Brgy.don Felipe, Ormoc, Leyte', N'          ', N'          ', CAST(N'2016-04-06 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (776, N'Lumbre', N'Restituto', N'A         ', N'St. Francis Of Assisi Parish, Pastrana, Leyte', N'          ', N'          ', CAST(N'2016-04-06 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (777, N'Phil Rigid Construction Corp.', N'', N'          ', N', Liloan, Cebu', N'          ', N'          ', CAST(N'2016-04-06 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (778, N'Phil.rigid Compound Tayod', N'', N'          ', N', Liloan, Cebu', N'          ', N'          ', CAST(N'2016-04-06 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (783, N'Ong', N'Lloyd', N'C         ', N'1845 Paz Mendoza, Paco, Manila', N'          ', N'          ', CAST(N'2016-04-06 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (792, N'Aviles', N'Jonathan', N'A         ', N'938 Carlota Hills Subd., Ormoc, Leyte', N'9397084465', N'          ', CAST(N'2016-04-06 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (798, N'Benett', N'Richelle', N'T         ', N'Buena Vista, Baybay, Leyte', N'-   -     ', N'          ', CAST(N'2016-04-06 00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (800, N'sdasds', N'sdasda', NULL, N'asasadsdsads', NULL, NULL, CAST(N'2018-10-31 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (801, N'asdsada', N'sadas', N'r         ', N'dasdsadasd', NULL, NULL, CAST(N'2018-11-15 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (802, N'wqewqe', N'asdasd', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (803, N'QWEQWE', N'ADDDFDSF', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (804, N'WQEWQE', N'ASDSADSAD', N'AS        ', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (805, N'eqwe', N'werer', NULL, NULL, NULL, NULL, CAST(N'2019-02-12 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (806, N'asdasd', N'eqwe', NULL, NULL, N'09295737238', NULL, CAST(N'2019-02-14 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (807, N'Alcala', N'Aladdine', N'T         ', NULL, N'09295737438', NULL, CAST(N'2019-02-19 00:00:00.000' AS DateTime), N'Hedico Sugar Milling Co.,Inc.')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (808, N'CANLAS', N'DAROL', NULL, NULL, N'09988843622', NULL, CAST(N'2019-05-27 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (809, N'Alcala', N'Jourgena', N'T         ', NULL, NULL, NULL, CAST(N'2019-03-06 00:00:00.000' AS DateTime), N'asdasd')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (810, N'NewCustomer', N'NewCustomer', NULL, NULL, N'09295737438', NULL, CAST(N'2019-03-14 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (811, N'Customer101', N'Customer101', NULL, NULL, N'09295737438', NULL, CAST(N'2019-03-16 00:00:00.000' AS DateTime), N'sadas')
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (812, N'Customer101', N'Customer101', NULL, NULL, N'09295737438', NULL, CAST(N'2019-03-16 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (813, N'CustomerNew', N'CutomerNew', NULL, NULL, N'09295737000', NULL, NULL, NULL)
INSERT [dbo].[Customer] ([c_Id], [lastname], [firstname], [middle], [address], [contact1], [contact2], [datereg], [company]) VALUES (814, N'City Gov Ormoc', N'OrmocCity', NULL, NULL, N'09295737238', NULL, CAST(N'2019-03-16 00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Customer] OFF
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([deptId], [deptName]) VALUES (1, N'Commisary')
INSERT [dbo].[Department] ([deptId], [deptName]) VALUES (2, N'Soda')
INSERT [dbo].[Department] ([deptId], [deptName]) VALUES (3, N'PineApple')
SET IDENTITY_INSERT [dbo].[Department] OFF
SET IDENTITY_INSERT [dbo].[Discount] ON 

INSERT [dbo].[Discount] ([disc_Id], [discCode], [disctype], [discount], [discStartdate], [discEnddate]) VALUES (1, N'disc001', N'percentage', CAST(3.00 AS Decimal(18, 2)), CAST(N'2019-03-01 00:00:00.000' AS DateTime), CAST(N'2019-03-30 00:00:00.000' AS DateTime))
INSERT [dbo].[Discount] ([disc_Id], [discCode], [disctype], [discount], [discStartdate], [discEnddate]) VALUES (2, N'disc002', N'amount    ', CAST(500.00 AS Decimal(18, 2)), NULL, NULL)
INSERT [dbo].[Discount] ([disc_Id], [discCode], [disctype], [discount], [discStartdate], [discEnddate]) VALUES (4, N'discount04', N'amount    ', CAST(350.00 AS Decimal(18, 2)), NULL, NULL)
INSERT [dbo].[Discount] ([disc_Id], [discCode], [disctype], [discount], [discStartdate], [discEnddate]) VALUES (5, N'DescountGov', N'percentage', CAST(5.00 AS Decimal(18, 2)), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Discount] OFF
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0001', 1, N'Bacon Wrapped Beef Rolls', 1, NULL, CAST(N'2018-10-22 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0002', 1, N'Beef Teppanyakki', 1, NULL, CAST(N'2018-10-31 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0004', 1, N'Callos', 1, NULL, CAST(N'2018-11-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0005', 1, N'Beef with Onions', 1, NULL, CAST(N'2018-11-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0006', 4, N'Chaofatchen', 1, NULL, CAST(N'2018-11-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0007', 6, N'Baked Spaghetti', 1, NULL, CAST(N'2018-11-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0008', 5, N'Chicken Cashew', 1, NULL, CAST(N'2018-11-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0009', 8, N'Rice(Plain)', 1, NULL, CAST(N'2019-01-01 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0010', 2, N'Soda', 2, NULL, CAST(N'2019-01-01 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0012', 3, N'Kinilaw na Tangigi', 1, NULL, CAST(N'2019-03-12 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0013', 4, N'NewVegetableMenu', 1, N'NewNote', CAST(N'2019-03-12 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0014', 2, N'asdsa', 2, N'sdsd', CAST(N'2019-03-12 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0015', 3, N'dasd', 1, NULL, CAST(N'2019-03-12 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0016', 5, N'newchicken', 1, NULL, CAST(N'2019-03-12 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0017', 6, N'sadas', 1, NULL, CAST(N'2019-03-12 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0018', 5, N'asdasd', 1, NULL, CAST(N'2019-03-13 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0019', 2, N'xczczxc', 1, N'sads', CAST(N'2019-03-13 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0020', 3, N'xzczxc', 1, NULL, CAST(N'2019-03-13 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0021', 3, N'xzczxc', 1, NULL, CAST(N'2019-03-13 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0022', 3, N'Alimango', 1, N'Alimango  Note', CAST(N'2019-03-16 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0023', 11, N'new Course', 1, NULL, CAST(N'2019-03-16 00:00:00.000' AS DateTime))
INSERT [dbo].[Menu] ([menuid], [CourserId], [menu_name], [deptId], [note], [date_added]) VALUES (N'0024', 13, N'Manggo Float', 1, NULL, CAST(N'2019-03-16 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[PackageAreaCoverage] ON 

INSERT [dbo].[PackageAreaCoverage] ([p_areaNo], [p_id], [aID], [is_extended], [ext_amount]) VALUES (14, 105, 140, 0, CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[PackageAreaCoverage] ([p_areaNo], [p_id], [aID], [is_extended], [ext_amount]) VALUES (15, 120, 140, 0, CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[PackageAreaCoverage] ([p_areaNo], [p_id], [aID], [is_extended], [ext_amount]) VALUES (17, 97, 6, 0, CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[PackageAreaCoverage] ([p_areaNo], [p_id], [aID], [is_extended], [ext_amount]) VALUES (18, 103, 140, 0, CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[PackageAreaCoverage] ([p_areaNo], [p_id], [aID], [is_extended], [ext_amount]) VALUES (25, 98, 93, 0, CAST(0.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[PackageAreaCoverage] OFF
SET IDENTITY_INSERT [dbo].[PackageBody] ON 

INSERT [dbo].[PackageBody] ([No], [p_id], [mainCourse], [sea_vegi], [noodlepasta], [salad], [dessert], [pineapple], [drinks], [rice]) VALUES (61, 97, 1, 1, 1, 1, 1, 1, 1, 1)
INSERT [dbo].[PackageBody] ([No], [p_id], [mainCourse], [sea_vegi], [noodlepasta], [salad], [dessert], [pineapple], [drinks], [rice]) VALUES (62, 98, 2, 1, 1, 1, 1, 1, 1, 1)
INSERT [dbo].[PackageBody] ([No], [p_id], [mainCourse], [sea_vegi], [noodlepasta], [salad], [dessert], [pineapple], [drinks], [rice]) VALUES (65, 103, 2, 1, 1, 1, 1, 1, 1, 1)
INSERT [dbo].[PackageBody] ([No], [p_id], [mainCourse], [sea_vegi], [noodlepasta], [salad], [dessert], [pineapple], [drinks], [rice]) VALUES (67, 105, 2, 1, 1, 1, 1, 1, 1, 1)
INSERT [dbo].[PackageBody] ([No], [p_id], [mainCourse], [sea_vegi], [noodlepasta], [salad], [dessert], [pineapple], [drinks], [rice]) VALUES (80, 116, 2, 1, 1, 1, 1, 1, 1, 1)
INSERT [dbo].[PackageBody] ([No], [p_id], [mainCourse], [sea_vegi], [noodlepasta], [salad], [dessert], [pineapple], [drinks], [rice]) VALUES (81, 117, 1, 1, 1, 1, 1, 1, 1, 1)
INSERT [dbo].[PackageBody] ([No], [p_id], [mainCourse], [sea_vegi], [noodlepasta], [salad], [dessert], [pineapple], [drinks], [rice]) VALUES (82, 118, 2, 2, 1, 1, 1, 1, 1, 1)
INSERT [dbo].[PackageBody] ([No], [p_id], [mainCourse], [sea_vegi], [noodlepasta], [salad], [dessert], [pineapple], [drinks], [rice]) VALUES (83, 119, 2, 1, 0, 1, 1, 1, 1, 1)
INSERT [dbo].[PackageBody] ([No], [p_id], [mainCourse], [sea_vegi], [noodlepasta], [salad], [dessert], [pineapple], [drinks], [rice]) VALUES (84, 120, 2, 1, 1, 1, 1, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[PackageBody] OFF
SET IDENTITY_INSERT [dbo].[Packages] ON 

INSERT [dbo].[Packages] ([p_id], [p_descripton], [p_amountPax], [p_min]) VALUES (97, N'Package 200', CAST(200.00 AS Decimal(18, 2)), 100)
INSERT [dbo].[Packages] ([p_id], [p_descripton], [p_amountPax], [p_min]) VALUES (98, N'Package 200', CAST(200.00 AS Decimal(18, 2)), 100)
INSERT [dbo].[Packages] ([p_id], [p_descripton], [p_amountPax], [p_min]) VALUES (103, N'Aladdine''s 101', CAST(350.00 AS Decimal(18, 2)), 100)
INSERT [dbo].[Packages] ([p_id], [p_descripton], [p_amountPax], [p_min]) VALUES (105, N'asdsa', CAST(234.00 AS Decimal(18, 2)), 100)
INSERT [dbo].[Packages] ([p_id], [p_descripton], [p_amountPax], [p_min]) VALUES (116, N'P123', CAST(324.00 AS Decimal(18, 2)), 100)
INSERT [dbo].[Packages] ([p_id], [p_descripton], [p_amountPax], [p_min]) VALUES (117, N'qqq', CAST(345.00 AS Decimal(18, 2)), 100)
INSERT [dbo].[Packages] ([p_id], [p_descripton], [p_amountPax], [p_min]) VALUES (118, N'wwww', CAST(435.00 AS Decimal(18, 2)), 100)
INSERT [dbo].[Packages] ([p_id], [p_descripton], [p_amountPax], [p_min]) VALUES (119, N'Gov.Package', CAST(270.00 AS Decimal(18, 2)), 100)
INSERT [dbo].[Packages] ([p_id], [p_descripton], [p_amountPax], [p_min]) VALUES (120, N'BUFFET PACKAGE A', CAST(295.00 AS Decimal(18, 2)), 100)
SET IDENTITY_INSERT [dbo].[Packages] OFF
SET IDENTITY_INSERT [dbo].[PackagesRangeBelowMin] ON 

INSERT [dbo].[PackagesRangeBelowMin] ([no], [pMin], [pMax], [Amt_added]) VALUES (1, 80, 99, CAST(10.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagesRangeBelowMin] ([no], [pMin], [pMax], [Amt_added]) VALUES (2, 50, 79, CAST(20.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[PackagesRangeBelowMin] OFF
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([payNo], [trn_Id], [dateofPayment], [particular], [payType], [amtPay], [pay_means], [checkNo], [notes]) VALUES (69, 54, CAST(N'2019-03-15 05:42:51.000' AS DateTime), N'SL1234', 1, CAST(10000.00 AS Decimal(18, 2)), N'Cash      ', NULL, N'Down Payment')
INSERT [dbo].[Payments] ([payNo], [trn_Id], [dateofPayment], [particular], [payType], [amtPay], [pay_means], [checkNo], [notes]) VALUES (73, 61, CAST(N'2019-03-16 23:19:01.000' AS DateTime), N'SL321', 1, CAST(12000.00 AS Decimal(18, 2)), N'Cash      ', NULL, NULL)
INSERT [dbo].[Payments] ([payNo], [trn_Id], [dateofPayment], [particular], [payType], [amtPay], [pay_means], [checkNo], [notes]) VALUES (75, 62, CAST(N'2019-03-17 00:40:57.000' AS DateTime), N'SL33', 1, CAST(10000.00 AS Decimal(18, 2)), N'Cash      ', NULL, N'dp')
SET IDENTITY_INSERT [dbo].[Payments] OFF
SET IDENTITY_INSERT [dbo].[Reservations] ON 

INSERT [dbo].[Reservations] ([resId], [c_Id], [resDate], [noofPax], [occasion], [reserveStat]) VALUES (1, 612, CAST(N'2019-04-28 19:16:00.000' AS DateTime), 120, N'Party', 1)
SET IDENTITY_INSERT [dbo].[Reservations] OFF
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (N'1', N'superadmin')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (N'2', N'admin')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (N'3', N'user')
SET IDENTITY_INSERT [dbo].[ServiceType] ON 

INSERT [dbo].[ServiceType] ([serviceId], [servicetypedetails]) VALUES (1, N'Guided')
INSERT [dbo].[ServiceType] ([serviceId], [servicetypedetails]) VALUES (2, N'None')
SET IDENTITY_INSERT [dbo].[ServiceType] OFF
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'02b765fa-0c00-4f16-a615-d149bcb11fce', N'2')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'02b765fa-0c00-4f16-a615-d149bcb11fce', N'3')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'541ebf94-d28d-4311-a3e8-7061ec1951cc', N'1')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'541ebf94-d28d-4311-a3e8-7061ec1951cc', N'3')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'6c0a4727-a4f3-45bd-abb2-aca485ba76d3', N'3')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'732be7fe-5b7f-4cd5-9ff7-2658fa1d3220', N'3')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'db678ee4-23ec-48ae-9485-1c23bb7cc2b9', N'2')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'db678ee4-23ec-48ae-9485-1c23bb7cc2b9', N'3')
INSERT [dbo].[Users] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'02b765fa-0c00-4f16-a615-d149bcb11fce', N'admin@gmail.com', 0, N'AMUUIjx8jzInpXonQ8+eETye0Umu+paxpy0ThCWTPRVr+g0OvVFIrXP1zHhN/6ildw==', N'c679522d-961c-46e6-b6ac-079fa769b4b7', NULL, 0, 0, NULL, 1, 0, N'admin')
INSERT [dbo].[Users] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'541ebf94-d28d-4311-a3e8-7061ec1951cc', N'stingray1126@gmail.com', 0, N'AMI6KRPuU3D16UMI2GGuqP1E9gF80cSsRFm3YibADqNmgDwE/ysYf1S9/D7D3PYi3A==', N'30fdf2bf-5eaa-4964-82a1-5091d5b71f9f', NULL, 0, 0, NULL, 1, 0, N'stingray1126')
INSERT [dbo].[Users] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6c0a4727-a4f3-45bd-abb2-aca485ba76d3', N'aladdinealcala@gmail.com', 0, N'ACIDcrMOiBgfb7ue19+b4KlJg7zzTy4icUl0gzzKpPTaFhLyX+s+WKsistxdfVAOVQ==', N'b65d0c24-8ccb-4d3e-96a7-01857adfd11d', NULL, 0, 0, NULL, 1, 0, N'ata94')
INSERT [dbo].[Users] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'732be7fe-5b7f-4cd5-9ff7-2658fa1d3220', N'ata@gmail.com', 0, N'ALZ+VRTUi1nA1Sqw2dS9LZdWaQ4wdMU+w1jUDm0d4rQc17BGMtbevrtFxprpBBAX4g==', N'adfd5615-8ffc-475d-b2e3-6c1df2a06669', NULL, 0, 0, NULL, 1, 0, N'ata')
INSERT [dbo].[Users] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'db678ee4-23ec-48ae-9485-1c23bb7cc2b9', N'ata947@gmail.com', 0, N'AAxFXDJ/tR+V+xr9PxQJqUE+OVRAihUJOLMZnIUMbpIpSjuk7rptOihBbDKiDJny6Q==', N'40974f7d-ab56-4637-a326-6806b55eb0a1', NULL, 0, 0, NULL, 1, 0, N'ata947')
/****** Object:  Index [IX_Customer]    Script Date: 3/23/2019 6:53:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_Customer] ON [dbo].[Customer]
(
	[c_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Book_Discount]  WITH CHECK ADD  CONSTRAINT [FK_Book_Discount_Bookings] FOREIGN KEY([trn_Id])
REFERENCES [dbo].[Bookings] ([trn_Id])
GO
ALTER TABLE [dbo].[Book_Discount] CHECK CONSTRAINT [FK_Book_Discount_Bookings]
GO
ALTER TABLE [dbo].[Book_Discount]  WITH CHECK ADD  CONSTRAINT [FK_Book_Discount_Discount] FOREIGN KEY([disc_Id])
REFERENCES [dbo].[Discount] ([disc_Id])
GO
ALTER TABLE [dbo].[Book_Discount] CHECK CONSTRAINT [FK_Book_Discount_Discount]
GO
ALTER TABLE [dbo].[Book_Menus]  WITH CHECK ADD  CONSTRAINT [FK_Book_Menus_Bookings] FOREIGN KEY([trn_Id])
REFERENCES [dbo].[Bookings] ([trn_Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Book_Menus] CHECK CONSTRAINT [FK_Book_Menus_Bookings]
GO
ALTER TABLE [dbo].[Book_Menus]  WITH CHECK ADD  CONSTRAINT [FK_Book_Menus_Menu] FOREIGN KEY([menuid])
REFERENCES [dbo].[Menu] ([menuid])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Book_Menus] CHECK CONSTRAINT [FK_Book_Menus_Menu]
GO
ALTER TABLE [dbo].[BookingAddons]  WITH CHECK ADD  CONSTRAINT [FK_BookingAddons_Bookings] FOREIGN KEY([trn_Id])
REFERENCES [dbo].[Bookings] ([trn_Id])
GO
ALTER TABLE [dbo].[BookingAddons] CHECK CONSTRAINT [FK_BookingAddons_Bookings]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_Customer] FOREIGN KEY([c_Id])
REFERENCES [dbo].[Customer] ([c_Id])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_Customer]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_Packages] FOREIGN KEY([p_id])
REFERENCES [dbo].[Packages] ([p_id])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_Packages]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_ServiceType] FOREIGN KEY([typeofservice])
REFERENCES [dbo].[ServiceType] ([serviceId])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_ServiceType]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_Menu_CourseCategory] FOREIGN KEY([CourserId])
REFERENCES [dbo].[CourseCategory] ([CourserId])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_Menu_CourseCategory]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_Menu_Department] FOREIGN KEY([deptId])
REFERENCES [dbo].[Department] ([deptId])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_Menu_Department]
GO
ALTER TABLE [dbo].[PackageAreaCoverage]  WITH CHECK ADD  CONSTRAINT [FK_PackageAreaCoverage_Areas] FOREIGN KEY([aID])
REFERENCES [dbo].[Areas] ([aID])
GO
ALTER TABLE [dbo].[PackageAreaCoverage] CHECK CONSTRAINT [FK_PackageAreaCoverage_Areas]
GO
ALTER TABLE [dbo].[PackageAreaCoverage]  WITH CHECK ADD  CONSTRAINT [FK_PackageAreaCoverage_Packages] FOREIGN KEY([p_id])
REFERENCES [dbo].[Packages] ([p_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PackageAreaCoverage] CHECK CONSTRAINT [FK_PackageAreaCoverage_Packages]
GO
ALTER TABLE [dbo].[PackageBody]  WITH CHECK ADD  CONSTRAINT [FK_PackageBody_Packages] FOREIGN KEY([p_id])
REFERENCES [dbo].[Packages] ([p_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PackageBody] CHECK CONSTRAINT [FK_PackageBody_Packages]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Bookings] FOREIGN KEY([trn_Id])
REFERENCES [dbo].[Bookings] ([trn_Id])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Bookings]
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD  CONSTRAINT [FK_Reservations_Customer] FOREIGN KEY([c_Id])
REFERENCES [dbo].[Customer] ([c_Id])
GO
ALTER TABLE [dbo].[Reservations] CHECK CONSTRAINT [FK_Reservations_Customer]
GO
ALTER TABLE [dbo].[UserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserClaims_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClaims] CHECK CONSTRAINT [FK_dbo.UserClaims_dbo.Users_UserId]
GO
ALTER TABLE [dbo].[UserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserLogins_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLogins] CHECK CONSTRAINT [FK_dbo.UserLogins_dbo.Users_UserId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRoles_dbo.Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_dbo.UserRoles_dbo.Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRoles_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_dbo.UserRoles_dbo.Users_UserId]
GO
/****** Object:  StoredProcedure [dbo].[Generate_MenuCode]    Script Date: 3/23/2019 6:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Generate_MenuCode]
	
	(@series INT OUTPUT)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select @series=(SELECT CAST(RIGHT(MAX(menuid), 4) AS INT)FROM dbo.Menu)
	SET @series=@series+1
	select @series

END

GO
ALTER AUTHORIZATION ON [dbo].[Generate_MenuCode] TO  SCHEMA OWNER 
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[48] 4[21] 2[5] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Book_Menus"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 209
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Menu"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 227
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CourseCategory"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 224
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 5655
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CourseCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CourseCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[21] 2[17] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PackageAreaCoverage"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 211
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Areas"
            Begin Extent = 
               Top = 14
               Left = 357
               Bottom = 166
               Right = 527
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[11] 2[21] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Book_Menus"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 211
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CourseCategory"
            Begin Extent = 
               Top = 0
               Left = 610
               Bottom = 211
               Right = 785
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Menu"
            Begin Extent = 
               Top = 0
               Left = 435
               Bottom = 211
               Right = 604
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_3'
GO
USE [master]
GO
ALTER DATABASE [Pegasus] SET  READ_WRITE 
GO
