﻿using System;
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
                int daysToDeadline;
                if (item.DateDue != null)
                {
                    daysToDeadline = (int) ((DateTime) item.DateDue - DateTime.Now).TotalDays;
                }
                else
                {
                    daysToDeadline = 0;
                }

                TodoViewModel viewModel = new TodoViewModel(item.Id, item.Text, item.DateDue, daysToDeadline);
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
            if (model.DateDue != null)
            {
                newItem.DateDue = (DateTime) model.DateDue;
            }
            else
            {
                newItem.DateDue = null;
            }
            if (!string.IsNullOrWhiteSpace(model.Labels))
            {
                string[] labelValues = model.Labels.Split(',');
                foreach (var labelValue in labelValues)
                {
                    string trimmedLabelValue = labelValue.Trim();

                    //if it isnt empty or null
                    if (!string.IsNullOrWhiteSpace(trimmedLabelValue))
                    {
                        TodoLabel existingLabel = _repository.GetLabel(trimmedLabelValue);
                        if (existingLabel == null)
                        {
                            TodoLabel newLabel = new TodoLabel(trimmedLabelValue);
                            _repository.AddLabel(newLabel);
                            newItem.Labels.Add(newLabel);
                        }
                        else
                        {
                            newItem.Labels.Add(existingLabel);
                        }
                    }
                }
            }

            _repository.Add(newItem);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Completed()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            List<TodoItem> todoItems = _repository.GetCompleted(new Guid(user.Id));
            List<TodoViewModel> todoViewModels = new List<TodoViewModel>();
            foreach (var item in todoItems)
            {
                TodoViewModel viewModel = new TodoViewModel(item.Id, item.Text, item.DateCompleted);
                todoViewModels.Add(viewModel);
            }

            CompletedViewModel indexViewModel = new CompletedViewModel(todoViewModels);

            return View(indexViewModel);
        }

        [HttpGet("MarkAsCompleted/{itemId}")]
        public async Task<IActionResult> MarkAsCompleted(Guid itemId)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            Guid userId = new Guid(user.Id);

            _repository.MarkAsCompleted(itemId, userId);
            return RedirectToAction("Index");
        }

        [HttpGet("RemoveFromCompleted/{itemId}")]
        public async Task<IActionResult> RemoveFromCompleted(Guid itemId)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            Guid userId = new Guid(user.Id);

            _repository.Remove(itemId, userId);
            return RedirectToAction("Completed");
        }
    }
}