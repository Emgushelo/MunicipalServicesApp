using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp
{
    public partial class ReportIssueForm : Form
    {

        // Master storage for all requests - accessible by other forms
        public static Dictionary<string, ServiceRequest> MasterStore = new Dictionary<string, ServiceRequest>();

        private ServiceRequest _newIssue;
        private string _attachedFilePath = string.Empty;
        private readonly string[] _categories = {
            "Select a Category",
            "Water Leak",
            "Pothole",
            "Street Light",
            "Garbage Collection",
            "Sewerage",
            "Road Damage",
            "Traffic Signal",
            "Parks Maintenance",
            "Building Violation",
            "Noise Complaint",
            "Other"
        };
        private readonly string[] _engagementMessages = {
            "📌 Don't forget to add your location!",
            "📍 Almost there, add a description!",
            "📎 You can attach a photo for evidence!",
            "🌟 You're doing great! Keep going!",
            "🎯 One more step to complete your report!",
            "💪 Excellent! You're almost done!",
            "🏆 Fantastic work! Submit your report!"
        };
        private readonly Random _random = new Random();


        public ReportIssueForm()
        {
            InitializeComponent();
            PopulateCategories();
            ResetForm();
        }

        private void PopulateCategories()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.AddRange(_categories);
            cmbCategory.SelectedIndex = 0;
        }

        private void ResetForm()
        {
            txtLocation.Clear();
            cmbCategory.SelectedIndex = 0;
            rtbDescription.Clear();
            _attachedFilePath = string.Empty;
            lblFileName.Text = "No file selected";
            progressBar1.Value = 0;
            lblEngagementMsg.Text = "📝 Fill in the form to report an issue!";
            lblEngagementMsg.ForeColor = System.Drawing.Color.Gray;
            txtLocation.Focus();
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            int progress = 0;
            int totalFields = 4; // Location, Category, Description, Attachment

            if (!string.IsNullOrWhiteSpace(txtLocation.Text)) progress++;
            if (cmbCategory.SelectedIndex > 0) progress++;
            if (!string.IsNullOrWhiteSpace(rtbDescription.Text)) progress++;
            if (!string.IsNullOrEmpty(_attachedFilePath)) progress++;

            progressBar1.Value = (progress * 100) / totalFields;
            UpdateEngagementMessage(progress);
        }

        private void UpdateEngagementMessage(int progress)
        {
            if (progress == 0)
            {
                lblEngagementMsg.Text = "📝 Fill in the form to report an issue!";
                lblEngagementMsg.ForeColor = System.Drawing.Color.Gray;
            }
            else if (progress == 1)
            {
                lblEngagementMsg.Text = "📍 Good start! Add a category and description.";
                lblEngagementMsg.ForeColor = System.Drawing.Color.DarkBlue;
            }
            else if (progress == 2)
            {
                lblEngagementMsg.Text = "📌 Almost there! Add a description and optional file.";
                lblEngagementMsg.ForeColor = System.Drawing.Color.DarkGreen;
            }
            else if (progress == 3)
            {
                lblEngagementMsg.Text = "🌟 You're doing great! Just one more step!";
                lblEngagementMsg.ForeColor = System.Drawing.Color.Orange;
            }
            else if (progress == 4)
            {
                lblEngagementMsg.Text = "🎉 Perfect! You're ready to submit!";
                lblEngagementMsg.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            UpdateProgress();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProgress();
        }

        private void rtbDescription_TextChanged(object sender, EventArgs e)
        {
            UpdateProgress();
        }


        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select a file to attach";
                openFileDialog.Filter = "All Files (*.*)|*.*|Image Files (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp|Document Files (*.pdf;*.docx;*.txt)|*.pdf;*.docx;*.txt";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _attachedFilePath = openFileDialog.FileName;
                    lblFileName.Text = Path.GetFileName(_attachedFilePath);
                    lblFileName.ForeColor = System.Drawing.Color.Green;

                    // Show file size
                    FileInfo fileInfo = new FileInfo(_attachedFilePath);
                    string size = fileInfo.Length > 1024 * 1024
                        ? $"{fileInfo.Length / (1024 * 1024):F1} MB"
                        : $"{fileInfo.Length / 1024:F0} KB";
                    lblFileName.Text += $" ({size})";

                    UpdateProgress();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Clear all form fields?", "Confirm Clear",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ResetForm();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Validate form
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Please enter a location.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLocation.Focus();
                return;
            }

            if (cmbCategory.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a category.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(rtbDescription.Text))
            {
                MessageBox.Show("Please enter a description.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rtbDescription.Focus();
                return;
            }

            // Create new service request
            _newIssue = new ServiceRequest
            {
                Location = txtLocation.Text.Trim(),
                Category = cmbCategory.SelectedItem.ToString(),
                Description = rtbDescription.Text.Trim(),
                AttachedFilePath = _attachedFilePath,
                Status = "Submitted",
                Priority = (int)PriorityLevel.Medium
            };

            // Store in master dictionary
            MasterStore[_newIssue.RequestId] = _newIssue;

            // Also add to ServiceStatusForm's data structures
            ServiceRequestStatusForm.AddRequest(_newIssue);

            // Display success message
            string message = $"✅ Issue reported successfully!\n\n" +
                            $"📋 Reference Number: {_newIssue.RequestId}\n" +
                            $"📂 Category: {_newIssue.Category}\n" +
                            $"📍 Location: {_newIssue.Location}\n" +
                            $"📅 Date: {_newIssue.SubmittedDate:yyyy-MM-dd HH:mm}\n\n" +
                            "Thank you for helping improve our municipality! 🙏";

            MessageBox.Show(message, "✅ Report Submitted",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Update engagement message
            lblEngagementMsg.Text = "🎉 Report submitted successfully! Thank you for your contribution! 🎉";
            lblEngagementMsg.ForeColor = System.Drawing.Color.Green;

            // Reset form for next report
            ResetForm();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to go back? Any unsaved data will be lost.",
                "Confirm Navigation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}