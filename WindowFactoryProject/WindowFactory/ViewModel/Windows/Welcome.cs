using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WindowFactory.Command;
using WindowFactory.NotifyObject;
using WindowFactory.Service;

namespace WindowFactory.ViewModel.Windows
{
    class Welcome : NotificationObject
    {
        private Action<string> _onSuccess;
        private ILoginService _loginService;


        public string LoginText { get; set; }
        public string PasswordText { get; set; }

        public Brush LoginStatusColor { get; set; }
        public Brush PasswordStatusColor { get; set; }

        public string LoginStatusText { get; set; }
        public string PasswordStatusText { get; set; }

        public ICommand ButtonCommand { get; set; }
        public Visibility LoadingVisibility { get; set; }

        public Welcome(ILoginService loginService, Action<string> onSuccess)
        {
            if (onSuccess == null) throw new ArgumentNullException(nameof(onSuccess));
            if (loginService == null) throw new ArgumentNullException(nameof(loginService));

            _onSuccess = onSuccess;
            _loginService = loginService;

            LoadingVisibility = Visibility.Hidden;

            ButtonCommand = new DelegateCommand(OnButtonPress);
        }

        private async void OnButtonPress()
        {
            IsLoading(true);

            var status = await LoginAsync(LoginText, PasswordText);

            IsLoading(false);

            if (status.loginIsCorrect && status.passwordIsCorrect)
                ShowUserWorkPlace(LoginText);
            else
                UpdateStatuses(status);
        }

        private void IsLoading(bool isLoading)
        {
            LoadingVisibility = isLoading ? Visibility.Visible : Visibility.Hidden;
            OnPropertyChanged(nameof(LoadingVisibility));
        }
        private async Task<(bool loginIsCorrect, bool passwordIsCorrect)> LoginAsync(string login, string password)
        {
            return await Task.Run(() => Login(login, password));
        }

        private (bool loginIsCorrect, bool passwordIsCorrect) Login(string login, string password)
        {
            return (_loginService.LoginIsValid(login), _loginService.Login(login, password));
        }

        private void ShowUserWorkPlace(string login)
        {
            _onSuccess.Invoke(login);
        }

        private void UpdateStatuses((bool loginIsCorrect, bool passwordIsCorrect) status)
        {
            SetLoginStatusLabel(status.loginIsCorrect);

            if (status.loginIsCorrect)
            {
                SetPasswordStatusLabel(status.passwordIsCorrect);
            }
            else
            {
                PasswordStatusText = "";
                OnPropertyChanged(nameof(PasswordStatusText));
            }
        }

        private void SetLoginStatusLabel(bool isCorrect)
        {
            LoginStatusText = isCorrect ? "✓" : "✗";
            LoginStatusColor = new SolidColorBrush(isCorrect ? Colors.Green : Colors.Red);

            OnPropertyChanged(nameof(LoginStatusText));
            OnPropertyChanged(nameof(LoginStatusColor));
        }

        private void SetPasswordStatusLabel(bool isCorrect)
        {
            PasswordStatusText = isCorrect ? "✓" : "✗";
            PasswordStatusColor = new SolidColorBrush(isCorrect ? Colors.Green : Colors.Red);

            OnPropertyChanged(nameof(PasswordStatusText));
            OnPropertyChanged(nameof(PasswordStatusColor));
        }
    }
}