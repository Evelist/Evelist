using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EveList8._1.Annotations;
using EveList8._1.DataModel;

namespace EveList8._1.ViewModel
{
    public class EventViewModel : INotifyPropertyChanged
    {
        private readonly Event _event;

        public EventViewModel(Event @event)
        {
            _event = @event;
        }

        public string Name        
        {
            get { return _event.Name; }
            set
            {
                if(value == Name) return;

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

        #region INotifyPropertyChanged members

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
