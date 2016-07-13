// ***********************************************************************
// Assembly         : FenrirFS
// Component        : Tasks.cs
// Author           : vonderborch
// Created          : 07-13-2016
// 
// Version          : 1.0.0
// Last Modified By : vonderborch
// Last Modified On : 07-13-2016
// ***********************************************************************
// <copyright file="Tasks.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      A collection of helper functions for tasks.
// </summary>
//
// Changelog: 
//            - 1.0.1 (07-13-2016)- Removed VerifyCancellationToken function.
//            - 1.0.0 (07-13-2016) - Initial version created.
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS.Helpers
{
    /// <summary>
    /// Class Tasks.
    /// </summary>
    public static class Tasks
    {
        #region Public Methods

        /// <summary>
        /// Schedules the task.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Awaiter.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
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