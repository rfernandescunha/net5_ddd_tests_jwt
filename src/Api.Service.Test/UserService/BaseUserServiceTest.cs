using Api.Domain.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Service.Test.UserService
{
    public class BaseUserServiceTest
    {
        public static Guid IdUser { get; set; }
        public string NameUser { get; set; }
        public string EmailUser { get; set; }
        public string NameUserAterado { get; set; }
        public string EmailUserAterado { get; set; }

        public List<UserDtoFindResult> listUserDtoFindResult = new List<UserDtoFindResult>();
        public UserDtoFindResult userDtoFindResult;
        public UserDtoCreate userDtoCreate;
        public UserDtoCreateResult userDtoCreateResult;
        public UserDtoUpdate userDtoUpdate;
        public UserDtoUpdateResult userDtoUpdateResult;

        public BaseUserServiceTest()
        {
            IdUser = Guid.NewGuid();
            NameUser = Faker.Name.FullName();
            EmailUser = Faker.Internet.Email();
            NameUserAterado = Faker.Name.FullName();
            EmailUserAterado = Faker.Internet.Email();

            for (int i = 0; i < 10; i++)
            {

                listUserDtoFindResult.Add(new UserDtoFindResult()
                                                                {
                                                                    Id = Guid.NewGuid(),
                                                                    Name = Faker.Name.FullName(),
                                                                    Email = Faker.Internet.Email()
                                                                });
            }

            userDtoFindResult = new UserDtoFindResult()
            {
                Id = IdUser,
                Name = NameUser,
                Email = EmailUser
            };

            userDtoCreate = new UserDtoCreate()
            {
                Name = NameUser,
                Email = EmailUser
            };

            userDtoCreateResult = new UserDtoCreateResult()
            {
                Id = IdUser,
                Name = NameUser,
                Email = EmailUser,
                DateCreate = DateTime.Now

            };

            userDtoUpdate = new UserDtoUpdate()
            {
                Id = IdUser,
                Name = NameUserAterado,
                Email = EmailUserAterado
            };

            userDtoUpdateResult = new UserDtoUpdateResult()
            {
                Id = IdUser,
                Name = NameUserAterado,
                Email = EmailUserAterado,
                DateCreate = DateTime.Now,
                DateUpdate = DateTime.Now
            };
        }

    }
}
