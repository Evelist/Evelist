using System.Collections.Generic;

namespace EvelistApi.Results
{
    public class GetProfileInfoResult : BaseResult
    {
        public ProfileInfo info { get; set; }
    }

    public class ProfileInfo
    {
        public string firstname { get; set; }
        public string surname { get; set; }
        public string sex { get; set; }
        public string avatar { get; set; }
    }
}
