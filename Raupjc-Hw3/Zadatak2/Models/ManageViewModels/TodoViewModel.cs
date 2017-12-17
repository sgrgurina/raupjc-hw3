using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak2.Models.ManageViewModels
{
    public class TodoViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? DateCompleted { get; set; } 
        public int? DaysToDeadline { get; set; }

        public TodoViewModel(Guid id, string text, DateTime? dateCompleted)
        {
            Id = id;
            Text = text;
            DateCompleted = dateCompleted;
        }

        public TodoViewModel(Guid id, string text, DateTime dueDate, int daysToDeadline)
        {
            Id = id;
            Text = text;
            DueDate = dueDate;
            DaysToDeadline = daysToDeadline;
        }


    }
}
