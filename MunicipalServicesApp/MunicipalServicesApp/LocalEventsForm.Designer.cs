using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class LocalEventsForm : Form
    {
        private ListView lvEvents;
        private TextBox txtSearch;
        private ComboBox cmbCategory;
        private Button btnSearch;
        private Button btnReset;

        private List<LocalEvent> events = new List<LocalEvent>();

        public LocalEventsForm()
        {
            InitializeComponent();
            LoadSampleEvents();
            LoadEventsToList();
        }

        private void InitializeComponent()
        {
            this.Text = "Local Events";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // HEADER
            Label lblTitle = new Label
            {
                Text = "📅 LOCAL COMMUNITY EVENTS",
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 14, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 51, 102),
                ForeColor = Color.White
            };

            // SEARCH PANEL
            Panel topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60
            };

            txtSearch = new TextBox
            {
                Location = new Point(20, 15),
                Width = 200
            };

            cmbCategory = new ComboBox
            {
                Location = new Point(230, 15),
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

  
            cmbCategory.Items.AddRange(new string[]
            {
    "All",
    "Music",
    "Sports",
    "Community",
    "Education"
            });

            cmbCategory.SelectedIndex = 0;

            btnSearch = new Button
            {
                Text = "Search",
                Location = new Point(390, 13),
                Width = 100
            };

            btnReset = new Button
            {
                Text = "Reset",
                Location = new Point(500, 13),
                Width = 100
            };

            btnSearch.Click += BtnSearch_Click;
            btnReset.Click += (s, e) => LoadEventsToList();

            topPanel.Controls.Add(txtSearch);
            topPanel.Controls.Add(cmbCategory);
            topPanel.Controls.Add(btnSearch);
            topPanel.Controls.Add(btnReset);

            // LIST VIEW
            lvEvents = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true
            };

            lvEvents.Columns.Add("Event Name", 200);
            lvEvents.Columns.Add("Category", 120);
            lvEvents.Columns.Add("Date", 120);
            lvEvents.Columns.Add("Location", 200);

            this.Controls.Add(lvEvents);
            this.Controls.Add(topPanel);
            this.Controls.Add(lblTitle);
        }

        private void LoadSampleEvents()
        {
            events.Add(new LocalEvent("Jazz Night", "Music", DateTime.Now.AddDays(2), "Town Hall"));
            events.Add(new LocalEvent("Football Match", "Sports", DateTime.Now.AddDays(5), "City Stadium"));
            events.Add(new LocalEvent("Coding Workshop", "Education", DateTime.Now.AddDays(3), "Library"));
            events.Add(new LocalEvent("Community Cleanup", "Community", DateTime.Now.AddDays(1), "Main Park"));
        }

        private void LoadEventsToList()
        {
            lvEvents.Items.Clear();

            foreach (var ev in events)
            {
                ListViewItem item = new ListViewItem(ev.Name);
                item.SubItems.Add(ev.Category);
                item.SubItems.Add(ev.Date.ToShortDateString());
                item.SubItems.Add(ev.Location);

                lvEvents.Items.Add(item);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.ToLower();
            string category = cmbCategory.SelectedItem?.ToString() ?? "All";

            var filtered = events.Where(ev =>
                (string.IsNullOrWhiteSpace(search) || ev.Name.ToLower().Contains(search)) &&
                (category == "All" || ev.Category == category)
            ).ToList();

            lvEvents.Items.Clear();

            foreach (var ev in filtered)
            {
                ListViewItem item = new ListViewItem(ev.Name);
                item.SubItems.Add(ev.Category);
                item.SubItems.Add(ev.Date.ToShortDateString());
                item.SubItems.Add(ev.Location);

                lvEvents.Items.Add(item);
            }
        }
    }

    // ✅ FIXED: Properly placed OUTSIDE the form class
    public class LocalEvent
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }

        public LocalEvent(string name, string category, DateTime date, string location)
        {
            Name = name;
            Category = category;
            Date = date;
            Location = location;
        }
    }
}