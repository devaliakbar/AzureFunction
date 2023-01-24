
namespace BlogAppFunction.Model
{
    public class CreateBlogDto
    {
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public bool IsPrivate { get; set; }
    }
}