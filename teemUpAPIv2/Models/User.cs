namespace teemUpAPIv2.Models
{
    public class User
    {
        public int id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
         

    }
}
