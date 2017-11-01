using System;
using System.Text;
using System.Configuration;
using System.Linq;

namespace Algorithm
{    
    /// <summary>
    /// Provides methods and operations to work with polynomials.
    /// </summary>
    public sealed class Polynomial
    {
        #region Private fields

        private static double _epsilon = 0.000001;
        private readonly double[] _coefficients = {};

        #endregion

        #region Constructors        

        /// <summary>
        /// Initializes a new instance of <see cref="Polynomial"/> class
        /// </summary>
        /// <param name="coefficients">Coefficients of the polynomial</param>
        /// <exception cref="ArgumentNullException">
        /// Throws if coefficients array is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Throws if coefficients array is empty
        /// </exception>
        public Polynomial(params double[] coefficients)
        {
            if (coefficients == null)            
                throw new ArgumentNullException($"{nameof(coefficients)} cannot be null.");            

            if (coefficients.Length == 0)            
                throw new ArgumentException($"{nameof(coefficients)} cannot be empty.");            

            _coefficients = RemoveZeros(coefficients);
        }        

        #endregion

        #region Public properties

        public double[] Coefficients => _coefficients;

        /// <summary>
        /// Max degree of the polynomial
        /// </summary>
        public int MaxDegree
        {
            get
            {
                if (_coefficients.Length < 2)
                    return 0;
                int i;
                for (i = _coefficients.Length - 1; i >= 0; i--)
                    if (Math.Abs(_coefficients[i]) > _epsilon)
                        break;
                return i;
            }
        }

        /// <summary>
        /// Returns the coefficient at the given degree
        /// </summary>
        /// <param name="degree">Degree that corresponds to the coefficient</param>
        /// <returns>The coefficient at the given degree</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws if the degree is not valid
        /// </exception>
        public double this[int degree]
        {
            get
            {
                if (degree < 0) throw new ArgumentOutOfRangeException(nameof(degree));
                if (degree >= _coefficients.Length) return 0;
                return _coefficients[degree];            
            }
            private set
            {
                if (degree >= 0 || degree < _coefficients.Length)
                {
                    _coefficients[degree] = value;
                }
                throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Calculates the value of polynomial with argument
        /// </summary>
        /// <param name="arg">Argument of polynomial</param>
        /// <returns>the value of polynomial with argument</returns>
        public double Calculate(double arg)
        {
            double result = 0;
            for (int i = 0; i < MaxDegree + 1; i++)
            {
                result += Math.Pow(arg, i) * _coefficients[i];
            }
            return result;
        }

        /// <summary>
        /// Compares two polynomials on equality
        /// </summary>
        /// <param name="other">Polynomial to compare with the current instance</param>  
        /// <returns>Result of comparasion</returns>
        public bool Equals(Polynomial other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            if (MaxDegree != other.MaxDegree)
                return false;

            for (int i = 0; i <= MaxDegree; i++)
                if (Math.Abs(this[i] - other[i]) > _epsilon)
                    return false;

            return true;
        }

        #endregion

        #region Overloaded operators

        /// <summary>
        /// Adds two polynomials
        /// </summary>
        /// <param name="lhs">First addendum</param>
        /// <param name="rhs">Second addendum</param>
        /// <returns>Result of addition</returns>
        /// <exception cref="ArgumentNullException">
        /// Throws if one of addendums is null
        /// </exception>
        public static Polynomial operator +(Polynomial lhs, Polynomial rhs)
        {
            return Add(lhs, rhs);
        }

        /// <summary>
        /// Subtracts two polynomials
        /// </summary>
        /// <param name="lhs">Minuend</param>
        /// <param name="rhs">Subtrahend</param>
        /// <returns>Result of subtraction</returns>
        /// <exception cref="ArgumentNullException">
        /// Throws if one of parameters is null
        /// </exception>
        public static Polynomial operator -(Polynomial lhs, Polynomial rhs)
        {
            return Subtract(lhs, rhs);
        }

        /// <summary>
        /// Multiplies polynomial and multiplier
        /// </summary>
        /// <param name="polynomial">Source polynomial</param>        
        /// <param name="multiplier">Multiplier</param> 
        /// <returns>Result of multiplication</returns>
        /// <exception cref="ArgumentNullException">
        /// Throws if polynomial is null
        /// </exception>
        public static Polynomial operator *(Polynomial polynomial, double multiplier)
        {
            return Multiply(polynomial, multiplier);
        }

        /// <summary>
        /// Multiplies multiplier and polynomial
        /// </summary>
        /// <param name="polynomial">Source polynomial</param>        
        /// <param name="multiplier">Multiplier</param> 
        /// <returns>Result of multiplication</returns>
        /// <exception cref="ArgumentNullException">
        /// Throws if polynomial is null
        /// </exception>
        public static Polynomial operator *(double multiplier, Polynomial polynomial)
        {
            return Multiply(polynomial, multiplier);
        }

        /// <summary>
        /// Multiplies two polynomials
        /// </summary>
        /// <param name="lhs">First multiplier</param>        
        /// <param name="rhs">Second multiplier</param> 
        /// <returns>Result of multiplication</returns>
        /// <exception cref="ArgumentNullException">
        /// Throws if any polynomial is null
        /// </exception>
        public static Polynomial operator *(Polynomial lhs, Polynomial rhs)
        {
            return Multiply(lhs, rhs);
        }

        /// <summary>
        /// Divides polynomial and number
        /// </summary>
        /// <param name="polynomial">Source polynomial</param>        
        /// <param name="divider">Divider</param> 
        /// <returns>Result of division</returns>
        /// <exception cref="ArgumentNullException">
        /// Throws if polynomial is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Throws if divider is zero
        /// </exception>
        public static Polynomial operator /(Polynomial polynomial, double divider)
        {
            return Divide(polynomial, divider);
        }

        /// <summary>
        /// Compares two polynomials on equality
        /// </summary>
        /// <param name="lhs">First polynomial</param>
        /// <param name="rhs">Second polynomial</param>
        /// <returns>Result of comparasion</returns>
        public static bool operator ==(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, rhs)) return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) return false;

            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Compares two polynomials on non equality
        /// </summary>
        /// <param name="lhs">First polynomial</param>
        /// <param name="rhs">Second polynomial</param>
        /// <returns>Result of comparasion</returns>
        public static bool operator !=(Polynomial lhs, Polynomial rhs)
        {
            return !(lhs == rhs);
        }

        #endregion

        #region Static duplicates of operators

        /// <summary>
        /// Adds two polynomials
        /// </summary>
        /// <param name="lhs">First addendum</param>
        /// <param name="rhs">Second addendum</param>
        /// <returns>Result of addition</returns>
        /// <exception cref="ArgumentNullException">
        /// Throws if one of addendums is null
        /// </exception>
        public static Polynomial Add(Polynomial lhs, Polynomial rhs)
        {
            if (lhs == null || rhs == null)
                throw new ArgumentNullException();

            int maxLength = (lhs.MaxDegree > rhs.MaxDegree ? lhs.MaxDegree : rhs.MaxDegree) + 1;
            int minLength = (lhs.MaxDegree < rhs.MaxDegree ? lhs.MaxDegree : rhs.MaxDegree) + 1;

            var newCoefficients = new double[maxLength];
            for (int i = 0; i < minLength; i++)
                newCoefficients[i] = lhs[i] + rhs[i];

            if (lhs.MaxDegree > rhs.MaxDegree)
                Array.Copy(lhs._coefficients, minLength, newCoefficients, minLength, maxLength - minLength);
            else
                Array.Copy(rhs._coefficients, minLength, newCoefficients, minLength, maxLength - minLength);

            return new Polynomial(newCoefficients);
        }

        /// <summary>
        /// Subtracts two polynomials
        /// </summary>
        /// <param name="lhs">Minuend</param>
        /// <param name="rhs">Subtrahend</param>
        /// <returns>Result of subtraction</returns>
        /// <exception cref="ArgumentNullException">
        /// Throws if one of parameters is null
        /// </exception>
        public static Polynomial Subtract(Polynomial lhs, Polynomial rhs)
        {
            if (lhs == null || rhs == null)
                throw new ArgumentNullException();

            return Add(lhs, Negate(rhs));
        }

        /// <summary>
        /// Negates polynomial's coefficients
        /// </summary>
        /// <param name="polynomial">Source polynomial</param>        
        /// <returns>Result of negation</returns>
        /// <exception cref="ArgumentNullException">
        /// Throws if polynomial is null
        /// </exception>
        public static Polynomial Negate(Polynomial polynomial)
        {
            return Multiply(polynomial, -1d);
        }

        /// <summary>
        /// Multiplies polynomial and multiplier
        /// </summary>
        /// <param name="polynomial">Source polynomial</param>        
        /// <param name="multiplier">Multiplier</param> 
        /// <returns>Result of multiplication</returns>
        /// <exception cref="ArgumentNullException">
        /// Throws if polynomial is null
        /// </exception>
        public static Polynomial Multiply(Polynomial polynomial, double multiplier)
        {
            if (polynomial == null)
                throw new ArgumentNullException(nameof(polynomial));

            var newCoefficients = new double[polynomial.MaxDegree + 1];
            for (int i = 0; i < polynomial.MaxDegree + 1; i++)
                newCoefficients[i] = polynomial[i] * multiplier;
            return new Polynomial(newCoefficients);
        }

        /// <summary>
        /// Multiplies multiplier and polynomial
        /// </summary>
        /// <param name="polynomial">Source polynomial</param>        
        /// <param name="multiplier">Multiplier</param> 
        /// <returns>Result of multiplication</returns>
        /// <exception cref="ArgumentNullException">
        /// Throws if polynomial is null
        /// </exception>
        public static Polynomial Multiply(double multiplier, Polynomial polynomial)
        {
            return Multiply(polynomial, multiplier);
        }

        /// <summary>
        /// Multiplies two polynomials
        /// </summary>
        /// <param name="lhs">First multiplier</param>        
        /// <param name="rhs">Second multiplier</param> 
        /// <returns>Result of multiplication</returns>
        /// <exception cref="ArgumentNullException">
        /// Throws if any polynomial is null
        /// </exception>
        public static Polynomial Multiply(Polynomial lhs, Polynomial rhs)
        {
            if (lhs == null || rhs == null)
                throw new ArgumentNullException();

            int newDegree = lhs.MaxDegree + rhs.MaxDegree;
            var newCoefficients = new double[newDegree + 1];

            for (int i = 0; i <= lhs.MaxDegree; i++)
            for (int j = 0; j <= rhs.MaxDegree; j++)
                newCoefficients[i + j] += lhs[i] * rhs[j];

            return new Polynomial(newCoefficients);        
        }

        /// <summary>
        /// Divides polynomial and number
        /// </summary>
        /// <param name="polynomial">Source polynomial</param>        
        /// <param name="divider">Divider</param> 
        /// <returns>Result of division</returns>
        /// <exception cref="ArgumentNullException">
        /// Throws if polynomial is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Throws if divider is zero
        /// </exception>
        public static Polynomial Divide(Polynomial polynomial, double divider)
        {
            if (polynomial == null)
                throw new ArgumentNullException(nameof(polynomial));

            if (Math.Abs(divider) < _epsilon)
                throw new ArgumentException(nameof(divider) + " can not be zero");

            var newCoefficients = new double[polynomial.MaxDegree + 1];
            for (int i = 0; i < polynomial.MaxDegree + 1; i++)
                newCoefficients[i] = polynomial[i] / divider;
            return new Polynomial(newCoefficients);
        }

        #endregion Static duplicates of operators

        #region Overloaded methods of class Object

        /// <summary>
        /// Converts the value of this instance to a <see cref="string"/>
        /// </summary>
        /// <returns>String representation of polynomial</returns>
        public override string ToString()
        {
            var str = new StringBuilder();

            if (MaxDegree >= 0)            
                str.AppendFormat($"{_coefficients[0]}");            

            for (int i = 1; i < _coefficients.Length; i++)
            {                                
                if (_coefficients[i] > 0)
                    str.AppendFormat($" + {_coefficients[i]}*x^{i}");
                else
                    str.AppendFormat($" {_coefficients[i]}*x^{i}");
            }

            return str.ToString();
        }

        /// <summary>
        /// Determines whether the specified instance of <see cref="Polynomial"/>
        /// class is equal to the current instance
        /// </summary>
        /// <param name="other">Polynomial to compare with the current instance</param>
        /// <returns>Result of comparasion</returns>
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            if (other.GetType() != GetType()) return false;

            return Equals((Polynomial)other);
        }

        /// <summary>
        /// Returns the hash code for this instance of <see cref="Polynomial"/> class
        /// </summary>
        /// <returns>The hash code for this instance</returns>
        public override int GetHashCode()
        {
            int result = 0;
            foreach (var coefficient in _coefficients)
            {
                result += coefficient.GetHashCode() ^ MaxDegree;
            }
            return result + MaxDegree.GetHashCode();
        }

        #endregion

        #region Private methods

        private double[] RemoveZeros(double[] coefficients)
        {
            Array.Reverse(coefficients);
            int nonZeroItemIndex = Array.FindIndex(coefficients, coefficient => Math.Abs(coefficient) > _epsilon);

            double[] newCoefficients;
            Array.Reverse(coefficients);
            if (nonZeroItemIndex == -1)
            {
                newCoefficients = new double[] { 0 };
            }
            else
            {
                newCoefficients = new double[coefficients.Length - nonZeroItemIndex];
                Array.Copy(coefficients, 0, newCoefficients, 0, newCoefficients.Length);
            }

            return newCoefficients;
        }

        #endregion
    }
}
