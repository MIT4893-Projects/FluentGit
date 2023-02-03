using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;

namespace FluentGit.Components
{
    public struct DialogDisplayer
    {
        private static void WindowHandle(object dialog)
        {
            // Get the current window's HWND by passing in the Window object.
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(ApplicationReferences.MainWindowReference);

            // Associate the HWND with the file picker.
            WinRT.Interop.InitializeWithWindow.Initialize(dialog, hwnd);
        }

        public static async void ShowMessage(string message, string title = "")
        {
            MessageDialog messageDialog = new(message) { Title = title };
            WindowHandle(messageDialog);
            await messageDialog.ShowAsync();
        }

        public static async Task<string> ShowFolderPicker()
        {
            FolderPicker directoryBrowser = new();

            WindowHandle(directoryBrowser);

            directoryBrowser.SuggestedStartLocation = PickerLocationId.Desktop;
            directoryBrowser.FileTypeFilter.Add("*");

            StorageFolder folder = await directoryBrowser.PickSingleFolderAsync();

            return folder?.Path;
        }
    }
}
