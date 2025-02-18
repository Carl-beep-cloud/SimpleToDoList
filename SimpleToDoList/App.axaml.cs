using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using SimpleToDoList.Service;
using SimpleToDoList.ViewModels;
using SimpleToDoList.Views;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleToDoList
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
 
        private readonly MainWindowViewModel _mainViewModel = new MainWindowViewModel();
        public override async void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = _mainViewModel // Remember to change this line to use our private reference to the MainViewModel
                };

                // Listen to the ShutdownRequested-event
                desktop.ShutdownRequested += DesktopOnShutdownRequested;
            }

            base.OnFrameworkInitializationCompleted();

            // Init the MainViewModel 
            await InitMainViewModelAsync();
        }

        private async Task InitMainViewModelAsync()
        {
            // get the items to load
            var itemsLoaded = await ToDoListFileService.LoadFromFileAsync();

            if (itemsLoaded is not null)
            {
                foreach (var item in itemsLoaded)
                {
                    _mainViewModel.ToDoItems.Add(new ToDoItemViewModel(item));
                }
            }
        }


        private bool _canClose; // This flag is used to check if window is allowed to close
        private async void DesktopOnShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
        {
            e.Cancel = !_canClose; // cancel closing event first time

            if (!_canClose)
            {
                // To save the items, we map them to the ToDoItem-Model which is better suited for I/O operations
                var itemsToSave = _mainViewModel.ToDoItems.Select(item => item.GetToDoItem());

                await ToDoListFileService.SaveToFileAsync(itemsToSave);

                // Set _canClose to true and Close this Window again
                _canClose = true;
                if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    desktop.Shutdown();
                }
            }
        }
       
    }
}