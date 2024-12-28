namespace SiteYonetimApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public static readonly User AdminUser = new User
        {
            Id = 1,
            UserName = "admin@admin.com",
            Password = "admin123" // Güvenlik için hashlenmesi tavsiye edilir.
        };
    }
}
