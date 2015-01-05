using System.ComponentModel;
using System.Runtime.CompilerServices;
using EveList8._1.Annotations;
using EveList8._1.DataModel;

namespace EveList8._1.Common
{
    class Session : INotifyPropertyChanged
    {
        private static Session _session;
        private Person _currentUser;

        private Session()
        {
            _currentUser = new Person();
        }

        public static Session GetInstance()
        {
            return _session ?? (_session = new Session());
        }
        public string CurrentSession { get; set; }

        public Person CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        #region 

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
