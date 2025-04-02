CREATE TABLE [dbo].[User]
(
	Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT newId(), 
    [Username] NVARCHAR(50) NOT NULL, 
    [Firstname] NVARCHAR(50) NOT NULL, 
    [Lastname] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(MAX) NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [ProfilePictureId] UNIQUEIDENTIFIER NULL, 
    [Notifications] BIT NOT NULL,
    CONSTRAINT [FK_User_File] FOREIGN KEY ([ProfilePictureId]) REFERENCES [Files]([Id]) ON DELETE CASCADE
)
