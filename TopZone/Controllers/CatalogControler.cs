using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Text;

namespace TopZone.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CatalogControler : ControllerBase
{
    private readonly string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task UploadCatalogs(IFormFile[] catalogFiles)
    {

        if (!validCatalogImages(catalogFiles))
            return;

        foreach (var file in catalogFiles)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            using var FileCreate = new FileStream($"./wwwroot/images/catalog/{Guid.NewGuid()}{extension}", FileMode.Create, FileAccess.Write);
            await file.CopyToAsync(FileCreate);
        };

        Response.StatusCode = StatusCodes.Status201Created;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<string> GetCatalogs()
    {
        var catalogFolder = new DirectoryInfo("./wwwroot/images/catalog");
        var catalogFiles = catalogFolder.GetFiles().Select(f => f.Name);
        return catalogFiles;
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public void DeleteCatalogs(IEnumerable<string> deleteFiles)
    {
        var catalogFolder = new DirectoryInfo("./wwwroot/images/catalog");
        foreach (var item in catalogFolder.GetFiles())
        {
            if(deleteFiles.Contains(item.Name))
            {
                item.Delete();
            }
        }
    }

    private bool validCatalogImages(IFormFile[] catalogFiles)
    {
        foreach (var file in catalogFiles)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return false;
            }
        }

        return true;
    }
}
