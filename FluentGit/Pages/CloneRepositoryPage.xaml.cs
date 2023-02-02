// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using FluentGit.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using FluentGit.Components.Commands;
using System.Text.RegularExpressions;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentGit.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CloneRepositoryPage : Page
    {
        public CloneRepositoryPage()
        {
            this.InitializeComponent();
            this.DataContext = new CloneRepositoryPageDataContext();
        }

        private CloneRepositoryPageDataContext GetDataContext()
        {
            return DataContext as CloneRepositoryPageDataContext;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.NavigateLastPage();
        }

        private void CloneButton_Click(object sender, RoutedEventArgs e)
        {
            CloneRepositoryPageDataContext dataContext = GetDataContext();
            if (GitCommands.Clone(dataContext.BrowsingSourceLocation, dataContext.BrowsingTargetDirectory) == 0)
            {
                PageNavigator.Navigate(typeof(RepositoryManagementPage));
            }
        }

        private async void BrowseTargetDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            string path = await DialogDisplayer.ShowFolderPicker();

            // If there is a choosed folder.
            if (path != null)
                GetDataContext().BrowsingTargetDirectory = path;
        }
    }

    internal sealed partial class CloneRepositoryPageDataContext : PageDataContext
    {
        private bool IsValidUrl(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
        }

        private bool IsValidPath(string path)
        {
            string regex = @"^[a-zA-Z]:";
            return Regex.IsMatch(path, regex);
        }

        private string BrowsingSourceLocationProperty = "";
        public string BrowsingSourceLocation
        {
            get
            {
                return BrowsingSourceLocationProperty;
            }
            set
            {
                BrowsingSourceLocationProperty = value;
                OnPropertyChange(nameof(BrowsingSourceLocation));
                OnPropertyChange(nameof(BrowsingTargetDirectory));
                OnPropertyChange(nameof(ErrorMessage));
                OnPropertyChange(nameof(Clonable));
            }
        }

        private string BrowsingTargetDirectoryProperty = "";
        public string BrowsingTargetDirectory
        {
            get
            {
                return BrowsingTargetDirectoryProperty;
            }
            set
            {
                BrowsingTargetDirectoryProperty = value;
                OnPropertyChange(nameof(BrowsingTargetDirectory));
                OnPropertyChange(nameof(BrowsingTargetDirectory));
                OnPropertyChange(nameof(ErrorMessage));
                OnPropertyChange(nameof(Clonable));
            }
        }

        private bool IsValidSourceLocation
        {
            get => IsValidUrl(BrowsingSourceLocation);
        }

        public string ErrorMessage
        {
            get
            {
                if (BrowsingSourceLocation == string.Empty)
                    return "Missing source location.";
                if (BrowsingTargetDirectory == string.Empty)
                    return "Missing target directory.";

                if (!IsValidSourceLocation)
                    return "Not a valid source location; please provide a valid repository's URL.";
                if (!IsValidPath(BrowsingTargetDirectory))
                    return "Invalid target directory.";
                return "";
            }
        }

        public bool Clonable
        {
            get
            {
                return ErrorMessage == string.Empty;
            }
        }
    }
}
