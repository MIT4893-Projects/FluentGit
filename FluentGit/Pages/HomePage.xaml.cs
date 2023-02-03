// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using FluentGit.Components;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentGit.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Type createRepositoryPage = typeof(CreateRepositoryPage);
            PageNavigator.Navigate(createRepositoryPage);
        }

        private void Clone_Click(object sender, RoutedEventArgs e)
        {
            Type cloneRepositoryPage = typeof(CloneRepositoryPage);
            PageNavigator.Navigate(cloneRepositoryPage);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Type openRepositoryPage = typeof(OpenRepositoryPage);
            PageNavigator.Navigate(openRepositoryPage);
        }
    }
}
