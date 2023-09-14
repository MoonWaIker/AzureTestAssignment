using Azure.Identity;
using Azure.Storage.Blobs;
using AzureTestAssignment.Services.Interfaces;

namespace AzureTestAssignment.Services.Services
{
    public class Uploader : IUploader
    {
        private const string connectionString = "https://azuretestassignment.blob.core.windows.net/blobcontainer";

        public void Upload(IFormFile file)
        {
            BlobContainerClient containerClient = new(new Uri(connectionString), new DefaultAzureCredential());

            BlobClient blobClient = containerClient.GetBlobClient(file.FileName);

            Stream fileStream = file.OpenReadStream();
            blobClient.Upload(fileStream, true);
            fileStream.Close();
        }
    }
}