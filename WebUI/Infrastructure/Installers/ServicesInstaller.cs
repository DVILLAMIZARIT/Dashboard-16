using Castle.MicroKernel.Registration;
using DAL.Repositories;
using Infra.Interfaces.DAL;
using Infra.Interfaces.Services;
using Services;

namespace WebUI.Infrastructure.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Component.For<IMembershipService>().ImplementedBy<MembershipService>().LifestyleSingleton()
            );

            container.Register(
                Component.For<IUserProfileRepository>().ImplementedBy<UserProfileRepository>().LifestyleSingleton()
            );
            container.Register(
                Component.For<IUserProfileService>().ImplementedBy<UserProfileService>().LifestyleSingleton()
            );
        }
    }
}