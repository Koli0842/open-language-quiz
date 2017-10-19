using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace OpenQuiz.Control
{
    class UserDictionaryService
    {
        private ApplicationDataContainer container = App.localSettings.CreateContainer("UserDictionary", ApplicationDataCreateDisposition.Always);

        public async Task<string> ReadFileAsync(StorageFile file)
        {
            return await FileIO.ReadTextAsync(file);
        }

        public async Task<StorageFile> SelectJsonFromDialogAsync()
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".json");
            return await picker.PickSingleFileAsync();
        }

        public void Write(string name, string content)
        {
            container.Values[name] = content;
        }

        public void Delete(string name)
        {
            container.Values[name] = null;
        }

        public string Read(string name)
        {
            return container.Values[name] as string;
        }

        public List<string> GetDictionaryList()
        {
            return container.Values.Keys.ToList();
        }
    }
}
