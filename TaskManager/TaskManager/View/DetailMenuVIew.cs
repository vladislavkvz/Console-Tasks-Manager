namespace TaskManager.View
{
    using System;
    using System.Threading;
    using Repositories;
    using Tools;

    public class DetailMenuVIew
    {
        internal void View()
        {
            TaskManagerView taskView = new TaskManagerView();
            taskView.All();
            Console.WriteLine("*** CHOOSE BY ID TASK ADDITIONAL INFO ***");
            Console.Write("           ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            TaskRepo repo = new TaskRepo("tasks.txt");
            PrintRepo(repo.GetById(id));
            Console.WriteLine();
            RenderMenu(repo.GetById(id));
            Console.Clear();
        }

        internal void PrintRepo(Entities.Task task)
        {
            Console.Write("Title: ");
            Console.WriteLine(task.Title);
            Console.Write("Description: ");
            Console.WriteLine(task.Description);
            Console.Write("Creator: ");
            Console.WriteLine(task.Creator);
            Console.Write("Status: ");
            Console.WriteLine(task.Status);
            Console.Write("LastChange: ");
            Console.WriteLine(task.LastChange);
            Console.Write("CreateTime: ");
            Console.WriteLine(task.CreateTime);
        }

        internal void RenderMenu(Entities.Task task)
        {
            while (true)
            {
                Console.WriteLine("***********************");
                Console.WriteLine("******** MENU: ********");
                Console.WriteLine("**** [S]TATUS *********");
                Console.WriteLine("**** [C]OMMENT ********");
                Console.WriteLine("**** [L]OGWORK ********");
                Console.WriteLine("**** [O]THER TASK *****");
                Console.WriteLine("**** E[X]IT ***********");
                Console.WriteLine("***********************");
                Console.Write("***** YOUR CHOICE : ");
                var choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "S": ChangeStatus(task); break;
                    case "C": CommentView comView = new CommentView(); comView.CommentMenu(task); break;
                    case "L": LoggedWorkView lg = new LoggedWorkView(); lg.View(task); break;
                    case "O": View(); break;
                    case "X": return;
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("*******************************");
                            Console.WriteLine("******** INVALID CHOICE! *******");
                            Console.WriteLine("*******************************");
                            Thread.Sleep(1300);
                            break;
                        }
                }
            }
        }

        private void ChangeStatus(Entities.Task task)
        {
            Console.Clear();
            TaskRepo repo = new TaskRepo("tasks.txt");
            Entities.Task u = new Entities.Task();
            u.Id = task.Id;
            u.ParentId = task.ParentId;
            u.Title = task.Title;
            u.Description = task.Description;
            u.ResponsibleUser = task.ResponsibleUser;
            u.Creator = task.Creator;
            Console.WriteLine("********************************");
            Console.Write("******** OLD STATUS : ");
            Console.WriteLine(task.Status);
            Console.Write("******** NEW STATUS : ");
            u.Status = Convert.ToString(WhichStatus());
            Console.WriteLine("********************************");
            var time = DateTime.Now;
            u.LastChange = time;
            u.CreateTime = task.CreateTime;
            repo.Save(u);
            CommentView comentView = new CommentView();
            comentView.AddComment(task);
            Console.Clear();
            Console.WriteLine("*******************************");
            Console.WriteLine("********* COMPLETED!!! ********");
            Console.WriteLine("*******************************");
            Thread.Sleep(1500);
            Console.Clear();
            PrintRepo(u);
            Console.WriteLine();
            Console.WriteLine();
            RenderMenu(u);
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
    }
}