using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp
{
    public partial class LocalEventsForm : Form
    {
        private SortedDictionary<DateTime, List<LocalEvent>> _eventsByDate = new SortedDictionary<DateTime, List<LocalEvent>>();
        private Dictionary<string, List<LocalEvent>> _eventsByCategory = new Dictionary<string, List<LocalEvent>>();
        private HashSet<string> _uniqueCategories = new HashSet<string>();
        private Queue<LocalEvent> _recentSearches = new Queue<LocalEvent>();
        private Stack<LocalEvent> _searchHistory = new Stack<LocalEvent>();
        private PriorityQueue<LocalEvent, int> _recommendations = new PriorityQueue<LocalEvent, int>();

        private List<LocalEvent> _allEvents = new List<LocalEvent>();

        public LocalEventsForm()
        {
            InitializeComponent();
            LoadSampleEvents();
            PopulateCategoryFilter();
            RefreshEventDisplay();
        }

        private void LoadSampleEvents()
        {
            var events = new[]
{
    new LocalEvent(
        1,
        "Community Cleanup Drive",
        "Join us for a neighborhood cleanup. Gloves and bags provided.",
        DateTime.Now.AddDays(7),
        "Central Park",
        "Cleanup",
        10),

    new LocalEvent(
        2,
        "Town Hall Meeting",
        "Monthly community meeting with municipal officials.",
        DateTime.Now.AddDays(14),
        "City Hall",
        "Meeting",
        8),

    new LocalEvent(
        3,
        "Recycling Workshop",
        "Learn about proper recycling practices in our community.",
        DateTime.Now.AddDays(21),
        "Community Center",
        "Education",
        7),

    new LocalEvent(
        4,
        "Water Conservation Seminar",
        "Tips and techniques for saving water at home.",
        DateTime.Now.AddDays(5),
        "Library",
        "Education",
        9),

    new LocalEvent(
        5,
        "Neighborhood Watch Meeting",
        "Discuss safety concerns with local police.",
        DateTime.Now.AddDays(10),
        "Police Station",
        "Safety",
        6)
};

            foreach (var evt in events)
            {
                AddEvent(evt);
            }
        }

        private void AddEvent(LocalEvent evt)
        {
            _allEvents.Add(evt);

            // SortedDictionary for date-based organization
            if (!_eventsByDate.ContainsKey(evt.EventDate.Date))
                _eventsByDate[evt.EventDate.Date] = new List<LocalEvent>();
            _eventsByDate[evt.EventDate.Date].Add(evt);

            // Dictionary for category-based filtering
            if (!_eventsByCategory.ContainsKey(evt.Category))
                _eventsByCategory[evt.Category] = new List<LocalEvent>();
            _eventsByCategory[evt.Category].Add(evt);

            // HashSet for unique categories
            _uniqueCategories.Add(evt.Category);
        }

        private void PopulateCategoryFilter()
        {
            cmbFilterCategory.Items.Clear();
            cmbFilterCategory.Items.Add("All Categories");
            foreach (var category in _uniqueCategories.OrderBy(c => c))
            {
                cmbFilterCategory.Items.Add(category);
            }
            cmbFilterCategory.SelectedIndex = 0;
        }

        private void RefreshEventDisplay()
        {
            string searchTerm = txtSearchEvents.Text.Trim().ToLower();
            string selectedCategory = cmbFilterCategory.SelectedItem?.ToString();

            var filteredEvents = _allEvents;

            // Filter by search term
            if (!string.IsNullOrEmpty(searchTerm))
            {
                filteredEvents = filteredEvents.Where(e =>
                    e.Title.ToLower().Contains(searchTerm) ||
                    e.Description.ToLower().Contains(searchTerm) ||
                    e.Category.ToLower().Contains(searchTerm)
                ).ToList();
            }

            // Filter by category
            if (!string.IsNullOrEmpty(selectedCategory) && selectedCategory != "All Categories")
            {
                filteredEvents = filteredEvents.Where(e => e.Category == selectedCategory).ToList();
            }

            // Sort by date (upcoming first)
            filteredEvents = filteredEvents.OrderBy(e => e.EventDate).ToList();

            // Display in DataGridView
            dgvEvents.DataSource = null;
            dgvEvents.DataSource = filteredEvents.Select(e => new
            {
                e.Title,
                e.Category,
                Date = e.EventDate.ToString("yyyy-MM-dd"),
                Time = e.EventDate.ToString("HH:mm"),
                e.Location,
                e.Description
            }).ToList();

            lblEventCount.Text = $"📊 {filteredEvents.Count} events found";

            // Update recommendations after search
            if (!string.IsNullOrEmpty(searchTerm) && filteredEvents.Any())
            {
                UpdateRecommendations(filteredEvents.First());
            }
        }

        private void UpdateRecommendations(LocalEvent viewedEvent)
        {
            // Queue for recent searches (FIFO)
            _recentSearches.Enqueue(viewedEvent);
            if (_recentSearches.Count > 5)
                _recentSearches.Dequeue();

            // Stack for search history (LIFO)
            _searchHistory.Push(viewedEvent);

            // PriorityQueue for recommendations based on category and priority
            _recommendations.Clear();
            foreach (var evt in _allEvents)
            {
                if (evt.Title != viewedEvent.Title)
                {
                    int priorityScore = evt.Priority;
                    if (evt.Category == viewedEvent.Category)
                        priorityScore += 5; // Boost same category
                    _recommendations.Enqueue(evt, -priorityScore); // Negative for higher priority
                }
            }

            // Display recommendations
            lstRecommendations.Items.Clear();
            int count = 0;
            while (_recommendations.Count > 0 && count < 5)
            {
                var rec = _recommendations.Dequeue();
                lstRecommendations.Items.Add($"{rec.Title} - {rec.EventDate:yyyy-MM-dd}");
                count++;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RefreshEventDisplay();
        }

        private void txtSearchEvents_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                RefreshEventDisplay();
                e.Handled = true;
            }
        }

        private void cmbFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshEventDisplay();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearchEvents.Clear();
            cmbFilterCategory.SelectedIndex = 0;
            RefreshEventDisplay();
        }

  

        #region Designer Code
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearchEvents = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.cmbFilterCategory = new System.Windows.Forms.ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.dgvEvents = new System.Windows.Forms.DataGridView();
            this.lblEventCount = new System.Windows.Forms.Label();
            this.grpRecommendations = new System.Windows.Forms.GroupBox();
            this.lstRecommendations = new System.Windows.Forms.ListBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.grpSearch.SuspendLayout();
            this.grpFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).BeginInit();
            this.grpRecommendations.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(291, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📅 Local Events & News";
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Controls.Add(this.txtSearchEvents);
            this.grpSearch.Controls.Add(this.lblSearch);
            this.grpSearch.Location = new System.Drawing.Point(12, 55);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(480, 65);
            this.grpSearch.TabIndex = 1;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Search Events";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(390, 22);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 28);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "🔍 Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearchEvents
            // 
            this.txtSearchEvents.Location = new System.Drawing.Point(80, 24);
            this.txtSearchEvents.Name = "txtSearchEvents";
            this.txtSearchEvents.Size = new System.Drawing.Size(300, 22);
            this.txtSearchEvents.TabIndex = 1;
            this.txtSearchEvents.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchEvents_KeyPress);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(10, 27);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(56, 16);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Keyword:";
            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.cmbFilterCategory);
            this.grpFilter.Controls.Add(this.lblFilter);
            this.grpFilter.Location = new System.Drawing.Point(500, 55);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(220, 65);
            this.grpFilter.TabIndex = 2;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Filter by Category";
            // 
            // cmbFilterCategory
            // 
            this.cmbFilterCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterCategory.Location = new System.Drawing.Point(80, 24);
            this.cmbFilterCategory.Name = "cmbFilterCategory";
            this.cmbFilterCategory.Size = new System.Drawing.Size(130, 24);
            this.cmbFilterCategory.TabIndex = 1;
            this.cmbFilterCategory.SelectedIndexChanged += new System.EventHandler(this.cmbFilterCategory_SelectedIndexChanged);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(10, 27);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(55, 16);
            this.lblFilter.TabIndex = 0;
            this.lblFilter.Text = "Category:";
            // 
            // dgvEvents
            // 
            this.dgvEvents.AllowUserToAddRows = false;
            this.dgvEvents.AllowUserToDeleteRows = false;
            this.dgvEvents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEvents.Location = new System.Drawing.Point(12, 130);
            this.dgvEvents.Name = "dgvEvents";
            this.dgvEvents.ReadOnly = true;
            this.dgvEvents.RowHeadersWidth = 51;
            this.dgvEvents.Size = new System.Drawing.Size(708, 230);
            this.dgvEvents.TabIndex = 3;
            // 
            // lblEventCount
            // 
            this.lblEventCount.AutoSize = true;
            this.lblEventCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEventCount.Location = new System.Drawing.Point(12, 370);
            this.lblEventCount.Name = "lblEventCount";
            this.lblEventCount.Size = new System.Drawing.Size(110, 23);
            this.lblEventCount.TabIndex = 4;
            this.lblEventCount.Text = "📊 0 events found";
            // 
            // grpRecommendations
            // 
            this.grpRecommendations.Controls.Add(this.lstRecommendations);
            this.grpRecommendations.Location = new System.Drawing.Point(12, 400);
            this.grpRecommendations.Name = "grpRecommendations";
            this.grpRecommendations.Size = new System.Drawing.Size(530, 120);
            this.grpRecommendations.TabIndex = 5;
            this.grpRecommendations.TabStop = false;
            this.grpRecommendations.Text = "🌟 Recommendations (PriorityQueue)";
            // 
            // lstRecommendations
            // 
            this.lstRecommendations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRecommendations.FormattingEnabled = true;
            this.lstRecommendations.ItemHeight = 16;
            this.lstRecommendations.Location = new System.Drawing.Point(3, 18);
            this.lstRecommendations.Name = "lstRecommendations";
            this.lstRecommendations.Size = new System.Drawing.Size(524, 99);
            this.lstRecommendations.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(550, 405);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 40);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(640, 405);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(80, 40);
            this.btnBack.TabIndex = 7;
            this.btnBack.Text = "🔙 Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // LocalEventsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(734, 531);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.grpRecommendations);
            this.Controls.Add(this.lblEventCount);
            this.Controls.Add(this.dgvEvents);
            this.Controls.Add(this.grpFilter);
            this.Controls.Add(this.grpSearch);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LocalEventsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Local Events";
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).EndInit();
            this.grpRecommendations.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearchEvents;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.ComboBox cmbFilterCategory;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.DataGridView dgvEvents;
        private System.Windows.Forms.Label lblEventCount;
        private System.Windows.Forms.GroupBox grpRecommendations;
        private System.Windows.Forms.ListBox lstRecommendations;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnBack;
        #endregion
    }
}