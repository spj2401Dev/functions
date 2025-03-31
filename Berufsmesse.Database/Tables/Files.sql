CREATE TABLE [dbo].[Files] 
(
	Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT newId(), 
    [FileName] NCHAR(255) NULL, 
    [FileType] NCHAR(255) NULL, 
    [FileContentId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_FileContentId FOREIGN KEY (FileContentId) REFERENCES FileContent(Id) ON DELETE CASCADE
)
