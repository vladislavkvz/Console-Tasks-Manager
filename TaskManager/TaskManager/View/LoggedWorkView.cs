namespace TaskManager.View
{
    using System;
    using Entities;
    using Service;
    using Repositories;
    using System.Threading;

    public class LoggedWorkView
    {
        public void View(Entities.Task task)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("***********************");
                Console.WriteLine("***** LOGWORK MENU ****");
                Console.WriteLine("***** [W]RITE *********");
                Console.WriteLine("***** [R]EAD **********");
                Console.WriteLine("***** E[X]IT **********");
                Console.WriteLine("***********************");
                Console.Write("***** YOUR CHOICE : ");
                var choice = Console.ReadLine();
                Console.Clear();
                CommentView comView = new CommentView();
                switch (choice.ToUpper())
                {
                    case "W": MakeWorkTime(task); break;
                    case "R": GetAll(task); break;
                    case "X": Console.Clear(); return;
                    default:
                        {
                            Console.WriteLine("*******************************");
                            Console.WriteLine("******** INVALID CHOICE! *******");
                            Console.WriteLine("*******************************");
                            Thread.Sleep(1300);
                            break;
                        }
                }
            }
        }

        private void GetAll(Entities.Task task)
        {
            var logedUser = AuthenticationService.LoggedUser;
            LoggedWorkRepo repo = new LoggedWorkRepo("worktime.txt");
            var time = repo.GetAll();
            Console.Clear();
            foreach (var t in time)
            {
                if (t.TaskId == task.Id)
                {
                    Console.Write("Created by ID: ");
                    Console.WriteLine(t.UserId);
                    Console.Write("Logged on: ");
                    Console.WriteLine(t.LoggedOn);
                    Console.Write("LOGWORK: ");
                    Console.WriteLine(t.TimeSpent);
                    Console.WriteLine("###############################");
                }
            }
            Console.ReadKey();
            Console.Clear();
            View(task);
        }

        private void MakeWorkTime(Entities.Task task)
        {
            Console.WriteLine();
            LoggedWork lg = new LoggedWork();
            LoggedWorkRepo repo = new LoggedWorkRepo("worktime.txt");
            Console.WriteLine("How Much Time Takes U TO DO The Task? ");
            Console.Write("(int) ");
            lg.TimeSpent = int.Parse(Console.ReadLine());
            lg.LoggedOn = DateTime.Now;
            lg.UserId = AuthenticationService.LoggedUser.Id;
            lg.TaskId = task.Id;
            repo.Save(lg);
            View(task);
        }
    }
}