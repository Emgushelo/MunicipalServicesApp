using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class FormLocalEvents : Form
    {
        // Data Structures for Part 2 requirements
        private SortedDictionary<DateTime, Queue<LocalEvent>> eventsByDate;
        private Dictionary<string, HashSet<LocalEvent>> eventsByCategory;
       private List<LocalEvent> priorityEventQueue;
        private Stack<LocalEvent> recentEventsStack;
        private Queue<LocalEvent> upcomingEventsQueue;

        // For recommendation feature
        private Dictionary<string, int> searchPatterns;
        private List<LocalEvent> allEvents;
        private List<LocalEvent> recommendedEvents;

        public FormLocalEvents()
        {
            InitializeComponent();
            InitializeDataStructures();
            LoadSampleEvents();
            RefreshEventDisplay();
        }

        private void InitializeDataStructures()
        {
            // SortedDictionary for organizing events by date (automatically sorted)
            eventsByDate = new SortedDictionary<DateTime, Queue<LocalEvent>>();

            // Dictionary with HashSets for category-based organization
            eventsByCategory = new Dictionary<string, HashSet<LocalEvent>>();

            // Priority queue for high-priority events
            priorityEventQueue = new List<LocalEvent>();

            // Stack for recently viewed events
            recentEventsStack = new Stack<LocalEvent>();

            // Queue for upcoming events
            upcomingEventsQueue = new Queue<LocalEvent>();

            // For search tracking and recommendations
            searchPatterns = new Dictionary<string, int>();
            allEvents = new List<LocalEvent>();
            recommendedEvents = new List<LocalEvent>();
        }

        private void LoadSampleEvents()
        {
            var sampleEvents = new[]
            {
                new LocalEvent(1, "Community Clean-up Day", "Environment",
                    DateTime.Now.AddDays(5), "Central Park", "Join us to clean up the community", 1),
                new LocalEvent(2, "Waste Management Workshop", "Education",
                    DateTime.Now.AddDays(7), "Community Hall", "Learn about recycling", 2),
                new LocalEvent(3, "Town Hall Meeting", "Government",
                    DateTime.Now.AddDays(3), "City Hall", "Discuss municipal issues", 1),
                new LocalEvent(4, "Water Conservation Fair", "Environment",
                    DateTime.Now.AddDays(10), "Recreation Center", "Tips for saving water", 2),
                new LocalEvent(5, "Youth Sports Day", "Sports",
                    DateTime.Now.AddDays(14), "Sports Complex", "Fun activities for youth", 3),
                new LocalEvent(6, "Electricity Safety Seminar", "Safety",
                    DateTime.Now.AddDays(4), "Library", "Learn about electrical safety", 1),
                new LocalEvent(7, "Garden Competition", "Community",
                    DateTime.Now.AddDays(12), "Botanical Gardens", "Showcase your garden", 2),
                new LocalEvent(8, "Public Transport Forum", "Transport",
                    DateTime.Now.AddDays(8), "Transit Center", "Discuss transport improvements", 2),
            };

            foreach (var evt in sampleEvents)
            {
                AddEventToStructures(evt);
            }
        }

        private void AddEventToStructures(LocalEvent evt)
        {
            allEvents.Add(evt);

            // Add to SortedDictionary by date
            if (!eventsByDate.ContainsKey(evt.EventDate.Date))
            {
                eventsByDate[evt.EventDate.Date] = new Queue<LocalEvent>();
            }
            eventsByDate[evt.EventDate.Date].Enqueue(evt);

            // Add to Dictionary with HashSet by category
            if (!eventsByCategory.ContainsKey(evt.Category))
            {
                eventsByCategory[evt.Category] = new HashSet<LocalEvent>();
            }
            eventsByCategory[evt.Category].Add(evt);

            // Add to priority queue
            priorityEventQueue.Add(evt);

            // Add to upcoming queue (if date is today or future)
            if (evt.EventDate.Date >= DateTime.Now.Date)
            {
                upcomingEventsQueue.Enqueue(evt);
            }
        }

        private void RefreshEventDisplay()
        {
            lstEvents.Items.Clear();

            // Display events organized by date using SortedDictionary
            foreach (var dateEntry in eventsByDate)
            {
                lstEvents.Items.Add($"=== {dateEntry.Key:dddd, MMMM d, yyyy} ===");
                foreach (var evt in dateEntry.Value)
                {
                    string priorityIcon = evt.Priority == 1 ? "🔴 " : (evt.Priority == 2 ? "🟡 " : "🟢 ");
                    lstEvents.Items.Add($"   {priorityIcon}{evt.Title} - {evt.Location}");
                    lstEvents.Items.Add($"      Category: {evt.Category} | {evt.Description}");
                }
                lstEvents.Items.Add("");
            }
        }

        private void RefreshCategoryFilter()
        {
            cmbCategoryFilter.Items.Clear();
            cmbCategoryFilter.Items.Add("All Categories");
            foreach (var category in eventsByCategory.Keys.OrderBy(c => c))
            {
                cmbCategoryFilter.Items.Add(category);
            }
            cmbCategoryFilter.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim().ToLower();
            string selectedCategory = cmbCategoryFilter.SelectedItem?.ToString();
            DateTime? filterDate = dtpFilterDate.Checked ? dtpFilterDate.Value.Date : (DateTime?)null;

            // Track search pattern for recommendations
            if (!string.IsNullOrEmpty(searchTerm))
            {
                if (!searchPatterns.ContainsKey(searchTerm))
                    searchPatterns[searchTerm] = 0;
                searchPatterns[searchTerm]++;
            }

            var results = PerformSearch(searchTerm, selectedCategory, filterDate);

            // Update recommendations based on search
            UpdateRecommendations(searchTerm, selectedCategory);

            DisplaySearchResults(results);
        }

        private List<LocalEvent> PerformSearch(string searchTerm, string selectedCategory, DateTime? filterDate)
        {
            var results = new List<LocalEvent>();

            foreach (var evt in allEvents)
            {
                bool matches = true;

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    matches = evt.Title.ToLower().Contains(searchTerm) ||
                             evt.Description.ToLower().Contains(searchTerm) ||
                             evt.Location.ToLower().Contains(searchTerm);
                }

                if (matches && selectedCategory != null && selectedCategory != "All Categories")
                {
                    matches = evt.Category == selectedCategory;
                }

                if (matches && filterDate.HasValue)
                {
                    matches = evt.EventDate.Date == filterDate.Value;
                }

                if (matches)
                {
                    results.Add(evt);
                }
            }

            // Sort results by date
            results.Sort();
            return results;
        }

        private void DisplaySearchResults(List<LocalEvent> results)
        {
            lstEvents.Items.Clear();

            if (results.Count == 0)
            {
                lstEvents.Items.Add("No events found matching your criteria.");
                return;
            }

            lstEvents.Items.Add($"Found {results.Count} event(s):");
            lstEvents.Items.Add("");

            foreach (var evt in results)
            {
                string priorityIcon = evt.Priority == 1 ? "🔴 " : (evt.Priority == 2 ? "🟡 " : "🟢 ");
                lstEvents.Items.Add($"{priorityIcon}{evt.EventDate:yyyy-MM-dd} - {evt.Title}");
                lstEvents.Items.Add($"   📍 Location: {evt.Location}");
                lstEvents.Items.Add($"   📂 Category: {evt.Category}");
                lstEvents.Items.Add($"   📝 {evt.Description}");
                lstEvents.Items.Add("");
            }
        }

        private void UpdateRecommendations(string searchTerm, string category)
        {
            recommendedEvents.Clear();
            lstRecommendations.Items.Clear();

            // Use search patterns to find related events
            var relatedCategories = new HashSet<string>();
            var relatedKeywords = new HashSet<string>();

            // Analyze search patterns
            foreach (var pattern in searchPatterns.OrderByDescending(p => p.Value).Take(5))
            {
                relatedKeywords.Add(pattern.Key);
            }

            // Find events matching search patterns
            foreach (var evt in allEvents)
            {
                bool isRecommended = false;

                // Check keyword matches
                foreach (var keyword in relatedKeywords)
                {
                    if (evt.Title.ToLower().Contains(keyword) ||
                        evt.Description.ToLower().Contains(keyword))
                    {
                        isRecommended = true;
                        break;
                    }
                }

                // Check category matches
                if (!isRecommended && !string.IsNullOrEmpty(category) && category != "All Categories")
                {
                    isRecommended = evt.Category == category;
                }

                if (isRecommended && !recommendedEvents.Contains(evt))
                {
                    recommendedEvents.Add(evt);
                }
            }

            // Display recommendations
            if (recommendedEvents.Count > 0)
            {
                lblRecommendationsTitle.Visible = true;
                lstRecommendations.Visible = true;

                foreach (var evt in recommendedEvents.Take(5))
                {
                    lstRecommendations.Items.Add($"💡 {evt.Title} ({evt.EventDate:yyyy-MM-dd})");
                }
            }
            else
            {
                lblRecommendationsTitle.Visible = false;
                lstRecommendations.Visible = false;
            }
        }

        private void btnViewHighPriority_Click(object sender, EventArgs e)
        {
            lstEvents.Items.Clear();
            lstEvents.Items.Add("=== HIGH PRIORITY EVENTS (Priority 1) ===");
            lstEvents.Items.Add("");

     var highPriorityEvents = priorityEventQueue
    .Where(evt => evt.Priority == 1)
    .OrderBy(evt => evt.EventDate);

            foreach (var evt in highPriorityEvents)
            {
                lstEvents.Items.Add($"🔴 {evt.EventDate:yyyy-MM-dd} - {evt.Title}");
                lstEvents.Items.Add($"   📍 {evt.Location}");
                lstEvents.Items.Add($"   📝 {evt.Description}");
                lstEvents.Items.Add("");
            }
        }

        private void btnUpcomingEvents_Click(object sender, EventArgs e)
        {
            lstEvents.Items.Clear();
            lstEvents.Items.Add("=== UPCOMING EVENTS (Next 7 Days) ===");
            lstEvents.Items.Add("");

            var upcoming = allEvents.Where(evt => evt.EventDate.Date >= DateTime.Now.Date &&
                                                  evt.EventDate.Date <= DateTime.Now.AddDays(7))
                                     .OrderBy(evt => evt.EventDate);

            foreach (var evt in upcoming)
            {
                string daysUntil = (evt.EventDate.Date - DateTime.Now.Date).Days == 0 ? "Today!" :
                                   $"in {(evt.EventDate.Date - DateTime.Now.Date).Days} days";
                lstEvents.Items.Add($"📅 {evt.EventDate:yyyy-MM-dd} - {evt.Title} ({daysUntil})");
                lstEvents.Items.Add($"   📍 {evt.Location} | Category: {evt.Category}");
                lstEvents.Items.Add("");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}