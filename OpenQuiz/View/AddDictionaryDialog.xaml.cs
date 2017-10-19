using OpenQuiz.Control;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OpenQuiz
{
    public sealed partial class AddDictionaryDialog : ContentDialog
    {
        private UserDictionaryService dictionaryService = new UserDictionaryService();
        private Task<StorageFile> selectedFile;

        public AddDictionaryDialog()
        {
            InitializeComponent();
        }

        private void BrowseUserDictionary_Click(object sender, RoutedEventArgs args)
        {
            selectedFile = dictionaryService.SelectJsonFromDialogAsync();
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            StorageFile file = await selectedFile;
            string fileContent = await dictionaryService.ReadFileAsync(file);
            dictionaryService.Write(UserDictionaryName.Text, fileContent);
        }
    }
}
