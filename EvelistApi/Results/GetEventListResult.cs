using System.Collections.Generic;

namespace EvelistApi.Results
{
    public class GetEventListResult:BaseResult
    {
        public List<EvelistEvent> eventlist { get; set; }
    }

    public class EvelistEvent
    {
        public string event_id { get; set; }
        public string event_name { get; set; }
        public string status_e_u { get; set; }
    }

}
