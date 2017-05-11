namespace TaskManager.View
{
    using Entities;
    using Repositories;
    using Service;
    using System;
    using System.Threading;
    using Tools;

    public class TaskManagerView : BaseView
    {
        protected override void GetAll()
        {
            All();
            Console.WriteLine("------- ADDITIONAL MENU --------");
            Console.WriteLine("------------- [Y]ES ------------");
            Console.WriteLine("------------- [N]O -------------");
            var addmenu = Console.ReadLine();
            if (addmenu.ToUpper() == "Y")
            {
                DetailMenuVIew detailMenu = new DetailMenuVIew();
                detailMenu.View();
            }
            else
            {
                BaseView taskManager = new TaskManagerView();
                taskManager.View();
            }
        }

        protected override void Add()
        {
            Console.Clear();
            TaskRepo repo = new TaskRepo("tasks.txt");
            Task task = new Task();
            task.ParentId = AuthenticationService.LoggedUser.Id;
            Console.WriteLine("*****  ADD NEW TASK  *****");
            Console.WriteLine("Title");
            task.Title = Console.ReadLine();
            Console.WriteLine("Description");
            task.Description = Console.ReadLine();
            Console.WriteLine("ResponsibleUserID");
            task.ResponsibleUser = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Creator");
            task.Creator = Console.ReadLine();
            Console.WriteLine("****** S T A T U S ******");
            task.Status = Convert.ToString(WhichStatus());
            var time = DateTime.Now;
            task.CreateTime = time;
            task.LastChange = time;
            repo.Save(task);
            Console.Clear();
            Console.WriteLine("*******************************");
            Console.WriteLine("********* COMPLETED!!! ********");
            Console.WriteLine("*******************************");
            Thread.Sleep(1500);
        }

        protected override void Edit()
        {
            Console.Clear();
            All();
            Console.WriteLine("*** CHOOSE BY ID WHO TO EDIT ***");
            Console.Write("         ID: ");
            int id = int.Parse(Console.ReadLine());
            TaskRepo repo = new TaskRepo("tasks.txt");
            Task task = new Task();
            Task u = new Task();
            task = repo.GetById(id);
            u.Id = task.Id;
            u.ParentId = task.ParentId;
            Console.Write("******** OLD TITLE : ");
            Console.WriteLine(task.Title);
            Console.Write("******** NEW TITLE : ");
            u.Title = Console.ReadLine();
            Console.Write("******** OLD DESCRIPTION : ");
            Console.WriteLine(task.Description);
            Console.Write("******** OLD DESCRIPTION : ");
            u.Description = Console.ReadLine();
            Console.Write("******** OLD RESPONSIBLE USER ID : ");
            Console.WriteLine(task.ResponsibleUser);
            Console.Write("******** OLD RESPONSIBLE USER ID : ");
            u.ResponsibleUser = Convert.ToInt32(Console.ReadLine());
            Console.Write("******** OLD CREATOR : ");
            Console.WriteLine(task.Creator);
            Console.Write("******** NEW CREATOR : ");
            u.Creator = Console.ReadLine();
            Console.Write("******** OLD STATUS : ");
            Console.WriteLine(task.Status);
            Console.Write("******** NEW STATUS : ");
            u.Status = Convert.ToString(WhichStatus());
            var time = DateTime.Now;
            u.LastChange = time;
            u.CreateTime = task.CreateTime;
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
            All();
            Console.WriteLine("*** CHOOSE BY ID WHO TO DELETE ***");
            Console.Write("           ID: ");
            int id = int.Parse(Console.ReadLine());
            TaskRepo repo = new TaskRepo("tasks.txt");
            repo.Delete(repo.GetById(id));
            Console.Clear();
            Console.WriteLine("*******************************");
            Console.WriteLine("********* COMPLETED!!! ********");
            Console.WriteLine("*******************************");
            Thread.Sleep(1500);
        }

        private Status WhichStatus()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("***************************");
                Console.WriteLine("********* [W]AITING *******");
                Console.WriteLine("********* [D]ONE **********");
                Console.WriteLine("***************************");
                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "W":
                        {
                            return Status.Waiting;
                        }
                    case "D":
                        {
                            return Status.Done;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("*******************************");
                            Console.WriteLine("******* WRONG INPUT !! ********");
                            Console.WriteLine("*******************************");
                            Thread.Sleep(1300);
                            break;
                        }
                }
            }
        }

        public void All()
        {
            var logedUser = AuthenticationService.LoggedUser;
            TaskRepo repo = new TaskRepo("tasks.txt");
            var tasks = repo.GetAll();
            Console.Clear();
            foreach (var task in tasks)
            {
                if (task.ParentId == logedUser.Id || task.ResponsibleUser == logedUser.Id)
                {
                    Console.Write("ID: ");
                    Console.WriteLine(task.Id);
                    Console.Write("Title: ");
                    Console.WriteLine(task.Title);
                    Console.Write("Description: ");
                    Console.WriteLine(task.Description);
                    Console.Write("ResponsibleUserID: ");
                    Console.WriteLine(task.ResponsibleUser);
                    Console.Write("Creator: ");
                    Console.WriteLine(task.Creator);
                    Console.Write("Status: ");
                    Console.WriteLine(task.Status);
                    Console.Write("LastChange: ");
                    Console.WriteLine(task.LastChange);
                    Console.Write("CreateTime: ");
                    Console.WriteLine(task.CreateTime);
                    Console.WriteLine("###############################");
                }
            }
        }
    }
}