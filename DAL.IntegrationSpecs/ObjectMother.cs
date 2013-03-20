using System;
using System.Collections.Generic;
using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using Infra.Model;

namespace DAL.IntegrationSpecs
{
    public static class ObjectMother
    {
        private const Int32 DefaultCount = 5;

        private static readonly IGenerationSession session = CreateSession();

        public static User User()
        {
            return session.Single<User>().Get();
        }

        public static IEnumerable<User> Users(Int32 count = DefaultCount)
        {
            return session.List<User>(count).Get();
        }

        private static IGenerationSession CreateSession()
        {
            return AutoPocoContainer.Configure(config =>
            {
                config.AddFromAssemblyContainingType<User>();

                config.Include<User>()
                    .Setup(u => u.UserName).Use<RandomStringSource>(4, 50)
                    .Setup(u => u.EmailAddress).Use<EmailAddressSource>()
                    .Setup(u => u.DisplayName).Use<FirstNameSource>();

            }).CreateSession();
        }
    }
}
