﻿namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using System;
    using Products.Services;

    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
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
            apiService = new ApiService();
            dialogService = new DialogService();
            IsEnabled = true;
            IsToggled = true;
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
            _isEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                _isEnabled = true;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var response = await apiService.GetToken("http://productsapi82.azurewebsites.net", Email, Password);

            if (response == null || string.IsNullOrEmpty( response.AccessToken ))
            {

                IsRunning = false;
                _isEnabled = true;
                await dialogService.ShowMessage("Error", response.ErrorDescription);
                Password = null;
                return;
            }

            await dialogService.ShowMessage("taran", "welcome");
            IsRunning = false;
            _isEnabled = true;
        }
        #endregion

    }
}
