using Materiales_Hernandez.Services.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xomega.Framework;

namespace Materiales_Hernandez.Services.Rest
{
    public class AppServicesInit : AppInitializer
    {
        public override IServiceProvider ConfigureServices()
        {
            // Load native SQL Server types
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.RelativeSearchPath);

            IServiceCollection container = new ServiceCollection();

            // framework services configuration
            container.AddErrors();
            container.AddSingletonLookupCacheProvider();

            // app services configuration
            // NOTE: make sure to build the Xomega model project first for the code below to compile
            container.AddServices();
            container.AddControllers();
            container.AddScoped<LookupTableController>();
            container.AddLookupCacheLoaders();
            container.AddXmlResourceCacheLoader(typeof(Enumerations.Operators).Assembly, ".enumerations.xml", true);

            // TODO: configure container with other services as needed

            return container.BuildServiceProvider();
        }
    }
}
