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
using DeliveryService.Models;
using DeliveryService.ViewModels;

namespace DeliveryService
{
    /// <summary>
    /// Вам необходимо в проекте "DeliveryService" реализовать отображение списка товаров, который входит в состав конкретного заказа на доставку.
    /// При этом следует задействовать элемент управления ListView. 
    /// Далее будет нужно добавить функционал для выполнения основных CRUD-операций с заказами и товарами.
    /// </summary>
    public partial class MainWindow : Window
    {
        OrderView _ov = new OrderView();
        ProductView _pv = new ProductView();
        ProductView _pvAll = new ProductView();

        Order _selectedOrder = null;
        Product _selectedProduct = null;

        public MainWindow()
        {
            InitializeComponent();

            lvOrders.ItemsSource = _ov.ObjCollection;
            lvProducts.ItemsSource = _pv.ObjCollection;

            _pvAll.LoadData();
            cbProductToAdd.ItemsSource = _pvAll.ObjCollection;
            cbProductToAdd.SelectedValuePath = "Id";
            cbProductToAdd.DisplayMemberPath = "Name";

        }

        private void lvOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvOrders.SelectedIndex != -1)
            {
                _selectedOrder = lvOrders.SelectedItem as Order;
                tbOrderAdress.Text = _selectedOrder.Adress;
                tbOrderPhone.Text = _selectedOrder.Mobile;
                tbOrderEmail.Text = _selectedOrder.Email;
                tbOrderDelTime.Text = _selectedOrder.DelivTime.ToString();
                tbOrderStatus.Text = _selectedOrder.Status;
                _pv.LoadData(_selectedOrder.Id);
            }
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            if (IsInputOrderValid())
            {
                EditLocalSelectedOrder();
                _ov.AddObj(_selectedOrder);
                _ov.LoadData();
            }
        }

        private void btnEditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (IsInputOrderValid())
            {
                EditLocalSelectedOrder();
                try
                {
                    _ov.EditObj(_selectedOrder);
                    _ov.LoadData();
                }
                catch (ArgumentNullException anx)
                {
                    Console.WriteLine(anx);
                    MessageBox.Show("Such object doesn't exist", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void btnRemoveOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedOrder != null)
            {
                try
                {
                    _ov.RemoveObj(_selectedOrder.Id);
                    _ov.LoadData();
                }
                catch (ArgumentNullException anx)
                {
                    Console.WriteLine(anx);
                    MessageBox.Show("Such object doesn't exist", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void btnClearOrder_Click(object sender, RoutedEventArgs e)
        {
            tbOrderAdress.Text = tbOrderPhone.Text = tbOrderEmail.Text = tbOrderDelTime.Text = tbOrderStatus.Text = "";
        }

        private bool IsInputOrderValid()
        {
            if (!string.IsNullOrEmpty(tbOrderAdress.Text.Trim(' ')))
                if (!string.IsNullOrEmpty(tbOrderPhone.Text.Trim(' ')))
                    if (!string.IsNullOrEmpty(tbOrderEmail.Text.Trim(' ')))
                        if (!string.IsNullOrEmpty(tbOrderDelTime.Text.Trim(' ')))
                        {
                            try
                            {
                                DateTime dt = DateTime.Parse(tbOrderDelTime.Text.Trim(' '));
                                if (dt.Year < 2000)
                                    throw new FormatException();
                            }
                            catch (FormatException)
                            {
                                MessageBox.Show("Wrong date format", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return false;
                            }
                            if (!string.IsNullOrEmpty(tbOrderStatus.Text.Trim(' ')))
                                return true;
                            else
                                MessageBox.Show("Status can't be empty", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                            MessageBox.Show("Delivery time can't be empty", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else
                        MessageBox.Show("Email can't be empty", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    MessageBox.Show("Phone can't be empty", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
                MessageBox.Show("Adress can't be empty", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

            return false;
        }

        private bool IsInputProductValid()
        {
            if (!string.IsNullOrEmpty(tbProductName.Text.Trim(' ')))
                if (!string.IsNullOrEmpty(tbProductValue.Text.Trim(' ')))
                {
                    try
                    {
                        var value = decimal.Parse(tbProductValue.Text.Trim(' ').Replace('.', ','));
                        if (value < 0)
                            throw new ApplicationException("Calue can't be less then zero");
                        else
                            return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Wrong product value format : {ex.Message}");
                        MessageBox.Show("Wrong product value format", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                    MessageBox.Show("Product value can't be empty", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
                MessageBox.Show("Product name can't be empty", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

            return false;
        }

        private void EditLocalSelectedOrder()
        {
            if (_selectedOrder == null)
                _selectedOrder = new Order();

            _selectedOrder.Adress = tbOrderAdress.Text.Trim(' ');
            _selectedOrder.Mobile = tbOrderPhone.Text.Trim(' ');
            _selectedOrder.Email = tbOrderEmail.Text.Trim(' ');
            _selectedOrder.DelivTime = DateTime.Parse(tbOrderDelTime.Text.Trim(' '));
            _selectedOrder.Status = tbOrderStatus.Text.Trim(' ');
        }

        private void EditLocalSelectedProduct()
        {
            if (_selectedProduct == null)
                _selectedProduct = new Product();

            _selectedProduct.Name = tbProductName.Text.Trim(' ');
            _selectedProduct.Value = decimal.Parse(tbProductValue.Text.Trim(' ').Replace('.', ','));
        }

        private void lvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvProducts.SelectedIndex != -1)
            {
                _selectedProduct = lvProducts.SelectedItem as Product;
                tbProductName.Text = _selectedProduct.Name;
                tbProductValue.Text = _selectedProduct.Value.ToString();
                cbProductToAdd.SelectedIndex = -1;
            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (cbProductToAdd.SelectedIndex != -1 && IsInputProductValid())
            {
                if (_selectedOrder != null)
                {
                    _selectedProduct = cbProductToAdd.SelectedItem as Product;
                    try
                    {
                        _selectedOrder.Products.Add(_selectedProduct);
                        //_ov.EditObj(_selectedOrder);
                        _ov.AddProductToOrder(_selectedProduct.Id, _selectedOrder.Id);
                        _pv.LoadData(_selectedOrder.Id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nAdd product to order ex : {ex}\n");
                        MessageBox.Show("Item already exist", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                    MessageBox.Show("There no selected order", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (IsInputProductValid())
            {
                EditLocalSelectedProduct();
                _pv.AddObj(_selectedProduct);
                _pvAll.LoadData();
            }
        }

        private void btnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedProduct != null)
            {
                try
                {
                    if (MessageBox.Show("Editing of this this object cause Editing equalent objects in another orders.\nAre you sure? ", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (IsInputProductValid())
                        {
                            EditLocalSelectedProduct();
                            _pv.EditObj(_selectedProduct);
                            _pvAll.LoadData();
                            _pv.LoadData(_selectedOrder.Id);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Edit product exception : {ex.Message}");
                    MessageBox.Show("Such object doesn't exist", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }

        private void btnRemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedProduct != null)
            {
                try
                {
                    if (MessageBox.Show("Removal of this this object cause removal equalent objects from another orders.\nAre you sure? ", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        _pv.RemoveObj(_selectedProduct.Id);
                        _pvAll.LoadData();
                        _pv.LoadData(_selectedOrder.Id);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Edit product exception : {ex.Message}");
                    MessageBox.Show("Such object doesn't exist", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void btnClearProduct_Click(object sender, RoutedEventArgs e)
        {
            tbProductName.Text = tbProductValue.Text = "";
            cbProductToAdd.SelectedIndex = -1;
        }

        private void tbProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cbProductToAdd.SelectedIndex != -1)
                cbProductToAdd.SelectedIndex = -1;
        }
    }
}
