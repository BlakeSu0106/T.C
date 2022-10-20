using System.Reflection;
using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Telligent.Core.Domain.Repositories;
using Telligent.Core.Infrastructure.Repositories;
using Telligent.Core.Infrastructure.Services;

namespace Telligent.Core.Infrastructure.IoC;

public static class AutofacExtension
{
    public static void RegisterAutoMappers(this ContainerBuilder builder)
    {
        builder.RegisterAutoMapper(false, AppDomain.CurrentDomain.GetAssemblies());
    }

    public static void RegisterAppServices(this ContainerBuilder builder)
    {
        var types = Assembly
            .GetEntryAssembly()
            ?.GetReferencedAssemblies()
            .Select(Assembly.Load)
            .SelectMany(x => x.DefinedTypes)
            .Where(type => !type.IsAbstract && !type.IsInterface && typeof(IAppService).IsAssignableFrom(type))
            .Select(typeInfo => typeInfo.AsType());

        if (types == null) return;

        foreach (var type in types) builder.RegisterType(type).AsSelf();
    }

    public static void RegisterAppServices(this ContainerBuilder builder, List<string> assemblies)
    {
        foreach (var type in
                 assemblies
                     .Select(assembly => Assembly.Load(assembly)
                         .DefinedTypes
                         .Where(type =>
                             !type.IsAbstract &&
                             !type.IsInterface &&
                             typeof(IAppService).IsAssignableFrom(type))
                         .Select(typeInfo => typeInfo.AsType()))
                     .SelectMany(types => types))
            builder.RegisterType(type).AsSelf();
    }

    public static void RegisterRepositories(this ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>));
        builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
    }
}