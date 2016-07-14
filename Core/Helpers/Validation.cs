using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FenrirFS.Helpers
{
    public static class Validation
    {
        public static void NotNullCheck<T>(T value, string parameterName) where T : class
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
        }

        public static void NotNullOrEmptyCheck(string value, string parameterName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException($"{parameterName} can not be null or empty.");
        }

        public static void NotNullOrWhiteSpaceCheck(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{parameterName} can not be null, empty, or contain only white space.");
        }
    }
}
