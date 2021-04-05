using Mapster;
using Saga.Orchestration.Core.Mappings.Abstract;

namespace Saga.Orchestration.Core.Mappings.Concrete.Mapster
{
    /// <summary>
    /// 
    /// </summary>
    public class MapsterMapping : IMapping
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return source.Adapt<TDestination>();
        }
    }
}
