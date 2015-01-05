using System.ComponentModel;
using System.Runtime.CompilerServices;
using EveList8._1.Annotations;
using EveList8._1.Common;
using EveList8._1.DataModel;

namespace EveList8._1.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private Person _user;

        public UserViewModel(Person user)
        {
            _user = user;
            Session.GetInstance().PropertyChanged += (sender, args) =>
            {
                _user = Session.GetInstance().CurrentUser;
                OnPropertyChanged("UserName");
                OnPropertyChanged("Sex");
                OnPropertyChanged("Avatar");
            };
        }
        public string UserName
        {
            get { return _user.FirstName; }
            set
            {
                if(_user.FirstName == value) return;

                _user.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get { return _user.SurName; }
        }

        public string Sex
        {
            get { return _user.Sex; }
            set
            {
                if(_user.Sex == value) return;

                _user.Sex = value;
                OnPropertyChanged();
            }
        }

        public string Avatar
        {
            get { return _user.AvatarUrl; }
            set 
            { 
                if(_user.AvatarUrl == value) return;

                _user.AvatarUrl = value;
                OnPropertyChanged();
            }
        }


        #region INotify members

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
