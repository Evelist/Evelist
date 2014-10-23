using System.Collections.Generic;
using System.Linq;

namespace EvelistApi.Results
{
    public class Service
    {
        public string service { get; set; }
    }

    public class GetLinkedServicesResult : BaseResult
    {
        public List<Service> service { get; set; }

        public ServicesArray ServicesArray
        {
            get { return new ServicesArray(service.Select(s => s.service).ToList()); }
        }
    }
}
