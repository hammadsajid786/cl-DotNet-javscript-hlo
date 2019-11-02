CREATE TABLE [dbo].[jl_ContactsHistory]
(
[ContacHistoryId] [int] NOT NULL IDENTITY(1, 1),
[Id] [bigint] NOT NULL,
[BusinessId] [int] NOT NULL,
[ContextType] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Date] [datetime] NOT NULL,
[Status] [int] NULL,
[card_Id] [int] NULL,
[resume_Id] [int] NULL,
[fax_Id] [int] NULL,
[email_Id] [int] NULL,
[letter_Id] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_ContactsHistory] ADD CONSTRAINT [PK_jl_ContactsHistory] PRIMARY KEY CLUSTERED  ([ContacHistoryId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_ContactsHistory_jl_BusinessInfo] ON [dbo].[jl_ContactsHistory] ([BusinessId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_ContactsHistory_jl_BusinessCard] ON [dbo].[jl_ContactsHistory] ([card_Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_ContactsHistory_jl_Email] ON [dbo].[jl_ContactsHistory] ([email_Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_ContactsHistory_jl_Fax] ON [dbo].[jl_ContactsHistory] ([fax_Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_ContactsHistory_jl_Users] ON [dbo].[jl_ContactsHistory] ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_ContactsHistory_jl_BusinessLetter] ON [dbo].[jl_ContactsHistory] ([letter_Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_ContactsHistory_jl_Resume] ON [dbo].[jl_ContactsHistory] ([resume_Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_ContactsHistory] ADD CONSTRAINT [FK_jl_ContactsHistory_AspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[jl_ContactsHistory] ADD CONSTRAINT [FK_jl_ContactsHistory_jl_BusinessCard] FOREIGN KEY ([card_Id]) REFERENCES [dbo].[jl_BusinessCard] ([UserDesignId])
GO
ALTER TABLE [dbo].[jl_ContactsHistory] ADD CONSTRAINT [FK_jl_ContactsHistory_jl_BusinessLetter] FOREIGN KEY ([letter_Id]) REFERENCES [dbo].[jl_BusinessLetter] ([LetterPadeDesignId])
GO
ALTER TABLE [dbo].[jl_ContactsHistory] ADD CONSTRAINT [FK_jl_ContactsHistory_jl_Email] FOREIGN KEY ([email_Id]) REFERENCES [dbo].[jl_Email] ([EmailId])
GO
ALTER TABLE [dbo].[jl_ContactsHistory] ADD CONSTRAINT [FK_jl_ContactsHistory_jl_Fax] FOREIGN KEY ([fax_Id]) REFERENCES [dbo].[jl_Fax] ([FaxId])
GO
ALTER TABLE [dbo].[jl_ContactsHistory] ADD CONSTRAINT [FK_jl_ContactsHistory_jl_Resume] FOREIGN KEY ([resume_Id]) REFERENCES [dbo].[jl_Resume] ([ResumeId])
GO
