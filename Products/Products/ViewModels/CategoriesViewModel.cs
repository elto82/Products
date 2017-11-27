using GalaSoft.MvvmLight.Command;
using Products.Models;
using Products.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Products.ViewModels
{
    public class CategoriesViewModel: INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Atributes
        bool _isRefreshing;
        List<Category> categories;
        ObservableCollection<Category> _categories;
        #endregion

        #region Services
        DialogService dialogService;
        ApiService apiService;

        #endregion

        #region Properties
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
                }
            }
        }
        public ObservableCollection<Category> CategoriesList
        {
            get
            {
                return _categories;
            }
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CategoriesList)));
                }
            }
        }
        #endregion

        #region Contructrs
        public CategoriesViewModel()
        {
            instance = this;
            dialogService = new DialogService();
            apiService = new ApiService();
            LoadCategories();
        }

        #endregion

        #region Singleton
        static CategoriesViewModel instance;
        public static CategoriesViewModel GetInstance()
        {
            if (instance == null)
            {
                return new CategoriesViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods
        public async Task DeleteCategory(Category category)
        {
            IsRefreshing = true;

         
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRefreshing = false;

                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }



            var mainViewModel = MainViewModel.GetInstance();
            var response = await apiService.Delete(
                "http://productsapi82.azurewebsites.net",
               "/api",
               "/Categories",
               mainViewModel.Token.TokenType,
               mainViewModel.Token.AccessToken,
               category);

            if (!response.IsSuccess)
            {
                IsRefreshing = false;

                await dialogService.ShowMessage(
                    "Error",
                    response.Message);
                return;
            }

            categories.Remove(category);
            CategoriesList = new ObservableCollection<Category>(
                categories.OrderBy(c => c.Description));
            IsRefreshing = false;

        }

        public void AddCategory(Category category)
        {
            categories.Add(category);
            CategoriesList = new ObservableCollection<Category>(categories.OrderBy(c => c.Description));
            IsRefreshing = false;

        }
        public void UpdateCategory(Category category)
        {
            var oldCategory = categories.Where(c => c.CategoryId == category.CategoryId).FirstOrDefault();
            oldCategory = category;

            CategoriesList = new ObservableCollection<Category>(categories.OrderBy(c => c.Description));
            IsRefreshing = false;

        }
        async void LoadCategories()
        {
            IsRefreshing = true;
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage(
                    "Error", 
                    connection.Message);
                return;

            }

            var mainViewModel = MainViewModel.GetInstance();
            var response = await apiService.GetList<Category>(
                "http://productsapi82.azurewebsites.net",
                "/api",
                "/Categories"
                , mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage(
                    "Error", 
                    response.Message);
                return;
            }

            categories =(List<Category>)response.Result;
            CategoriesList = new ObservableCollection<Category>(categories.OrderBy(c => c.Description));
            IsRefreshing = false;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadCategories);
            }
        }
        #endregion

    }

}
