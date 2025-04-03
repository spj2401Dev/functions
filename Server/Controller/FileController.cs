using Functions.Server.Interfaces;
using Functions.Server.Model;
using Functions.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Functions.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController(IRepository<Files> fileRepository,
                                IConfiguration configuration) : FunctionsControllerBase(configuration)
    {
        [HttpGet("downloadfile")]
        public async Task<IActionResult> DownloadFile([FromQuery] Guid file)
        {
            var stopwatch = Stopwatch.StartNew();

            var fileEntity = await fileRepository.GetByIdAsync(file);
            if (fileEntity == null || fileEntity.FileContent == null)
            {
                stopwatch.Stop();
                Console.WriteLine($"DownloadFile execution time: {stopwatch.ElapsedMilliseconds} ms");
                return NotFound();
            }

            var fileContent = Convert.FromBase64String(fileEntity.FileContent.Base64Content);
            var memoryStream = new MemoryStream(fileContent);

            stopwatch.Stop();
            Console.WriteLine($"DownloadFile execution time: {stopwatch.ElapsedMilliseconds} ms");

            return File(memoryStream, "application/octet-stream", fileEntity.FileName);
        }
    }
}
