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

        public static UserProfile User()
        {
            return session.Single<UserProfile>().Get();
        }

        public static IEnumerable<UserProfile> Users(Int32 count = DefaultCount)
        {
            return session.List<UserProfile>(count).Get();
        }

        private static IGenerationSession CreateSession()
        {
            return AutoPocoContainer.Configure(config =>
            {
                config.AddFromAssemblyContainingType<UserProfile>();

                config.Include<UserProfile>()
                    .Setup(u => u.UserName).Use<RandomStringSource>(4, 50)
                    .Setup(u => u.EmailAddress).Use<EmailAddressSource>()
                    .Setup(u => u.DisplayName).Use<FirstNameSource>();

            }).CreateSession();
        }
    }
}
