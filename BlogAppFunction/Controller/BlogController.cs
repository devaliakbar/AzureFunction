using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;
using System;
using BlogAppFunction.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlogAppFunction.Controller
{
    public class BlogController
    {
        private readonly Container container;
        private const string PartitionKey = "Blogs";
        public BlogController(CosmosClient cosmosClient)
        {
            Database database = cosmosClient.GetDatabase(id: "BlogApp");
            container = database.GetContainer(id: "blogs");
        }

        [FunctionName("CreateBlog")]
        public async Task<ActionResult> CreateBlog([HttpTrigger(AuthorizationLevel.Anonymous, "post")] CreateBlogDto createBlogDto, HttpRequest req, ILogger log)
        {
            Blog blogDto = new Blog()
            {
                Id = Guid.NewGuid().ToString(),
                PartitionKey = PartitionKey,
                BlogTitle = createBlogDto.BlogTitle,
                BlogContent = createBlogDto.BlogContent,
                IsPrivate = createBlogDto.IsPrivate
            };

            ItemResponse<Blog> response = await this.container.CreateItemAsync<Blog>(blogDto, new PartitionKey(PartitionKey));

            return new JsonResult(blogDto);
        }

        [FunctionName("UpdateBlog")]
        public async Task<ActionResult> UpdateBlog([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "blog/{id}")]
       CreateBlogDto updateBlogDto, HttpRequest req, string id, ILogger log)
        {
            ItemResponse<Blog> response = await this.container.ReadItemAsync<Blog>(id, new PartitionKey(PartitionKey));
            Blog itemBody = response.Resource;

            itemBody.BlogTitle = updateBlogDto.BlogTitle;
            itemBody.BlogContent = updateBlogDto.BlogContent;
            itemBody.IsPrivate = updateBlogDto.IsPrivate;
            await this.container.ReplaceItemAsync<Blog>(itemBody, itemBody.Id, new PartitionKey(itemBody.PartitionKey));

            return new JsonResult(itemBody);
        }

        [FunctionName("DeleteBlog")]
        public async Task<ActionResult> DeleteBlog([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "blog/{id}")] HttpRequest req, string id, ILogger log)
        {
            ItemResponse<Blog> response = await this.container.ReadItemAsync<Blog>(id, new PartitionKey(PartitionKey));
            Blog itemBody = response.Resource;

            await this.container.DeleteItemAsync<Blog>(id, new PartitionKey(PartitionKey));
            return new JsonResult(itemBody);
        }

        [FunctionName("GetBlog")]
        public async Task<ActionResult> GetBlog([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "blog/{id}")] HttpRequest req, string id, ILogger log)
        {
            ItemResponse<Blog> response = await this.container.ReadItemAsync<Blog>(id, new PartitionKey(PartitionKey));

            return new JsonResult(response.Resource);
        }

        [FunctionName("GetBlogs")]
        public async Task<ActionResult> GetBlogs([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req, ILogger log)
        {
            var sqlQueryText = "SELECT * FROM c ORDER BY c.BlogTitle ASC";

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Blog> queryResultSetIterator = this.container.GetItemQueryIterator<Blog>(queryDefinition);

            List<Blog> blogs = new List<Blog>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Blog> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Blog blog in currentResultSet)
                {
                    blogs.Add(blog);
                }
            }

            return new JsonResult(blogs);
        }
    }
}

