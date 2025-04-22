using CommunityToolkit.Mvvm.ComponentModel;
using SimpleToDoList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDoList.ViewModels
{
    public partial class ToDoItemViewModel : ViewModelBase
    {
        [ObservableProperty]
        private bool _isChecked;

        [ObservableProperty]
        private string? _content;

        [ObservableProperty]
        private int? _Weight;

        [ObservableProperty]
        private int? _Reps;

        public ToDoItemViewModel()
        {
            // empty blub
       
            _Weight=5;
            _Reps = 5;


        }

        public ToDoItemViewModel(ToDoItem item)
        {
            IsChecked = item.IsChecked;
            Content = item.Content;
            Weight = item.WeightInt;
            Reps = item.RepsInt;
            Result = item.Result;
            
        }

        public ToDoItem GetToDoItem()
        {
            return new ToDoItem()
            {
                IsChecked = this.IsChecked,
                Content = this.Content,
                WeightInt = this.Weight,
                RepsInt = this.Reps,
                Result = this.Result

            };
        }

    }
}
