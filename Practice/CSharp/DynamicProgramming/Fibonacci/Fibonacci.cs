using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.DynamicProgramming.Fibonacci
{
    /// <summary>
    /// Fibonacci sequence calculator.
    /// </summary>
    public class Fibonacci
    {
        private Dictionary<int, int> cache;
        
        /// <summary>
        /// Class used to calculate fibonacci sequences.
        /// </summary>
        /// <param name="useCache"></param>
        public Fibonacci(bool useCache = false)
        {
            UseCache = useCache;
            this.cache = new Dictionary<int, int>();;
        }

        /// <summary>
        /// Gets or sets a value indicating if the calculator should use a cache.
        /// </summary>
        public bool UseCache
        {
            get;
            set;
        }

        /// <summary>
        /// Calculate the <paramref name="sequence"/>th fibonacci number.
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public int Calculate(int sequence)
        {
            int value = 0;

            // check the cache for the value.
            if (cache.ContainsKey(sequence) && UseCache)
            {
                // cache contains the sequence value.
                value = cache[sequence];
            }
            else
            {
                // cache does not contain the value so calculate it.
                if (sequence <= 0)
                {
                    value = 0;
                }
                else if (sequence == 1)
                {
                    value = 1;
                }
                else
                {
                    value = Calculate(sequence - 1) + Calculate(sequence - 2);
                }

                // add the calculated value to the cache.
                if (UseCache)
                {
                    cache.Add(sequence, value);
                }
            }

            return value;
        }

        /// <summary>
        /// Run a sample.
        /// </summary>
        public static void Run()
        {
            var stopwatch = new Stopwatch();
            var test = new Fibonacci();

            // do a test with no cache.
            test.UseCache = false;
            stopwatch.Start();
            for (var i = 0; i < 40; i++)
            {
                //test.Calculate(i);
                Console.WriteLine(string.Format("input: {0} -> output: {1}", i, test.Calculate(i)));
            }

            stopwatch.Stop();
            Console.WriteLine(string.Format("took: {0}ms.", stopwatch.ElapsedMilliseconds));

            // do a test with a cache.
            test.UseCache = true;
            stopwatch.Restart();
            for (var i = 0; i < 40; i++)
            {
                //test.Calculate(i);
                Console.WriteLine(string.Format("input: {0} -> output: {1}", i, test.Calculate(i)));
            }

            stopwatch.Stop();
            Console.WriteLine(string.Format("took: {0}ms.", stopwatch.ElapsedMilliseconds));

            Console.Read();
        }
    }
}
