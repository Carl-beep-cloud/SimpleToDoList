using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using SimpleToDoList.Models;

namespace SimpleToDoList.Service
{
    public static class ToDoListFileService
    {

        private static string _jsonFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Avalonia.SimpleToDoList", "MyToDoList.txt");


        public static async Task SaveToFileAsync(IEnumerable<ToDoItem> itemsToSave)
        {
            // Ensure all directories exists
            Directory.CreateDirectory(Path.GetDirectoryName(_jsonFileName)!);

            // We use a FileStream to write all items to disc
            using (var fs = File.Create(_jsonFileName))
            {
                await JsonSerializer.SerializeAsync(fs, itemsToSave);
            }
        }
        public static async Task<IEnumerable<ToDoItem>?> LoadFromFileAsync()
        {
            try
            {
                // We try to read the saved file and return the ToDoItemsList if successful
                using (var fs = File.OpenRead(_jsonFileName))
                {
                    return await JsonSerializer.DeserializeAsync<IEnumerable<ToDoItem>>(fs);
                }
            }
            catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
            {
                // In case the file was not found, we simply return null
                return null;
            }
        }

    }


}
