using System;
using System.Collections.Generic;

namespace Algorithm
{
    /// <summary>
    /// Provides sort criterions
    /// </summary>
    public class SortCriterionProvider
    {
        #region Enums
        /// <summary>
        /// The criterion of rows sorting
        /// </summary>
        public enum SortCriterion
        {
            RowElementsSum,
            MaxRowElement,
            MinRowElement
        }
        #endregion

        #region Delegates
        /// <summary>
        /// Determines the method of rows sorting
        /// </summary>
        public delegate int GetCriterion(int[] array);
        #endregion

        #region Private fields
        private Dictionary<SortCriterion, GetCriterion> criterions;
        #endregion

        #region Constructors

        public SortCriterionProvider()
        {
            criterions = new Dictionary<SortCriterion, GetCriterion>
            {
                { SortCriterion.RowElementsSum, GetRowSum },
                { SortCriterion.MaxRowElement, GetMaxRowElement },
                { SortCriterion.MinRowElement, GetMinRowElement }
            };
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns method that corresponds to the required criterion
        /// </summary>
        /// <param name="sortCriterion"> Required sort criterion</param>
        /// <returns>
        /// Method that corresponds to the required criterion
        /// </returns>
        public GetCriterion GetCriterionDelegate(SortCriterion sortCriterion)
        {
            return criterions[sortCriterion];
        }

        #endregion
               
        #region Private methods                      
        private static int GetMinRowElement(int[] row)
        {
            int result = row[0];
            foreach (var el in row)
            {
                if (el < result)
                    result = el;
            }
            return result;
        }

        private static int GetRowSum(int[] row)
        {
            int result = 0;
            foreach (var el in row)
                result += el;
            return result;
        }

        private static int GetMaxRowElement(int[] row)
        {
            int result = row[0];
            foreach (var el in row)
            {
                if (el > result)
                    result = el;
            }
            return result;
        }
        #endregion
    }
}
