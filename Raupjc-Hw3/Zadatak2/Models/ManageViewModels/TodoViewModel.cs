using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak2.Models.ManageViewModels
{
    public class TodoViewModel
    {
        public string Text { get; set; }
        public DateTime DueDate { get; set; }
        public int? DaysToDeadline { get; set; }

        public TodoViewModel(string text, DateTime dueDate, int daysToDeadline)
        {
            Text = text;
            DueDate = dueDate;
            DaysToDeadline = daysToDeadline;
        }
    }
}
