CREATE TABLE [dbo].[Module_Access_Company]
(
[Id] [int] NOT NULL,
[CompanyId] [int] NULL,
[ModuleId] [int] NULL,
[Permit_Remote_Login] [bit] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Module_Access_Company] ADD CONSTRAINT [PK_Module_Access_Company] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'Can the user access this company outside of work', 'SCHEMA', N'dbo', 'TABLE', N'Module_Access_Company', 'COLUMN', N'Permit_Remote_Login'
GO
