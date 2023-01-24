using Newtonsoft.Json;

namespace BlogAppFunction.Model
{
    public class Blog : CreateBlogDto
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; }
    }
}