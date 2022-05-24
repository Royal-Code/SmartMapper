﻿
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configurations.Adapters;

public interface IAdapterOptionsBuilder
{
    IAdapterOptionsBuilder Configure<TSource, TTarget>(Action<IAdapterOptionsBuilder<TSource, TTarget>> configure);

    IAdapterOptionsBuilder<TSource, TTarget> Configure<TSource, TTarget>();
}

public interface IAdapterOptionsBuilder<TSource, TTarget>
{
    IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(Expression<Func<TSource, TProperty>> propertySelection);

    IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(string propertyName);
}

public static class ConfigureSample
{
    public static void Configure(IAdapterOptionsBuilder builder)
    {
        builder.Configure<MyDto, MyEntity>(b =>
        {
            b.Map(d => d.Id).To(e => e.Id);
        });
    }

    public class MyEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    public class MyDto
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}