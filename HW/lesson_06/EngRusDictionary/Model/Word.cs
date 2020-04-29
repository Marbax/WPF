using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace EngRusDictionary.Model
{
    class Word : INotifyPropertyChanged
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                if (!_id.Equals(value))
                {
                    _id = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Id)));
                }
            }
        }

        private string _engWord;

        public string EngWord
        {
            get { return _engWord; }
            set
            {
                if (_engWord == null || !_engWord.Equals(value))
                {
                    _engWord = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(EngWord)));
                }
            }
        }

        private string _rusWord;

        public string RusWord
        {
            get { return _rusWord; }
            set
            {
                if (_rusWord == null || !_rusWord.Equals(value))
                {
                    _rusWord = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(RusWord)));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public override string ToString()
        {
            return $"{Id},{EngWord},{RusWord}{Environment.NewLine}";
        }
    }
}
