CREATE TABLE [dbo].[Application_Pending_Requests]
(
[id] [int] NULL
) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'When a user creates a request that requires approval of another user', 'SCHEMA', N'dbo', 'TABLE', N'Application_Pending_Requests', NULL, NULL
GO
