using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test6.Solution
{
    public class Generator<T>
    {
        private readonly Func<T, T, T> _calculationRule;

        public Generator(Func<T, T, T> calculationRule)
        {
            _calculationRule = calculationRule;
        }

        public IEnumerable<T> Generate(T first, T second, int count)
        {
            T current = first;
            T next = second;
            yield return current;
            yield return next;
            for (int i = 2; i < count; i++)
            {
                var temp = _calculationRule(current, next);
                current = next;
                next = temp;
                yield return next;                
            }            
        }
    }
}
