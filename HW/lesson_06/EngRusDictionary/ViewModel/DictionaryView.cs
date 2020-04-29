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
        private Word _testinWord = new Word();

        public Word TestingWord
        {
            get { return _testinWord; }
            set
            {
                _testinWord = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TestingWord)));
            }
        }

        public RelayCommand DeleteRowCommand { get; private set; }
        public RelayCommand AddWordCommand { get; private set; }
        public RelayCommand UpdateTableCommand { get; private set; }
        public RelayCommand FilterWordsCommand { get; private set; }
        public RelayCommand PassTestCommand { get; private set; }
        public RelayCommand AnswerTestCommand { get; private set; }



        public DictionaryView(bool fill = false) : base(new string[] { "Words" }, "LocalWordsDB", fill)
        {
            if (fill)
                LoadData();

            DeleteRowCommand = new RelayCommand(RemoveWord, (obj) => obj != null);
            AddWordCommand = new RelayCommand(AddWord, CanAddWord);
            UpdateTableCommand = new RelayCommand(UpdateTable);
            FilterWordsCommand = new RelayCommand(FilterWords);
            PassTestCommand = new RelayCommand(PassTest);
            AnswerTestCommand = new RelayCommand(AnswerTest, CanAnswerTest);
        }

        private bool CanAnswerTest(object arg)
        {
            var values = (object[])arg;
            string engWord = (values[0] as string).Trim(' ');
            string rusWord = (values[1] as string).Trim(' ');

            return engWord != string.Empty && rusWord != string.Empty;
        }

        private void AnswerTest(object obj)
        {
            var values = (object[])obj;
            string engWord = (values[0] as string).Trim(' ');
            string rusWord = (values[1] as string).Trim(' ');

            string res = rusWord == TestingWord.RusWord ? "Correct" : "Wrong";
            MessageBox.Show($"{res}");
        }

        private void PassTest(object obj = null)
        {
            Random rnd = new Random();
            TestingWord = Words[rnd.Next(0, Words.Count - 1)];
        }

        private void FilterWords(object obj)
        {
            string word = obj as string;
            var rows = MainTable.Select($"EngWord not like '%'+'{word}'+'%' and RusWord not like '%'+'{word}'+'%'");

            foreach (var row in rows)
                MainTable.Rows.Remove(row);
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
