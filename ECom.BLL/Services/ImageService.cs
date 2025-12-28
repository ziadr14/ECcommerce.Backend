using ECom.BLL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ECom.BLL.Services
{
    public class ImageService : IImageService
    {
        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string folderPath)
        {
            List<string> savedNames = new List<string>();

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                    string fullPath = Path.Combine(folderPath, fileName);

                    using var stream = new FileStream(fullPath, FileMode.Create);
                    await file.CopyToAsync(stream);

                    savedNames.Add(fileName);
                }
            }

            return savedNames;
        }

        public void DeleteImage(string fileName, string folderPath)
        {
            string fullPath = Path.Combine(folderPath, fileName);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}

