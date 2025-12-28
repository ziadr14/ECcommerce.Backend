using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.Interfaces
{
    public interface IImageService
    {
        Task<List<string>> AddImageAsync(IFormFileCollection files, string folderPath);
        void DeleteImage(string fileName, string folderPath);
    }
}
