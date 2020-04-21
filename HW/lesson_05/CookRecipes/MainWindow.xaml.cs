using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace CookRecipes
{
    /// <summary>
    /// Попробуйте построить каркас приложения "Справочник кулинарных рецептов"
    /// по описанию в ДЗ к презентации "Потоковые документы".
    /// 
    /// Требования: ListBox с минимум 5-ю рецептами, отображение рецепта в FlowDocumentReader, 
    /// в документе должно отображаться в заголовке имя рецепта, Обязательно должна быть картинка готового блюда,
    /// так же необходима таблица или список продуктов которые необходимы для приготовления данного блюда, и само описание.
    /// Данные о рецептах можете «зашить» в программу или подключиться к XML или базе данных.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SwitchProvider();
        }

        private void SwitchProvider(string key = "RecipesProvider")
        {
            try
            {
                XmlDataProvider xmlData = FindResource(key) as XmlDataProvider;
                Binding binding = new Binding() { Source = xmlData };
                BindingOperations.SetBinding(tvRecipes, TreeView.ItemsSourceProperty, binding);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Switch provider ex: {ex}");
            }
        }

        private void tvRecipes_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                var xmlElem = ((TreeView)sender).SelectedItem as XmlElement;
                if (xmlElem.Attributes["name"] != null && xmlElem.Attributes["path"] != null)
                {
                    string fullPath = GetXmlElemPath(xmlElem);

                    if (File.Exists(fullPath))
                        using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                            fdrSelectedRecipe.Document = XamlReader.Load(fs) as FlowDocument;
                    else
                        fdrSelectedRecipe.Document = new FlowDocument();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TreeView item cahnge ex : {ex}");
                MessageBox.Show("404");
            }
        }
        private static string GetXmlElemPath(XmlElement xmlElem)
        {
            return @"..\..\XMLPages" + xmlElem.Attributes["path"].Value + xmlElem.Attributes["name"].Value + ".xaml";
        }
    }
}
