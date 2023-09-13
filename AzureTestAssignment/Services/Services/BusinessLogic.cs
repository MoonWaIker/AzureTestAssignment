using System.Net.Mail;
using AzureTestAssignment.Services.Interfaces;
using AzureTestAssignment.Services.Requests;

namespace AzureTestAssignment.Services.Services
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly IUploader uploader;

        public BusinessLogic(IServiceProvider serviceProvider)
        {
            uploader = serviceProvider.GetRequiredService<IUploader>();
        }

        public void ProcessRequest(UploadRequest request)
        {
            if (request.File is null || request.File.Length <= 0)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (Path.GetExtension(request.File.FileName) != ".docx")
            {
                throw new ArgumentException("File must be 'docx' extension", nameof(request));
            }
            if (string.IsNullOrEmpty(request.Email))
            {
                throw new ArgumentNullException(nameof(request));
            }
            _ = new MailAddress(request.Email);

            uploader.Upload(request.File);
        }
    }
}
