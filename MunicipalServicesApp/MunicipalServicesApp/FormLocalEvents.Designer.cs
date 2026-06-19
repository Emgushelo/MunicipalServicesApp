using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    partial class FormLocalEvents
    {
        /// <summary>
        /// Required designer variable
        /// </summary>
        private IContainer components = null;

        // Controls
        private ListBox lstEvents;
        private ComboBox cmbCategoryFilter;
        private TextBox txtSearch;
        private DateTimePicker dtpFilterDate;
        private ListBox lstRecommendations;
        private Label lblRecommendationsTitle;

        private Button btnSearch;
        private Button btnViewHighPriority;
        private Button btnUpcomingEvents;
        private Button btnBack;

        /// <summary>
        /// Clean up resources
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lstEvents = new System.Windows.Forms.ListBox();
            this.cmbCategoryFilter = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dtpFilterDate = new System.Windows.Forms.DateTimePicker();
            this.lstRecommendations = new System.Windows.Forms.ListBox();
            this.lblRecommendationsTitle = new System.Windows.Forms.Label();

            this.btnSearch = new System.Windows.Forms.Button();
            this.btnViewHighPriority = new System.Windows.Forms.Button();
            this.btnUpcomingEvents = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // 
            // lstEvents
            // 
            this.lstEvents.FormattingEnabled = true;
            this.lstEvents.ItemHeight = 16;
            this.lstEvents.Location = new System.Drawing.Point(20, 80);
            this.lstEvents.Name = "lstEvents";
            this.lstEvents.Size = new System.Drawing.Size(600, 260);

            // 
            // cmbCategoryFilter
            // 
            this.cmbCategoryFilter.FormattingEnabled = true;
            this.cmbCategoryFilter.Location = new System.Drawing.Point(20, 20);
            this.cmbCategoryFilter.Name = "cmbCategoryFilter";
            this.cmbCategoryFilter.Size = new System.Drawing.Size(150, 24);

            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(180, 20);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(180, 22);

            // 
            // dtpFilterDate
            // 
            this.dtpFilterDate.Location = new System.Drawing.Point(370, 20);
            this.dtpFilterDate.Name = "dtpFilterDate";
            this.dtpFilterDate.Size = new System.Drawing.Size(200, 22);
            this.dtpFilterDate.ShowCheckBox = true;

            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(580, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 30);
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // 
            // btnViewHighPriority
            // 
            this.btnViewHighPriority.Location = new System.Drawing.Point(20, 360);
            this.btnViewHighPriority.Name = "btnViewHighPriority";
            this.btnViewHighPriority.Size = new System.Drawing.Size(150, 35);
            this.btnViewHighPriority.Text = "High Priority";
            this.btnViewHighPriority.UseVisualStyleBackColor = true;
            this.btnViewHighPriority.Click += new System.EventHandler(this.btnViewHighPriority_Click);

            // 
            // btnUpcomingEvents
            // 
            this.btnUpcomingEvents.Location = new System.Drawing.Point(180, 360);
            this.btnUpcomingEvents.Name = "btnUpcomingEvents";
            this.btnUpcomingEvents.Size = new System.Drawing.Size(170, 35);
            this.btnUpcomingEvents.Text = "Upcoming Events";
            this.btnUpcomingEvents.UseVisualStyleBackColor = true;
            this.btnUpcomingEvents.Click += new System.EventHandler(this.btnUpcomingEvents_Click);

            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(360, 360);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 35);
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // 
            // lblRecommendationsTitle
            // 
            this.lblRecommendationsTitle.AutoSize = true;
            this.lblRecommendationsTitle.Location = new System.Drawing.Point(650, 80);
            this.lblRecommendationsTitle.Name = "lblRecommendationsTitle";
            this.lblRecommendationsTitle.Size = new System.Drawing.Size(160, 16);
            this.lblRecommendationsTitle.Text = "Recommended Events";

            // 
            // lstRecommendations
            // 
            this.lstRecommendations.FormattingEnabled = true;
            this.lstRecommendations.ItemHeight = 16;
            this.lstRecommendations.Location = new System.Drawing.Point(650, 110);
            this.lstRecommendations.Name = "lstRecommendations";
            this.lstRecommendations.Size = new System.Drawing.Size(250, 228);

            // 
            // FormLocalEvents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 430);

            this.Controls.Add(this.lstEvents);
            this.Controls.Add(this.cmbCategoryFilter);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dtpFilterDate);

            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnViewHighPriority);
            this.Controls.Add(this.btnUpcomingEvents);
            this.Controls.Add(this.btnBack);

            this.Controls.Add(this.lblRecommendationsTitle);
            this.Controls.Add(this.lstRecommendations);

            this.Name = "FormLocalEvents";
            this.Text = "Local Events";

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}