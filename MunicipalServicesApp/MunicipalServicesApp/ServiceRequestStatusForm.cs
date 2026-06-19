using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MunicipalServicesApp.Models;
using MunicipalServicesApp.DataStructures;


namespace MunicipalServicesApp
{
    public partial class ServiceRequestStatusForm : Form
    {
        // Advanced Data Structures for Task 3
        private static AVLTree<string, ServiceRequest> _requestTree = new AVLTree<string, ServiceRequest>();
        private static MinHeap<ServiceRequest> _priorityQueue = new MinHeap<ServiceRequest>();
        // FIXED: Changed from Graph<string> to Graph<ServiceRequest>
        private static Graph<ServiceRequest> _requestGraph = new Graph<ServiceRequest>();
        private static Dictionary<string, ServiceRequest> _masterStore = new Dictionary<string, ServiceRequest>();

        private ServiceRequest _selectedRequest;

       

        // Method to add requests from ReportIssueForm
        public static void AddRequest(ServiceRequest request)
        {
            // Master store
            _masterStore[request.RequestId] = request;

            // AVL Tree for O(log n) search
            _requestTree.Insert(request.RequestId, request);

            // MinHeap for priority processing
            _priorityQueue.Insert(request);

            // Graph for relationships (based on location and category)
            foreach (var existing in _masterStore.Values)
            {
                if (existing.RequestId != request.RequestId)
                {
                    // Connect requests in same location
                    if (existing.Location == request.Location)
                    {
                        _requestGraph.AddEdge(request, existing);
                    }
                    // Connect requests in same category
                    else if (existing.Category == request.Category)
                    {
                        _requestGraph.AddEdge(request, existing);
                    }
                }
            }
        }

        private void LoadExistingRequests()
        {
            // Load requests from ReportIssueForm's master store
            foreach (var kvp in ReportIssueForm.MasterStore)
            {
                if (!_masterStore.ContainsKey(kvp.Key))
                {
                    AddRequest(kvp.Value);
                }
            }
        }

        private void RefreshDisplay()
        {
            // Display all requests in DataGridView
            var allRequests = _requestTree.InOrderTraversal();
            dgvRequests.DataSource = null;
            dgvRequests.DataSource = allRequests.Select(r => new
            {
                RequestId = r.RequestId,
                r.Location,
                r.Category,
                r.Status,
                Priority = r.Priority.ToString(),
                Submitted = r.SubmissionDate.ToString("yyyy-MM-dd HH:mm"),
                HasAttachment = !string.IsNullOrEmpty(r.AttachedFilePath)
            }).ToList();

            // Update priority queue display
            UpdatePriorityQueueDisplay();

            // Clear selection
            ClearDetails();
            UpdateStatistics();
        }

        private void UpdatePriorityQueueDisplay()
        {
            lstPriorityQueue.Items.Clear();
            var queueItems = _priorityQueue.GetAll();
            queueItems.Sort(); // Sort by priority (Critical first)

            foreach (var item in queueItems)
            {
                string display = $"{item.Priority} - {item.RequestId} - {item.Category}";
                int index = lstPriorityQueue.Items.Count;
                lstPriorityQueue.Items.Add(display);

                // FIXED: Cast int to PriorityLevel for switch comparison
                switch ((PriorityLevel)item.Priority)
                {
                    case PriorityLevel.Critical:
                        lstPriorityQueue.Items[index] = $"🔴 CRITICAL - {item.RequestId}";
                        break;
                    case PriorityLevel.High:
                        lstPriorityQueue.Items[index] = $"🟠 HIGH - {item.RequestId}";
                        break;
                    case PriorityLevel.Medium:
                        lstPriorityQueue.Items[index] = $"🟡 MEDIUM - {item.RequestId}";
                        break;
                    case PriorityLevel.Low:
                        lstPriorityQueue.Items[index] = $"🟢 LOW - {item.RequestId}";
                        break;
                    default:
                        lstPriorityQueue.Items[index] = $"⚪ {item.Priority} - {item.RequestId}";
                        break;
                }
            }
        }

        private void UpdateStatistics()
        {
            int total = _masterStore.Count;
            int critical = _masterStore.Values.Count(r => (PriorityLevel)r.Priority == PriorityLevel.Critical);
            int inProgress = _masterStore.Values.Count(r => r.Status == "In Progress");
            int resolved = _masterStore.Values.Count(r => r.Status == "Resolved" || r.Status == "Closed");

            lblStats.Text = $"📊 Total: {total} | 🔴 Critical: {critical} | ⏳ In Progress: {inProgress} | ✅ Resolved: {resolved}";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string requestId = txtSearchId.Text.Trim();
            if (string.IsNullOrEmpty(requestId))
            {
                MessageBox.Show("Please enter a Request ID.", "Search",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // AVL Tree search - O(log n)
            var request = _requestTree.Search(requestId);
            if (request != null)
            {
                DisplayRequestDetails(request);
                ShowRelatedRequests(request.RequestId);
                HighlightRequestInGrid(request.RequestId);
            }
            else
            {
                MessageBox.Show($"Request {requestId} not found.", "Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearDetails();
            }
        }

        private void HighlightRequestInGrid(string requestId)
        {
            foreach (DataGridViewRow row in dgvRequests.Rows)
            {
                if (row.Cells[0].Value?.ToString() == requestId)
                {
                    row.Selected = true;
                    dgvRequests.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }

        private void DisplayRequestDetails(ServiceRequest request)
        {
            _selectedRequest = request;
            txtDetailsRequestId.Text = request.RequestId;
            txtDetailsLocation.Text = request.Location;
            txtDetailsCategory.Text = request.Category;
            txtDetailsDescription.Text = request.Description;
            txtDetailsStatus.Text = request.Status;
            txtDetailsPriority.Text = request.Priority.ToString();
            txtDetailsDate.Text = request.SubmissionDate.ToString("yyyy-MM-dd HH:mm");

            // FIXED: Cast int to PriorityLevel for switch comparison
            switch ((PriorityLevel)request.Priority)
            {
                case PriorityLevel.Critical:
                    txtDetailsPriority.BackColor = Color.FromArgb(255, 200, 200);
                    break;
                case PriorityLevel.High:
                    txtDetailsPriority.BackColor = Color.FromArgb(255, 230, 200);
                    break;
                case PriorityLevel.Medium:
                    txtDetailsPriority.BackColor = Color.FromArgb(255, 255, 200);
                    break;
                case PriorityLevel.Low:
                    txtDetailsPriority.BackColor = Color.FromArgb(200, 255, 200);
                    break;
                default:
                    txtDetailsPriority.BackColor = SystemColors.Window;
                    break;
            }

            // Show attachment info
            if (!string.IsNullOrEmpty(request.AttachedFilePath))
            {
                lblAttachmentInfo.Text = $"📎 {System.IO.Path.GetFileName(request.AttachedFilePath)}";
                lblAttachmentInfo.ForeColor = Color.Green;
            }
            else
            {
                lblAttachmentInfo.Text = "No attachment";
                lblAttachmentInfo.ForeColor = Color.Gray;
            }

            // Set status in dropdown
            cmbNewStatus.SelectedItem = request.Status;
        }

        private void ShowRelatedRequests(string requestId)
        {
            lstRelatedRequests.Items.Clear();

            if (!_masterStore.ContainsKey(requestId))
            {
                lstRelatedRequests.Items.Add("Request not found.");
                return;
            }

            ServiceRequest request = _masterStore[requestId];

            // BFS returns List<ServiceRequest>
            var relatedRequests = _requestGraph.BFS(request);

            if (relatedRequests != null && relatedRequests.Count() > 1)
            {
                lstRelatedRequests.Items.Add(
                    $"📌 {relatedRequests.Count() - 1} related request(s) found:");

                foreach (ServiceRequest related in relatedRequests)
                {
                    if (related.RequestId != requestId)
                    {
                        lstRelatedRequests.Items.Add(
                            $"   • {related.RequestId} - {related.Category} ({related.Status})");
                    }
                }
            }
            else
            {
                lstRelatedRequests.Items.Add("No related requests found.");
            }

            int vertexCount = _requestGraph.GetVertexCount();
            int edgeCount = _requestGraph.GetEdgeCount();

            lblGraphStats.Text = $"Graph: {vertexCount} vertices, {edgeCount} edges";
        }

        private void ClearDetails()
        {
            _selectedRequest = null;
            txtDetailsRequestId.Text = "";
            txtDetailsLocation.Text = "";
            txtDetailsCategory.Text = "";
            txtDetailsDescription.Text = "";
            txtDetailsStatus.Text = "";
            txtDetailsPriority.Text = "";
            txtDetailsDate.Text = "";
            txtDetailsPriority.BackColor = SystemColors.Window;
            lblAttachmentInfo.Text = "No attachment";
            lblAttachmentInfo.ForeColor = Color.Gray;
            lstRelatedRequests.Items.Clear();
            lstRelatedRequests.Items.Add("No request selected.");
            cmbNewStatus.SelectedIndex = -1;
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (_selectedRequest == null)
            {
                MessageBox.Show("Please select or search for a request first.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newStatus = cmbNewStatus.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(newStatus))
            {
                MessageBox.Show("Please select a new status.",
                    "Status Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Update status
            _selectedRequest.Status = newStatus;
            _masterStore[_selectedRequest.RequestId] = _selectedRequest;
            ReportIssueForm.MasterStore[_selectedRequest.RequestId] = _selectedRequest;

            // Rebuild data structures
            RebuildDataStructures();

            RefreshDisplay();
            DisplayRequestDetails(_selectedRequest);

            MessageBox.Show($"✅ Status updated to: {newStatus}", "Status Updated",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RebuildDataStructures()
        {
            // Rebuild AVL Tree and Heap from master store
            _requestTree = new AVLTree<string, ServiceRequest>();
            _priorityQueue = new MinHeap<ServiceRequest>();
            _requestGraph = new Graph<ServiceRequest>();

            foreach (var kvp in _masterStore)
            {
                _requestTree.Insert(kvp.Key, kvp.Value);
                _priorityQueue.Insert(kvp.Value);
            }

            // Rebuild graph edges
            foreach (var kvp1 in _masterStore)
            {
                foreach (var kvp2 in _masterStore)
                {
                    if (kvp1.Key != kvp2.Key)
                    {
                        if (kvp1.Value.Location == kvp2.Value.Location ||
                            kvp1.Value.Category == kvp2.Value.Category)
                        {
                            _requestGraph.AddEdge(kvp1.Value, kvp2.Value);
                        }
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDisplay();
        }

        private void dgvRequests_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string requestId = dgvRequests.Rows[e.RowIndex].Cells[0].Value.ToString();
                var request = _requestTree.Search(requestId);
                if (request != null)
                {
                    DisplayRequestDetails(request);
                    ShowRelatedRequests(request.RequestId);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Designer Code
        // FIXED: Remove duplicate InitializeComponent - this is generated by the designer
        // The designer already generates this method, so we should not have it here
        #endregion

        private System.Windows.Forms.DataGridView dgvRequests;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearchId;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.Label lblRequestId;
        private System.Windows.Forms.TextBox txtDetailsRequestId;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtDetailsLocation;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox txtDetailsCategory;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDetailsDescription;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtDetailsStatus;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox txtDetailsDate;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.TextBox txtDetailsPriority;
        private System.Windows.Forms.Label lblAttachmentInfo;
        private System.Windows.Forms.GroupBox grpStatusUpdate;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.ComboBox cmbNewStatus;
        private System.Windows.Forms.Label lblNewStatus;
        private System.Windows.Forms.GroupBox grpPriorityQueue;
        private System.Windows.Forms.ListBox lstPriorityQueue;
        private System.Windows.Forms.GroupBox grpRelated;
        private System.Windows.Forms.ListBox lstRelatedRequests;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Label lblGraphStats;
    }
}