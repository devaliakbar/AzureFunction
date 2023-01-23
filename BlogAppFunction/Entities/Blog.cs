using System.ComponentModel.DataAnnotations;

namespace BlogAppFunction.Entities
{
        public class Blog
        {
            [Required]
            public string Id { get; set; }
            [Required]
            public User Owner { get; set; }
            [Required]
            public bool IsPrivate { get; set; }
            [Required]
            public string BlogTitle { get; set; }
            [Required]
            public string BlogContent { get; set; }
        }
}

