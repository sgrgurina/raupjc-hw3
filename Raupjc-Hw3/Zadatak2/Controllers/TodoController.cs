using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Frameworks;
using Zadatak1;
using Zadatak2.Models;
using Zadatak2.Models.ManageViewModels;

namespace Zadatak2.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoController(ITodoRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            List<TodoItem> todoItems = _repository.GetActive(new Guid(user.Id));
            List<TodoViewModel> todoViewModels = new List<TodoViewModel>();
            foreach (var item in todoItems)
            {
                int daysToDeadline = (int) (item.DateDue - DateTime.Now).TotalDays;

                TodoViewModel viewModel = new TodoViewModel(item.Text, item.DateDue, daysToDeadline);
                todoViewModels.Add(viewModel);
            }

            TodoIndexViewModel indexViewModel = new TodoIndexViewModel(todoViewModels);

            return View(indexViewModel);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTodoViewModel model)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            Guid userId = new Guid(user.Id);

            TodoItem newItem = new TodoItem(model.Text, userId);
            newItem.DateDue = (DateTime) model.DateDue;
            _repository.Add(newItem);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Completed()
        {
            return null;
        }
    }
}