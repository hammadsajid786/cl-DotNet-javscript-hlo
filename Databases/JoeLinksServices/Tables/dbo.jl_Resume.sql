CREATE TABLE [dbo].[jl_Resume]
(
[ResumeId] [int] NOT NULL IDENTITY(1, 1),
[FirstName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StreetAddress] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[City] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StateZipCode] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[HomePhoneNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CellPhoneNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EmailAddress] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ProfessionalSummary] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Highligth1] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Highligth2] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Highligth3] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Highligth4] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Highligth5] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Highligth6] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Highligth7] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Highligth8] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Experience1] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Experience2] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Experience3] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Experience4] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Experience5] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Experience6] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Experience7] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Experience8] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Educationtxtdate] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Education] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Institute] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AdditionalInfo] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ResumeDesign] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[backgroundcolor] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FontSizeNormal] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ResumeDesignPic] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EducationTitle] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[InstituteName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FontSizeHeading] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Experience9] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Experience10] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[State] [varchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Id] [bigint] NULL,
[ResumeName] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DesignType] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_Resume] ADD CONSTRAINT [PK_jl_Resume] PRIMARY KEY CLUSTERED  ([ResumeId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_Resume_jl_Users] ON [dbo].[jl_Resume] ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_Resume] ADD CONSTRAINT [FK_jl_Resume_AspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
GO
