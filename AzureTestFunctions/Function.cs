using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AzureTestFunctions
{
    public static class Function
    {
        [FunctionName("BlobTriggerCSharp")]
        public static async Task RunAsync(
        [BlobTrigger("blobcontainer/{name}", Connection = "connection")] Stream myBlob,
        string name,
        ILogger log)
        {
            log.LogInformation($"C# Blob trigger function processed blob Name: {name}, Size: {myBlob.Length} Bytes");

            try
            {
                var client = new SecretClient(new Uri("https://testassignmentfsecrets.vault.azure.net/"), new DefaultAzureCredential());
                string gridKey = client.GetSecret("SendGridApiKey").Value.Value;

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