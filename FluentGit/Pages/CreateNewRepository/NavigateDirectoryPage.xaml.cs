// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentGit.Pages.CreateNewRepository
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NavigateDirectoryPage : Page
    {
        public NavigateDirectoryPage()
        {
            this.InitializeComponent();
            this.DataContext = new NavigateDirectoryPageDataContext();
        }

        /// <summary>
        /// Get this Page's data context.
        /// </summary>
        /// <returns>A data context has type NavigateDirectoryPageDataContext</returns>
        private NavigateDirectoryPageDataContext GetDataContext()
        {
            return DataContext as NavigateDirectoryPageDataContext;
        }

        /// <summary>
        /// Pick an existing folder an write it path to BrowsingDirectory.
        /// </summary>
        private async void pickFolder()
        {
            FolderPicker directoryBrowser = new();

            // Get the current window's HWND by passing in the Window object
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(ApplicationReferences.MainWindowReference);

            // Associate the HWND with the file picker
            WinRT.Interop.InitializeWithWindow.Initialize(directoryBrowser, hwnd);

            directoryBrowser.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            directoryBrowser.FileTypeFilter.Add("*");

            StorageFolder folder = await directoryBrowser.PickSingleFolderAsync();
            if (folder != null)
                GetDataContext().BrowsingDirectory = folder.Path;
        }

        /// <summary>
        /// Event handler for BrowseButton, invoke when Click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            pickFolder();
        }
    }

    /// <summary>
    /// Data context for NavigateDirectoryPage.
    /// </summary>
    internal class NavigateDirectoryPageDataContext : PageDataContext
    {
        private string BrowsingDirectoryProperty;
        public string BrowsingDirectory
        {
            get => BrowsingDirectoryProperty;
            set
            {
                BrowsingDirectoryProperty = value;
                OnPropertyChange(nameof(BrowsingDirectory));
                OnPropertyChange(nameof(IsValidDirectory));
            }
        }

        public bool IsValidDirectory
        {
            get => Directory.Exists(BrowsingDirectory);
        }
    }
}
