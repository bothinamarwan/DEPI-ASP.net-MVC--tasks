using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskEvaluate.Models;
using TaskEvaluate.Repositories.Interfaces;

namespace TaskEvaluate.Controllers
{
    public class TaskItemController : Controller
    {
        ITaskItemRepository _taskItemRepository;

        // DI : 
        // 1- ASk 
        public TaskItemController(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }


        // GET: /TaskItem/Index
        public IActionResult Index()
        {
            return View(_taskItemRepository.GetAll()); 
        }

        // GET: TaskItem/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskItem = _taskItemRepository.GetById(id.Value);
            if (taskItem == null)
            {
                return NotFound();
            }

            return View(taskItem);
        }

        // GET: TaskItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
               _taskItemRepository.Add(taskItem);
                _taskItemRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(taskItem);
        }

        // GET: TaskItem/Edit/5
        public IActionResult Edit (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskItem =  _taskItemRepository.GetById(id.Value);
            if (taskItem == null)
            {
                return NotFound();
            }
            return View(taskItem);
        }

        // POST: TaskItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _taskItemRepository.Update(taskItem);
                _taskItemRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(taskItem);
        }

        // GET: TaskItem/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskItem = _taskItemRepository.GetById(id.Value);
            if (taskItem == null)
            {
                return NotFound();
            }

            return View(taskItem);
        }

        // POST: TaskItem/Delete/5
        // req
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var taskItem =  _taskItemRepository.GetById(id);
            if (taskItem != null)
            {
                _taskItemRepository.Delete(taskItem);
            }
            _taskItemRepository.Save();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult ToggleComplete(int id)
        {
            var taskItem = _taskItemRepository.GetById(id);
            if (taskItem == null)
            {
                return Json(new { success = false, message = "Task not found" });
            }

            taskItem.IsCompleted = !taskItem.IsCompleted;
            _taskItemRepository.Update(taskItem);
            _taskItemRepository.Save();

            return Json(new { success = true, isCompleted = taskItem.IsCompleted });
        }

        [HttpGet]
        public IActionResult GetServerInfo()
        {
            return Json(new { message = "Server is running smoothly!", time = DateTime.Now.ToString("F") });
        }
    }
}
