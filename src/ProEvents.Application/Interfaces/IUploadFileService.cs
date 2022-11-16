using Microsoft.AspNetCore.Http;

namespace ProEvents.Application.Interfaces;
public interface IUploadFileService
{
    Task<string> SaveImage(IFormFile imgFile, string path);

    void DeleteImage(string imgName, string path);
}

