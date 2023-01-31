using FluentGit.Pages;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentGit.Components
{
    static class PageNavigator
    {
        public static void Navigate(Type page)
        {
            ApplicationReferences.MainFrameReference.Navigate(page);
        }

        public static void NavigateLastPage()
        {
            Frame mainFrame = ApplicationReferences.MainFrameReference;
            if (mainFrame.CanGoBack)
                mainFrame.GoBack();
        }
    }
}
