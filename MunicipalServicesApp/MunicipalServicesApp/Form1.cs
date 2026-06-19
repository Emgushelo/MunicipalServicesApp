using System;
using System.Drawing;
using System.Windows.Forms;
using MunicipalServicesApp;

namespace MunicipalServicesApp
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    namespace MunicipalServicesApp
    {
        public class MainMenuForm : Form
        {
            private Button btnReportIssues;
            private Button btnLocalEvents;
            private Button btnServiceStatus;
            private Label lblTitle;
            private Label lblWelcome;

            public MainMenuForm()
            {
                InitializeComponent();
                SetupForm();
            }

            private void InitializeComponent()
            {
                this.btnReportIssues = new System.Windows.Forms.Button();
                this.btnLocalEvents = new System.Windows.Forms.Button();
                this.btnServiceStatus = new System.Windows.Forms.Button();
                this.lblTitle = new System.Windows.Forms.Label();
                this.lblWelcome = new System.Windows.Forms.Label();
                this.SuspendLayout();

                // btnReportIssues
                this.btnReportIssues.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
                this.btnReportIssues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.btnReportIssues.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
                this.btnReportIssues.ForeColor = System.Drawing.Color.White;
                this.btnReportIssues.Location = new System.Drawing.Point(100, 150);
                this.btnReportIssues.Name = "btnReportIssues";
                this.btnReportIssues.Size = new System.Drawing.Size(250, 50);
                this.btnReportIssues.TabIndex = 0;
                this.btnReportIssues.Text = "📝 Report Issues";
                this.btnReportIssues.UseVisualStyleBackColor = false;
                this.btnReportIssues.Click += new System.EventHandler(this.BtnReportIssues_Click);

                // btnLocalEvents
                this.btnLocalEvents.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
                this.btnLocalEvents.Enabled = false;
                this.btnLocalEvents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.btnLocalEvents.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
                this.btnLocalEvents.ForeColor = System.Drawing.Color.White;
                this.btnLocalEvents.Location = new System.Drawing.Point(100, 220);
                this.btnLocalEvents.Name = "btnLocalEvents";
                this.btnLocalEvents.Size = new System.Drawing.Size(250, 50);
                this.btnLocalEvents.TabIndex = 1;
                this.btnLocalEvents.Text = "🎪 Local Events & Announcements";
                this.btnLocalEvents.UseVisualStyleBackColor = false;

                // btnServiceStatus
                this.btnServiceStatus.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
                this.btnServiceStatus.Enabled = false;
                this.btnServiceStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.btnServiceStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
                this.btnServiceStatus.ForeColor = System.Drawing.Color.White;
                this.btnServiceStatus.Location = new System.Drawing.Point(100, 290);
                this.btnServiceStatus.Name = "btnServiceStatus";
                this.btnServiceStatus.Size = new System.Drawing.Size(250, 50);
                this.btnServiceStatus.TabIndex = 2;
                this.btnServiceStatus.Text = "📊 Service Request Status";
                this.btnServiceStatus.UseVisualStyleBackColor = false;

                // lblTitle
                this.lblTitle.AutoSize = true;
                this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
                this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(41, 128, 185);
                this.lblTitle.Location = new System.Drawing.Point(60, 30);
                this.lblTitle.Name = "lblTitle";
                this.lblTitle.Size = new System.Drawing.Size(343, 37);
                this.lblTitle.TabIndex = 3;
                this.lblTitle.Text = "Municipal Services Application";
                this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                // lblWelcome
                this.lblWelcome.AutoSize = true;
                this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 10F);
                this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
                this.lblWelcome.Location = new System.Drawing.Point(95, 80);
                this.lblWelcome.Name = "lblWelcome";
                this.lblWelcome.Size = new System.Drawing.Size(270, 19);
                this.lblWelcome.TabIndex = 4;
                this.lblWelcome.Text = "Welcome! Please select a service below:";
                this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                // MainMenuForm
                this.BackColor = System.Drawing.Color.White;
                this.ClientSize = new System.Drawing.Size(484, 411);
                this.Controls.Add(this.lblWelcome);
                this.Controls.Add(this.lblTitle);
                this.Controls.Add(this.btnServiceStatus);
                this.Controls.Add(this.btnLocalEvents);
                this.Controls.Add(this.btnReportIssues);
                this.Name = "MainMenuForm";
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.Text = "Municipal Services - Main Menu";
                this.ResumeLayout(false);
                this.PerformLayout();
            }

            private void SetupForm()
            {
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.MaximizeBox = false;
            }

            private void BtnReportIssues_Click(object sender, EventArgs e)
            {
                ReportIssueForm reportForm = new ReportIssueForm();
                reportForm.ShowDialog();
            }
        }
    }
}
public class MainMenuForm : Form
    {
        private Button btnReportIssues;
        private Button btnLocalEvents;
        private Button btnServiceStatus;
        private Label lblTitle;
        private Label lblWelcome;

        public MainMenuForm()
        {
            InitializeComponent();
            SetupForm();
        }

        private void InitializeComponent()
        {
            this.btnReportIssues = new System.Windows.Forms.Button();
            this.btnLocalEvents = new System.Windows.Forms.Button();
            this.btnServiceStatus = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // btnReportIssues
            this.btnReportIssues.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnReportIssues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportIssues.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnReportIssues.ForeColor = System.Drawing.Color.White;
            this.btnReportIssues.Location = new System.Drawing.Point(100, 150);
            this.btnReportIssues.Name = "btnReportIssues";
            this.btnReportIssues.Size = new System.Drawing.Size(250, 50);
            this.btnReportIssues.TabIndex = 0;
            this.btnReportIssues.Text = "📝 Report Issues";
            this.btnReportIssues.UseVisualStyleBackColor = false;
            this.btnReportIssues.Click += new System.EventHandler(this.BtnReportIssues_Click);

            // btnLocalEvents
            this.btnLocalEvents.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnLocalEvents.Enabled = false;
            this.btnLocalEvents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocalEvents.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLocalEvents.ForeColor = System.Drawing.Color.White;
            this.btnLocalEvents.Location = new System.Drawing.Point(100, 220);
            this.btnLocalEvents.Name = "btnLocalEvents";
            this.btnLocalEvents.Size = new System.Drawing.Size(250, 50);
            this.btnLocalEvents.TabIndex = 1;
            this.btnLocalEvents.Text = "🎪 Local Events & Announcements";
            this.btnLocalEvents.UseVisualStyleBackColor = false;

            // btnServiceStatus
            this.btnServiceStatus.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.btnServiceStatus.Enabled = false;
            this.btnServiceStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnServiceStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnServiceStatus.ForeColor = System.Drawing.Color.White;
            this.btnServiceStatus.Location = new System.Drawing.Point(100, 290);
            this.btnServiceStatus.Name = "btnServiceStatus";
            this.btnServiceStatus.Size = new System.Drawing.Size(250, 50);
            this.btnServiceStatus.TabIndex = 2;
            this.btnServiceStatus.Text = "📊 Service Request Status";
            this.btnServiceStatus.UseVisualStyleBackColor = false;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.lblTitle.Location = new System.Drawing.Point(60, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(343, 37);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Municipal Services Application";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblWelcome.Location = new System.Drawing.Point(95, 80);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(270, 19);
            this.lblWelcome.TabIndex = 4;
            this.lblWelcome.Text = "Welcome! Please select a service below:";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // MainMenuForm
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 411);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnServiceStatus);
            this.Controls.Add(this.btnLocalEvents);
            this.Controls.Add(this.btnReportIssues);
            this.Name = "MainMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Municipal Services - Main Menu";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void SetupForm()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

    private void BtnReportIssues_Click(object sender, EventArgs e)
    {
        ReportIssueForm reportForm = new ReportIssueForm();
        reportForm.ShowDialog();
    }
}

