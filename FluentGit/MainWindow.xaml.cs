// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using FluentGit.Components.Commands;
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
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentGit
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Navigate to another page.
        /// </summary>
        /// <param name="page">Page to navigate's type</param>
        public void NavigatePage(Type page)
        {
            MainFrame.Navigate(page);
        }

        /// <summary>
        /// Event handler for MainFrame, invoke when loaded. Also is the event handler for the MainWindow,
        /// invoke when loaded. Configure the global application's data context ApplicationDataContext to
        /// store a reference of this MainWindow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            NavigatePage(typeof(HomePage));
            ApplicationReferences.MainWindowReference = this;
        }
    }

    /// <summary>
    /// Global Application's Data context.
    /// </summary>
    static class ApplicationReferences
    {
        public static MainWindow MainWindowReference;
    }
}
