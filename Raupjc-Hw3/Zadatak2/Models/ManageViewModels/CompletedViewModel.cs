using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak2.Models.ManageViewModels
{
    public class CompletedViewModel
    {
        public List<TodoViewModel> TodoViewModels;

        public CompletedViewModel(List<TodoViewModel> todoViewModels)
        {
            TodoViewModels = todoViewModels;
        }
    }
}
