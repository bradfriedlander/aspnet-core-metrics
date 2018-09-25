using System;

namespace demoWebApi.Models
{
    public class HealthCheckStatus
    {
        public HealthCheckStatus(DateTime startTime)
        {
            StartTime = startTime;
        }
        public int AliveSeconds { get; set; }

        public int RequestsProcessed { get; set; }

        public DateTime StartTime { get; set; }
    }
}
