using System.Windows;
using EngRusDictionary.ViewModel;

namespace EngRusDictionary
{
    /// <summary>
    /// Ваше задание состоит в разработке WPF-приложения, в котором демонстрируется использование команд и привязок.
    /// Пусть это будет интерактивный англо-русский словарь с возможностью добавления слов с переводом, 
    /// поиска перевода по словам, а также, - изменения или удаления записей.
    /// Будет плюсом наличие режима тренировки с проверкой знаний перевода слов.
    /// Графический интерфейс следует спроектировать самостоятельно.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DictionaryView(true);
        }

    }
}
