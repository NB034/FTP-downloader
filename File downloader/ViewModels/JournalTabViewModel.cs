using File_downloader.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_downloader.ViewModels
{
    internal class JournalTabViewModel : INotifyPropertyChanged
    {
        private readonly AutoEventCommandBase _searchCommand;
        private readonly AutoEventCommandBase _resetCommand;
        private readonly AutoEventCommandBase _removeEntryCommand;
        private readonly AutoEventCommandBase _removeAllEntriesCommand;


        public JournalTabViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public AutoEventCommandBase SearchCommand => _searchCommand;
        public AutoEventCommandBase ResetCommand => _resetCommand;
        public AutoEventCommandBase RemoveEntryCommand => _removeEntryCommand;
        public AutoEventCommandBase RemoveAllEntriesCommand => _removeAllEntriesCommand;
    }
}
