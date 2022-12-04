using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FileManager
    {
        public async Task SaveImageAsync(IFormFile file, string path)
        {
            var dirInfo = new DirectoryInfo(path);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            var fullPath = Path.Combine(path, $"{file.FileName}");

            await using var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);

            await file.CopyToAsync(fileStream);
        }

        public void DeleteImage(IFormFile file, string path)
        {
            var dirInfo = new DirectoryInfo(path);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            var fullPath = Path.Combine(path, $"{file.FileName}");

            var fileStream = new FileInfo(fullPath);

            if (fileStream.Exists)
            {
                fileStream.Delete();
            }
        }
    }
}
