using System;

namespace MunicipalServicesApp.Models
{
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        public int RequestID { get; set; }
        public string RequestId { get; set; } // String version for compatibility
        public string CitizenName { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime SubmissionDate { get; set; } // Fixed: was SubmittedDate
        public int Priority { get; set; } // 1=Low, 2=Medium, 3=High, 4=Critical
        public string AttachedFilePath { get; set; }
        public object SubmittedDate { get; internal set; }

        public ServiceRequest()
        {
            SubmissionDate = DateTime.Now;
            Status = "Pending";
            Priority = 2; // Medium
        }

        public ServiceRequest(string requestId, string location, string category,
                            string description, int priority = 2)
        {
            RequestId = requestId;
            RequestID = int.TryParse(requestId, out int id) ? id : new Random().Next(1000, 9999);
            Location = location;
            Category = category;
            Description = description;
            Priority = priority;
            Status = "Pending";
            SubmissionDate = DateTime.Now;
        }

        public int CompareTo(ServiceRequest other)
        {
            if (other == null) return 1;
            int priorityCompare = this.Priority.CompareTo(other.Priority);
            if (priorityCompare != 0)
                return priorityCompare;
            return this.SubmissionDate.CompareTo(other.SubmissionDate);
        }

        public override bool Equals(object obj)
        {
            return obj is ServiceRequest other && RequestID == other.RequestID;
        }

        public override int GetHashCode()
        {
            return RequestID.GetHashCode();
        }
    }

    public enum PriorityLevel
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }
}