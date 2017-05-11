namespace TaskManager.View
{
    using Entities;
    using Repositories;
    using System;
    using System.Threading;

    public class UserManagerView : BaseView
    {
        public void SelectMenu()
        {
            Console.Clear();
            Console.WriteLine("*******************************");
            Console.WriteLine("********* WORK WITH : *********");
            Console.WriteLine("********* [U]SER MANAGER ******");
            Console.WriteLine("********* [T]ASK MANAGER ******");
            Console.WriteLine("*******************************");
            string choice = Console.ReadLine();
            if ("U" == choice.ToUpper())
            {
                View();
            }
            else if ("T" == choice.ToUpper())
            {
                BaseView taskManager = new TaskManagerView();
                taskManager.View();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("*******************************");
                Console.WriteLine("******** INVALID INPUT! *******");
                Console.WriteLine("*******************************");
                Thread.Sleep(1300);
                SelectMenu();
            }
        }

        protected override void GetAll()
        {
            Console.Clear();
            BaseRepo<User> repo = new UserRepo("users.txt");
            var users = repo.GetAll();
            foreach (var user in users)
            {
                Console.Write("ID: ");
                Console.WriteLine(user.Id);
                Console.Write("Username: ");
                Console.WriteLine(user.Username);
                Console.Write("Name: ");
                Console.WriteLine(user.Name);
                Console.Write("Admin: ");
                Console.WriteLine(user.IsAdmin);
                Console.WriteLine("###############################");
            }
            Console.ReadKey(true);
        }

        protected override void Add()
        {
            Console.Clear();
            UserRepo repo = new UserRepo("users.txt");
            User user = new User();
            Console.WriteLine("Username");
            user.Username = Console.ReadLine();
            Console.WriteLine("Password");
            user.Password = Console.ReadLine();
            Console.WriteLine("Name");
            user.Name = Console.ReadLine();
            Console.WriteLine("Is Admin");
            user.IsAdmin = Convert.ToBoolean(Console.ReadLine());
            repo.Save(user);
            Console.Clear();
            Console.WriteLine("*******************************");
            Console.WriteLine("********* COMPLETED!!! ********");
            Console.WriteLine("*******************************");
            Thread.Sleep(1500);
        }

        protected override void Edit()
        {
            Console.Clear();
            GetAll();
            Console.WriteLine("*** CHOOSE BY ID WHO TO EDIT ***");
            Console.Write("         ID: ");
            int id = int.Parse(Console.ReadLine());
            UserRepo repo = new UserRepo("users.txt");
            User user = new User();
            User u = new User();
            user = repo.GetById(id);
            u.Id = user.Id;
            Console.Write("******** OLD USERNAME : ");
            Console.WriteLine(user.Username);
            Console.Write("******** NEW USERNAME : ");
            u.Username = Console.ReadLine();
            Console.Write("******** OLD PASSWORD : ");
            Console.WriteLine(user.Password);
            Console.Write("******** OLD PASSWORD : ");
            u.Password = Console.ReadLine();
            Console.Write("******** OLD NAME : ");
            Console.WriteLine(user.Name);
            Console.Write("******** OLD NAME : ");
            u.Name = Console.ReadLine();
            Console.Write("******** OLD ISADMIN : ");
            Console.WriteLine(user.IsAdmin);
            Console.Write("******** NEW ISADMIN : ");
            u.IsAdmin = Convert.ToBoolean(Console.ReadLine());
            repo.Save(u);
            Console.Clear();
            Console.WriteLine("*******************************");
            Console.WriteLine("********* COMPLETED!!! ********");
            Console.WriteLine("*******************************");
            Thread.Sleep(1500);
        }

        protected override void Delete()
        {
            Console.Clear();
            GetAll();
            Console.WriteLine("*** CHOOSE BY ID WHO TO DELETE ***");
            Console.Write("           ID: ");
            int id = int.Parse(Console.ReadLine());
            UserRepo repo = new UserRepo("users.txt");
            repo.Delete(repo.GetById(id));
            Console.Clear();
            Console.WriteLine("*******************************");
            Console.WriteLine("********* COMPLETED!!! ********");
            Console.WriteLine("*******************************");
            Thread.Sleep(1500);
        }
    }
}