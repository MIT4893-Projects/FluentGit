// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using FluentGit.Components.Commands;
using FluentGit.Components;
using FluentGit.Pages;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentGit.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OpenRepositoryPage : Page
    {
        public OpenRepositoryPage()
        {
            this.InitializeComponent();
            this.DataContext = new OpenRepositoryPageDataContext();
        }

        /// <summary>
        /// Get this Page's data context.
        /// </summary>
        /// <returns>A data context has type NavigateDirectoryPageDataContext</returns>
        private OpenRepositoryPageDataContext GetDataContext()
        {
            return DataContext as OpenRepositoryPageDataContext;
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

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            GitCommands.Status(GetDataContext().BrowsingDirectory);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.NavigateLastPage();
        }
    }

    /// <summary>
    /// Data context for NavigateDirectoryPage.
    /// </summary>
    internal class OpenRepositoryPageDataContext : PageDataContext
    {
        private string BrowsingDirectoryProperty;
        public string BrowsingDirectory
        {
            get => BrowsingDirectoryProperty;
            set
            {
                BrowsingDirectoryProperty = value;
                OnPropertyChange(nameof(BrowsingDirectory));
                OnPropertyChange(nameof(IsValidRepository));
                OnPropertyChange(nameof(ErrorMessage));
            }
        }

        private bool IsValidDirectory
        {
            get => Directory.Exists(BrowsingDirectory);
        }

        public bool IsValidRepository
        {
            get => Directory.Exists(BrowsingDirectory) && GitCommands.ValidRepoAt(BrowsingDirectory);
        }

        readonly string InvalidDirectoryMessage = "Invalid directory path, please check the path exists.";
        readonly string InvalidRepositoryMessage =
            "Invalid repository path, please check there is a repository at this repository. If not, please Create New Repository at this directory to continue.";
        public string ErrorMessage
        {
            get
            {
                if (!IsValidRepository)
                {
                    if (!IsValidDirectory)
                        return InvalidDirectoryMessage;
                    return InvalidRepositoryMessage;
                }
                return string.Empty;
            }
        }
    }
}
