using System;

namespace demoWebApi.Models
{
    /// <summary>
    ///     Class for all health check status data.
    /// </summary>
    /// <seealso cref="IHealthCheckStatus" />
    public class HealthCheckStatus : IHealthCheckStatus
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HealthCheckStatus" /> class.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        public HealthCheckStatus(DateTime startTime)
        {
            StartTime = startTime;
        }

        /// <summary>
        ///     Gets the alive seconds.
        /// </summary>
        /// <value>The alive seconds.</value>
        public int AliveSeconds { get; private set; }

        /// <summary>
        ///     Gets the requests processed.
        /// </summary>
        /// <value>The requests processed.</value>
        public int RequestsProcessed { get; private set; }

        /// <summary>
        ///     Gets the start time.
        /// </summary>
        /// <value>The start time.</value>
        public DateTime StartTime { get; private set; }

        /// <summary>
        ///     Increments the requests processed.
        /// </summary>
        // TODO: This may need to be made thread safe.
        public void IncrementRequestsProcessed() => RequestsProcessed++;

        /// <summary>
        ///     Updates the alive seconds.
        /// </summary>
        // TODO: This may need to be made thread safe.
        public void UpdateAliveSeconds() => AliveSeconds = Convert.ToInt32((DateTime.UtcNow - StartTime).TotalSeconds);
    }
}
