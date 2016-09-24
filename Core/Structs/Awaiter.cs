// ***********************************************************************
// Assembly         : FenrirFS
// Component        : Awaiter.cs
// Author           : vonderborch
// Created          : 09-22-2016
// 
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="Awaiter.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the awaiter structure.
// </summary>
//
// Changelog: 
//            - 2.0.0 (09-24-2016) - Beta version.
// ***********************************************************************
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS
{
    /// <summary>
    /// Allows a function to wait for the scheduler to pick it up (allowing the function to work asynchronously)
    /// </summary>
    /// <seealso cref="System.Runtime.CompilerServices.INotifyCompletion" />
    public struct Awaiter : INotifyCompletion
    {
        #region Private Fields

        /// <summary>
        /// The cancellation token
        /// </summary>
        private CancellationToken cancellationToken;
        /// <summary>
        /// The scheduler
        /// </summary>
        private TaskScheduler scheduler;
        /// <summary>
        /// The task creation options
        /// </summary>
        private TaskCreationOptions taskCreationOptions;

        #endregion Private Fields

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Awaiter"/> struct.
        /// </summary>
        /// <param name="scheduler">The scheduler.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="taskCreationOptions">The task creation options.</param>
        internal Awaiter(TaskScheduler scheduler, CancellationToken? cancellationToken, TaskCreationOptions taskCreationOptions = TaskCreationOptions.None)
        {
            this.scheduler = scheduler;
            switch (cancellationToken == null)
            {
                case true:
                    this.cancellationToken = new CancellationToken();
                    break;

                case false:
                    this.cancellationToken = (CancellationToken)cancellationToken;
                    break;
            }
            this.taskCreationOptions = taskCreationOptions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Awaiter"/> struct.
        /// </summary>
        /// <param name="scheduler">The scheduler.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="taskCreationOptions">The task creation options.</param>
        internal Awaiter(TaskScheduler scheduler, CancellationToken cancellationToken, TaskCreationOptions taskCreationOptions = TaskCreationOptions.None)
        {
            this.scheduler = scheduler;
            this.cancellationToken = cancellationToken;
            this.taskCreationOptions = taskCreationOptions;
        }

        #endregion Internal Constructors

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this instance is completed.
        /// </summary>
        /// <value><c>true</c> if this instance is completed; otherwise, <c>false</c>.</value>
        public bool IsCompleted
        {
            get { return this.scheduler == null; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Gets the instance of the awaiter.
        /// </summary>
        /// <returns>The awaiter instance.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public Awaiter GetAwaiter()
        {
            return this;
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public void GetResult()
        {
            this.cancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>
        /// Schedules the continuation action that's invoked when the instance completes.
        /// </summary>
        /// <param name="continuation">The action to invoke when the operation completes.</param>
        /// <exception cref="InvalidOperationException">Fires if the scheduler is null.</exception>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public void OnCompleted(Action continuation)
        {
            if (this.scheduler == null)
                throw new InvalidOperationException("Invalid scheduler!");

            Task.Factory.StartNew(continuation, CancellationToken.None, TaskCreationOptions.None, this.scheduler);
        }

        #endregion Public Methods
    }
}