using SiteYonetimApp.Models;

namespace SiteYonetimAppServer.Services
{
    public class AuthenticationService
    {
        public User CurrentUser { get; private set; }

        public bool Login(string userName, string password)
        {
            if (User.AdminUser.UserName == userName && User.AdminUser.Password == password)
            {
                CurrentUser = User.AdminUser;
                return true;
            }
            return false;
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public bool IsLoggedIn => CurrentUser != null;
    }
}
