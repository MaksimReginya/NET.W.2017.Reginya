﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace BinarySearchTree
{    
    /// <summary>
    /// Binary search tree collection.
    /// </summary>    
    public class BinarySearchTree<T> : IEnumerable<T>, IEnumerable
    {
        #region Private fields

        private Comparison<T> _comparer;
        private TreeNode<T> _head;

        #endregion

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of <see cref="BinarySearchTree{T}"/> class.
        /// </summary>        
        /// <exception cref="ArgumentNullException"></exception>
        public BinarySearchTree()
        {
            if (!IsComparerExists())
            {
                throw new ArgumentNullException("comparer", "Comparer must be specified for types without IComparable.");
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BinarySearchTree{T}"/> class.
        /// </summary>
        /// <param name="comparer">Comparer to determine the insert order of elements.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public BinarySearchTree(IComparer<T> comparer)
        {
            if (comparer != null)
            {
                this._comparer = comparer.Compare;
            }

            if (!IsComparerExists())
            {
                throw new ArgumentNullException("comparer", "Comparer must be specified for types without IComparable.");
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BinarySearchTree{T}"/> class.
        /// </summary>
        /// <param name="comparer">Comparer to determine the insert order of elements.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public BinarySearchTree(Comparison<T> comparer)
        {
            this._comparer = comparer;
            if (!IsComparerExists())
            {
                throw new ArgumentNullException("comparer", "Comparer must be specified for types without IComparable.");
            }
        }

        #endregion

        #region Public methods
        
        /// <summary>
        /// Clears the tree.
        /// </summary>
        public void Clear()
        {
            _head = null;
        }

        /// <summary>
        /// Inserts element in the binary search tree.
        /// </summary>
        /// <param name="value">Value to insert.</param>
        public void Insert(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (_head == null)
            {
                _head = new TreeNode<T>(value);
                return;
            }

            Insert(_head, value);
        }

        /// <summary>
        /// Provides preorder way of iterating through collection
        /// </summary>
        /// <returns>Collection in "preorder"</returns>
        public IEnumerable<T> Preorder()
        {
            var stack = new Stack<TreeNode<T>>();            
            stack.Push(_head);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                yield return node.Value;
                if (node.Right != null)
                {
                    stack.Push(node.Right);
                }

                if (node.Left != null)
                {
                    stack.Push(node.Left);
                }
            }
        }

        /// <summary>
        /// Provides in-order way of iterating through binary search tree.
        /// </summary>
        /// <returns>Tree elements in in-order.</returns>
        public IEnumerable<T> Inorder()
        {
            var stack = new Stack<TreeNode<T>>();
            var node = _head;

            while (stack.Count > 0 || node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    yield return node.Value;
                    node = node.Right;
                }
                else
                {
                    stack.Push(node);
                    node = node.Left;
                }
            }
        }

        /// <summary>
        /// Provides post-order way of iterating through binary search tree.
        /// </summary>
        /// <returns>Tree elements in post-order.</returns>
        public IEnumerable<T> Postorder()
        {
            var stack = new Stack<TreeNode<T>>();
            var node = _head;

            while (stack.Count > 0 || node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    if (stack.Count > 0 && node.Right == stack.Peek())
                    {
                        stack.Pop();
                        stack.Push(node);
                        node = node.Right;
                    }
                    else
                    {
                        yield return node.Value;
                        node = null;
                    }
                }
                else
                {
                    if (node.Right != null)
                    {
                        stack.Push(node.Right);
                    }

                    stack.Push(node);
                    node = node.Left;
                }
            }
        }
        
        #endregion

        #region IEnumerable implementation

        /// <inheritdoc />
        /// <summary>
        /// Gets enumerator of the object (By default, inorder way of iterating is used).
        /// </summary>
        /// <returns>Enumerator of the object.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return Inorder().GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Private methods

        private bool IsComparerExists()
        {
            if (_comparer == null)
            {
                if (typeof(T).GetInterface("IComparable`1") != null || typeof(T).GetInterface("IComparable") != null)
                {
                    _comparer = Comparer<T>.Default.Compare;
                    return true;
                }

                return false;
            }

            return true;
        }

        private void Insert(TreeNode<T> node, T value)
        {
            if (node == null)
            {
                node = new TreeNode<T>(value);
            }

            int compareResult = _comparer(value, node.Value);            

            if (compareResult >= 0)
            {
                if (node.Right == null)
                {
                    node.Right = new TreeNode<T>(value);
                }
                else
                {
                    Insert(node.Right, value);
                }
            }
            else
            {
                if (node.Left == null)
                {
                    node.Left = new TreeNode<T>(value);
                }
                else
                {
                    Insert(node.Left, value);
                }
            }
        }

        #endregion

        #region Node of the tree

        private class TreeNode<TN>
        {
            public TreeNode()
            {
                this.Value = default(TN);
            }

            public TreeNode(TN value)
            {
                this.Value = value;
            }

            public TN Value { get; }

            public TreeNode<TN> Left { get; set; }

            public TreeNode<TN> Right { get; set; }
        }

        #endregion
    }
}
