using Saga.Orchestration.Core.Settings.Abstract;

namespace Saga.Orchestration.Core.Settings.Concrete.Monitoring
{
    /// <summary>
    /// Jaeger Settings
    /// </summary>
    public class JaegerSettings : ISettings
    {
        /// <summary>
        /// Application Name
        /// </summary>
        public string ApplicationName { get; set; }
    }
}
