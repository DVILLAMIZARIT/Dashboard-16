using System.Web.Mvc;
using Castle.MicroKernel.Registration;

// http://docs.castleproject.org/Windsor.Windsor-tutorial-part-three-writing-your-first-installer.ashx

// if you ever wanted to break controllers out in to other libraries
// see also http://geekswithblogs.net/aftabq/archive/2011/06/12/mvc-3-and-castle-windsor-a-modular-approach---part.aspx

namespace WebUI.Infrastructure.Installers
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient()
            );
        }
    }
}