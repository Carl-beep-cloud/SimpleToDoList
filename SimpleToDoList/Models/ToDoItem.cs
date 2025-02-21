using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDoList.Models
{
    public class ToDoItem
    {
        public bool IsChecked {  get; set; }

        public string? Content { get; set; }

        public int? WeightInt { get; set; }

        public int? RepsInt { get; set; }

        public int? Result { get; set; }

        //   public string? Result { get; set; }


    }
}
