CREATE TABLE [dbo].[jl_Jobs]
(
[JobId] [int] NOT NULL IDENTITY(1, 1),
[PlaceId] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CompanyName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreatedbyUserId] [bigint] NOT NULL,
[CreatedDateTime] [datetime] NOT NULL,
[Title] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Location] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[JobFunctions] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[EmploymentType] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CompanyIndustries] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SeniorityLevel] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ResumeEmail] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[JobDescription] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Active] [bit] NOT NULL,
[ThirdPartyId] [int] NULL,
[ThirdPartySource] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_Jobs] ADD CONSTRAINT [PK_jl_Jobs] PRIMARY KEY CLUSTERED  ([JobId]) ON [PRIMARY]
GO
