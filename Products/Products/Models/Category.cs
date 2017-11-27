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
        DialogService dialogService;
        #endregion

        #region Constructors
        public Category()
        {
            dialogService = new DialogService();
            navigationService = new NavigationService();
        }
        #endregion

        #region Properties
        public int CategoryId { get; set; }
        public string Description { get; set; }

        public List<Product> Products { get; set; }
        #endregion


        #region Commands
        public ICommand DeleteCommand { get { return new RelayCommand(Delete); } }

        async void Delete()
        {
            var response = await dialogService.ShowConfirm("Confirm", "Are you sure to delete this record?");
            if (!response)
            {
                return;
            }

            CategoriesViewModel.GetInstance().DeleteCategory(this);
        }

        public ICommand EditCommand { get { return new RelayCommand(Edit); } }

        async void Edit()
        {
            MainViewModel.GetInstance().EditCategory = new EditCategoryViewModel(this);
            await navigationService.Navigate("EditCategoryView");
        }

        public ICommand SelectCategoryCommand { get { return new RelayCommand(SelectCategory); } }

        private async void SelectCategory()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Products = new ProductsViewModel(Products);
            await navigationService.Navigate("ProductsView");

        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return CategoryId;
        }
        #endregion
    }
}
