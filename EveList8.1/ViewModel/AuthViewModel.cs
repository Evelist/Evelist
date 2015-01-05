using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Core;
using Windows.UI.Popups;
using EveList8._1.Annotations;
using EveList8._1.Common;
using EveList8._1.DataModel;
using EvelistApi;

namespace EveList8._1.ViewModel
{
    public sealed class AuthViewModel : INotifyPropertyChanged
    {

        public AuthViewModel()
        {
#if DEBUG
            Login = "nadstas@gmail.com";
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
        
        private RelayCommand _authCommand;
        public ICommand AuthCommand
        {
            get { return _authCommand ?? (_authCommand = new RelayCommand(Auth)); }
        }
        private void Auth()
        {
            var api = new EvelistApiClient();
            var dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;

            api.Auth(Login, Pass).ContinueWith(r =>
            {
                dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                    if (r.Result.IsSuccessed)
                    {
                        var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                        if (!localSettings.Values.ContainsKey("session"))
                            localSettings.Values.Add("session", r.Result.session);
                        else
                            localSettings.Values["session"] = r.Result.session;

                        Session.GetInstance().CurrentSession = r.Result.session;

                        api.GetProfileInfo(r.Result.session).ContinueWith(res =>
                        {
                            var tr = res.Result.info;
                            Session.GetInstance().CurrentUser = new Person(tr.firstname, tr.surname, tr.avatar, tr.sex);
                        });

                        Messenger<NavigationMessage>.Send(new NavigationMessage("Main", ""));
                    }
                    else
                        new MessageDialog(@"Неверный логин\пароль", "Ошибка").ShowAsync();
                });
            });
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
