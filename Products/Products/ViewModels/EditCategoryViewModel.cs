using GalaSoft.MvvmLight.Command;
using Products.Models;
using Products.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Products.ViewModels
{
    public class EditCategoryViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #region Services
        NavigationService navigationService;
        DialogService dialogService;
        ApiService apiService;
        #endregion
        #region Atributes
        bool _isRunning;
        bool _isEnabled;
        private Category category;

        #endregion
        #region Properties
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value) _isEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
            }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                if (_isRunning != value) _isRunning = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
            }
        }

        public string Description { get; set; }

        #endregion
        #region constructors
        public EditCategoryViewModel(Category category)
        {
            this.category = category;
            navigationService = new NavigationService();
            apiService = new ApiService();
            dialogService = new DialogService();

            Description = category.Description;
            IsEnabled = true;
        }
        #endregion
        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        async void Save()
        {
            if (string.IsNullOrEmpty(Description))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "You must enter a category description");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            category.Description = Description;
      

            var mainViewModel = MainViewModel.GetInstance();
            var response = await apiService.Put("http://productsapi82.azurewebsites.net",
               "/api",
               "/Categories",
               mainViewModel.Token.TokenType,
               mainViewModel.Token.AccessToken,
               category);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage(
                    "Error",
                    response.Message);
                return;
            }

            var categoriesViewModel = CategoriesViewModel.GetInstance();
            categoriesViewModel.UpdateCategory(category);
            await navigationService.Back();

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion


    }
}
