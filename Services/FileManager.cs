using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FileManager : ControllerBase
    {
        public static async Task SaveImageAsync(IFormFile file, string path)
        {
            var dirInfo = new DirectoryInfo(path);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            var fullPath = Path.Combine(path, $"{file.FileName}");

            await using var fileStream = new FileStream(fullPath, FileMode.Create);

            await file.CopyToAsync(fileStream);
        }

        public async Task<IActionResult> GetImageAsync(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                byte[] reader = await System.IO.File.ReadAllBytesAsync(filePath);

                return File(reader, "image/png");
            }

            return new BadRequestObjectResult(ModelState);
        }

        public static void DeleteImage(string path)
        {
            var fileStream = new FileInfo(path);

            if (fileStream.Exists)
            {
                fileStream.Delete();
            }
        }
    }
}
