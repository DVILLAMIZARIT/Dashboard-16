using System;
using Castle.MicroKernel.Registration;
using DAL;
using DAL.Interfaces;

namespace WebUI.Infrastructure.Installers
{
    public class ContextInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {

            String connectionString = "DefaultConnection";
            var appHarborConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLSERVER_CONNECTION_STRING"];
            if (appHarborConnectionString != null)
            {
                connectionString = appHarborConnectionString.ConnectionString;
            }

            container.Register(
                Component.For<IDataContextFactory>().ImplementedBy<DataContextFactory>().DependsOn(Dependency.OnValue("nameOrConnectionString", connectionString)).LifestyleSingleton()
            );
        }
    }
}