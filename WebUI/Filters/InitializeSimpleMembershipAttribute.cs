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

                    //using (var context = IoC.Resolve<IDataContextFactory>().Create())
                    //{
                    var context = IoC.Resolve<IDataContextFactory>().Create();

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

                            #region dead
                            //Type userTableType = typeof(UserProfile);
                            //String userTableName = String.Empty;
                            //String userIdColumn = String.Empty;
                            //String userNameColumn = String.Empty;
                            //// attempt to grab the information dynamically
                            //// http://stackoverflow.com/a/6463557/298053
                            //dynamic objSet = typeof(ObjectContext)
                            //    .GetMethod("CreateObjectSet", new Type[0])
                            //    .MakeGenericMethod(userTableType)
                            //    .Invoke(contextAdapter.ObjectContext, null);
                            //String tableScript = objSet.ToTraceString();
                            //Regex reTableName = new Regex(@"FROM (?:\[[^]+\])*\[([^]+)\] AS");
                            //Match mTableName = reTableName.Match(tableScript);
                            //if (!mTableName.Success)
                            //{
                            //    throw new ApplicationException("Unable to retrieve userTable name for WebSecurity.");
                            //}
                            //userTableName = mTableName.Value;

                            ////TODO: Work on getting column names as well
                            //userIdColumn = "Id";
                            //userNameColumn = "UserName";


                            //if (String.IsNullOrEmpty(userTableName) || String.IsNullOrEmpty(userIdColumn) || String.IsNullOrEmpty(userNameColumn))
                            //{
                            //    throw new ApplicationException("Unable to retrieve userTable details for WebSecurity.");
                            //}
                            #endregion
                        }
                    //}

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

                    // Add a default admin account
                    if (!WebSecurity.UserExists(Administrator))
                    {
                        WebSecurity.CreateUserAndAccount(Administrator, "changeme", new
                        {
                            DisplayName = Administrator,
                            EmailAddress = Administrator + "@contoso.com",
                            IsDeleted = false
                        }, false);
                        Roles.AddUserToRole(Administrator, Administrator);

                        WebSecurity.Login(Administrator, "changeme", true);
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