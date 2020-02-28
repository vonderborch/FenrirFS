// ***********************************************************************
// Assembly         : FenrirFS
// Component        : AsyncHelper.cs
// Author           : Christian Webber
// Created          : 2016-07-13
//
// Version          : 3.0.0
// Last Modified By : Christian Webber
// Last Modified On : 2020-02-27
// ***********************************************************************
// <copyright file="AsyncHelper.cs">
//     Copyright © 2020
// </copyright>
// <summary>
//     A collection of helper functions for asynchronous methods.
// </summary>
//
// Changelog:
//            - 3.0.0 (2020-02-27) - Re-organized and renamed.
//            - 2.0.0 (2016-09-24) - Beta version.
//            - 2.0.0 (2016-07-13) - Removed VerifyCancellationToken function.
//            - 2.0.0 (2016-07-13) - Initial version created.
// ***********************************************************************

using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS.Helpers
{
    /// <summary>
    /// Provides static functions to help with threading.
    /// </summary>
    public static class AsyncHelper
    {
        /// <summary>
        /// Schedules the task to run asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The scheduled task awaiter</returns>
        public static Awaiter ScheduleTask(CancellationToken? cancellationToken)
        {
            // Check if a cancellation has been requested, and error out if one has.
            if (cancellationToken != null)
            {
                var token = (CancellationToken)cancellationToken;
                token.ThrowIfCancellationRequested();
            }

            // if possible, get a new scheduler
            var scheduler = SynchronizationContext.Current != null
                ? TaskScheduler.Default
                : null;

            // return the task scheduler awaiter
            return new Awaiter(scheduler, cancellationToken);
        }
    }
}
