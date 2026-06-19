using System;

namespace MunicipalServicesApp.Models
{
    public class LocalEvent : IComparable<LocalEvent>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int Priority { get; set; }

        public int CompareTo(LocalEvent other)
        {
            return this.EventDate.CompareTo(other.EventDate);
        }

        public override string ToString()
        {
            return $"{EventDate:yyyy-MM-dd} - {Title}";
        }
    }
}