using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models;
using TaskManagementSystem.Repositories;

namespace TaskManagementSystem.Controllers
{
    public class TaskController : Controller
    {
		private readonly ITaskRepository _repo = new TaskRepository();

		public IActionResult Index()
		{
			return View(_repo.GetAll());
		}

		public IActionResult Details(int id)
		{
			return View(_repo.GetById(id));
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(TaskItem task)
		{
			_repo.Add(task);
			return RedirectToAction("Index");
		}

		public IActionResult Edit(int id)
		{
			return View(_repo.GetById(id));
		}

		[HttpPost]
		public IActionResult Edit(TaskItem task)
		{
			_repo.Update(task);
			return RedirectToAction("Index");
		}

		public IActionResult Delete(int id)
		{
			_repo.Delete(id);
			return RedirectToAction("Index");
		}

		// BONUS
		public IActionResult Filter(bool status)
		{
			return View("Index", _repo.GetByStatus(status));
		}

		// BONUS
		public IActionResult Sort()
		{
			return View("Index", _repo.SortByDueDate());
		}
	}
}
