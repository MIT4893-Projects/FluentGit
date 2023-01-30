using FluentGit.Pages.CreateNewRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentGit.Components
{
    static class PageNavigator
    {
        private static Stack<Type> VisitedPage = new();

        public static void Navigate(Type page)
        {
            ApplicationReferences.MainWindowReference.NavigatePage(page);
            VisitedPage.Push(page);
        }

        public static void NavigateLastPage()
        {
            Navigate(VisitedPage.Pop());
        }
    }
}
