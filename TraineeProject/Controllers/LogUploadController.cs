using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace TraineeProject.Controllers
{
    [Route("api/log-upload")]
    [ApiController]
    public class LogUploadController : ControllerBase
    {
        private readonly BlobServiceClient blobServiceClient;
        public LogUploadController(BlobServiceClient blobServiceClient)
        {
            this.blobServiceClient = blobServiceClient;
        }

        [HttpPost]
        public async Task<IActionResult> uploadLog([FromForm] IFormFile file)
        {
            if(file.Length > 0)
            {
                using(MemoryStream memStream = new MemoryStream())
                {
                    await file.CopyToAsync(memStream);
                    var containerClient = this.blobServiceClient.GetBlobContainerClient("traineeprojectblobstorage");
                    containerClient.UploadBlob(Path.GetRandomFileName(), memStream);
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
