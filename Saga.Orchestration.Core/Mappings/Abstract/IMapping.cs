namespace Saga.Orchestration.Core.Mappings.Abstract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMapping
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
