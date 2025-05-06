using Functions.Server.Interfaces;
using Functions.Server.Model;

namespace Functions.Server.Services.File
{
    public class FilesService(IRepository<Files> fileRepository)
    {
        public async Task<Guid?> SaveFileAsync(string ProfilePictureBase64, string FileName, string FileType)
        {
            try
            {
                var fileContent = new FileContent
                {
                    Id = Guid.NewGuid(), // think about it
                    Base64Content = ProfilePictureBase64
                };

                var fileRecord = new Files
                {
                    Id = Guid.NewGuid(),
                    FileName = FileName,
                    FileType = FileType,
                    FileContentId = fileContent.Id,
                    FileContent = fileContent
                };

                await fileRepository.AddAsync(fileRecord);
                return fileRecord.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}"); // logger implementation?
                return null;
            }
        }
    }
}
