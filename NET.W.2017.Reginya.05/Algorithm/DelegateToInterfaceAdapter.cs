using System;
using System.Collections.Generic;

namespace Algorithm
{
    public class DelegateToInterfaceAdapter : IComparer<int[]>
    {
        private readonly Comparison<int[]> _delegate;

        public DelegateToInterfaceAdapter(Comparison<int[]> comparison)
        {
            if (ReferenceEquals(comparison, null))
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            _delegate = comparison;
        }

        public int Compare(int[] lhs, int[] rhs) =>
            _delegate(lhs, rhs);
    }
}
