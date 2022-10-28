using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BlobTriggerFunctionApp
{
    public class BlobTriggerFunction
    {
        [FunctionName("ResizeFunction")]
        public void ResizeFunction([BlobTrigger("udemy-pictures/{name}", Connection = "LocalAzureStorage")] Stream myBlob, string name, ILogger log, [Blob("udemy-pictures-resize/{name}", FileAccess.Write, Connection = "LocalAzureStorage")] Stream outputBlob)
        {
            var format = Image.DetectFormat(myBlob);
            var resizeImage = Image.Load(myBlob);

            resizeImage.Mutate(x => x.Resize(100, 100));
            resizeImage.Save(outputBlob, format);

            log.LogInformation("Resim resize yapıldı.");
        }
    }
}