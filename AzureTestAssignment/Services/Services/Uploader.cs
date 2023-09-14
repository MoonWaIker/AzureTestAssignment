using Azure.Identity;
using Azure.Storage.Blobs;
using AzureTestAssignment.Services.Interfaces;

namespace AzureTestAssignment.Services.Services
{
    public class Uploader : IUploader
    {
        private readonly string connectionString = "https://azuretestassignment.blob.core.windows.net/";
        private readonly string containerName = "blobcontainer";

        public void Upload(IFormFile file)
        {
            BlobServiceClient blobServiceClient = new(new Uri(connectionString), new DefaultAzureCredential());

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            BlobClient blobClient = containerClient.GetBlobClient(file.FileName);

            Stream fileStream = file.OpenReadStream();
            blobClient.Upload(fileStream, true);
            fileStream.Close();
        }
    }
}
