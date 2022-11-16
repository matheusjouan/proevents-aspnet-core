using Microsoft.AspNetCore.Http;
using ProEvents.Application.Interfaces;

namespace ProEvents.Application.Services;
public class UploadFileService : IUploadFileService
{
    public async Task<string> SaveImage(IFormFile imgFile, string path)
    {
        var fileName = new String(
            Path.GetFileNameWithoutExtension(imgFile.FileName)
            .Take(10)
            .ToArray()
        ).Replace(" ", "-");

        fileName = $"{fileName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imgFile.FileName)}";

        var imgPath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

        using var fileStream = new FileStream(imgPath, FileMode.Create);
        await imgFile.CopyToAsync(fileStream);
        
        return fileName;
    }

    public void DeleteImage(string imgName, string path)
    {
        if (imgName != null)
        {
            var imgPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                path, imgName);

            if (File.Exists(imgPath))
                File.Delete(imgPath);
        }
    }
}