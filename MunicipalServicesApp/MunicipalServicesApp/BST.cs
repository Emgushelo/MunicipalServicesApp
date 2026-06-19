using System;
using System.Collections.Generic;
using MunicipalServicesApp.Models;  

namespace MunicipalServicesApp
{
    public class BST<T> where T : class
    {
        private class Node
        {

            public T Data { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node(T data) { Data = data; }
        }


        private Node _root;
        private readonly Func<T, T, int> _comparer;

        public BST(Func<T, T, int> comparer)
        {
            _comparer = comparer;
        }

        public void Insert(T data)
        {
            _root = InsertRec(_root, data);
        }

        private Node InsertRec(Node root, T data)
        {
            if (root == null) return new Node(data);

            if (_comparer(data, root.Data) < 0)
                root.Left = InsertRec(root.Left, data);
            else if (_comparer(data, root.Data) > 0)
                root.Right = InsertRec(root.Right, data);

            return root;
        }

        public T Find(string requestId)
        {
            return FindRec(_root, requestId);
        }

        private T FindRec(Node root, string requestId)
        {
            if (root == null) return null;

            var req = root.Data as ServiceRequest;
            if (req == null) return null;

            if (req.RequestId == requestId) return root.Data;

            if (string.Compare(requestId, req.RequestId) < 0)
                return FindRec(root.Left, requestId);
            else
                return FindRec(root.Right, requestId);
        }

        public List<T> InOrderTraversal()
        {
            var result = new List<T>();
            InOrderRec(_root, result);
            return result;
        }

        private void InOrderRec(Node root, List<T> result)
        {
            if (root != null)
            {
                InOrderRec(root.Left, result);
                result.Add(root.Data);
                InOrderRec(root.Right, result);
            }
        }

        public List<T> PreOrderTraversal()
        {
            var result = new List<T>();
            PreOrderRec(_root, result);
            return result;
        }

        private void PreOrderRec(Node root, List<T> result)
        {
            if (root != null)
            {
                result.Add(root.Data);
                PreOrderRec(root.Left, result);
                PreOrderRec(root.Right, result);
            }
        }

        public List<T> PostOrderTraversal()
        {
            var result = new List<T>();
            PostOrderRec(_root, result);
            return result;
        }

        private void PostOrderRec(Node root, List<T> result)
        {
            if (root != null)
            {
                PostOrderRec(root.Left, result);
                PostOrderRec(root.Right, result);
                result.Add(root.Data);
            }
        }
    }
}