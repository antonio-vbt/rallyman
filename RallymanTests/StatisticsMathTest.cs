using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rallyman;

namespace RallymanTests
{
    [TestClass]
    public class StatisticsMathTest
    {
        [TestMethod]
        public void FactorialValueRange()
        {
            ulong expectedResult = 1;

            for (ulong n = 1; n < 5; ++n, expectedResult *= n)
            {
                var actualResult = StatisticsMath.Factorial(n);
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [TestMethod]
        public void FactorialZero()
        {
            ulong expectedResult = 1;
            ulong actualResult = StatisticsMath.Factorial(0);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void CombinationValueRange()
        {
            // Test all combinations of n elements possible in a range with 5 elements

            ulong[] expectedResult = { 1, 5, 10, 10, 5, 1 };
            ulong range_len = (ulong)expectedResult.Length - 1;

            for (ulong n = 0; n <= range_len; ++n)
            {
                ulong actualResult = StatisticsMath.Combination(n, range_len);
                Assert.AreEqual(expectedResult[n], actualResult);
            }
        }

        [TestMethod]
        public void CombinationTooMuchElements()
        {
            // Test a combination where the number of desired elements is greater than the whole set

            ulong expectedResult = 0;
            ulong actualResult = StatisticsMath.Combination(20, 10);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void CombinationZeroElements()
        {
            // Test trying to get 0 elements from a set

            ulong expectedResult = 1;
            ulong actualResult = StatisticsMath.Combination(0, 10);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void CombinationZeroSet()
        {
            // Test trying to get some elements from an empty set

            ulong expectedResult = 0;
            ulong actualResult = StatisticsMath.Combination(1, 0);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void CombinationZeroElementsZeroSet()
        {
            // Test trying to get 0 elements from an empty set

            ulong expectedResult = 1;
            ulong actualResult = StatisticsMath.Combination(0, 0);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void BinomialExactlyValueRange()
        {
            // Test the chance of getting 0, 1, 2 or 3 heads in 3 fair coin tosses

            double tossProbability = 0.5;
            ulong numberOfTosses = 3;
            double[] expectedResult = { .125, .375, .375, .125 };

            for (ulong heads = 0; heads <= numberOfTosses; ++heads)
            {
                var actualResult = StatisticsMath.BinomialExactly(tossProbability, heads, numberOfTosses);
                Assert.AreEqual(expectedResult[heads], actualResult, 0.001);
            }
        }

        [TestMethod]
        public void BinomialExactlyImpossibleResult()
        {
            // Test the chance of an event happening a number of times greater than the number of experiments, i.e. an impossible occurrence

            double eventProbability = 0.5;
            ulong numberOfExperiments = 3;
            ulong desiredNumberOfSuccesses = 4;

            double expectedResult = 0.0;
            double actualResult = StatisticsMath.BinomialExactly(eventProbability, desiredNumberOfSuccesses, numberOfExperiments);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void BinomialExactlyZeroProbability()
        {
            // Test the chance of an impossible event, i.e. P(A) = 0, happening. The only possible outcome
            // is the number of successes being 0

            double eventProbability = 0.0;
            ulong numberOfExperiments = 3;
            ulong desiredNumberOfSuccesses = 0;

            double expectedResult = 1.0;
            double actualResult = StatisticsMath.BinomialExactly(eventProbability, desiredNumberOfSuccesses, numberOfExperiments);
            Assert.AreEqual(expectedResult, actualResult);

            desiredNumberOfSuccesses = 1;
            expectedResult = 0.0;
            actualResult = StatisticsMath.BinomialExactly(eventProbability, desiredNumberOfSuccesses, numberOfExperiments);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void BinomialExactlyCertainProbability()
        {
            // Test the chance of a certain event, i.e. P(A) = 1, happenning. The only possible outcome is
            // the number of successes being equal to the number of experiments

            double eventProbability = 1.0;
            ulong numberOfExperiments = 5;
            ulong desiredNumberOfSuccesses = 5;

            double expectedResult = 1.0;
            double actualResult = StatisticsMath.BinomialExactly(eventProbability, desiredNumberOfSuccesses, numberOfExperiments);
            Assert.AreEqual(expectedResult, actualResult);

            desiredNumberOfSuccesses = 1;
            expectedResult = 0.0;
            actualResult = StatisticsMath.BinomialExactly(eventProbability, desiredNumberOfSuccesses, numberOfExperiments);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void BinomialExactlyNegativeProbability()
        {
            try
            {
                _ = StatisticsMath.BinomialExactly(-0.5, 1, 1);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void BinomialExactlyProbabilityGreaterThanOne()
        {
            try
            {
                _ = StatisticsMath.BinomialExactly(1.5, 1, 1);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void BinomialAtLeastValueRange()
        {
            // Test the probability of an event with P(A) = 0.5 happening at least N times (0 <= N <= 3)
            // in 3 experiments

            double eventProbability = 0.5;
            ulong numberOfExperiments = 3;
            double[] expectedResults = { 1.0, .875, .5, .125 };

            for (ulong n = 0; n <= numberOfExperiments; ++n)
            {
                double actualResult = StatisticsMath.BinomialAtLeast(eventProbability, n, numberOfExperiments);
                Assert.AreEqual(expectedResults[n], actualResult, 0.001);
            }
        }

        [TestMethod]
        public void BinomialAtLeastImpossibleCondition()
        {
            double eventProbability = 0.5;
            ulong numberOfExperiments = 3;
            ulong desiredNumberOfSuccesses = 4;

            double expectedResult = 0.0;
            double actualResult = StatisticsMath.BinomialAtLeast(eventProbability, desiredNumberOfSuccesses, numberOfExperiments);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void BinomialAtLeastZeroProbability()
        {
            double eventProbability = 0.0;
            ulong numberOfExperiments = 3;
            ulong desiredNumberOfSuccesses = 0;

            double expectedResult = 1.0;
            double actualResult = StatisticsMath.BinomialAtLeast(eventProbability, desiredNumberOfSuccesses, numberOfExperiments);
            Assert.AreEqual(expectedResult, actualResult);

            desiredNumberOfSuccesses = 1;
            expectedResult = 0.0;
            actualResult = StatisticsMath.BinomialAtLeast(eventProbability, desiredNumberOfSuccesses, numberOfExperiments);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void BinomialAtLeastCertainProbability()
        {
            // For any number K of events, where K <= N, there is 100% chance that at least K events will happen

            double eventProbability = 1.0;
            ulong numberOfExperiments = 3;
            double expectedResult = 1.0;

            for (ulong k = 0; k <= numberOfExperiments; ++k)
            {
                double actualResult = StatisticsMath.BinomialAtLeast(eventProbability, k, numberOfExperiments);
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [TestMethod]
        public void BinomialAtLeastProbabilityNegative()
        {
            try
            {
                _ = StatisticsMath.BinomialAtLeast(-0.5, 1, 1);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void BinomialAtLeastProbabilityGreaterThanOne()
        {
            try
            {
                _ = StatisticsMath.BinomialAtLeast(1.5, 1, 1);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return;
            }
            Assert.Fail();
        }
    }
}
