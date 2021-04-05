using Microsoft.OpenApi.Models;
using Saga.Orchestration.Core.Settings.Abstract;

namespace Saga.Orchestration.Core.Settings.Concrete
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerSettings : OpenApiInfo, ISettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string VersionName { get; set; } = "v1";

        /// <summary>
        /// 
        /// </summary>
        public string RoutePrefix { get; set; } = "";
    }
}
