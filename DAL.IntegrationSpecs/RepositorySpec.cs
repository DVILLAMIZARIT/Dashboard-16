using System;
using DAL.Repositories;
using Infra.Model;
using Machine.Specifications;

namespace DAL.IntegrationSpecs
{
    [Subject(typeof(Repository<UserProfile>))]
    public class when_saving_new_user : DatabaseSpec
    {
        private static Repository<UserProfile> repository;
        private static UserProfile user;

        Establish context = () =>
        {
            repository = new Repository<UserProfile>(contextFactory);
        };

        Because of = () =>
        {
            user = ObjectMother.User();
            repository.Save(user);
            unitOfWork.Commit();
        };

        It should_generate_new_id = () => user.Id.ShouldNotEqual(default(Int32));
        It should_persist = () => repository.Any(u => u.Id == user.Id).ShouldBeTrue();
    }
}
