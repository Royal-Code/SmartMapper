using System.Linq.Expressions;
using System.Reflection;
using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class PropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> 
    : IPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> 
{
    private readonly ToPropertyResolutionOptions propertyResolutionOptions;

    /// <summary>
    /// Creates a new instance of <see cref="PropertyToPropertyOptionsBuilder{TSource, TTarget, TProperty, TTargetProperty}"/>.
    /// </summary>
    /// <param name="propertyResolutionOptions">The property resolution options.</param>
    public PropertyToPropertyOptionsBuilder(ToPropertyResolutionOptions propertyResolutionOptions)
    {
        this.propertyResolutionOptions = propertyResolutionOptions;
    }
    
    /// <inheritdoc />
    public IPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> CastValue()
    {
        var assigmentOptions = propertyResolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseCast();
        return this;
    }

    /// <inheritdoc />
    public IPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> UseConverter(
        Expression<Func<TProperty, TTargetProperty>> converter)
    {
        var assigmentOptions = propertyResolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseConverter(converter);
        return this;
    }

    /// <inheritdoc />
    public IPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> Adapt()
    {
        var assigmentOptions = propertyResolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseAdapt();
        return this;
    }

    /// <inheritdoc />
    public IPropertyToPropertyOptionsBuilder<TSource, TTarget, TProperty, TTargetProperty> Select()
    {
        var assigmentOptions = propertyResolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseSelect();
        return this;
    }

    /// <inheritdoc />
    public IPropertyThenToOptionsBuilder<TProperty, TTargetProperty, TNextProperty> ThenTo<TNextProperty>(
        Expression<Func<TTargetProperty, TNextProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));
        
        var thenToPropertyOptions = propertyResolutionOptions.ThenTo(propertyInfo);
        var builder = new PropertyThenToOptionsBuilder<TProperty, TTargetProperty, TNextProperty>(thenToPropertyOptions);
        return builder;
    }

    /// <inheritdoc />
    public IPropertyThenOptionsBuilder<TProperty, TTargetProperty> Then()
    {
        return new InternalPropertyThenOptionsBuilder<TProperty, TTargetProperty>(propertyResolutionOptions);
    }

    /// <inheritdoc />
    private sealed class InternalPropertyThenOptionsBuilder<TTProperty, TTTargetProperty>
        : IPropertyThenOptionsBuilder<TTProperty, TTTargetProperty>
    {
        private readonly ToPropertyResolutionOptions propertyResolutionOptions;

        /// <summary>
        /// Crea
        /// </summary>
        /// <param name="propertyResolutionOptions"></param>
        public InternalPropertyThenOptionsBuilder(
            ToPropertyResolutionOptions propertyResolutionOptions)
        {
            this.propertyResolutionOptions = propertyResolutionOptions;
        }

        /// <inheritdoc />
        public IPropertyThenToOptionsBuilder<TTProperty, TTTargetProperty, TNextProperty> To<TNextProperty>(
            Expression<Func<TTTargetProperty, TNextProperty>> propertySelector)
        {
            if (!propertySelector.TryGetMember(out var member))
                throw new InvalidPropertySelectorException(nameof(propertySelector));

            if (member is not PropertyInfo propertyInfo)
                throw new InvalidPropertySelectorException(nameof(propertySelector));

            var thenToPropertyOptions = propertyResolutionOptions.ThenTo(propertyInfo);
            var builder = new PropertyThenToOptionsBuilder<TTProperty, TTTargetProperty, TNextProperty>(thenToPropertyOptions);
            return builder;
        }

        /// <inheritdoc />
        public IPropertyThenToOptionsBuilder<TTProperty, TTTargetProperty, TNextProperty> To<TNextProperty>(
            string propertyName)
        {
            // get target property by name, including inherited type properties
            var propertyInfo = typeof(TTargetProperty).GetRuntimeProperty(propertyName);

            // check if property exists
            if (propertyInfo is null)
                throw new InvalidPropertyNameException(
                    $"The type '{typeof(TTTargetProperty).Name}' does not have a property with name '{propertyName}'.",
                    nameof(propertyName));

            // validate the property type
            if (propertyInfo.PropertyType != typeof(TNextProperty))
                throw new InvalidPropertyTypeException(
                    $"Property '{propertyName}' on type '{typeof(TTTargetProperty).Name}' " +
                    $"is not of type '{typeof(TNextProperty).Name}', " +
                    $"but of type '{propertyInfo.PropertyType.Name}'.",
                    nameof(propertyName));

            var thenToPropertyOptions = propertyResolutionOptions.ThenTo(propertyInfo);
            var builder = new PropertyThenToOptionsBuilder<TTProperty, TTTargetProperty, TNextProperty>(thenToPropertyOptions);
            return builder;
        }

        /// <inheritdoc />
        public IPropertyToMethodOptionsBuilder<TTTargetProperty, TTProperty> ToMethod()
        {
            ThenToMethodOptions options = propertyResolutionOptions.ThenCall();
            var builder = new PropertyThenToMethodOptionsBuilder<TTTargetProperty, TTProperty>(options);
            return builder;
        }

        /// <inheritdoc />
        public IPropertyToMethodOptionsBuilder<TTTargetProperty, TTProperty> ToMethod(
            Expression<Func<TTTargetProperty, Delegate>> methodSelector)
        {
            if (!methodSelector.TryGetMethod(out var method))
                throw new InvalidMethodDelegateException(nameof(methodSelector));

            ThenToMethodOptions options = propertyResolutionOptions.ThenCall();

            options.MethodOptions.Method = method;
            options.MethodOptions.MethodName = method.Name;

            var builder = new PropertyThenToMethodOptionsBuilder<TTTargetProperty, TTProperty>(options);
            return builder;
        }
    }
}

