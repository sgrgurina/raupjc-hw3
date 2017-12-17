using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak2.Models.ManageViewModels
{
    public class TodoIndexViewModel
    {
        public List<TodoViewModel> TodoViewModels;

        public TodoIndexViewModel(List<TodoViewModel> todoViewModels)
        {
            TodoViewModels = todoViewModels;
        }
    }
}