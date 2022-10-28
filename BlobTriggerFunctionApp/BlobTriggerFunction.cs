using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace BlobTriggerFunctionApp
{
    public class BlobTriggerFunction
    {
        [FunctionName("Function1")]
        public void Run([BlobTrigger("udemy-pictures/{name}", Connection = "LocalAzureStorage")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}