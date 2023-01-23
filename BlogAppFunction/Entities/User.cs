using System.ComponentModel.DataAnnotations;

namespace BlogAppFunction.Entities
{
        public class User
        {
            [Required]
            public string Id { get; set; }
            [Required]
            public string UserName { get; set; }
            [Required]
            public byte[] PasswordHash { get; set; }
            [Required]
            public byte[] PasswordSalt { get; set; }
        }
}

