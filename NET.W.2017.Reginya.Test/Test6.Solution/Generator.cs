using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test6.Solution
{
    public static class Generator<T>
    {            
        public static IEnumerable<T> Generate(T first, T second, int count, Func<T, T, T> calculationRule)
        {
            VerifyInput(first, second, calculationRule);

            T current = first;
            T next = second;
            yield return current;
            yield return next;
            for (int i = 2; i < count; i++)
            {
                var temp = calculationRule(current, next);
                current = next;
                next = temp;
                yield return next;                
            }            
        }

        private static void VerifyInput(T first, T second, Func<T, T, T> calculationRule)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (calculationRule == null)
            {
                throw new ArgumentNullException(nameof(calculationRule));
            }
        }
    }
}
