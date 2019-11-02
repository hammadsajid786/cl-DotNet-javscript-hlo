CREATE TABLE [dbo].[jl_Users]
(
[UserId] [bigint] NOT NULL IDENTITY(1, 1),
[PasswordHint] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Password] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Status] [bit] NOT NULL,
[DateCreated] [datetime] NOT NULL,
[FirstName] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[State] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Country] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Title] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[QRcodeURL] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[RoleId] [int] NULL,
[ImagePath] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[phoneNo] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsSendSMS] [bit] NULL,
[IsActive] [bit] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_Users] ADD CONSTRAINT [PK_jl_Users] PRIMARY KEY CLUSTERED  ([UserId]) ON [PRIMARY]
GO
