using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace QueueTriggerFunctionApp
{
    public class QueueTriggerFunction
    {
        [FunctionName("QueueTrigger")]
        public void Run([QueueTrigger("udemy-myqueue", Connection = "LocalAzureStorage")] string myQueueMessage, ILogger log, [Blob("udemy-pictures/{queueTrigger}", FileAccess.Read, Connection = "LocalAzureStorage")] CloudBlockBlob cloudBlockBlob)
        {
            log.LogInformation("Blob Name: " + cloudBlockBlob.Name);
            log.LogInformation("Blob Type: " + cloudBlockBlob.BlobType.ToString());
            log.LogInformation("Blob ContentType: " + cloudBlockBlob.Properties.ContentType);
        }
    }
}