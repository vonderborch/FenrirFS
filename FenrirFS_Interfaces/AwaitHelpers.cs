﻿/*
 * This file is subject to the terms and conditions defined in the
 * license.txt file, which is part of this source code package.
 */

using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS
{
    /// <summary>
    /// Various helpers for asynchronous functions.
    /// </summary>
    public static class AwaitHelpers
    {
        #region Public Methods

        /// <summary>
        /// Checks the cancellation token and returns a new token if the supplied value is null.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to check. Defaults to null.</param>
        /// <returns>A cancellation token</returns>
        public static CancellationToken CheckCancellationToken(CancellationToken? cancellationToken = null)
        {
            return cancellationToken == null
                ? new CancellationToken()
                : (CancellationToken)cancellationToken;
        }

        /// <summary>
        /// When called in an async function, will return a valid await for a new task.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A new Awaiter.</returns>
        public static Awaiter CreateTaskScheduler(CancellationToken cancellationToken)
        {
            // Check if a cancellation has been requested, and error out if one has.
            cancellationToken.ThrowIfCancellationRequested();

            // get a new task scheduler, if possible
            TaskScheduler scheduler = SynchronizationContext.Current != null
                                        ? TaskScheduler.Default
                                        : null;

            // return the task scheduler awaiter
            return new Awaiter(scheduler, cancellationToken);
        }

        #endregion Public Methods
    }
}
