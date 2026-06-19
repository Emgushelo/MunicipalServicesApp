using MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServicesApp.DataStructures
{
    public class Graph<T> where T : ServiceRequest
    {
        private class GraphNode
        {
            public T Data { get; set; }
            public Dictionary<GraphNode, int> Neighbors { get; set; }

            public GraphNode(T data)
            {
                Data = data;
                Neighbors = new Dictionary<GraphNode, int>();
            }
        }


        private List<GraphNode> nodes;
        public int NodeCount => nodes.Count;

        public Graph()
        {
            nodes = new List<GraphNode>();
        }

        // Add a node/vertex
        public void AddNode(T data)
        {
            if (!nodes.Any(n => n.Data.RequestID == data.RequestID))
            {
                nodes.Add(new GraphNode(data));
            }
        }

        // Add an edge with weight
        public void AddEdge(T from, T to, int weight = 1)
        {
            var fromNode = nodes.FirstOrDefault(n => n.Data.RequestID == from.RequestID);
            var toNode = nodes.FirstOrDefault(n => n.Data.RequestID == to.RequestID);

            if (fromNode != null && toNode != null)
            {
                if (!fromNode.Neighbors.ContainsKey(toNode))
                {
                    fromNode.Neighbors[toNode] = weight;
                    toNode.Neighbors[fromNode] = weight;
                }
            }
        }

        // Breadth-First Search
        public List<T> BreadthFirstSearch(T start)
        {
            var startNode = nodes.FirstOrDefault(n => n.Data.RequestID == start.RequestID);
            if (startNode == null) return new List<T>();

            var visited = new HashSet<GraphNode>();
            var queue = new Queue<GraphNode>();
            var result = new List<T>();

            visited.Add(startNode);
            queue.Enqueue(startNode);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current.Data);

                foreach (var neighbor in current.Neighbors.Keys)
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return result;
        }

        // Depth-First Search
        public List<T> DepthFirstSearch(T start)
        {
            var startNode = nodes.FirstOrDefault(n => n.Data.RequestID == start.RequestID);
            if (startNode == null) return new List<T>();

            var visited = new HashSet<GraphNode>();
            var result = new List<T>();
            DFSRecursive(startNode, visited, result);
            return result;
        }

        private void DFSRecursive(GraphNode node, HashSet<GraphNode> visited, List<T> result)
        {
            visited.Add(node);
            result.Add(node.Data);

            foreach (var neighbor in node.Neighbors.Keys)
            {
                if (!visited.Contains(neighbor))
                {
                    DFSRecursive(neighbor, visited, result);
                }
            }
        }

        // Get Minimum Spanning Tree using Prim's algorithm
        public List<(T Source, T Destination, int Weight)> GetMinimumSpanningTree()
        {
            if (nodes.Count < 2) return new List<(T, T, int)>();

            var mst = new List<(T, T, int)>();
            var visited = new HashSet<GraphNode>();
            var edges = new List<(GraphNode From, GraphNode To, int Weight)>();

            var start = nodes[0];
            visited.Add(start);

            foreach (var neighbor in start.Neighbors)
            {
                edges.Add((start, neighbor.Key, neighbor.Value));
            }

            while (edges.Count > 0 && visited.Count < nodes.Count)
            {
                edges.Sort((a, b) => a.Weight.CompareTo(b.Weight));
                var minEdge = edges.FirstOrDefault(e => !visited.Contains(e.To));
                if (minEdge.From == null) break;

                mst.Add((minEdge.From.Data, minEdge.To.Data, minEdge.Weight));
                visited.Add(minEdge.To);

                foreach (var neighbor in minEdge.To.Neighbors)
                {
                    if (!visited.Contains(neighbor.Key))
                    {
                        edges.Add((minEdge.To, neighbor.Key, neighbor.Value));
                    }
                }
            }

            return mst;
        }

        // Display Graph in ListView
        public void DisplayGraph(ListView lv)
        {
            lv.Items.Clear();
            foreach (var node in nodes)
            {
                var item = new ListViewItem($"📌 {node.Data.RequestID} - {node.Data.CitizenName}");
                item.SubItems.Add(node.Neighbors.Count.ToString());
                item.SubItems.Add(string.Join(", ", node.Neighbors.Keys.Select(n => n.Data.RequestID)));
                lv.Items.Add(item);
            }
        }

        // Display Traversal result in ListView
        public void DisplayTraversal(ListView lv, List<T> traversal)
        {
            lv.Items.Clear();
            int order = 1;
            foreach (var item in traversal)
            {
                var lvItem = new ListViewItem(order.ToString());
                lvItem.SubItems.Add(item.RequestID.ToString());
                lvItem.SubItems.Add(item.CitizenName);
                lv.Items.Add(lvItem);
                order++;
            }
        }

        // Display MST in ListView
        public void DisplayMST(ListView lv, List<(T Source, T Destination, int Weight)> mst)
        {
            lv.Items.Clear();
            foreach (var edge in mst)
            {
                var item = new ListViewItem(edge.Source.RequestID.ToString());
                item.SubItems.Add(edge.Destination.RequestID.ToString());
                item.SubItems.Add(edge.Weight.ToString());
                lv.Items.Add(item);
            }
        }

        public void Clear()
        {
            nodes.Clear();
        }

        internal IEnumerable<object> BFS(ServiceRequest request)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<object> BFS(string requestId)
        {
            throw new NotImplementedException();
        }

        internal int GetVertexCount()
        {
            throw new NotImplementedException();
        }

        internal int GetEdgeCount()
        {
            throw new NotImplementedException();
        }
    }
}