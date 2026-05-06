using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyPatternTask
{
    public interface IFilterStrategy
    {
        IEnumerable<int> Filter(IEnumerable<int> data);
    }

    public class EvenNumberFilter : IFilterStrategy
    {
        public IEnumerable<int> Filter(IEnumerable<int> data)
        {
            return data.Where(n => n % 2 == 0);
        }
    }

    public class PrimeNumberFilter : IFilterStrategy
    {
        public IEnumerable<int> Filter(IEnumerable<int> data)
        {
            return data.Where(IsPrime);
        }

        private bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }

    public class RangeFilter : IFilterStrategy
    {
        private readonly int _min;
        private readonly int _max;

        public RangeFilter(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public IEnumerable<int> Filter(IEnumerable<int> data)
        {
            return data.Where(n => n >= _min && n <= _max);
        }
    }

    public class DataFilter
    {
        private IFilterStrategy? _strategy;

        public void SetStrategy(IFilterStrategy strategy)
        {
            _strategy = strategy;
        }

        public IEnumerable<int> FilterData(IEnumerable<int> data)
        {
            if (_strategy == null)
            {
                throw new InvalidOperationException("Filter strategy is not set.");
            }

            return _strategy.Filter(data);
        }
    }

    class Program
    {
        static void Main()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            DataFilter context = new DataFilter();

            context.SetStrategy(new EvenNumberFilter());
            Console.WriteLine("Even: " + string.Join(", ", context.FilterData(numbers)));

            context.SetStrategy(new PrimeNumberFilter());
            Console.WriteLine("Primes: " + string.Join(", ", context.FilterData(numbers)));

            context.SetStrategy(new RangeFilter(5, 10));
            Console.WriteLine("Range (5-10): " + string.Join(", ", context.FilterData(numbers)));
        }
    }
}
