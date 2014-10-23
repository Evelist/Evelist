namespace EveList8._1.DataModel
{
    public class Event
    {
        public string Name        { get; set; }
        public string Description { get; set; }
        public string Picture     { get; set; }

        public Event(string name, string description, string picture)
        {
            Name = name;
            Description = description;
            Picture = picture;
        }
    }
}
