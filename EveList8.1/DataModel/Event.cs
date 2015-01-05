using System;

namespace EveList8._1.DataModel
{
    public class Event
    {
        public int Id { get; set; }
        public string Name        { get; set; }
        public string Description { get; set; }
        public string Picture     { get; set; }
        public string InitiatorName { get; set; }
        public string Place { get; set; }
        public string City { get; set; }
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
        public int OpenStatus { get; set; }//TODO WTF?
        public Privacy PrivasyStatus { get; set; }

        public Event(int id, string name, string description, string picture)
        {
            Name = name;
            Description = description;
            Picture = picture;
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Event) obj);
        }
        private bool Equals(Event other)
        {
            return string.Equals(Name, other.Name);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Picture != null ? Picture.GetHashCode() : 0);
                return hashCode;
            }
        }

    }
    public enum Privacy { Public = 1 }
}
