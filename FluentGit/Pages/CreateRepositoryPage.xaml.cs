// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using FluentGit.Components;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.IO;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentGit.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateRepositoryPage : Page
    {
        public CreateRepositoryPage()
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

            Type page = typeof(RepositoryManagementPage);
            PageNavigator.Navigate(page);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.NavigateLastPage();
        }
    }

    /// <summary>
    /// Data context for NavigateDirectoryPage.
    /// </summary>
    internal sealed partial class NavigateDirectoryPageDataContext : PageDataContext
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
                OnPropertyChange(nameof(ErrorMessage));
            }
        }

        public bool IsValidDirectory
        {
            get => Directory.Exists(BrowsingDirectory);
        }

        readonly string InvalidDirectoryMessage = "Invalid directory path, please check the path exists.";
        public string ErrorMessage
        {
            get
            {
                if (!IsValidDirectory)
                    return InvalidDirectoryMessage;
                return "";
            }
        }
    }
}
