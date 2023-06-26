using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProEventos.API.Helpers.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.API.Helpers
{
    public class Util : IUtil
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Util(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void DeleteImage(string imageName, string destino)
        {
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, @$"Resources/{destino}", imageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        public async Task<string> SaveImage(IFormFile imageFile, string destino)
        {
            var imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName)
                                           .Take(10)
                                           .ToArray()
            ).Replace(' ', '-');

            imageName = $"{imageName}-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}{Path.GetExtension(imageFile.FileName)}";

            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, @$"Resources/{destino}", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }
     
    }
}
