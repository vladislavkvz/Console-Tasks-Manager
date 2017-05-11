namespace TaskManager.View
{
    using System;
    using Repositories;
    using Entities;
    using System.Threading;
    using Service;

    public class CommentView
    {
        internal void CommentMenu(Entities.Task task)
        {
            Console.Clear();
            Console.WriteLine("**** COMMENT MENU ****");
            Console.WriteLine("** [V]IEW COMMENTS **");
            Console.WriteLine("** [M]AKE COMMENTS **");
            var choice = Console.ReadLine();
            switch (choice.ToUpper())
            {
                case "V": GetAllComments(task); break;
                case "M": AddComment(task); break;
            }
        }

        internal void AddComment(Entities.Task task)
        {
            Console.Clear();
            CommentRepo repo = new CommentRepo("comments.txt");
            Comment com = new Comment();
            Console.WriteLine("***************************");
            Console.WriteLine("***** ADD NEW COMMENT *****");
            Console.Write("Comment: ");
            com.Comments = Console.ReadLine();
            com.ParentId = Convert.ToInt32(task.Id);
            com.CreatedBy = Convert.ToInt32(AuthenticationService.LoggedUser.Id);
            repo.Save(com);
            Console.Clear();
            Console.WriteLine("*******************************");
            Console.WriteLine("********* COMPLETED!!! ********");
            Console.WriteLine("*******************************");
            Thread.Sleep(1500);
            Console.Clear();

            DetailMenuVIew detailMenu = new DetailMenuVIew();
            detailMenu.PrintRepo(task);
        }

        private void GetAllComments(Entities.Task task)
        {
            var logedUser = AuthenticationService.LoggedUser;
            CommentRepo repo = new CommentRepo("comments.txt");
            var coms = repo.GetAll();
            Console.Clear();
            foreach (var com in coms)
            {
                if (com.ParentId == task.Id)
                {
                    Console.Write("Created by ID: ");
                    Console.WriteLine(com.CreatedBy);
                    Console.Write("Comments: ");
                    Console.WriteLine(com.Comments);
                    Console.WriteLine("###############################");
                }
            }
            Console.ReadKey();
            Console.Clear();
            DetailMenuVIew detailMenu = new DetailMenuVIew();
            detailMenu.PrintRepo(task);
        }
    }
}