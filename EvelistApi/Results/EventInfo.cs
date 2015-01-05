using System.Collections.Generic;

namespace EvelistApi.Results
{
    public class EventInfoResult : BaseResult
    {
        public IList<EventInfo> eventinfo { get; set; }
    }
    public class EventInfo
    {
        public string event_name 	 { get; set; }
        public string initiator_name { get; set; }
        public string city_name 	 { get; set; }
        public string date_start 	 { get; set; }
        public string date_end 		 { get; set; }
        public int   open_status 	 { get; set; }
        public string event_desc 	 { get; set; }
        public int    event_privacy  { get; set; }
        public string place 		 { get; set; }
        public string avatar 		 { get; set; }
    }
}
