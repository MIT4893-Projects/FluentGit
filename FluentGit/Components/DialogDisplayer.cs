using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.Gaming.Input.ForceFeedback;

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
