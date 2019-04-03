using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Backend.Chain;
using Backend.Services.InventoryService;
using Backend.Services.RentService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NServiceBus;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend
{
    public class Program
    {

        static async Task Main(string[] args)
        {     
            var provider = ConfigureServices();
            var dataContext = provider.GetService<DataContext>();
            await dataContext.Database.MigrateAsync();
            
            Console.ReadKey();        
        }

        public static AutofacServiceProvider ConfigureServices()
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var builder = new ContainerBuilder();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                .As<Profile>();

            builder.Register(m => new MapperConfiguration(cfg =>
            {
                foreach (var profile in m.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>()
            .CreateMapper(c.Resolve))
            .As<IMapper>()
            .InstancePerLifetimeScope();

            var options = new DbContextOptionsBuilder<DataContext>().UseSqlServer(config.GetConnectionString("DefaultConnection")).Options;
            builder.Register(b => new DataContext(options));          
            

            builder.RegisterType<RentService>().As<IRentService>().InstancePerLifetimeScope();
            builder.Register(c => new RentHandler()).OnActivated(e => e.Instance.rentService = e.Context.Resolve<IRentService>());
           
            builder.RegisterType<InvetoryService>().As<IInvetoryService>().InstancePerLifetimeScope();
            builder.Register(c => new InventoryHandler()).OnActivated(e => e.Instance.inventoryService = e.Context.Resolve<IInvetoryService>());

            builder.RegisterType<InvoiceService>().As<IInvoiceService>().InstancePerLifetimeScope();
            builder.Register(c => new InvoiceHandler()).OnActivated(e => e.Instance.InvoiceService = e.Context.Resolve<IInvoiceService>());

            IEndpointInstance endpoint = null;
            builder.Register(x => endpoint)
                .As<IEndpointInstance>()
                .SingleInstance();

            var container = builder.Build();

            var endpointConfiguration = new EndpointConfiguration("Backend.Endpoint");
            endpointConfiguration.UseTransport<LearningTransport>();
            endpointConfiguration.UseContainer<AutofacBuilder>(
            customizations: customizations =>
            {
                customizations.ExistingLifetimeScope(container);
            });   

            Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

            return new AutofacServiceProvider(container);         

        }
    }   
}
