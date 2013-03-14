using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Codell.Pies.Common.Extensions;

namespace Codell.Pies.Common.Mapping
{
    public class AutoMapperEngine : IMappingEngine
    {
        public static IMappingEngine Configure(IAutoMapperConfiguration configuration)
        {
            configuration.Configure();
            MapCustomTypeConverters();
            return new AutoMapperEngine(Mapper.Engine);
        }

        private static void MapCustomTypeConverters()
        {
            var converters = AppDomain.CurrentDomain.GetProjectTypesImplementing(typeof (ITypeConverter<,>));
            foreach (var converter in converters)
            {
                var conversions = converter.GetInterfaces(typeof(ITypeConverter<,>));
                foreach (var args in conversions.Select(conversion => conversion.GetGenericArguments().ToArray()))
                {
                    Mapper.CreateMap(args[0], args[1]).ConvertUsing(converter);
                }
            }
        }

        private readonly IMappingEngine _inner;

        public AutoMapperEngine(IMappingEngine inner)
        {
            Verify.NotNull(inner, "inner");
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public TDestination Map<TDestination>(object source)
        {
            return _inner.Map<TDestination>(source);
        }

        public TDestination Map<TDestination>(object source, Action<IMappingOperationOptions> opts)
        {
            return _inner.Map<TDestination>(source, opts);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            EnsureMapped<TSource, TDestination>();
            return _inner.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions> opts)
        {
            EnsureMapped<TSource, TDestination>();
            return _inner.Map<TSource, TDestination>(source, opts);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            EnsureMapped<TSource, TDestination>();
            return _inner.Map(source, destination);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions> opts)
        {
            EnsureMapped<TSource, TDestination>();
            return _inner.Map(source, destination, opts);
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return _inner.Map(source, sourceType, destinationType);
        }

        public object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
        {
            return _inner.Map(source, sourceType, destinationType, opts);
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            return _inner.Map(source, destination, sourceType, destinationType);
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
        {
            return _inner.Map(source, destination, sourceType, destinationType, opts);
        }

        public TDestination DynamicMap<TSource, TDestination>(TSource source)
        {
            return _inner.DynamicMap<TSource, TDestination>(source);
        }

        public TDestination DynamicMap<TDestination>(object source)
        {
            return _inner.DynamicMap<TDestination>(source);
        }

        public object DynamicMap(object source, Type sourceType, Type destinationType)
        {
            return _inner.DynamicMap(source, sourceType, destinationType);
        }

        public void DynamicMap<TSource, TDestination>(TSource source, TDestination destination)
        {
            _inner.DynamicMap(source, destination);
        }

        public void DynamicMap(object source, object destination, Type sourceType, Type destinationType)
        {
            _inner.DynamicMap(source, destination, sourceType, destinationType);
        }

        public Expression<Func<TSource, TDestination>> CreateMapExpression<TSource, TDestination>()
        {
            return _inner.CreateMapExpression<TSource, TDestination>();
        }

        private void EnsureMapped<TSource, TDestination>()
        {
            var source = ResolveType(typeof(TSource));
            var destination = ResolveType(typeof(TDestination));
            if (!HasMapFor(source, destination))
                Mapper.CreateMap(source, destination);
        }

        private Type ResolveType(Type type)
        {
            return IsEnumerable(type) ? type.GetGenericArguments()[0] : type;
        }

        private bool IsEnumerable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        private bool HasMapFor(Type source, Type destination)
        {
            return GetTypeMapFor(source, destination) != null;
        }

        private TypeMap GetTypeMapFor(Type source, Type destination)
        {
            return Mapper.FindTypeMapFor(source, destination);
        }
    }
}