﻿namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using System;
    using Products.Services;
    using Xamarin.Forms;
    using Products.Views;

    public class LoginViewModel : INotifyPropertyChanged
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
        string _email;
        string _password;
        bool _isToggled;
        bool _isRunning;
        bool _isEnabled;
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

        public bool IsToggled
        {
            get { return _isToggled; }
            set
            {
                if (_isToggled != value) _isToggled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsToggled)));
            }
        }

        public string  Email
        {
            get { return _email; }
            set
            {
                if (_email!= value) _email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value) _password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
            }
        }
       
        #endregion

        #region Constructrs
        public LoginViewModel()
        {
            navigationService = new NavigationService();
            apiService = new ApiService();
            dialogService = new DialogService();
            IsEnabled = true;
            IsToggled = true;
            Email= "antonio@gmail.com";
            Password = "123456";
        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowMessage("Error", "You must enter email");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "You must enter password");
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

            var response = await apiService.GetToken("http://productsapi82.azurewebsites.net",
                Email,
                Password);

            if (response == null)
            {

                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage(
                    "Error", 
                    "the service is not available, please try latter.");
                Password = null;
                return;
            }

            if (string.IsNullOrEmpty(response.AccessToken))
            {

                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage(
                    "Error", 
                    response.ErrorDescription);
                Password = null;
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Categories = new CategoriesViewModel();
            mainViewModel.Token = response;
            await navigationService.Navigate("CategoriesView");

            Email = null;
            Password = null;

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion

    }
}
