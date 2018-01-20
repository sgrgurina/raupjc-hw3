using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak2.Models.ManageViewModels
{
    public class AddTodoViewModel
    {
        [Required]
        [MaxLength(255)]
        [Display(Name = "Item Name")]
        public string Text { get; set; }
        public DateTime? DateDue { get; set; }
        [MaxLength(255)]
        public string Labels { get; set; }

    }
}
