using System;

namespace Saga.Orchestration.Core.MessageBrokers.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class EventModel
    {
        /// <summary>
        /// ctor
        /// </summary>
        public EventModel()
        {
            CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid CorrelationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
