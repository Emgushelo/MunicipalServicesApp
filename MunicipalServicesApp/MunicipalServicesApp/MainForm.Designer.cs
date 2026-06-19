using System;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>

        private void InitializeComponent()
        {
            this.btnReportIssues = new System.Windows.Forms.Button();
            this.btnLocalEvents = new System.Windows.Forms.Button();
            this.btnServiceRequestManager = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // btnReportIssues
            this.btnReportIssues.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnReportIssues.Location = new System.Drawing.Point(50, 100);
            this.btnReportIssues.Name = "btnReportIssues";
            this.btnReportIssues.Size = new System.Drawing.Size(250, 50);
            this.btnReportIssues.Text = "📝 Report Issues";
            this.btnReportIssues.Click += new System.EventHandler(this.btnReportIssues_Click);

            // btnLocalEvents
            this.btnLocalEvents.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnLocalEvents.Location = new System.Drawing.Point(50, 170);
            this.btnLocalEvents.Name = "btnLocalEvents";
            this.btnLocalEvents.Size = new System.Drawing.Size(250, 50);
            this.btnLocalEvents.TabIndex = 1;
            this.btnLocalEvents.Text = "📅 Local Events";
            this.btnLocalEvents.UseVisualStyleBackColor = true;
            this.btnLocalEvents.Click += new System.EventHandler(this.btnLocalEvents_Click);
            // 
            // btnServiceRequestManager
            // 
            this.btnServiceRequestManager.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnServiceRequestManager.Location = new System.Drawing.Point(50, 240);
            this.btnServiceRequestManager.Name = "btnServiceRequestManager";
            this.btnServiceRequestManager.Size = new System.Drawing.Size(250, 50);
            this.btnServiceRequestManager.TabIndex = 2;
            this.btnServiceRequestManager.Text = "📊 Service Request Manager";
            this.btnServiceRequestManager.UseVisualStyleBackColor = true;
            this.btnServiceRequestManager.Click += new System.EventHandler(this.btnServiceRequestManager_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnExit.Location = new System.Drawing.Point(50, 310);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(250, 50);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "🚪 Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(44, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(269, 32);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "🏛️ Municipal Services";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(350, 400);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnServiceRequestManager);
            this.Controls.Add(this.btnLocalEvents);
            this.Controls.Add(this.btnReportIssues);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Municipal Services";

            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Panel panelHeader;

        private System.Windows.Forms.Button btnReportIssues;
        private System.Windows.Forms.Button btnLocalEvents;
        private System.Windows.Forms.Button btnServiceRequestManager;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTitle;


    }
        #endregion
}