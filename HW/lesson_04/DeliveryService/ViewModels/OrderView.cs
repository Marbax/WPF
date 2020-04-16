using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations; // add or update
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DeliveryService.Models;

namespace DeliveryService.ViewModels
{
    public class OrderView : AViewModel<Order>
    {
        public OrderView() : base()
        {
            LoadData();
        }

        public override void LoadData()
        {
            ObjCollection.Clear();
            foreach (var order in _dsm.Orders)
                ObjCollection.Add(order);
        }

        public override void AddObj(Order toAdd)
        {
            _dsm.Orders.Add(toAdd);
            _dsm.SaveChanges();
        }

        public void AddProductToOrder(int prodId ,int orderId )
        {
            var order = _dsm.Orders.SingleOrDefault(item => item.Id == orderId);
            var prod = _dsm.Products.SingleOrDefault(item => item.Id == prodId);

            order.Products.Add(prod);
            //prod.Orders.Add(order);
            //_dsm.Products.Attach(prod);
            _dsm.Orders.Attach(order);
            _dsm.SaveChanges();
        }

        //just example
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(
                    obj =>
                    {
                        Order order = obj as Order;
                        AddObj(order);
                        Console.WriteLine($"Added order : \n{order.ToString()}");
                        LoadData();
                    }, (obj) => obj is Order));
            }
        }


        public override void EditObj(Order toEdit)
        {
            try
            {
                var obj = _dsm.Orders.SingleOrDefault(item => item.Id == toEdit.Id);
                /*
                obj.Adress = toEdit.Adress;
                obj.DelivTime = toEdit.DelivTime;
                obj.Email = toEdit.Email;
                obj.Mobile = toEdit.Mobile;
                obj.Status = toEdit.Status;
                obj.Products = toEdit.Products;
                */
                //_dsm.Orders.AddOrUpdate(obj); // should be same as above , but that's bad method
                _dsm.Entry(obj).CurrentValues.SetValues(toEdit); // same as above , good method without property numeration
                _dsm.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void RemoveObj(int id)
        {
            try
            {
                _dsm.Orders.Remove(_dsm.Orders.Where(item => item.Id == id).FirstOrDefault());
                _dsm.SaveChanges();
                LoadData();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }
    }
}
