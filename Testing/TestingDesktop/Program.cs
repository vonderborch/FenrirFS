/*
 * This file is subject to the terms and conditions defined in the
 * license.txt file, which is part of this source code package.
 */

using System;
using System.Collections.Generic;
using TestingCore;

namespace TestingDesktop
{
    internal class Program
    {
        #region Private Delegates

        /// <summary>
        /// The test suite delegate.
        /// </summary>
        private delegate void TestSuite();

        #endregion Private Delegates

        #region Private Methods

        /// <summary>
        /// The desktop test program.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            // setup the tests...
            var tests = new Dictionary<string, TestSuite>();
            tests.Add("Basic Suite", BasicFileTests.TestSuite);

            // begin the tests...
            Console.WriteLine("Beginning tests...");
            Console.WriteLine("--------------------------------------------------");

            // run the tests...
            foreach (var test in tests)
            {
                Console.WriteLine(String.Format("\nTest: {0}", test.Key));
                try
                {
                    test.Value.Invoke();
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Passed")
                        Console.WriteLine("Passed");
                    else
                        Console.WriteLine("Failed");
                }
            }

            // end the tests...
            Console.WriteLine("\n--------------------------------------------------");
            Console.WriteLine("Testing Complete!");
            Console.ReadKey();
        }

        #endregion Private Methods
    }
}