using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(BlogAppFunction.Startup))]

namespace BlogAppFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string EndpointUri = System.Environment.GetEnvironmentVariable("EndpointUri", System.EnvironmentVariableTarget.Process);
            string PrimaryKey = System.Environment.GetEnvironmentVariable("PrimaryKey", System.EnvironmentVariableTarget.Process);

            CosmosClient cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions() { ApplicationName = "CosmosDBAndAzureFunction" });

            builder.Services.AddSingleton<CosmosClient>((s) =>
            {
                return cosmosClient;
            });
        }
    }
}

