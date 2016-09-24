// ***********************************************************************
// Assembly         : FenrirFS
// Component        : Tasks.cs
// Author           : vonderborch
// Created          : 07-13-2016
// 
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="Tasks.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      A collection of helper functions for tasks.
// </summary>
//
// Changelog: 
//            - 2.0.0 (09-24-2016) - Beta version.
//            - 2.0.0 (07-13-2016)- Removed VerifyCancellationToken function.
//            - 2.0.0 (07-13-2016) - Initial version created.
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS.Helpers
{
    /// <summary>
    /// Provides static functions to help with threading.
    /// </summary>
    public static class Tasks
    {
        #region Public Methods

        /// <summary>
        /// Schedules the task to run asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The scheduled task awaiter.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static Awaiter ScheduleTask(CancellationToken? cancellationToken)
        {
            // Check if a cancellation has been requested, and error out if one has.
            if (cancellationToken != null)
            {
                CancellationToken token = (CancellationToken)cancellationToken;
                token.ThrowIfCancellationRequested();
            }

            // if possible, get a new scheduler
            TaskScheduler scheduler = SynchronizationContext.Current != null
                                        ? TaskScheduler.Default
                                        : null;

            // return the task scheduler awaiter
            return new Awaiter(scheduler, cancellationToken);
        }

        #endregion Public Methods
    }
}