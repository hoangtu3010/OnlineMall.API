using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMall.API.Services
{
    public class UploadSevices:IUploadSevice
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public UploadSevices(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public async Task<string> SaveImage(IFormFile file)
        {
            try
            {
                string fileName = new String(Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(" ", "_");
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return fileName;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
