namespace TaskManager.View
{
    using System;
    using System.Threading;
    using Tools;

    public class BaseView
    {
        public virtual void View()
        {
            while (true)
            {
                ManagerEnum choice = RenderMenu();
                switch (choice)
                {
                    case ManagerEnum.All:
                        GetAll();
                        break;
                    case ManagerEnum.Add:
                        Add();
                        break;
                    case ManagerEnum.Edit:
                        Edit();
                        break;
                    case ManagerEnum.Delete:
                        Delete();
                        break;
                    case ManagerEnum.Exit:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private ManagerEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("***************************");
                Console.WriteLine("******** [G]ET ALL ********");
                Console.WriteLine("******** [A]DD BY ID ******");
                Console.WriteLine("******** [E]DIT BY ID *****");
                Console.WriteLine("******** [D]ELETE BY ID ***");
                Console.WriteLine("******** E[X]IT ***********");
                Console.WriteLine("***************************");
                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "G":
                        {
                            return ManagerEnum.All;
                        }
                    case "A":
                        {
                            return ManagerEnum.Add;
                        }
                    case "E":
                        {
                            return ManagerEnum.Edit;
                        }
                    case "D":
                        {
                            return ManagerEnum.Delete;
                        }
                    case "X":
                        {
                            return ManagerEnum.Exit;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("*******************************");
                            Console.WriteLine("******** INVALID INPUT! *******");
                            Console.WriteLine("*******************************");
                            Thread.Sleep(1300);
                            break;
                        }
                }
            }
        }

        protected virtual void GetAll()
        {
        }

        protected virtual void Add()
        {
        }

        protected virtual void Edit()
        {
        }

        protected virtual void Delete()
        {
        }
    }
}