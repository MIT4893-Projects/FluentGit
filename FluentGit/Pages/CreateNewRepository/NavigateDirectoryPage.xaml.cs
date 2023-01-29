// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using FluentGit.Components;
using FluentGit.Components.Commands;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private async void PickFolder()
        {
            string path = await DialogDisplayer.ShowFolderPicker();

            // If there is a choosed folder.
            if (path != null)
                GetDataContext().BrowsingDirectory = path;
        }

        /// <summary>
        /// Event handler for BrowseButton, invoke when Click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            PickFolder();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            GitCommands.Init(GetDataContext().BrowsingDirectory);
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
