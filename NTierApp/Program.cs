using System;
using System.Collections.Generic;
using System.Linq;

namespace NTierTodoApp
{
    public class TaskRepository
    {
        private static List<TaskModel> tasks = new List<TaskModel>
        {
            new TaskModel { Id = 1, Title = "تدرب على MVC Design Pattern" },
            new TaskModel { Id = 2, Title = "تدرب على n-tier Architecture" }
        };

        public List<TaskModel> GetAll()
        {
            return tasks;
        }

        public void Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
            }
        }
    }

    public class TaskService
    {
        private readonly TaskRepository repository = new TaskRepository();

        public List<TaskModel> GetTasks()
        {
            return repository.GetAll();
        }

        public void DeleteTask(int id)
        {
            repository.Delete(id);
        }
    }

    public class HomeController
    {
        private readonly TaskService taskService = new TaskService();

        public void Index()
        {
            var allTasks = taskService.GetTasks();
            foreach (var task in allTasks)
            {
                Console.WriteLine(task.Id + " - " + task.Title);
            }
        }

        public void DeleteTask(int id)
        {
            taskService.DeleteTask(id);
        }
    }

    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            HomeController controller = new HomeController();
            
            Console.WriteLine("Before Delete:");
            controller.Index();

            controller.DeleteTask(1);

            Console.WriteLine("\nAfter Delete:");
            controller.Index();
        }
    }
}
