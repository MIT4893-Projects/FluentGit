using Microsoft.UI.Xaml.Controls;
using System;

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
