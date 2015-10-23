using System;

namespace FenrirFS
{
    /// <summary>
    /// Various exception helper functions.
    /// </summary>
    public static class Exceptions
    {
        #region Public Methods

        /// <summary>
        /// Generates a 'Can't Generate Unique Name' exception.
        /// </summary>
        /// <param name="iterations">The number of attempts.</param>
        /// <returns>Returns a 'Can't Generate Unique Name' exception.</returns>
        public static Exception CanNotGenerateUniqueNameException(int iterations)
        {
            return new Exception(string.Format("Can't generate a unique name in {0} attempts.", iterations));
        }

        /// <summary>
        /// Generates a 'Not Implemented in Current File-system' exception.
        /// </summary>
        /// <returns>Returns a 'Not Implemented in Current File-system' exception.</returns>
        public static Exception NotImplementedInCurrentFileSystemException()
        {
            return new NotImplementedException("This functionality is not implemented in the current file-system.");
        }

        /// <summary>
        /// Checks if a parameter is a null value, and throws if it is.
        /// </summary>
        /// <typeparam name="T">The object to check.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>Returns the value, or throws a null argument exception.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the value is null.</exception>
        public static T NotNullException<T>(T value, string parameter) where T : class
        {
            if (value == null)
                throw new ArgumentNullException(parameter);

            return value;
        }

        /// <summary>
        /// Checks if a parameter is a null value, and throws if it is.
        /// </summary>
        /// <typeparam name="T">The object to check.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>Returns the value, or throws a null argument exception.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the value is null.</exception>
        public static bool NotNullCheck<T>(T value, string parameter) where T : class
        {
            if (value == null)
                throw new ArgumentNullException(parameter);

            return true;
        }

        /// <summary>
        /// Checks if a string value is null or empty, and throws if it is.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>Returns the true, or throws a null argument exception.</returns>
        /// <exception cref="ArgumentException">Thrown if the string value is null or empty.</exception>
        public static bool NotNullOrEmptyCheck(string value, string parameter)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(String.Format("{0} can not be a null or empty string, or contain only whitespace", parameter));
            }

            return true;
        }

        /// <summary>
        /// Checks if a string value is null or empty, and throws if it is.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>Returns the value, or throws a null argument exception.</returns>
        /// <exception cref="ArgumentException">Thrown if the string value is null or empty.</exception>
        public static string NotNullOrEmptyException(string value, string parameter)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(String.Format("{0} can not be a null or empty string, or contain only whitespace", parameter));
            }

            return value;
        }

        #endregion Public Methods
    }
}