using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Wpf.MarkupExtensions.Binding.Collections
{
    internal sealed class DataSource
    {
        private readonly ICollection<int> nonNotifiableCollection = new List<int> { 1, 2, 3 };
        private readonly ICollection<int> notifiableCollection = new ObservableCollection<int> { 1, 2, 3 };

        public IEnumerable<int> NonNotifiableCollection => nonNotifiableCollection;

        public IEnumerable<int> NotifiableCollection => notifiableCollection;

        public void AddValueToNonNotifiableCollection()
        {
            nonNotifiableCollection.Add(nonNotifiableCollection.Count + 1);
        }

        public void AddValueToNotifiableCollection()
        {
            notifiableCollection.Add(notifiableCollection.Count + 1);
        }
    }
}