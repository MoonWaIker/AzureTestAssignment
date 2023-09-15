using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AzureTestFunctions
{
    public static class Function
    {
        private const string gridKey = "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";

        [FunctionName("BlobTriggerCSharp")]
        public static async Task RunAsync(
        [BlobTrigger("blobcontainer/{name}")] Stream myBlob,
        string name,
        ILogger log)
        {
            log.LogInformation($"C# Blob trigger function processed blob Name: {name}, Size: {myBlob.Length} Bytes");

            try
            {
                var sendGridClient = new SendGridClient(gridKey);
                var msg = new SendGridMessage();
                msg.SetFrom(new EmailAddress("muhinmihajlo40@gmail.com", "Mykhailo Mukhin"));
                // TODO send to user email
                msg.AddTo(new EmailAddress("muhinmihajlo40@gmail.com"));
                msg.SetSubject("File Uploaded Notification");
                msg.AddContent(MimeType.Text, "Congrat! Your file has been uploaded without errors, explosions and casualties. 🙂");
                // TODO Provide SAS token

                var response = await sendGridClient.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    log.LogInformation("Email sent successfully.");
                }
                else
                {
                    log.LogError($"Failed to send email. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                log.LogError($"An error occurred while sending email: {ex.Message}");
            }
        }
    }
}