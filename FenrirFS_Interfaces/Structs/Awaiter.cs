using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS
{
    /// <summary>
    /// An awaiter for asynchronous functions.
    /// </summary>
    public struct Awaiter : INotifyCompletion
    {
        #region Private Fields

        /// <summary>
        /// The cancellation token
        /// </summary>
        private CancellationToken cancellationToken;

        /// <summary>
        /// The task scheduler
        /// </summary>
        private TaskScheduler scheduler;

        #endregion Private Fields

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Awaiter"/> struct.
        /// </summary>
        /// <param name="scheduler">The scheduler.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        internal Awaiter(TaskScheduler scheduler, CancellationToken cancellationToken)
        {
            this.scheduler = scheduler;
            this.cancellationToken = cancellationToken;
        }

        #endregion Internal Constructors

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether the scheduler is completed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is completed; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompleted
        {
            get { return this.scheduler == null; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Gets the awaiter for the task.
        /// </summary>
        /// <returns>Returns the awaiter.</returns>
        public Awaiter GetAwaiter()
        {
            return this;
        }

        /// <summary>
        /// Gets the result for the task.
        /// </summary>
        public void GetResult()
        {
            this.cancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>
        /// Called when the task is completed.
        /// </summary>
        /// <param name="continuation">The continuation action.</param>
        /// <exception cref="InvalidOperationException">IsCompleted is true, so this is unexpected.</exception>
        public void OnCompleted(Action continuation)
        {
            if (this.scheduler == null)
                throw new InvalidOperationException("Error: scheduler is null!");

            Task.Factory.StartNew(continuation, CancellationToken.None, TaskCreationOptions.None, this.scheduler);
        }

        #endregion Public Methods
    }
}
