USE [boomrang]
GO
CREATE SCHEMA [PersonInfo]
GO
CREATE SCHEMA [Registry]
GO
CREATE SCHEMA [Security]
GO
CREATE SCHEMA [Setting]
GO
CREATE SCHEMA [Workshop]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PersonInfo].[Address](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[PersonId] [int] NOT NULL,
	[ZipCode] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[UserChangeId] [int] NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PersonInfo].[Email](
	[EmailId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[PersonId] [int] NOT NULL,
	[UserChangeId] [int] NOT NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Email] PRIMARY KEY CLUSTERED 
(
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PersonInfo].[Person](
	[PersonId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Family] [nvarchar](50) NOT NULL,
	[Father] [nvarchar](50) NOT NULL,
	[Mother] [nvarchar](50) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [bit] NOT NULL,
	[TypeOfRelation] [nvarchar](50) NULL,
	[IsInViber] [bit] NULL,
	[NationalNumber] [nvarchar](10) NOT NULL,
	[UserChangeId] [int] NOT NULL,
	[Picture] [image] NULL,
	[LiveWith] [nvarchar](500) NULL,
	[TakeChild] [nvarchar](500) NULL,
 CONSTRAINT [PK_Person_1] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PersonInfo].[Phone](
	[PhoneId] [int] IDENTITY(1,1) NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[PersonId] [int] NOT NULL,
	[UserChangeId] [int] NOT NULL,
 CONSTRAINT [PK_Phone] PRIMARY KEY CLUSTERED 
(
	[PhoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Registry].[Pay](
	[PayId] [int] IDENTITY(1,1) NOT NULL,
	[RegisterId] [int] NOT NULL,
	[DateOfPay] [date] NOT NULL,
	[TypeOfPay] [int] NULL,
	[AccountNumber] [nvarchar](50) NULL,
	[Price] [int] NOT NULL,
	[UserChangeId] [int] NOT NULL,
 CONSTRAINT [PK_Pay Fee] PRIMARY KEY CLUSTERED 
(
	[PayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Registry].[Registery](
	[RegisteryId] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[DateOfRegister] [date] NULL,
	[WorkshopId] [int] NOT NULL,
	[UserChangeId] [int] NOT NULL,
	[NumberOfSessionRegister] [int] NOT NULL,
	[DescriptionOfSessionRegister] [nvarchar](50) NULL,
	[Discount] [int] NOT NULL,
 CONSTRAINT [PK_Registery] PRIMARY KEY CLUSTERED 
(
	[RegisteryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Security].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[UserChangeId] [int] NULL,
	[IsSupperAdmin] [bit] NOT NULL,
 CONSTRAINT [Pk_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Setting].[Settings](
	[SettingsId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](100) NOT NULL,
	[UserChangeId] [int] NOT NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[SettingsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Workshop].[Teacher](
	[TeacherId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Family] [nvarchar](50) NOT NULL,
	[Field] [nvarchar](3050) NULL,
	[UserChangeId] [int] NOT NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Workshop].[Term](
	[TermId] [int] IDENTITY(1,1) NOT NULL,
	[Season] [nvarchar](50) NULL,
	[Year] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsCurrentTerm] [bit] NOT NULL,
	[UserChangeId] [int] NOT NULL,
 CONSTRAINT [PK_Term] PRIMARY KEY CLUSTERED 
(
	[TermId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Workshop].[Workshop](
	[WorkshopId] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[TermId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[TeacherId] [int] NULL,
	[Price] [int] NOT NULL,
	[UserChangeId] [int] NOT NULL,
	[NumberOfSession] [int] NOT NULL,
	[TeacherPortion] [int] NOT NULL,
 CONSTRAINT [PK_Kargah] PRIMARY KEY CLUSTERED 
(
	[WorkshopId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [Registry].[Registery] ADD  CONSTRAINT [DF_Registery_NumberOfSessionRegister]  DEFAULT ((0)) FOR [NumberOfSessionRegister]
GO
ALTER TABLE [Registry].[Registery] ADD  CONSTRAINT [DF_Registery_Discount]  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [Security].[User] ADD  CONSTRAINT [DF_User_Username]  DEFAULT (N'_') FOR [Username]
GO
ALTER TABLE [Security].[User] ADD  CONSTRAINT [DF_User_IsSupperAdmin]  DEFAULT ((0)) FOR [IsSupperAdmin]
GO
ALTER TABLE [Workshop].[Workshop] ADD  CONSTRAINT [DF_Workshop_NumberOfSession]  DEFAULT ((0)) FOR [NumberOfSession]
GO
ALTER TABLE [Workshop].[Workshop] ADD  CONSTRAINT [DF_Workshop_TeacherPortion]  DEFAULT ((0)) FOR [TeacherPortion]
GO
ALTER TABLE [PersonInfo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Person] FOREIGN KEY([PersonId])
REFERENCES [PersonInfo].[Person] ([PersonId])
GO
ALTER TABLE [PersonInfo].[Address] CHECK CONSTRAINT [FK_Address_Person]
GO
ALTER TABLE [PersonInfo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_User] FOREIGN KEY([UserChangeId])
REFERENCES [Security].[User] ([UserId])
GO
ALTER TABLE [PersonInfo].[Address] CHECK CONSTRAINT [FK_Address_User]
GO
ALTER TABLE [PersonInfo].[Email]  WITH CHECK ADD  CONSTRAINT [FK_Email_Person] FOREIGN KEY([PersonId])
REFERENCES [PersonInfo].[Person] ([PersonId])
GO
ALTER TABLE [PersonInfo].[Email] CHECK CONSTRAINT [FK_Email_Person]
GO
ALTER TABLE [PersonInfo].[Email]  WITH CHECK ADD  CONSTRAINT [FK_Email_User] FOREIGN KEY([UserChangeId])
REFERENCES [Security].[User] ([UserId])
GO
ALTER TABLE [PersonInfo].[Email] CHECK CONSTRAINT [FK_Email_User]
GO
ALTER TABLE [PersonInfo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_User] FOREIGN KEY([UserChangeId])
REFERENCES [Security].[User] ([UserId])
GO
ALTER TABLE [PersonInfo].[Person] CHECK CONSTRAINT [FK_Person_User]
GO
ALTER TABLE [PersonInfo].[Phone]  WITH CHECK ADD  CONSTRAINT [FK_Phone_Person] FOREIGN KEY([PersonId])
REFERENCES [PersonInfo].[Person] ([PersonId])
GO
ALTER TABLE [PersonInfo].[Phone] CHECK CONSTRAINT [FK_Phone_Person]
GO
ALTER TABLE [PersonInfo].[Phone]  WITH CHECK ADD  CONSTRAINT [FK_Phone_User] FOREIGN KEY([UserChangeId])
REFERENCES [Security].[User] ([UserId])
GO
ALTER TABLE [PersonInfo].[Phone] CHECK CONSTRAINT [FK_Phone_User]
GO
ALTER TABLE [Registry].[Pay]  WITH CHECK ADD  CONSTRAINT [FK_Pay Fee_Registery] FOREIGN KEY([RegisterId])
REFERENCES [Registry].[Registery] ([RegisteryId])
GO
ALTER TABLE [Registry].[Pay] CHECK CONSTRAINT [FK_Pay Fee_Registery]
GO
ALTER TABLE [Registry].[Pay]  WITH CHECK ADD  CONSTRAINT [FK_Pay_User] FOREIGN KEY([UserChangeId])
REFERENCES [Security].[User] ([UserId])
GO
ALTER TABLE [Registry].[Pay] CHECK CONSTRAINT [FK_Pay_User]
GO
ALTER TABLE [Registry].[Registery]  WITH CHECK ADD  CONSTRAINT [FK_Registery_Person] FOREIGN KEY([PersonId])
REFERENCES [PersonInfo].[Person] ([PersonId])
GO
ALTER TABLE [Registry].[Registery] CHECK CONSTRAINT [FK_Registery_Person]
GO
ALTER TABLE [Registry].[Registery]  WITH CHECK ADD  CONSTRAINT [FK_Registery_User] FOREIGN KEY([UserChangeId])
REFERENCES [Security].[User] ([UserId])
GO
ALTER TABLE [Registry].[Registery] CHECK CONSTRAINT [FK_Registery_User]
GO
ALTER TABLE [Registry].[Registery]  WITH CHECK ADD  CONSTRAINT [FK_Registery_Workshop] FOREIGN KEY([WorkshopId])
REFERENCES [Workshop].[Workshop] ([WorkshopId])
GO
ALTER TABLE [Registry].[Registery] CHECK CONSTRAINT [FK_Registery_Workshop]
GO
ALTER TABLE [Setting].[Settings]  WITH CHECK ADD  CONSTRAINT [FK_Settings_User] FOREIGN KEY([UserChangeId])
REFERENCES [Security].[User] ([UserId])
GO
ALTER TABLE [Setting].[Settings] CHECK CONSTRAINT [FK_Settings_User]
GO
ALTER TABLE [Workshop].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_Teacher_User] FOREIGN KEY([UserChangeId])
REFERENCES [Security].[User] ([UserId])
GO
ALTER TABLE [Workshop].[Teacher] CHECK CONSTRAINT [FK_Teacher_User]
GO
ALTER TABLE [Workshop].[Term]  WITH CHECK ADD  CONSTRAINT [FK_Term_User] FOREIGN KEY([UserChangeId])
REFERENCES [Security].[User] ([UserId])
GO
ALTER TABLE [Workshop].[Term] CHECK CONSTRAINT [FK_Term_User]
GO
ALTER TABLE [Workshop].[Workshop]  WITH CHECK ADD  CONSTRAINT [FK_Workshop_Teacher] FOREIGN KEY([TeacherId])
REFERENCES [Workshop].[Teacher] ([TeacherId])
GO
ALTER TABLE [Workshop].[Workshop] CHECK CONSTRAINT [FK_Workshop_Teacher]
GO
ALTER TABLE [Workshop].[Workshop]  WITH CHECK ADD  CONSTRAINT [FK_Workshop_Term] FOREIGN KEY([TermId])
REFERENCES [Workshop].[Term] ([TermId])
GO
ALTER TABLE [Workshop].[Workshop] CHECK CONSTRAINT [FK_Workshop_Term]
GO
ALTER TABLE [Workshop].[Workshop]  WITH CHECK ADD  CONSTRAINT [FK_Workshop_User] FOREIGN KEY([UserChangeId])
REFERENCES [Security].[User] ([UserId])
GO
ALTER TABLE [Workshop].[Workshop] CHECK CONSTRAINT [FK_Workshop_User]
GO
