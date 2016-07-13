using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS
{
    public struct Awaiter : INotifyCompletion
    {
        private CancellationToken cancellationToken;
        private TaskScheduler scheduler;
        private TaskCreationOptions taskCreationOptions;

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

        internal Awaiter(TaskScheduler scheduler, CancellationToken cancellationToken, TaskCreationOptions taskCreationOptions = TaskCreationOptions.None)
        {
            this.scheduler = scheduler;
            this.cancellationToken = cancellationToken;
            this.taskCreationOptions = taskCreationOptions;
        }

        public bool IsCompleted
        {
            get { return this.scheduler == null; }
        }

        public Awaiter GetAwaiter()
        {
            return this;
        }

        public void GetResult()
        {
            this.cancellationToken.ThrowIfCancellationRequested();
        }

        public void OnCompleted(Action continuation)
        {
            if (this.scheduler == null)
                throw new InvalidOperationException("");

            Task.Factory.StartNew(continuation, CancellationToken.None, TaskCreationOptions.None, this.scheduler);
        }
    }
}
