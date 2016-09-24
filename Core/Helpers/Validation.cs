// ***********************************************************************
// Assembly         : FenrirFS
// Component        : Validation.cs
// Author           : vonderborch
// Created          : 09-22-2016
// 
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="Validation.cs" company="">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the Validation class.
// </summary>
//
// Changelog: 
//            - 2.0.0 (09-24-2016) - Beta version.
//            - 2.0.0 (09-22-2016) - Initial version created.
// ***********************************************************************
using System;

namespace FenrirFS.Helpers
{
    /// <summary>
    /// Provides static functions to help with validating parameters.
    /// </summary>
    public static class Validation
    {
        #region Public Methods

        /// <summary>
        /// Verifies that the value is not null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Fires if the value is null.</exception>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static void NotNullCheck<T>(T value, string parameterName) where T : class
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// Verifies that the string value is not null or empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentException">Fires if the value is null or empty.</exception>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static void NotNullOrEmptyCheck(string value, string parameterName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException($"{parameterName} can not be null or empty.");
        }

        /// <summary>
        /// Verifies that the string value is not null, empty, or whitespace.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentException">Fires if the value is null, empty, or whitespace.</exception>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static void NotNullOrWhiteSpaceCheck(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{parameterName} can not be null, empty, or contain only white space.");
        }

        #endregion Public Methods
    }
}