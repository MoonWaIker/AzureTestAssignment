using Azure.Storage.Blobs;
using AzureTestAssignment.Services.Interfaces;

namespace AzureTestAssignment.Services.Services
{
    public class Uploader : IUploader
    {
        // TODO Make Managed identity
        private const string connectionString = "https://azuretestassignment.blob.core.windows.net/blobcontainer?sp=racwdl&st=2023-09-14T15:29:55Z&se=2023-09-20T23:29:55Z&spr=https&sv=2022-11-02&sr=c&sig=U%2BYqSNzP%2FAYXdK%2Fbnw%2BkBbTz9OHORb6poj5IUZEmgx4%3D";

        public void Upload(IFormFile file)
        {
            BlobContainerClient containerClient = new(new Uri(connectionString));

            BlobClient blobClient = containerClient.GetBlobClient(file.FileName);

            Stream fileStream = file.OpenReadStream();
            blobClient.Upload(fileStream, true);
            fileStream.Close();
        }
    }
}