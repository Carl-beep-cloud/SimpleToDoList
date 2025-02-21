using CommunityToolkit.Mvvm.ComponentModel;

namespace SimpleToDoList.ViewModels
{
    public partial class ViewModelBase : ObservableObject
    {

        [ObservableProperty]
        private int? _Result;
    }
}
