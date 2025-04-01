CREATE TABLE [dbo].[Message]
(
	Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT newId(), 
    [CreatorId] UNIQUEIDENTIFIER NOT NULL,
	[EventId] UNIQUEIDENTIFIER NOT NULL, 
    [Likes] INT NOT NULL, 
    [ParentId] UNIQUEIDENTIFIER NULL, 
    [MessageDate] DATETIME2 NOT NULL, 
    [Type] INT NOT NULL, 
    [Text] NVARCHAR(MAX) NOT NULL,
    CONSTRAINT [FK_Message_User] FOREIGN KEY ([CreatorId]) REFERENCES [User]([Id]),
    CONSTRAINT [FK_Message_Event] FOREIGN KEY ([EventId]) REFERENCES [Events]([Id]),
    CONSTRAINT [FK_Message_Message] FOREIGN KEY ([ParentId]) REFERENCES [Message]([Id])

)
