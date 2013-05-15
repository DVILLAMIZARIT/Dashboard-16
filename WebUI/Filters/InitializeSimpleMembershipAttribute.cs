using System;
using System.Data.Entity.Infrastructure;
using System.Data.EntityClient;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Core;
using DAL;
using DAL.Interfaces;
using Infra.Interfaces.Services;
using WebMatrix.WebData;

namespace WebUI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                new SchemaSynchonizer<DataContext>(() => HttpContext.Current.IsDebuggingEnabled).Execute(); //Database.SetInitializer<UsersContext>(null);

                try
                {
                    String connectionString = String.Empty;
                    String providerName = String.Empty;
                    String Administrator = "Administrator";

                    DataContext context = IoC.Resolve<IDataContextFactory>().Create();
                    IObjectContextAdapter contextAdapter = context as IObjectContextAdapter;
                    if (contextAdapter != null)
                    {
                        context.Database.CreateIfNotExists();

                        EntityConnection entityConnection = contextAdapter.ObjectContext.Connection as EntityConnection;
                        if (entityConnection != null)
                        {
                            EntityConnectionStringBuilder entityConnBuilder = new EntityConnectionStringBuilder(entityConnection.ConnectionString);
                            connectionString = entityConnBuilder.ProviderConnectionString;
                            providerName = entityConnBuilder.Provider;
                        }
                    }

                    // make things easy and share the connection instead of duplicating efforts by specifying it
                    // here and in the WebUI.Infrastructure.Installers.ContextInstaller
                    if (String.IsNullOrEmpty(connectionString) || String.IsNullOrEmpty(providerName))
                    {
                        throw new ApplicationException("Unable to retrieve connection details for WebSecurity.");
                    }
                    WebSecurity.InitializeDatabaseConnection(
                        connectionString: connectionString,
                        providerName: providerName,
                        userTableName: "UserProfiles",
                        userIdColumn: "Id",
                        userNameColumn: "UserName",
                        autoCreateTables: true
                    );

                    // Add default roles
                    if (!Roles.RoleExists(Administrator))
                    {
                        Roles.CreateRole(Administrator);
                    }

                    IMembershipService membershipService = IoC.Resolve<IMembershipService>();

                    // Add a default admin account
                    if (!membershipService.UserNameExists(Administrator))// if (!WebSecurity.UserExists(Administrator))
                    {
                        membershipService.CreateAccount(Administrator, "changeme", Administrator.ToLower() + "@contoso.com", Administrator);
                        membershipService.AddUserToRoles(Administrator, Administrator);
                    }
                    
                    // add a couple of other accounts just for playing
                    String[] demoAccounts = new[]{ "Guest1", "Guest2" };
                    foreach (String demoAccount in demoAccounts)
                    {
                        if (!membershipService.UserNameExists(demoAccount))
                        {
                            membershipService.CreateAccount(demoAccount, "changeme", demoAccount.ToLower() + "@contoso.com", demoAccount);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}