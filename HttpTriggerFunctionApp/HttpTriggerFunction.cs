using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using HttpTriggerFunctionApp.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Newtonsoft.Json;

namespace HttpTriggerFunctionApp
{
    public class HttpTriggerFunction
    {
        private const string Route = "Products";
        private readonly AppDbContext _dbContext;
        public HttpTriggerFunction(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [FunctionName("GetProducts")]
        public async Task<IActionResult> GetProducts(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = Route)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("tüm ürünleri getir");
            var products = await _dbContext.Products.ToListAsync();

            return new OkObjectResult(products);
        }

        [FunctionName("SaveProducts")]
        public async Task<IActionResult> SaveProducts(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = Route)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("ürün ekle");

            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var newProduct = JsonConvert.DeserializeObject<Product>(body);

            _dbContext.Products.Add(newProduct);
            await _dbContext.SaveChangesAsync();

            return new OkObjectResult(newProduct);
        }

        [FunctionName("UpdateProducts")]
        public async Task<IActionResult> UpdateProducts(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = Route)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("ürün güncelle");

            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var newProduct = JsonConvert.DeserializeObject<Product>(body);

            _dbContext.Products.Update(newProduct);
            await _dbContext.SaveChangesAsync();

            return new NoContentResult();
        }

        [FunctionName("DeleteProducts")]
        public async Task<IActionResult> DeleteProducts(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = Route+"/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("ürün sil");

            var product = await _dbContext.Products.FindAsync(id);

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}