using System;
using System.Text;

namespace Algorithm
{
    /// <summary>
    /// Provides methods to convert double into string in binary format
    /// </summary>
    public static class DoubleStringConvertion
    {
        #region Private consts
        private const int ExponentLength = 11;
        private const int MantissaLength = 52;
        private const int ExponentOffset = 1023;
        private const int SubnormalExponentOffset = -1022;
        #endregion

        #region Public methods DoubleStringConvertion
        /// <summary>
        /// Converts double into string in binary format
        /// </summary>
        /// <param name="source">Source double number to convert</param>
        /// <returns> String representation of double in binary format </returns>
        public static string ConvertToIEEE754(this double source)
        {
            string sign = "0";
            if (IsNegative(source) || double.IsNaN(source))
            {
                sign = "1";
                source = Math.Abs(source);
            }

            int exponent = GetExponent(source);
            string strExponent = ConvertExponentToString(exponent);

            double fractPart = GetMantissa(source, exponent);
            string strFractPart = ConverMantissaToString(fractPart);
            
            return sign + strExponent + strFractPart;
        }
        #endregion

        #region Private methods DoubleStringConvertion
        private static bool IsNegative(double source)
        {
            if (source < 0 || (source == 0.0 && double.IsNegativeInfinity(1.0 / source)))
            {
                return true;
            }

            return false;
        }
        
        private static int GetExponent(double source)
        {
            if (double.IsNaN(source))
            {
                // NaN's exponent consists only of one's
                return (int)(Math.Pow(2, ExponentLength) - 1);
            }

            int power = 0;

            double fractPart = (source / Math.Pow(2, power)) - 1;

            while (fractPart < 0 || fractPart >= 1)
            {
                power = fractPart < 1 ? --power : ++power;
                fractPart = (source / Math.Pow(2, power)) - 1;
            }
            
            power += ExponentOffset;

            power = power < 0 ? 0 : power;
            
            return power;
        }

        private static double GetMantissa(double number, int exponent)
        {
            if (double.IsNegativeInfinity(number) || double.IsPositiveInfinity(number))
            {
                return 0;
            }

            if (double.IsNaN(number))
            {
                return 0.1; // NaN has any not-zero mantissa
            }

            double fraction;

            exponent -= ExponentOffset;

            if (exponent <= -ExponentOffset)
            {
                fraction = number / Math.Pow(2, SubnormalExponentOffset);
            }
            else
            {
                fraction = (number / Math.Pow(2, exponent)) - 1;
            }
                        
            return fraction;
        }
            
        private static string ConvertExponentToString(int intPart)
        {
            var result = new StringBuilder();
            for (int i = 0; i < ExponentLength; i++)
            {
                result.Insert(0, intPart % 2);
                intPart /= 2;                
            }     
            
            return result.ToString();
        }

        private static string ConverMantissaToString(double fractPart)
        {
            var result = new StringBuilder();                    
                                     
            for (int i = 0; i < MantissaLength; i++)
            {
                fractPart *= 2;
                int intPart = (int)fractPart;
                result.Append(intPart.ToString());
                fractPart -= intPart;
            }

            return result.ToString();
        }
        #endregion
    }
}
