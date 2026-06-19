using System;

namespace MunicipalServicesApp
{
    public class LocalEvent : IComparable<LocalEvent>
    {
        public int EventID { get; set; }
        public string Title { get; set; }
        public string Cat { get; set; }
        public DateTime EventDate { get; set; }
        public string Loc { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string Category
        {
            get => Cat;
            set => Cat = value;
        }

        public string Location
        {
            get => Loc;
            set => Loc = value;
        }

        public LocalEvent(int id, string title, string category,
            DateTime eventDate, string location,
            string description, int priority = 2)
        {
            EventID = id;
            Title = title;
            Cat = category;
            EventDate = eventDate;
            Loc = location;
            Description = description;
            Priority = priority;
        }

        public int CompareTo(LocalEvent other)
        {
            int dateCompare = EventDate.CompareTo(other.EventDate);

            if (dateCompare != 0)
                return dateCompare;

            return Priority.CompareTo(other.Priority);
        }

        public override string ToString()
        {
            return $"{EventDate:yyyy-MM-dd} - {Title} ({Category})";
        }
    }
}