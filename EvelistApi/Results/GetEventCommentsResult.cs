using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvelistApi.Results
{
    public class GetEventCommentsResult : BaseResult
    {
        public IList<Comment> commentList;
    }

    public class Comment
    {
        public int    comment_id  { get; set; }
        public string firstname   { get; set; }
        public string surname     { get; set; }
        public string avatar_url  { get; set; }
        public string content     { get; set; }
        public string criate_date { get; set; }
        public int    create_time { get; set; }
    }
}
