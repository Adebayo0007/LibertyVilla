using LibertyVilla_Server.Service.IService;
using Microsoft.AspNetCore.Components.Forms;

namespace LibertyVilla_Server.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IHttpContextAccessor _httpContextAccessor;
        public FileUpload(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;

        }
        public bool DeleteFile(string fileName)
        {
            try
            {
                var path = $"{_webHostEnvironment.WebRootPath}\\RoomImages\\{fileName}";
                if(File.Exists(path)) 
                {
                    File.Delete(path);
                    return true;
                }
                return false;

            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            try
            {

               FileInfo fileInfo = new FileInfo(file.Name); // for retrieving the file details
                var fileName = Guid.NewGuid().ToString() + fileInfo.Extension; // renaming the file name
                var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\RoomImages"; // directory path
                var fullPath = Path.Combine(folderDirectory, fileName); //retrieving the full path

                var memoryStram = new MemoryStream();
                  await file.OpenReadStream().CopyToAsync(memoryStram); // copying the file to memory stream
                    if(!Directory.Exists(folderDirectory)) 
                    {
                        Directory.CreateDirectory(folderDirectory);
                    }

                    await using(var fs = new FileStream(fullPath,FileMode.Create,FileAccess.Write))
                    {
                       memoryStram.WriteTo(fs); // then writing it back from the memory stream to the fullpath 
                    }
                var url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/";
                var fullPathToReturn = $"{url}RoomImages/{fileName}";
                return fullPathToReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
