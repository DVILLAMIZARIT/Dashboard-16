using System;
using DAL.Repositories;
using Infra.Model;
using Machine.Specifications;

namespace DAL.IntegrationSpecs
{
    [Subject(typeof(Repository<User>))]
    public class when_saving_new_user : DatabaseSpec
    {
        private static Repository<User> repository;
        private static User user;

        Establish context = () =>
        {
            repository = new Repository<User>(contextFactory.GetContext());
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
