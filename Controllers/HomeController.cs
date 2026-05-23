using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MvcTodoApp.Controllers
{
    public class HomeController : Controller
    {
        private static List<TaskItem> tasks = new List<TaskItem>
        {
            new TaskItem { Id = 1, Title = "تدرب على MVC Design Pattern", IsComplete = false },
            new TaskItem { Id = 2, Title = "تدرب على n-tier Architecture", IsComplete = false }
        };

        public ActionResult Index() => View(tasks);

        [HttpPost]
        public IActionResult AddTask(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                int newId = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
                tasks.Add(new TaskItem { Id = newId, Title = title, IsComplete = false });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CompleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null) task.IsComplete = true;
            return RedirectToAction("Index");
        }

        // حل الوظيفة الأولى: ميزة التعديل
        [HttpPost]
        public IActionResult EditTask(int id, string newTitle)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null && !string.IsNullOrEmpty(newTitle))
            {
                task.Title = newTitle;
            }
            return RedirectToAction("Index");
        }
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }
    }
}
