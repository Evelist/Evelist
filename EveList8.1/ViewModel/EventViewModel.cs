using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Core;
using EveList8._1.Annotations;
using EveList8._1.Common;
using EveList8._1.DataModel;

namespace EveList8._1.ViewModel
{
    public sealed class EventViewModel : INotifyPropertyChanged
    {
        private readonly Event _event;
        private ObservableCollection<Comment> _comments;
 
        public EventViewModel(Event @event)
        {
            _event = @event;
            _comments = new ObservableCollection<Comment>();

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var api = new EvelistApi.EvelistApiClient();
            var dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;

            api.GetEventInfo(Session.GetInstance().CurrentSession, _event.Id.ToString()).ContinueWith(r =>
            {
                dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                    var e = r.Result.eventinfo.First();

                    _event.Picture       = e.avatar;
                    _event.StarTime      = GetTime(e.date_start);
                    _event.EndTime       = GetTime(e.date_end);
                    _event.InitiatorName = e.initiator_name;
                    _event.City          = e.city_name;
                    _event.OpenStatus    = e.open_status;
                           Description   = e.event_desc;
                    _event.PrivasyStatus = (Privacy)e.event_privacy;
                    _event.Place         = e.place;
                });
                
            });
            api.GetEventComment(Session.GetInstance().CurrentSession, _event.Id.ToString()).ContinueWith(r =>
            {
                dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                    _comments.Clear();
                    var e = r.Result.commentList;
                    foreach (var comment in e)
                    {
                        _comments.Add(new Comment(
                            comment.comment_id,
                            DateTime.Now,
                            DateTime.Now, //TODO что-то надо сделать со временем
                            comment.content,
                            new Person(
                                comment.firstname,
                                comment.surname,
                                comment.avatar_url)
                            ));
                    }
                });

            });
        }

        public string Name        
        {
            get { return _event.Name; }
            set
            {
                if (value == Name) return;

                _event.Name = value;
                OnPropertyChanged();
            }
        }
        public string Description 
        {
            get { return _event.Description; }
            set
            {
                if (value == _event.Description) return;

                _event.Description = value;
                OnPropertyChanged();
            }
        }
        public string Picture     
        {
            get { return _event.Picture; }
            set
            {
                if (value == _event.Picture) return;

                _event.Picture = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<Comment> Comments
        {
            get
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                var api = new EvelistApi.EvelistApiClient();
                var dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;

                api.GetEventComment(Session.GetInstance().CurrentSession, _event.Id.ToString()).ContinueWith(r =>
                {
                    dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                    {
                        _comments.Clear();
                        var e = r.Result.commentList;
                        foreach (var comment in e)
                        {
                            _comments.Add(new Comment(
                                comment.comment_id,
                                DateTime.Now, 
                                DateTime.Now, //TODO что-то надо сделать со временем
                                comment.content,
                                new Person(
                                    comment.firstname,
                                    comment.surname,
                                    comment.avatar_url)
                                ));
                        }
                    });

                });
                return _comments;
            }
        }

        public Event GetEvent()
        {
            return _event;
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

        private DateTime GetTime(string s)
        {
            return (s == "0000-00-00 00:00:00")
                ? DateTime.MinValue
                : DateTime.Parse(s);
        }
    }
}
