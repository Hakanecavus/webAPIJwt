using System.ComponentModel.DataAnnotations;

namespace teemUpAPIv2.Models
{
    public class User
    {
        [Key]
        public string email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}
