using AzureTestAssignment.Services.Requests;

namespace AzureTestAssignment.Services.Interfaces
{
    public interface IBusinessLogic
    {
        public void ProcessRequest(UploadRequest request);
    }
}
