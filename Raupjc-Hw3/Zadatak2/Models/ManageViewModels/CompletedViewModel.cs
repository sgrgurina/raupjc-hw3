using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak2.Models.ManageViewModels
{
    public class CompletedViewModel
    {
        public string Text { get; set; }
        public DateTime DateCompleted { get; set; }

        public CompletedViewModel(string text, DateTime dateCompleted)
        {
            Text = text;
            DateCompleted = dateCompleted;
        }
    }
}
