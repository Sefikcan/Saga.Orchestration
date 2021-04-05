using Saga.Orchestration.Core.Settings.Abstract;

namespace Saga.Orchestration.Core.Settings.Concrete.Databases
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderDbSettings : ISettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string ConnectionStrings { get; set; }
    }
}
