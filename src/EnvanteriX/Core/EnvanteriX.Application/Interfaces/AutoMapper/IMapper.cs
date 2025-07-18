﻿using AutoMapper;

namespace EnvanteriX.Application.Interfaces.AutoMapper
{
    public interface IMapper
    {
        TDestination Map<TDestination, TSource>(TSource source, string[]? ignore = null, Action<IMapperConfigurationExpression>? config = null);
        IList<TDestination> Map<TDestination, TSource>(IList<TSource> source, string[]? ignore = null, Action<IMapperConfigurationExpression>? config = null);
        TDestination Map<TDestination>(object source, string[]? ignore = null, Action<IMapperConfigurationExpression>? config = null);
        IList<TDestination> Map<TDestination>(IList<object> source, string[]? ignore = null, Action<IMapperConfigurationExpression>? config = null);
    }
}
