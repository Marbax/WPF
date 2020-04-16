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
    public class ProductView : AViewModel<Product>
    {
        Product SelectedProduct;

        public ProductView() : base()
        {
            //LoadData();
        }

        public override void LoadData()
        {
            ObjCollection.Clear();
            foreach (var prod in _dsm.Products)
                ObjCollection.Add(prod);
        }

        public void LoadData(int orderId)
        {
            ObjCollection.Clear();
            foreach (var prod in _dsm.Products)
                if (prod.Orders.Any(item => item.Id == orderId))
                    ObjCollection.Add(prod);
        }

        public override void AddObj(Product toAdd)
        {
            _dsm.Products.Add(toAdd);
            _dsm.SaveChanges();
        }

        public override void EditObj(Product toEdit)
        {
            try
            {
                var obj = _dsm.Products.SingleOrDefault(item => item.Id == toEdit.Id);
                _dsm.Entry(obj).CurrentValues.SetValues(toEdit);
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
                _dsm.Products.Remove(_dsm.Products.Where(item => item.Id == id).FirstOrDefault());
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
