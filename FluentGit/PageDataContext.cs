using System.ComponentModel;

namespace FluentGit
{
    /// <summary>
    /// Page's Data context to use in controls.
    /// </summary>
    public class PageDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
    }
}
