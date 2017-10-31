using GalaSoft.MvvmLight.Command;
using Products.Services;
using Products.ViewModels;
using Products.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Products.Models
{
    public class Category
    {
        #region Services
        NavigationService navigationService;
        #endregion

        #region Constructors
        public Category()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Properties
        public int CategoryId { get; set; }
        public string Description { get; set; }

        public List<Product> Products { get; set; } 
        #endregion

        #region Commands
        public ICommand SelectCategoryCommand { get { return new RelayCommand(SelectCategory); } }

        private async void SelectCategory()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Products = new ProductsViewModel(Products);
            await navigationService.Navigate("ProductsView");

        }
        #endregion
    }
}
