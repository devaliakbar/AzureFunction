using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace BlogAppFunction.Controller
{
    public class Init
    {
        private readonly CosmosClient CosmosClient;

        public Init(CosmosClient cosmosClient)
        {
            CosmosClient = cosmosClient;
        }

        [FunctionName("Init")]
        public async Task<ActionResult> CreateBlog([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, ILogger log)
        {
            Database database = await CosmosClient.CreateDatabaseIfNotExistsAsync("BlogApp");
            await database.CreateContainerIfNotExistsAsync("blogs", "/partitionKey");

            return new JsonResult("{\"status\":\"Success\"}");
        }
    }
}