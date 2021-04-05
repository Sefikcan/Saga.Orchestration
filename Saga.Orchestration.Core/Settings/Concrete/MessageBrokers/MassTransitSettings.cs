using Saga.Orchestration.Core.Settings.Abstract;

namespace Saga.Orchestration.Core.Settings.Concrete.MessageBrokers
{
    /// <summary>
    /// 
    /// </summary>
    public class MassTransitSettings : ISettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? TripThreshold { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ActiveThreshold { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ResetInterval { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? RateLimit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? RateLimiterInterval { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? TrackingPeriod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? MessageRetryCount { get; set; }
    }
}
