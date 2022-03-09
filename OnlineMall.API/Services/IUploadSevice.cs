using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMall.API.Services
{
    public interface IUploadSevice
    {
        Task<string> SaveImage(IFormFile file);
        void DeleteImage(string imageName);
    }
}
