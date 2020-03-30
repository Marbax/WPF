using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Необходимо разработать приложение «Калькулятор» . 
        /// В верхней части приложения необходимо использовать два поля для ввода текста.                                       +
        /// Первое используется для отображения предыдущих операций, а второе — для ввода текущего числа.                       +
        /// Оба поля должны запрещать редактировать свое содержимое посредством клавиатурного ввода.                            +
        /// Данные поля будут заполняться автоматически при нажатии на соответствующие кнопки, расположенные ниже.              +
        /// Кнопки «0» — «9» добавляют соответствующую цифру в конец текущего числа.                                            +
        /// При этом должны выполняться проверки, не допускающие неправильного ввода.                                           
        /// Например, нельзя вводить числа, начинающиеся с ноля, после которого нет десятичной точки.                           +
        /// Кнопка «.» добавляет (если это возможно) десятичную точку в текущее число.                                          +
        /// Кнопки «/», «*», «+», «-» выполняют соответствующую операцию над результатом предыдущей операции и текущим числом.  
        /// Кнопка «=» вычисляет выражение и выводит результат.                                                                 
        /// Кнопка «CE» очищает текущее число.                                                                                  +
        /// Кнопка «C» очищает текущее число и предыдущее выражение.                                                            +
        /// Кнопка «<» очищает последний введенный символ в текущем числе.                                                      +
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCE_Click(object sender, RoutedEventArgs e)
        {
            tbCurOp.Clear();
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            tbPrevOp.Text = tbCurOp.Text = "";
        }

        private void btnBkspc_Click(object sender, RoutedEventArgs e)
        {
            if (tbCurOp.Text.Length > 0)
                tbCurOp.Text = tbCurOp.Text.Remove(tbCurOp.Text.Length - 1, 1);
        }

        private void btnDiv_Click(object sender, RoutedEventArgs e)
        {
            tbCurOp.AppendText(" / ");
        }

        private void btnSeven_Click(object sender, RoutedEventArgs e)
        {
            RemZeroIfStartsWith();
            tbCurOp.AppendText("7");
        }

        private void btnEight_Click(object sender, RoutedEventArgs e)
        {
            RemZeroIfStartsWith();
            tbCurOp.AppendText("8");
        }

        private void btnNine_Click(object sender, RoutedEventArgs e)
        {
            RemZeroIfStartsWith();
            tbCurOp.AppendText("9");
        }

        private void btnMult_Click(object sender, RoutedEventArgs e)
        {
            tbCurOp.AppendText(" * ");
        }

        private void btnFour_Click(object sender, RoutedEventArgs e)
        {
            RemZeroIfStartsWith();
            tbCurOp.AppendText("4");
        }

        private void btnFive_Click(object sender, RoutedEventArgs e)
        {
            RemZeroIfStartsWith();
            tbCurOp.AppendText("5");
        }

        private void btnSix_Click(object sender, RoutedEventArgs e)
        {
            RemZeroIfStartsWith();
            tbCurOp.AppendText("6");
        }

        private void btnSub_Click(object sender, RoutedEventArgs e)
        {
            tbCurOp.AppendText(" - ");
        }

        private void btnOne_Click(object sender, RoutedEventArgs e)
        {
            RemZeroIfStartsWith();
            tbCurOp.AppendText("1");
        }

        private void btnTwo_Click(object sender, RoutedEventArgs e)
        {
            RemZeroIfStartsWith();
            tbCurOp.AppendText("2");
        }

        private void btnThree_Click(object sender, RoutedEventArgs e)
        {
            RemZeroIfStartsWith();
            tbCurOp.AppendText("3");
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            tbCurOp.AppendText(" + ");
        }

        private void btnDot_Click(object sender, RoutedEventArgs e)
        {
            if (tbCurOp.Text.Length == 0 || tbCurOp.Text[tbCurOp.Text.Length - 1] == ' ')
                tbCurOp.AppendText("0.");
            else if (!GetLastNumber().Contains('.'))
                tbCurOp.AppendText(".");
        }

        private void btnZero_Click(object sender, RoutedEventArgs e)
        {
            if (tbCurOp.Text.Length == 0 || tbCurOp.Text[tbCurOp.Text.Length - 1] == ' ')
                tbCurOp.AppendText("0");
            else if (!(GetLastNumber() == "0"))
                tbCurOp.AppendText("0");
        }

        private void btnEqual_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(GetLastNumber());
        }

        private string GetLastNumber()
        {
            string revNum = "";
            for (int i = tbCurOp.Text.Length - 1; i >= 0; i--)
            {
                if (tbCurOp.Text[i] == ' ')
                    break;
                revNum += tbCurOp.Text[i];
            }

            string num = "";
            for (int i = revNum.Length - 1; i >= 0; i--)
                num += revNum[i];

            return num;
        }

        private void RemZeroIfStartsWith()
        {
            if (tbCurOp.Text.Length > 0 && (GetLastNumber() == "0"))
                tbCurOp.Text = tbCurOp.Text.Remove(tbCurOp.Text.Length - 1);
        }
    }
}
