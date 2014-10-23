using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EveList8._1.Annotations;
using EveList8._1.DataModel;

namespace EveList8._1.ViewModel
{
    public sealed class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<EventViewModel> _events;
        private EventViewModel _selectedEventViewModel;
        public ObservableCollection<EventViewModel> Events 
        {
            get { return _events; }
            set
            {
                if(value == _events) return;

                _events = value;
                OnPropertyChanged();
            }
        }

        public EventViewModel Selected
        {
            get { return _selectedEventViewModel; }
            set
            {
                _selectedEventViewModel = value;
                NavigateOnEvent(value);
            }
        }

        public MainViewModel()
        {
            Events = new ObservableCollection<EventViewModel>(new []
            {
                new EventViewModel(new Event("LOLc", "fvgchvcaugcvjrgv aj v ajhfajhfg vajfhgv", "../Assets/Logo.png")),
                new EventViewModel(new Event("regt", "fvgchvcaugcvjrgv aj v ajhfajhfg vajfhgv", "../Assets/Logo.png")),
                new EventViewModel(new Event("brtd", "fvgchvcaugcvjrgv aj v ajhfajhfg vajfhgv", "../Assets/Logo.png")),
                new EventViewModel(new Event("verw", "fvgchvcaugcvjrgv aj v ajhfajhfg vajfhgv", "../Assets/Logo.png")),
                new EventViewModel(new Event("vwer", "fvgchvcaugcvjrgv aj v ajhfajhfg vajfhgv", "../Assets/Logo.png"))
            });
        }

        private void NavigateOnEvent(EventViewModel eventViewModel)
        {
            App.rootFrame.Navigate(typeof (ItemPage));
        }

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
