
using MunicipalServicesApp.DataStructures;
using MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class ServiceRequestStatusForm : Form
    {
        // Data Structures
        private AVLTree<int, ServiceRequest> avlTree;
        private Graph<ServiceRequest> graph;
        private MinHeap<ServiceRequest> heap;
        private BinarySearchTree<ServiceRequest> bst;
        private RedBlackTree<ServiceRequest> redBlackTree;
        private BasicTree categoryTree;

        private List<ServiceRequest> requests;

        public ServiceRequestStatusForm()
        {
            InitializeComponent();
            LoadSampleRequests();
            InitializeDataStructures();
            SetupUI();
            DisplayAllStructures();
        }

        private void LoadSampleRequests()
        {
            requests = new List<ServiceRequest>
            {
                new ServiceRequest { RequestID = 101, CitizenName = "Thabo Mbeki", Category = "Roads",
                    Location = "Soweto", Description = "Pothole on Main Street", Status = "Pending",
                    SubmissionDate = DateTime.Now.AddDays(-3), Priority = 3 },

                new ServiceRequest { RequestID = 102, CitizenName = "Sarah Johnson", Category = "Sanitation",
                    Location = "Cape Town", Description = "Missed refuse collection", Status = "In Progress",
                    SubmissionDate = DateTime.Now.AddDays(-5), Priority = 2 },

                new ServiceRequest { RequestID = 103, CitizenName = "Michael Ndlovu", Category = "Utilities",
                    Location = "Johannesburg", Description = "Water leakage", Status = "Completed",
                    SubmissionDate = DateTime.Now.AddDays(-10), Priority = 1 },

                new ServiceRequest { RequestID = 104, CitizenName = "Priya Naidoo", Category = "Street Lighting",
                    Location = "Durban", Description = "Burnt-out streetlight", Status = "Pending",
                    SubmissionDate = DateTime.Now.AddDays(-1), Priority = 3 },

                new ServiceRequest { RequestID = 105, CitizenName = "Khumalo Zulu", Category = "Parks",
                    Location = "Pretoria", Description = "Broken playground equipment", Status = "In Progress",
                    SubmissionDate = DateTime.Now.AddDays(-7), Priority = 2 },

                new ServiceRequest { RequestID = 106, CitizenName = "Jessica Williams", Category = "Roads",
                    Location = "Port Elizabeth", Description = "Damaged road signs", Status = "Pending",
                    SubmissionDate = DateTime.Now.AddDays(-2), Priority = 3 },

                new ServiceRequest { RequestID = 107, CitizenName = "David Mokoena", Category = "Sanitation",
                    Location = "Bloemfontein", Description = "Illegal dumping site", Status = "In Progress",
                    SubmissionDate = DateTime.Now.AddDays(-4), Priority = 2 },

                new ServiceRequest { RequestID = 108, CitizenName = "Amanda Smith", Category = "Utilities",
                    Location = "East London", Description = "Power outage", Status = "Completed",
                    SubmissionDate = DateTime.Now.AddDays(-8), Priority = 1 },

                new ServiceRequest { RequestID = 109, CitizenName = "Peter Molefe", Category = "Roads",
                    Location = "Kimberley", Description = "Pothole near school", Status = "Pending",
                    SubmissionDate = DateTime.Now.AddDays(-1), Priority = 3 },

                new ServiceRequest { RequestID = 110, CitizenName = "Grace Maluleke", Category = "Street Lighting",
                    Location = "Polokwane", Description = "Flickering streetlight", Status = "In Progress",
                    SubmissionDate = DateTime.Now.AddDays(-6), Priority = 2 }
            };
        }

        private void InitializeDataStructures()
        {
            // 1. AVL Tree - Balanced by RequestID
            avlTree = new AVLTree<int, ServiceRequest>();
            foreach (var req in requests)
                avlTree.Insert(req.RequestID, req);

            // 2. Graph - Relationships between requests
            graph = new Graph<ServiceRequest>();
            foreach (var req in requests)
                graph.AddNode(req);

            // Add edges between related requests (same category or location)
            for (int i = 0; i < requests.Count; i++)
            {
                for (int j = i + 1; j < requests.Count; j++)
                {
                    if (requests[i].Category == requests[j].Category ||
                        requests[i].Location == requests[j].Location)
                    {
                        graph.AddEdge(requests[i], requests[j],
                            Math.Abs(requests[i].RequestID - requests[j].RequestID));
                    }
                }
            }

            // 3. Min Heap - Ordered by Priority and Date
            heap = new MinHeap<ServiceRequest>();
            foreach (var req in requests)
                heap.Insert(req);

            // 4. BST - Binary Search Tree
            bst = new BinarySearchTree<ServiceRequest>();
            foreach (var req in requests)
                bst.Insert(req);

            // 5. Red-Black Tree
            redBlackTree = new RedBlackTree<ServiceRequest>();
            foreach (var req in requests)
                redBlackTree.Insert(req);

            // 6. Basic Tree - Category hierarchy
            categoryTree = new BasicTree("All Service Requests");
            var categories = requests.Select(r => r.Category).Distinct();
            foreach (var category in categories)
            {
                var categoryNode = new BasicTreeNode(category);
                foreach (var req in requests.Where(r => r.Category == category))
                {
                    categoryNode.AddRequest(req);
                }
                categoryTree.Root.AddChild(categoryNode);
            }
        }

        private void SetupUI()
        {
            this.Text = "Service Request Status - Advanced Data Structures";
            this.Size = new Size(1400, 900);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            // Tab Control
            var tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };

            // Tab 1: BST
            var tabBST = new TabPage("🌳 BST");
            tabBST.Controls.Add(CreateListView("lvBST", new[] { "ID", "Citizen", "Category", "Status" }));
            tabControl.TabPages.Add(tabBST);

            // Tab 2: AVL
            var tabAVL = new TabPage("🌳 AVL");
            tabAVL.Controls.Add(CreateListView("lvAVL", new[] { "ID", "Citizen", "Category", "Status", "Height" }));
            tabControl.TabPages.Add(tabAVL);

            // Tab 3: Red-Black
            var tabRB = new TabPage("🌳 Red-Black");
            tabRB.Controls.Add(CreateListView("lvRB", new[] { "ID", "Citizen", "Category", "Status", "Color" }));
            tabControl.TabPages.Add(tabRB);

            // Tab 4: Basic Tree
            var tabTree = new TabPage("📁 Tree");
            tabTree.Controls.Add(CreateListView("lvTree", new[] { "Category", "Requests" }));
            tabControl.TabPages.Add(tabTree);

            // Tab 5: Heap
            var tabHeap = new TabPage("📊 Heap");
            tabHeap.Controls.Add(CreateListView("lvHeap", new[] { "ID", "Citizen", "Category", "Priority", "Date" }));
            tabControl.TabPages.Add(tabHeap);

            // Tab 6: Graph
            var tabGraph = new TabPage("🔗 Graph");
            tabGraph.Controls.Add(CreateListView("lvGraph", new[] { "Node", "Connections", "Neighbors" }));
            tabControl.TabPages.Add(tabGraph);

            // Tab 7: BFS Traversal
            var tabBFS = new TabPage("🔍 BFS");
            tabBFS.Controls.Add(CreateListView("lvBFS", new[] { "Order", "Request ID", "Citizen" }));
            tabControl.TabPages.Add(tabBFS);

            // Tab 8: DFS Traversal
            var tabDFS = new TabPage("🔍 DFS");
            tabDFS.Controls.Add(CreateListView("lvDFS", new[] { "Order", "Request ID", "Citizen" }));
            tabControl.TabPages.Add(tabDFS);

            // Tab 9: MST
            var tabMST = new TabPage("🌐 MST");
            tabMST.Controls.Add(CreateListView("lvMST", new[] { "Source", "Destination", "Weight" }));
            tabControl.TabPages.Add(tabMST);

            // Control Panel
            // Control Panel
            var pnlControls = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.FromArgb(248, 249, 250)
            };

            // Create buttons individually to avoid deconstruction errors
            int x = 20;

            // Button 1: BFS
            var btnBFS = new Button
            {
                Text = "🚀 BFS Traversal",
                Location = new Point(x, 20),
                Size = new Size(160, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBFS.Click += (s, e) => RunBFSTraversal();
            pnlControls.Controls.Add(btnBFS);
            x += 170;

            // Button 2: DFS
            var btnDFS = new Button
            {
                Text = "🚀 DFS Traversal",
                Location = new Point(x, 20),
                Size = new Size(160, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDFS.Click += (s, e) => RunDFSTraversal();
            pnlControls.Controls.Add(btnDFS);
            x += 170;

            // Button 3: MST
            var btnMST = new Button
            {
                Text = "🌐 Compute MST",
                Location = new Point(x, 20),
                Size = new Size(160, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnMST.Click += (s, e) => ComputeMST();
            pnlControls.Controls.Add(btnMST);
            x += 170;

            // Button 4: Search AVL
            var btnSearch = new Button
            {
                Text = "🔍 Search AVL",
                Location = new Point(x, 20),
                Size = new Size(160, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSearch.Click += (s, e) => SearchAVL();
            pnlControls.Controls.Add(btnSearch);
            x += 170;

            // Button 5: View Heap
            var btnHeap = new Button
            {
                Text = "📊 View Heap",
                Location = new Point(x, 20),
                Size = new Size(160, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnHeap.Click += (s, e) => ViewHeap();
            pnlControls.Controls.Add(btnHeap);
            x += 170;

            // Button 6: Back
            var btnBack = new Button
            {
                Text = "↩ Back to Menu",
                Location = new Point(x, 20),
                Size = new Size(160, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBack.Click += (s, e) => this.Close();
            pnlControls.Controls.Add(btnBack);

            this.Controls.Add(tabControl);
            this.Controls.Add(pnlControls);
        }


        private ListView CreateListView(string name, string[] columns)
        {
            var lv = new ListView
            {
                Name = name,
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Font = new Font("Segoe UI", 9)
            };

            foreach (var col in columns)
            {
                lv.Columns.Add(col, 120);
            }

            return lv;
        }

        private ListView GetListView(string name)
        {
            return this.Controls.Find(name, true).FirstOrDefault() as ListView;
        }

        private void DisplayAllStructures()
        {
            // Display BST
            var lvBST = GetListView("lvBST");
            if (lvBST != null)
            {
                lvBST.Items.Clear();
                var inOrder = bst.InOrderTraversal();
                foreach (var req in inOrder)
                {
                    var item = new ListViewItem(req.RequestID.ToString());
                    item.SubItems.Add(req.CitizenName);
                    item.SubItems.Add(req.Category);
                    item.SubItems.Add(req.Status);
                    lvBST.Items.Add(item);
                }
            }

            // Display AVL
            var lvAVL = GetListView("lvAVL");
            if (lvAVL != null)
            {
                lvAVL.Items.Clear();
                var inOrder = avlTree.InOrderTraversal();
                foreach (var req in inOrder)
                {
                    var item = new ListViewItem(req.RequestID.ToString());
                    item.SubItems.Add(req.CitizenName);
                    item.SubItems.Add(req.Category);
                    item.SubItems.Add(req.Status);
                    // Height would be stored in AVL node
                    item.SubItems.Add("Balanced");
                    lvAVL.Items.Add(item);
                }
            }

            // Display Red-Black
            var lvRB = GetListView("lvRB");
            if (lvRB != null)
            {
                lvRB.Items.Clear();
                var inOrder = redBlackTree.InOrderTraversal();
                foreach (var req in inOrder)
                {
                    var item = new ListViewItem(req.RequestID.ToString());
                    item.SubItems.Add(req.CitizenName);
                    item.SubItems.Add(req.Category);
                    item.SubItems.Add(req.Status);
                    item.SubItems.Add("⚫ Black");
                    lvRB.Items.Add(item);
                }
            }

            // Display Tree
            var lvTree = GetListView("lvTree");
            if (lvTree != null)
            {
                lvTree.Items.Clear();
                DisplayTree(lvTree, categoryTree.Root, 0);
            }

            // Display Heap
            var lvHeap = GetListView("lvHeap");
            if (lvHeap != null)
            {
                lvHeap.Items.Clear();
                var allRequests = heap.GetAll();
                foreach (var req in allRequests)
                {
                    var item = new ListViewItem(req.RequestID.ToString());
                    item.SubItems.Add(req.CitizenName);
                    item.SubItems.Add(req.Category);
                    item.SubItems.Add(req.Priority.ToString());
                    item.SubItems.Add(req.SubmissionDate.ToString("dd MMM yyyy"));
                    lvHeap.Items.Add(item);
                }
            }

            // Display Graph
            var lvGraph = GetListView("lvGraph");
            if (lvGraph != null)
            {
                graph.DisplayGraph(lvGraph);
            }
        }

        private void DisplayTree(ListView lv, BasicTreeNode node, int level)
        {
            if (node == null) return;

            string indent = new string(' ', level * 4);
            var item = new ListViewItem(indent + "📁 " + node.Category);
            item.SubItems.Add(node.Requests.Count.ToString());
            lv.Items.Add(item);

            foreach (var child in node.Children)
            {
                DisplayTree(lv, child, level + 1);
            }
        }

        // ============================================================
        // ACTIONS
        // ============================================================

        private void RunBFSTraversal()
        {
            if (graph.NodeCount == 0)
            {
                MessageBox.Show("Graph is empty!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var start = requests.First();
            var traversal = graph.BreadthFirstSearch(start);
            var lv = GetListView("lvBFS");
            if (lv != null) graph.DisplayTraversal(lv, traversal);

            MessageBox.Show($"✅ BFS Traversal Complete!\nVisited {traversal.Count} nodes starting from ID: {start.RequestID}",
                "BFS Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RunDFSTraversal()
        {
            if (graph.NodeCount == 0)
            {
                MessageBox.Show("Graph is empty!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var start = requests.First();
            var traversal = graph.DepthFirstSearch(start);
            var lv = GetListView("lvDFS");
            if (lv != null) graph.DisplayTraversal(lv, traversal);

            MessageBox.Show($"✅ DFS Traversal Complete!\nVisited {traversal.Count} nodes starting from ID: {start.RequestID}",
                "DFS Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ComputeMST()
        {
            if (graph.NodeCount < 2)
            {
                MessageBox.Show("Need at least 2 nodes for MST!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var mst = graph.GetMinimumSpanningTree();
            var lv = GetListView("lvMST");
            if (lv != null) graph.DisplayMST(lv, mst);

            int totalWeight = mst.Sum(e => e.Weight);
            MessageBox.Show($"✅ Minimum Spanning Tree Complete!\n{graph.NodeCount} nodes connected with {mst.Count} edges\nTotal Weight: {totalWeight}",
                "MST Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SearchAVL()
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter Request ID to search in AVL Tree:",
                "Search AVL Tree",
                "101",
                -1, -1);

            if (int.TryParse(input, out int id))
            {
                var result = avlTree.Search(id);
                if (result != null)
                {
                    MessageBox.Show($"✅ Found Request in AVL Tree!\n\n" +
                                   $"ID: {result.RequestID}\n" +
                                   $"Citizen: {result.CitizenName}\n" +
                                   $"Category: {result.Category}\n" +
                                   $"Status: {result.Status}\n" +
                                   $"Location: {result.Location}\n" +
                                   $"Description: {result.Description}",
                                   "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"❌ Request ID {id} not found in AVL Tree.",
                        "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void ViewHeap()
        {
            string structure = heap.GetHeapStructure();
            if (!string.IsNullOrEmpty(structure))
            {
                MessageBox.Show($"Min Heap Structure:\n\n{structure}\n\n" +
                               $"Total Items: {heap.Count}\n" +
                               $"Min Item: {heap.Peek().RequestID} - {heap.Peek().CitizenName}",
                               "Heap Visualization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ServiceRequestStatusForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "ServiceRequestStatusForm";
            this.Load += new System.EventHandler(this.ServiceRequestStatusForm_Load);
            this.ResumeLayout(false);

        }

        private void ServiceRequestStatusForm_Load(object sender, EventArgs e)
        {

        }
    }

    // ============================================================
    // SUPPORTING CLASSES
    // ============================================================

    public class BasicTreeNode
    {
        public string Category { get; set; }
        public List<ServiceRequest> Requests { get; set; }
        public List<BasicTreeNode> Children { get; set; }

        public BasicTreeNode(string category)
        {
            Category = category;
            Requests = new List<ServiceRequest>();
            Children = new List<BasicTreeNode>();
        }

        public void AddChild(BasicTreeNode child)
        {
            Children.Add(child);
        }

        public void AddRequest(ServiceRequest request)
        {
            Requests.Add(request);
        }
    }

    public class BasicTree
    {
        public BasicTreeNode Root { get; set; }

        public BasicTree(string rootCategory)
        {
            Root = new BasicTreeNode(rootCategory);
        }
    }

    public class BSTNode
    {
        public ServiceRequest Request { get; set; }
        public BSTNode Left { get; set; }
        public BSTNode Right { get; set; }

        public BSTNode(ServiceRequest request)
        {
            Request = request;
        }
    }

    public class BinarySearchTree<T> where T : ServiceRequest
    {
        private BSTNode root;

        public void Insert(T request)
        {
            root = InsertRec(root, request);
        }

        private BSTNode InsertRec(BSTNode node, T request)
        {
            if (node == null)
                return new BSTNode(request);

            if (request.RequestID < node.Request.RequestID)
                node.Left = InsertRec(node.Left, request);
            else if (request.RequestID > node.Request.RequestID)
                node.Right = InsertRec(node.Right, request);

            return node;
        }

        public List<T> InOrderTraversal()
        {
            var result = new List<T>();
            InOrderRec(root, result);
            return result;
        }

        private void InOrderRec(BSTNode node, List<T> result)
        {
            if (node != null)
            {
                InOrderRec(node.Left, result);
                result.Add((T)node.Request);
                InOrderRec(node.Right, result);
            }
        }
    }

    public class RedBlackTree<T> where T : ServiceRequest
    {
        private enum Color { Red, Black }

        private class RBNode
        {
            public T Request { get; set; }
            public RBNode Left { get; set; }
            public RBNode Right { get; set; }
            public RBNode Parent { get; set; }
            public Color NodeColor { get; set; }

            public RBNode(T request)
            {
                Request = request;
                NodeColor = Color.Red;
            }
        }

        private RBNode root;
        private List<T> traversalResult;

        public void Insert(T request)
        {
            var newNode = new RBNode(request);
            root = InsertRec(root, newNode);
            FixViolation(newNode);
            root.NodeColor = Color.Black;
        }

        private RBNode InsertRec(RBNode node, RBNode newNode)
        {
            if (node == null)
                return newNode;

            if (newNode.Request.RequestID < node.Request.RequestID)
            {
                node.Left = InsertRec(node.Left, newNode);
                node.Left.Parent = node;
            }
            else if (newNode.Request.RequestID > node.Request.RequestID)
            {
                node.Right = InsertRec(node.Right, newNode);
                node.Right.Parent = node;
            }

            return node;
        }

        private void FixViolation(RBNode node)
        {
            while (node != root && node.Parent != null && node.Parent.NodeColor == Color.Red)
            {
                var parent = node.Parent;
                var grandParent = parent.Parent;

                if (grandParent == null) break;

                if (parent == grandParent.Left)
                {
                    var uncle = grandParent.Right;

                    if (uncle != null && uncle.NodeColor == Color.Red)
                    {
                        grandParent.NodeColor = Color.Red;
                        parent.NodeColor = Color.Black;
                        uncle.NodeColor = Color.Black;
                        node = grandParent;
                    }
                    else
                    {
                        if (node == parent.Right)
                        {
                            RotateLeft(parent);
                            node = parent;
                            parent = node.Parent;
                        }

                        RotateRight(grandParent);
                        parent.NodeColor = Color.Black;
                        grandParent.NodeColor = Color.Red;
                        node = parent;
                    }
                }
                else
                {
                    var uncle = grandParent.Left;

                    if (uncle != null && uncle.NodeColor == Color.Red)
                    {
                        grandParent.NodeColor = Color.Red;
                        parent.NodeColor = Color.Black;
                        uncle.NodeColor = Color.Black;
                        node = grandParent;
                    }
                    else
                    {
                        if (node == parent.Left)
                        {
                            RotateRight(parent);
                            node = parent;
                            parent = node.Parent;
                        }

                        RotateLeft(grandParent);
                        parent.NodeColor = Color.Black;
                        grandParent.NodeColor = Color.Red;
                        node = parent;
                    }
                }
            }
            root.NodeColor = Color.Black;
        }

        private void RotateLeft(RBNode x)
        {
            var y = x.Right;
            x.Right = y.Left;

            if (y.Left != null)
                y.Left.Parent = x;

            y.Parent = x.Parent;

            if (x.Parent == null)
                root = y;
            else if (x == x.Parent.Left)
                x.Parent.Left = y;
            else
                x.Parent.Right = y;

            y.Left = x;
            x.Parent = y;
        }

        private void RotateRight(RBNode y)
        {
            var x = y.Left;
            y.Left = x.Right;

            if (x.Right != null)
                x.Right.Parent = y;

            x.Parent = y.Parent;

            if (y.Parent == null)
                root = x;
            else if (y == y.Parent.Left)
                y.Parent.Left = x;
            else
                y.Parent.Right = x;

            x.Right = y;
            y.Parent = x;
        }

        public List<T> InOrderTraversal()
        {
            traversalResult = new List<T>();
            InOrderRec(root);
            return traversalResult;
        }

        private void InOrderRec(RBNode node)
        {
            if (node != null)
            {
                InOrderRec(node.Left);
                traversalResult.Add(node.Request);
                InOrderRec(node.Right);
            }
        }
    }
}