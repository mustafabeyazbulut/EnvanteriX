using AutoMapper;
using AutoMapper.Internal;

namespace EnvanteriX.Infrastructure.AutoMapper
{
    public class Mapper : EnvanteriX.Application.Interfaces.AutoMapper.IMapper
    {
        public static List<TypePair> typePairs = new();
        private IMapper MapperContainer;

        public TDestination Map<TDestination, TSource>(TSource source, string[]? ignore = null, Action<IMapperConfigurationExpression>? config = null)
        {
            Config<TDestination, TSource>(5, ignore, config);
            return MapperContainer.Map<TSource, TDestination>(source);
        }

        public IList<TDestination> Map<TDestination, TSource>(IList<TSource> source, string[]? ignore = null, Action<IMapperConfigurationExpression>? config = null)
        {
            Config<TDestination, TSource>(5, ignore, config);
            return MapperContainer.Map<IList<TSource>, IList<TDestination>>(source);
        }

        public TDestination Map<TDestination>(object source, string[]? ignore = null, Action<IMapperConfigurationExpression>? config = null)
        {
            Config<TDestination, object>(5, ignore, config);
            return MapperContainer.Map<TDestination>(source);
        }

        public IList<TDestination> Map<TDestination>(IList<object> source, string[]? ignore = null, Action<IMapperConfigurationExpression>? config = null)
        {
            Config<TDestination, IList<object>>(5, ignore, config);
            return MapperContainer.Map<IList<TDestination>>(source);
        }

        protected void Config<TDestination, TSoruce>(int depth = 5, string[]? ignore = null, Action<IMapperConfigurationExpression>? customConfig = null)
        {
            var typePair = new TypePair(typeof(TSoruce), typeof(TDestination));

            if (typePairs.Any(a => a.DestinationType == typePair.DestinationType && a.SourceType == typePair.SourceType) && (ignore == null || ignore.Length == 0) && customConfig == null)
                return;

            typePairs.Add(typePair);

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var item in typePairs)
                {
                    var map = cfg.CreateMap(item.SourceType, item.DestinationType).MaxDepth(depth);

                    // Gelen ignore alanlarını tek tek Ignore() yap
                    if (ignore is not null)
                    {
                        foreach (var prop in ignore)
                        {
                            map.ForMember(prop, x => x.Ignore());
                        }
                    }

                    map.ReverseMap();
                }

                customConfig?.Invoke(cfg);
            });

            MapperContainer = config.CreateMapper();
        }

    }
}
