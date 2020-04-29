using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Wpf.Commands.Events
{
    internal sealed class DataSource
    {
        private readonly ICollection<string> events = new ObservableCollection<string>();
        private readonly ICommand loadedCommand;
        private readonly ICommand selectionChangedCommand;
        private readonly ICommand stateChangedCommand;

        public DataSource()
        {
            loadedCommand = new DelegateCommand(OnLoad);
            selectionChangedCommand = new DelegateCommand(OnSelectionChanged);
            stateChangedCommand = new DelegateCommand(OnStateChanged);
        }

        public IEnumerable<string> Events => events;

        public ICommand LoadedCommand => loadedCommand;

        public ICommand SelectionChangedCommand => selectionChangedCommand;

        public ICommand StateChangedCommand => stateChangedCommand;

        private void OnLoad()
        {
            events.Add("Loaded");
        }

        private void OnSelectionChanged()
        {
            events.Add("SelectionChanged");
        }

        private void OnStateChanged()
        {
            events.Add("StateChanged");
        }
    }
}