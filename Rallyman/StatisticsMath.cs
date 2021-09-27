using System;


namespace Rallyman
{
    public class StatisticsMath
    {
        /// <summary>
        /// Calculates the factorial of <paramref name="n"/>
        /// </summary>
        static public ulong Factorial(ulong n)
        {
            if (n == 0)
                return 1;

            var result = n;
            while (--n != 0)
                result *= n;

            return result;
        }

        /// <summary>
        /// Calculates how many different combinations of <paramref name="k"/>
        /// elements exist in a set of <paramref name="n"/> elements
        /// </summary>
        static public ulong Combination(ulong k, ulong n)
        {
            if (k > n)
                return 0;

            return Factorial(n) / (Factorial(k) * Factorial(n - k));
        }

        /// <summary>
        /// Calculates the probability of an event A with P(A) = <paramref name="p"/>
        /// happening exactly <paramref name="k"/> times in a set of
        /// <paramref name="n"/> elements
        /// </summary>
        ///
        /// <exception cref="ArgumentOutOfRangeException"/>
        static public double BinomialExactly(double p, ulong k, ulong n)
        {
            if (Math.Sign(p) == -1 || p > 1.0)
                throw new ArgumentOutOfRangeException();

            return Combination(k, n) * Math.Pow(p, k) * Math.Pow(1 - p, n - k);
        }

        /// <summary>
        /// Calculates the probability of an event A with P(A) = <paramref name="p"/>
        /// happening at least <paramref name="k"/> times in a set of
        /// <paramref name="n"/> elements
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        static public double BinomialAtLeast(double p, ulong k, ulong n)
        {
            var result = 0.0;
            while (k <= n)
            {
                result += BinomialExactly(p, k, n);
                ++k;
            }
            return result;
        }
    }
}
