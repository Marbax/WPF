using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using EngRusDictionary.Model;

namespace EngRusDictionary.ViewModel
{
    partial class DictionaryView : AviewModel<Word>, INotifyPropertyChanged
    {
        ObservableCollection<Word> _words = new ObservableCollection<Word>();
        public ObservableCollection<Word> Words { get => _words; }

        public RelayCommand DeleteRowCommand { get; private set; }
        public RelayCommand AddWordCommand { get; private set; }
        public RelayCommand UpdateTableCommand { get; private set; }
        public RelayCommand FilterWordsCommand { get; private set; }
        public RelayCommand PassTestCommand { get; private set; }



        public DictionaryView(bool fill = false) : base(new string[] { "Words" }, "LocalWordsDB", fill)
        {
            if (fill)
                LoadData();

            DeleteRowCommand = new RelayCommand(RemoveWord, (obj) => obj != null);
            AddWordCommand = new RelayCommand(AddWord, CanAddWord);
            UpdateTableCommand = new RelayCommand(UpdateTable);


            FilterWordsCommand = new RelayCommand(FilterWords);
            PassTestCommand = new RelayCommand(UpdateTable);
        }

        private void FilterWords(object obj)
        {
            MainView = new DataView(); 
        }

        private void UpdateTable(object obj = null)
        {
            try
            {
                _adapter.Update(MainTable);
                LoadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Can't add such a word.");
            }
        }

        private void RemoveWord(object obj)
        {
            DataRow dr = ((DataRowView)obj).Row;
            var toDel = MainTable.Select($"Id = '{(int)dr["Id"]}'").FirstOrDefault();
            toDel.Delete();
            UpdateTable();
        }

        public void LoadData()
        {
            MainTable.Clear();
            _adapter.Fill(MainTable);
            _words.Clear();

            foreach (DataRow row in MainTable.Rows)
                _words.Add(new Word() { Id = (int)row["Id"], EngWord = row["EngWord"] as string, RusWord = row["RusWord"] as string });
        }


        private void AddWord(object parameter)
        {
            var values = (object[])parameter;
            string engWord = (values[0] as string).Trim(' ');
            string rusWord = (values[1] as string).Trim(' ');

            if (!EngWordExists(engWord))
                MainTable.Rows.Add(new object[] { null, engWord, rusWord });

            UpdateTable();
        }

        /// <summary>
        /// Check if word - translation combination exists
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanAddWord(object parameter)
        {
            var values = (object[])parameter;
            string engWord = (values[0] as string).Trim(' ');
            string rusWord = (values[1] as string).Trim(' ');
            if (string.IsNullOrEmpty(engWord) || string.IsNullOrEmpty(rusWord))
                return false;
            return !EngWordExists(engWord) && !RusWordExists(rusWord);
        }

        private bool EngWordExists(string word)
        {
            return _dataSet.Tables[MainTableName].Select($"EngWord = '{word}'").Length != 0 ? true : false;
            //return _dataSet.Tables["EngWords"].Select($"Word = '{word as string}'").Length != 0 ? false : true;
            //return !_dataSet.Tables["EngWords"].AsEnumerable().Any(row => (word as string) == row.Field<string>("Word"));
        }
        private bool RusWordExists(string word)
        {
            return _dataSet.Tables[MainTableName].Select($"RusWord = '{word}'").Length != 0 ? true : false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }

}
