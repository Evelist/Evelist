using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Security.Authentication.OnlineId;
using Windows.UI.Core;
using EveList8._1.Annotations;
using EveList8._1.Common;
using EveList8._1.DataModel;
using EvelistApi;

namespace EveList8._1.ViewModel
{
    public sealed class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<EventViewModel> _events;
        private EventViewModel _selectedEventViewModel;
        private UserViewModel _userViewModel;

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
                OnPropertyChanged();
            }
        }
        
        public UserViewModel UserViewModel
        {
            get { return _userViewModel ?? (_userViewModel = new UserViewModel(Session.GetInstance().CurrentUser)); }
        }

        public MainViewModel()
        {
            Events = new ObservableCollection<EventViewModel>();
        }

        private void NavigateOnEvent(EventViewModel eventViewModel)
        {
            Messenger<NavigationMessage>.Send(new NavigationMessage("ItemPage", eventViewModel));
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

        public void GetList()
        {
            var api = new EvelistApiClient();
            
            var dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            api.GetEventList(Session.GetInstance().CurrentSession).ContinueWith(r =>
            {
                dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                    foreach (var @event in r.Result
                                            .eventlist
                                            .Select(curr => new Event(int.Parse(curr.event_id), curr.event_name, "", "../Assets/Logo.png"))
                                            .Where(@event => !Events.Any(x => @event.Equals(x.GetEvent()))))
                        Events.Add(new EventViewModel(@event));
                });
                
            });
        }
    }
}
