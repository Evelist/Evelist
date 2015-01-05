using System;

namespace EveList8._1.DataModel
{
    public class Comment
    {
        public Comment(int commentId, DateTime date, DateTime time, string content, Person avtor)
        {
            CommentId = commentId;
            Date      = date;
            Time      = time;
            Content   = content;
            Avtor     = avtor;
        }

        public int      CommentId { get; set; }
        public DateTime Date      { get; set; }
        public DateTime Time      { get; set; }
        public string   Content   { get; set; }
        public Person   Avtor     { get; set; }

        public static bool operator ==(Comment a, Comment b)
        {
            return !ReferenceEquals(null, a) &&
                   !ReferenceEquals(null, b) &&
                   a.CommentId == b.CommentId;
        }

        public static bool operator !=(Comment a, Comment b)
        {
            return !(a == b);
        }
    }
}
