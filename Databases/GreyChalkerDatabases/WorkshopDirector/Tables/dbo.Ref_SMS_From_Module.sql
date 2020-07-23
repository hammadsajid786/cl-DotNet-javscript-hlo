CREATE TABLE [dbo].[Ref_SMS_From_Module]
(
[id] [smallint] NOT NULL,
[ModuleName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Ref_SMS_From_Module] ADD CONSTRAINT [PK_Ref_SMS_From_Module] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
