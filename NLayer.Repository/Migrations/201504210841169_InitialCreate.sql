IF schema_id('auth') IS NULL
    EXECUTE('CREATE SCHEMA [auth]')
CREATE TABLE [auth].[RoleGroup] (
    [Id] [uniqueidentifier] NOT NULL,
    [Name] [nvarchar](20),
    [Description] [nvarchar](255),
    [SortOrder] [int] NOT NULL,
    [Created] [datetime] NOT NULL,
    CONSTRAINT [PK_auth.RoleGroup] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Name] ON [auth].[RoleGroup]([Name])
CREATE TABLE [auth].[Role] (
    [Id] [uniqueidentifier] NOT NULL,
    [Name] [nvarchar](20),
    [Description] [nvarchar](255),
    [SortOrder] [int] NOT NULL,
    [Created] [datetime] NOT NULL,
    [RoleGroup_Id] [uniqueidentifier] NOT NULL,
    CONSTRAINT [PK_auth.Role] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Name] ON [auth].[Role]([Name])
CREATE TABLE [auth].[User] (
    [Id] [uniqueidentifier] NOT NULL,
    [Name] [nvarchar](20),
    [LoginName] [nvarchar](80),
    [LoginPwd] [nvarchar](50),
    [Email] [nvarchar](50),
    [Created] [datetime] NOT NULL,
    CONSTRAINT [PK_auth.User] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Email] ON [auth].[User]([Email])
CREATE UNIQUE INDEX [IX_LoginName] ON [auth].[User]([LoginName])
CREATE TABLE [auth].[Menu] (
    [Id] [uniqueidentifier] NOT NULL,
    [Name] [nvarchar](20),
    [Code] [nvarchar](255),
    [Url] [nvarchar](255),
    [Module] [nvarchar](50),
    [SortOrder] [int] NOT NULL,
    [Created] [datetime] NOT NULL,
    CONSTRAINT [PK_auth.Menu] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Url] ON [auth].[Menu]([Url])
CREATE TABLE [auth].[Permission] (
    [Id] [uniqueidentifier] NOT NULL,
    [Name] [nvarchar](20),
    [Code] [nvarchar](255),
    [ActionUrl] [nvarchar](255),
    [SortOrder] [int] NOT NULL,
    [Created] [datetime] NOT NULL,
    [Menu_Id] [uniqueidentifier] NOT NULL,
    CONSTRAINT [PK_auth.Permission] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ActionUrl] ON [auth].[Permission]([ActionUrl])
CREATE TABLE [auth].[Role_User] (
    [User_Id] [uniqueidentifier] NOT NULL,
    [Role_Id] [uniqueidentifier] NOT NULL,
    CONSTRAINT [PK_auth.Role_User] PRIMARY KEY ([User_Id], [Role_Id])
)
CREATE INDEX [IX_User_Id] ON [auth].[Role_User]([User_Id])
CREATE INDEX [IX_Role_Id] ON [auth].[Role_User]([Role_Id])
CREATE TABLE [auth].[User_Permission] (
    [User_Id] [uniqueidentifier] NOT NULL,
    [Permission_Id] [uniqueidentifier] NOT NULL,
    CONSTRAINT [PK_auth.User_Permission] PRIMARY KEY ([User_Id], [Permission_Id])
)
CREATE INDEX [IX_User_Id] ON [auth].[User_Permission]([User_Id])
CREATE INDEX [IX_Permission_Id] ON [auth].[User_Permission]([Permission_Id])
CREATE TABLE [auth].[Role_Permission] (
    [Role_Id] [uniqueidentifier] NOT NULL,
    [Permission_Id] [uniqueidentifier] NOT NULL,
    CONSTRAINT [PK_auth.Role_Permission] PRIMARY KEY ([Role_Id], [Permission_Id])
)
CREATE INDEX [IX_Role_Id] ON [auth].[Role_Permission]([Role_Id])
CREATE INDEX [IX_Permission_Id] ON [auth].[Role_Permission]([Permission_Id])
ALTER TABLE [auth].[Role] ADD CONSTRAINT [FK_auth.Role_auth.RoleGroup_RoleGroup_Id] FOREIGN KEY ([RoleGroup_Id]) REFERENCES [auth].[RoleGroup] ([Id]) ON DELETE CASCADE
ALTER TABLE [auth].[Permission] ADD CONSTRAINT [FK_auth.Permission_auth.Menu_Menu_Id] FOREIGN KEY ([Menu_Id]) REFERENCES [auth].[Menu] ([Id]) ON DELETE CASCADE
ALTER TABLE [auth].[Role_User] ADD CONSTRAINT [FK_auth.Role_User_auth.User_User_Id] FOREIGN KEY ([User_Id]) REFERENCES [auth].[User] ([Id]) ON DELETE CASCADE
ALTER TABLE [auth].[Role_User] ADD CONSTRAINT [FK_auth.Role_User_auth.Role_Role_Id] FOREIGN KEY ([Role_Id]) REFERENCES [auth].[Role] ([Id]) ON DELETE CASCADE
ALTER TABLE [auth].[User_Permission] ADD CONSTRAINT [FK_auth.User_Permission_auth.User_User_Id] FOREIGN KEY ([User_Id]) REFERENCES [auth].[User] ([Id]) ON DELETE CASCADE
ALTER TABLE [auth].[User_Permission] ADD CONSTRAINT [FK_auth.User_Permission_auth.Permission_Permission_Id] FOREIGN KEY ([Permission_Id]) REFERENCES [auth].[Permission] ([Id]) ON DELETE CASCADE
ALTER TABLE [auth].[Role_Permission] ADD CONSTRAINT [FK_auth.Role_Permission_auth.Role_Role_Id] FOREIGN KEY ([Role_Id]) REFERENCES [auth].[Role] ([Id]) ON DELETE CASCADE
ALTER TABLE [auth].[Role_Permission] ADD CONSTRAINT [FK_auth.Role_Permission_auth.Permission_Permission_Id] FOREIGN KEY ([Permission_Id]) REFERENCES [auth].[Permission] ([Id]) ON DELETE CASCADE
CREATE TABLE [dbo].[__MigrationHistory] (
    [MigrationId] [nvarchar](150) NOT NULL,
    [ContextKey] [nvarchar](300) NOT NULL,
    [Model] [varbinary](max) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201504210841169_InitialCreate', N'NLayer.Repository.Migrations.Configuration',  0x1F8B0800000000000400CD554D53DB3010BD77A6FFC1A33B51122E2DE3C0A449E8304348A70EF42CECB5D1A0AF4A6B06FFB61EFA93FA1758277612921202D3438F92566FDFBE7D5AFDF9F53B3E7BD42A7A001FA43503D6EB74590426B59934C58095981F7D6267A71F3FC4934C3F46376DDC711D47374D18B03B4477C27948EF408BD0D132F536D81C3BA9D55C6496F7BBDDCFBCD7E340108CB0A228FE5E1A941A160B5A8EAC49C16129D4D466A042B34F27C90235BA121A8213290CD8443BAC5834545250F20454CE22618C458144EDE43A4082DE9A2271B421D4BC724071B950011ACA27EBF043D977FB357BBEBED842A56540ABDF08D83B6EE4E0DBD7DF252A5BC945824D4858AC485014D2805F2837605797A2027F6D24CEF21FD6DFB3683B732DDC48F9462EF425A9C5D77DE0CB46B40DE32F742C9E0AE7C83A1B1D6C76A264D9BED151F2F622F51283A7615FADAB4C68BD2860EB945213D373E9038E058A5B51FB6194E99DB05DAD36756892AC84D82A386E92BFEEE31D36CB10167DF3F641663593A40A08BA530774929F6AA424185C074C859139049CDB7B20E391AFFA5BEFE2FFF1280F21530718F52F3DDAE3C25DB163BE395AE231045910FAC6A03190D655AC41DB980B935B92D681C72A01DCE4DA86B4C70DD929A0C888E7D0A3CC458A749C4208640716DD08552EA6D52D64176656A22B711802E85B553DAF697FFEC5537BCE399EB97A15FE45094453520930335F4AA9B215EFF3E5BCE40740D42DFB0AB4BF70070D5F822BAA15D29535070235F28DC181C9C8E973D04E11589899443CC07BB8D150BB8442A455FB665E0679BD11CF658FC752145EE8D060ACEFD7DF25AFFFCBD327A64385ED61070000 , N'6.1.3-40302')