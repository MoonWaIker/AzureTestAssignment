namespace AzureTestAssignment.Services.Requests
{
    public class UploadRequest
    {
        public required IFormFile File { get; set; }

        public required string Email { get; set; }
    }
}
