using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Products.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Products.ViewModels
{
    public class ProductsViewModel: INotifyPropertyChanged
    {
        #region Atributes
        private List<Product> products;
        ObservableCollection<Product> _products;
        #endregion

        #region Contructor
        public ProductsViewModel(List<Product> products)
        {
            this.products = products;
            Products = new ObservableCollection<Product>(products.OrderBy(p => p.Description));
        }


        #endregion

        #region Properties

        public ObservableCollection<Product> Products
        {
            get
            {
                return _products;
            }
            set
            {
                if (_products != value)
                {
                    _products = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));
                }
            }
        }
        #endregion


        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
