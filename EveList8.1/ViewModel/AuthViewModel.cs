using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Popups;
using EveList8._1.Annotations;
using EveList8._1.Common;
using EvelistApi;
using EvelistApi.Results;

namespace EveList8._1.ViewModel
{
    public sealed class AuthViewModel : INotifyPropertyChanged
    {

        public AuthViewModel()
        {
#if DEBUG
            Login = "3da@rambler.ru";
            Pass = "123123";
#endif
        }

        private string _login;
        private string _pass;

        public string Login
        {
            get { return _login; }
            set
            {
                if (value == _login) return;

                _login = value;
                OnPropertyChanged();
            }
        }
        public string Pass
        {
            get { return _pass; }
            set
            {
                if (value == _pass) return;

                _pass = value;
                OnPropertyChanged();
            }
        }

        #region Auth Command

        private AuthResult result;

        private RelayCommand _authCommand;
        public ICommand AuthCommand
        {
            get { return _authCommand ?? (_authCommand = new RelayCommand(Auth)); }
        }
        private void Auth()
        {
            var api = new EvelistApiClient();
            api.Auth(Login, Pass).ContinueWith(r => {

                                                        result = r.Result;
                                                        App.rootFrame.Navigate(typeof(PivotPage));
            });
            //App.rootFrame.Navigate(typeof(PivotPage));
        }


        #endregion

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
