// ***********************************************************************
// Assembly         : FenrirFS
// Component        : AsyncHelper.cs
// Author           : Christian Webber
// Created          : 2016-09-22
//
// Version          : 3.0.0
// Last Modified By : Christian Webber
// Last Modified On : 2020-02-27
// ***********************************************************************
// <copyright file="AsyncHelper.cs">
//     Copyright © 2020
// </copyright>
// <summary>
//      Defines the ParameterValidationHelper class.
// </summary>
//
// Changelog:
//            - 3.0.0 (2020-02-27) - Re-organized and renamed.
//            - 2.0.0 (2016-09-24) - Beta version.
//            - 2.0.0 (2016-09-22) - Initial version created.
// ***********************************************************************

using System;

namespace FenrirFS.Helpers
{
    /// <summary>
    /// Provides static function to help with validating parameters
    /// </summary>
    public static class ParameterValidationHelper
    {
        /// <summary>
        /// Verifies that the provided value is not null. Raises an exception on a validation failure.
        /// </summary>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <param name="value">The value to check for being null</param>
        /// <param name="parameterName">The name of the parameter</param>
        public static void NotNullCheck<T>(T value, string parameterName) where T : class
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// Verifies that the provided string is not null or empty. Raises an exception on a validation failure.
        /// </summary>
        /// <param name="value">The string to check for being null</param>
        /// <param name="parameterName">The name of the parameter</param>
        public static void NotNullOrEmptyCheck(string value, string parameterName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException($"{parameterName} can not be null or empty.");
        }

        /// <summary>
        /// Verifies that the provided string is not null or whitespace. Raises an exception on a validation failure.
        /// </summary>
        /// <param name="value">The string to check for being null</param>
        /// <param name="parameterName">The name of the parameter</param>
        public static void NotNullOrWhiteSpaceCheck(string value, string parameterName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException($"{parameterName} can not be null or empty.");
        }
    }
}
