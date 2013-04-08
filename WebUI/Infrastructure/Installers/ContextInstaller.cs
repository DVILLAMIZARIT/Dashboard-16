using Castle.MicroKernel.Registration;
using DAL;
using DAL.Interfaces;

namespace WebUI.Infrastructure.Installers
{
    public class ContextInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                //Component.For<IDataContextFactory>().Instance(new DataContextFactory("name=DefaultConnection")).LifestylePerWebRequest()
                Component.For<IDataContextFactory>().ImplementedBy<DataContextFactory>().DependsOn(Dependency.OnValue("nameOrConnectionString", "DefaultConnection")).LifestylePerWebRequest()
            );
        }
    }
}