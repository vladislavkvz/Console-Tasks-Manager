namespace TaskManager.Service
{
    using Entities;
    using Repositories;

    public class AuthenticationService
    {
        public static User LoggedUser { get; private set; }

        public static void AuthenticateUser(string username, string password)
        {
            BaseRepo<User> userRepo = new UserRepo("users.txt");
            AuthenticationService.LoggedUser = userRepo.GetAll().Find(u => u.Username == username && u.Password == password);
        }
    }
}