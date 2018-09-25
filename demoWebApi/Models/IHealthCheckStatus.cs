using System;

namespace demoWebApi.Models
{
    /// <summary>
    ///     Interface for all health check status data.
    /// </summary>
    public interface IHealthCheckStatus
    {
        /// <summary>
        ///     Gets the alive seconds.
        /// </summary>
        /// <value>The alive seconds.</value>
        int AliveSeconds { get; }

        /// <summary>
        ///     Gets the requests processed.
        /// </summary>
        /// <value>The requests processed.</value>
        int RequestsProcessed { get; }

        /// <summary>
        ///     Gets the start time.
        /// </summary>
        /// <value>The start time.</value>
        DateTime StartTime { get; }

        /// <summary>
        ///     Increments the requests processed.
        /// </summary>
        void IncrementRequestsProcessed();

        /// <summary>
        ///     Updates the alive seconds.
        /// </summary>
        void UpdateAliveSeconds();
    }
}
