namespace TaskManager
{
    using Service;
    using View;

    public class Program
    {
        static void Main()
        {
            LoginView login = new LoginView();
            login.View();

            if (AuthenticationService.LoggedUser.IsAdmin)
            {
                UserManagerView userManager = new UserManagerView();
                userManager.SelectMenu();
            }
            else
            {
                BaseView taskManager = new TaskManagerView();
                taskManager.View();
            }
        }
    }
}