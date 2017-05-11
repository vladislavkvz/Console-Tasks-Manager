namespace TaskManager.View
{
    using Service;
    using System;
    using System.Threading;

    public class LoginView
    {
        public void View()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("*******************************");
                Console.WriteLine("********** L O G I N **********");
                Console.Write("*** ");
                Console.Write("U S E R N A M E : ");
                string username = Console.ReadLine();
                Console.Write("*** ");
                Console.Write("P A S S W O R D : ");
                string password = Console.ReadLine();

                AuthenticationService.AuthenticateUser(username, password);

                if (AuthenticationService.LoggedUser != null)
                {
                    Console.Clear();
                    Console.WriteLine("*******************************");
                    Console.WriteLine("******* W E L C O M E *********");
                    Console.WriteLine("*******************************");
                    Thread.Sleep(1300);
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("*******************************");
                    Console.WriteLine("INVALID USERNAME OR PASSWORD !!!");
                    Console.WriteLine("*******************************");
                    Thread.Sleep(1300);
                }
            }
        }
    }
}