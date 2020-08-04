CREATE TABLE [dbo].[jl_Feedback]
(
[FeedbackId] [int] NOT NULL IDENTITY(1, 1),
[Email] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[FeedbackMessage] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedDateTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_Feedback] ADD CONSTRAINT [PK_jl_Feedback] PRIMARY KEY CLUSTERED  ([FeedbackId]) ON [PRIMARY]
GO
