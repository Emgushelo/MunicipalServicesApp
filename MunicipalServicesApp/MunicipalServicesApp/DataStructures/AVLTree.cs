using System;
using System.Collections.Generic;

namespace MunicipalServicesApp.DataStructures
{
    public class AVLTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        private class AVLNode
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public AVLNode Left { get; set; }
            public AVLNode Right { get; set; }
            public int Height { get; set; }

            public AVLNode(TKey key, TValue value)
            {
                Key = key;
                Value = value;
                Height = 1;
            }
        }

        private AVLNode root;

        public void Insert(TKey key, TValue value)
        {
            root = InsertRec(root, key, value);
        }

        private AVLNode InsertRec(AVLNode node, TKey key, TValue value)
        {
            if (node == null)
                return new AVLNode(key, value);

            int compare = key.CompareTo(node.Key);
            if (compare < 0)
                node.Left = InsertRec(node.Left, key, value);
            else if (compare > 0)
                node.Right = InsertRec(node.Right, key, value);
            else
                return node;

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            int balance = GetBalance(node);

            if (balance > 1 && key.CompareTo(node.Left.Key) < 0)
                return RotateRight(node);

            if (balance > 1 && key.CompareTo(node.Left.Key) > 0)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            if (balance < -1 && key.CompareTo(node.Right.Key) > 0)
                return RotateLeft(node);

            if (balance < -1 && key.CompareTo(node.Right.Key) < 0)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        public TValue Search(TKey key)
        {
            return SearchRec(root, key);
        }

        private TValue SearchRec(AVLNode node, TKey key)
        {
            if (node == null)
                return default(TValue);

            int compare = key.CompareTo(node.Key);
            if (compare == 0)
                return node.Value;
            else if (compare < 0)
                return SearchRec(node.Left, key);
            else
                return SearchRec(node.Right, key);
        }

        public List<TValue> InOrderTraversal()
        {
            var result = new List<TValue>();
            InOrderRec(root, result);
            return result;
        }

        private void InOrderRec(AVLNode node, List<TValue> result)
        {
            if (node != null)
            {
                InOrderRec(node.Left, result);
                result.Add(node.Value);
                InOrderRec(node.Right, result);
            }
        }

        private int GetHeight(AVLNode node) => node?.Height ?? 0;
        private int GetBalance(AVLNode node) => node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);

        private AVLNode RotateRight(AVLNode y)
        {
            var x = y.Left;
            var T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));
            x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));

            return x;
        }

        private AVLNode RotateLeft(AVLNode x)
        {
            var y = x.Right;
            var T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));
            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));

            return y;
        }
    }
}