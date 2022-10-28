using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace TimerTriggerFunctionApp
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([TimerTrigger("* * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}