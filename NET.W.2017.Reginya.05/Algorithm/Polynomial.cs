using System;
using System.Text;

namespace Algorithm
{
    /// <summary>
    /// Provides methods and operations to work with polynomials.
    /// </summary>
    public class Polynomial
    {
        #region Public constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Polynomial"/> class
        /// </summary>
        /// <param name="coefficients">Coefficients of the polynomial</param>
        public Polynomial(double[] coefficients)
        {
            Coefficients = coefficients;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Max degree of the polynomial
        /// </summary>
        public int MaxDegree => Coefficients.Length - 1;

        /// <summary>
        /// Coefficients of the polynomial
        /// </summary>
        public double[] Coefficients { get; }

        /// <summary>
        /// Returns the coefficient at the given degree
        /// </summary>
        /// <param name="degree">Degree that corresponds to the coefficient</param>
        /// <returns>The coefficient at the given degree</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws if the degree is negative
        /// </exception>
        public double this[int degree]
        {
            get
            {
                if (degree < 0) throw new ArgumentOutOfRangeException(nameof(degree));
                if (degree >= Coefficients.Length) return 0;
                return Coefficients[degree];            
            }
        }

        #endregion

        #region Public methods

        public double Calculate(double arg)
        {
            double result = 0;
            for (int i = 0; i < MaxDegree + 1; i++)
            {
                result += Math.Pow(arg, i) * Coefficients[i];
            }
            return result;
        }

        #endregion

        #region Overloaded operators

        public static Polynomial operator +(Polynomial first, Polynomial second)
        {
            if (first == null || second == null)
                throw new ArgumentNullException();

            int maxLength = (first.MaxDegree > second.MaxDegree ? first.MaxDegree : second.MaxDegree) + 1;
            int minLength = (first.MaxDegree < second.MaxDegree ? first.MaxDegree : second.MaxDegree) + 1;

            var newCoefficients = new double[maxLength];
            for (int i = 0; i < minLength; i++)
                newCoefficients[i] = first[i] + second[i];

            if (first.MaxDegree > second.MaxDegree)
                Array.Copy(first.Coefficients, minLength, newCoefficients, minLength, maxLength - minLength);
            else
                Array.Copy(second.Coefficients, minLength, newCoefficients, minLength, maxLength - minLength);

            return new Polynomial(newCoefficients);
        }

        public static Polynomial operator -(Polynomial first, Polynomial second)
        {
            if (first == null || second == null)
                throw new ArgumentNullException();

            int maxLength = (first.MaxDegree > second.MaxDegree ? first.MaxDegree : second.MaxDegree) + 1;
            int minLength = (first.MaxDegree < second.MaxDegree ? first.MaxDegree : second.MaxDegree) + 1;

            var newCoefficients = new double[maxLength];
            for (int i = 0; i < minLength; i++)
                newCoefficients[i] = first[i] - second[i];

            if (first.MaxDegree > second.MaxDegree)
                Array.Copy(first.Coefficients, minLength, newCoefficients, minLength, maxLength - minLength);
            else
                for (int i = minLength; i < maxLength; i++)
                    newCoefficients[i] = -second[i];

            return new Polynomial(newCoefficients);
        }
                        
        public static Polynomial operator *(Polynomial polynomial, double multiplier)
        {
            if (polynomial == null)
                throw new ArgumentNullException(nameof(polynomial));

            var newCoefficients = new double[polynomial.MaxDegree + 1];
            for (int i = 0; i < polynomial.MaxDegree + 1; i++)
                newCoefficients[i] = polynomial[i] * multiplier;
            return new Polynomial(newCoefficients);
        }
               
        public static Polynomial operator *(double multiplier, Polynomial polynomial)
        {
            return polynomial * multiplier;
        }

        public static Polynomial operator *(Polynomial first, Polynomial second)
        {
            if (first == null || second == null)
                throw new ArgumentNullException();

            int newDegree = first.MaxDegree + second.MaxDegree;
            var newCoefficients = new double[newDegree + 1];

            for (int i = 0; i <= first.MaxDegree; i++)
                for (int j = 0; j <= second.MaxDegree; j++)
                    newCoefficients[i + j] += first[i] * second[j];                            

            return new Polynomial(newCoefficients);
        }
                
        public static Polynomial operator /(Polynomial polynomial, double divider)
        {
            if (polynomial == null)
                throw new ArgumentNullException(nameof(polynomial));

            if (Math.Abs(divider - 0.0) < double.Epsilon)
                throw new ArgumentException(nameof(divider) + " can not be zero");

            var newCoefficients = new double[polynomial.MaxDegree + 1];
            for (int i = 0; i < polynomial.MaxDegree + 1; i++)
                newCoefficients[i] = polynomial[i] / divider;
            return new Polynomial(newCoefficients);
        }

        public static bool operator ==(Polynomial first, Polynomial second)
        {
            if (object.ReferenceEquals(first, null) && object.ReferenceEquals(second, null))
                return true;

            if (object.ReferenceEquals(first, null) || object.ReferenceEquals(second, null))
                return false;

            if (first.MaxDegree != second.MaxDegree)
                return false;

            for (int i = 0; i <= first.MaxDegree; i++)            
                if (Math.Abs(first[i] - second[i]) > double.Epsilon)
                    return false;            
            
            return true;
        }

        public static bool operator !=(Polynomial first, Polynomial second)
        {
            return !(first == second);
        }

        #endregion

        #region Overloaded methods of class Object

        /// <summary>
        /// Converts the value of this instance to a <see cref="string"/>
        /// </summary>
        /// <returns>String representation of polynomial</returns>
        public override string ToString()
        {
            var str = new StringBuilder();

            if (MaxDegree >= 0)            
                str.AppendFormat($"{Coefficients[0]}");            

            for (int i = 1; i < Coefficients.Length; i++)
            {                                
                if (Coefficients[i] > 0)
                    str.AppendFormat($" + {Coefficients[i]}*x^{i}");
                else
                    str.AppendFormat($" {Coefficients[i]}*x^{i}");
            }

            return str.ToString();
        }

        /// <summary>
        /// Determines whether the specified instance of <see cref="Polynomial"/>
        /// class is equal to the current instance
        /// </summary>
        /// <param name="obj">Polynomial to compare with the current instance</param>
        /// <returns>The result of checking equality</returns>
        public override bool Equals(object obj)
        {
            return this == obj as Polynomial;
        }

        /// <summary>
        /// Returns the hash code for this instance of <see cref="Polynomial"/> class
        /// </summary>
        /// <returns>The hash code for this instance</returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();            
        }

        #endregion
    }
}
