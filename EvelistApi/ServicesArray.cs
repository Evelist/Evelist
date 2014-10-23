using System.Collections.Generic;

namespace EvelistApi
{
    public class ServicesArray : List<string>
    {
        public ServicesArray(bool vk, bool facebook, bool twitter, bool google)
        {

            if (vk)
                Add("vkontakte");
            if (facebook)
                Add("facebook");
            if (twitter)
                Add("twitter");
            if (google)
                Add("google_oauth");
        }

        public ServicesArray()
        {

        }

        public ServicesArray(List<string> source)
            : base(source)
        {

        }
    }
}
