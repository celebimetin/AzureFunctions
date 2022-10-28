using System;
using System.IO;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace TimerTriggerFunctionApp
{
    public class TimerTriggerFunction
    {
        [FunctionName("Function1")]
        public void Run([TimerTrigger("*/3 * * * * *")] TimerInfo myTimer, ILogger log, [Blob("logs/data.txt", FileAccess.Write, Connection = "LocalAzureStorage")] Stream blobStream)
        {
            var ifade = Encoding.UTF8.GetBytes($"Logs: {DateTime.Now}");
            blobStream.Write(ifade, 0, ifade.Length);
            blobStream.Close();
            blobStream.Dispose();

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}