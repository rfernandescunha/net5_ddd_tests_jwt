using Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Api.Domain.Entities;
using Api.Domain.Dto.User;
using System.Linq;

namespace Api.Service.Test.AutoMapper
{
    public class UserMapperTest: BaseServiceTest
    {

        [Fact(DisplayName = "É Possivel Mapear os Modelos")]
        public void E_Possivel_Mapear_Os_Modelos()
        {
            var model = new UserModel()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                DateCreate = DateTime.UtcNow,
                DateUpdate = DateTime.UtcNow,
            };

            var listEntity = new List<User>()
            {
                new User()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    DateCreate = DateTime.UtcNow,
                    DateUpdate = DateTime.UtcNow,
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    DateCreate = DateTime.UtcNow,
                    DateUpdate = DateTime.UtcNow,
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    DateCreate = DateTime.UtcNow,
                    DateUpdate = DateTime.UtcNow,
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    DateCreate = DateTime.UtcNow,
                    DateUpdate = DateTime.UtcNow,
                }
            };

            //Model => Entity
            var modelToEntity = Mapper.Map<User>(model);

            Assert.Equal(modelToEntity.Id, model.Id);
            Assert.Equal(modelToEntity.Name, model.Name);
            Assert.Equal(modelToEntity.Email, model.Email);
            Assert.Equal(modelToEntity.DateCreate, model.DateCreate);
            Assert.Equal(modelToEntity.DateUpdate, model.DateUpdate);



            //Entity => Dto
            var entityToDtoCreate = Mapper.Map<UserDtoCreate>(modelToEntity);
            Assert.Equal(entityToDtoCreate.Name, model.Name);
            Assert.Equal(entityToDtoCreate.Email, model.Email);

            var entityToDtoCreateResult = Mapper.Map<UserDtoCreateResult>(modelToEntity);
            Assert.Equal(entityToDtoCreateResult.Id, model.Id);
            Assert.Equal(entityToDtoCreateResult.Name, model.Name);
            Assert.Equal(entityToDtoCreateResult.Email, model.Email);
            Assert.Equal(entityToDtoCreateResult.DateCreate, model.DateCreate);

            var entityToDtoUpdate = Mapper.Map<UserDtoUpdate>(modelToEntity);
            Assert.Equal(entityToDtoUpdate.Id, model.Id);
            Assert.Equal(entityToDtoUpdate.Name, model.Name);
            Assert.Equal(entityToDtoUpdate.Email, model.Email);

            var entityToDtoUpdateResult = Mapper.Map<UserDtoUpdateResult>(modelToEntity);
            Assert.Equal(entityToDtoUpdateResult.Id, model.Id);
            Assert.Equal(entityToDtoUpdateResult.Name, model.Name);
            Assert.Equal(entityToDtoUpdateResult.Email, model.Email);
            Assert.Equal(entityToDtoUpdateResult.DateCreate, model.DateCreate);
            Assert.Equal(entityToDtoUpdateResult.DateUpdate, model.DateUpdate);

            var entityToDtoFind = Mapper.Map<UserDtoFind>(modelToEntity);;
            Assert.Equal(entityToDtoFind.Name, model.Name);
            Assert.Equal(entityToDtoFind.Email, model.Email);

            var entityToDtoFindResult = Mapper.Map<UserDtoFindResult>(modelToEntity);
            Assert.Equal(entityToDtoFindResult.Id, model.Id);
            Assert.Equal(entityToDtoFindResult.Name, model.Name);
            Assert.Equal(entityToDtoFindResult.Email, model.Email);
            Assert.Equal(entityToDtoFindResult.DateCreate, model.DateCreate);
            Assert.Equal(entityToDtoFindResult.DateUpdate, model.DateUpdate);

            var listEntityToListDtoFindResult = Mapper.Map<List<UserDtoFindResult>>(listEntity);
            Assert.True(listEntityToListDtoFindResult.Count() == listEntity.Count());
            for (int i = 0; i < listEntityToListDtoFindResult.Count(); i++)
            {
                Assert.Equal(listEntityToListDtoFindResult[i].Id, listEntity[i].Id);
                Assert.Equal(listEntityToListDtoFindResult[i].Name, listEntity[i].Name);
                Assert.Equal(listEntityToListDtoFindResult[i].Email, listEntity[i].Email);
                Assert.Equal(listEntityToListDtoFindResult[i].DateCreate, listEntity[i].DateCreate);
                Assert.Equal(listEntityToListDtoFindResult[i].DateUpdate, listEntity[i].DateUpdate);
            }



            //Dto => Model
            var dtoCreateToModel = Mapper.Map<UserModel>(entityToDtoCreate);
            Assert.Equal(modelToEntity.Name, dtoCreateToModel.Name);
            Assert.Equal(modelToEntity.Email, dtoCreateToModel.Email);

            var dtoCreateResultoToModel = Mapper.Map<UserModel>(entityToDtoCreateResult);
            Assert.Equal(modelToEntity.Id, dtoCreateResultoToModel.Id);
            Assert.Equal(modelToEntity.Name, dtoCreateResultoToModel.Name);
            Assert.Equal(modelToEntity.Email, dtoCreateResultoToModel.Email);
            Assert.Equal(modelToEntity.DateCreate, dtoCreateResultoToModel.DateCreate);


            var dtoUpdateToModel = Mapper.Map<UserModel>(entityToDtoUpdate);
            Assert.Equal(modelToEntity.Id, dtoUpdateToModel.Id);
            Assert.Equal(modelToEntity.Name, dtoUpdateToModel.Name);
            Assert.Equal(modelToEntity.Email, dtoUpdateToModel.Email);
            Assert.Equal(modelToEntity.DateCreate, dtoUpdateToModel.DateCreate);
            Assert.Equal(modelToEntity.DateUpdate, dtoUpdateToModel.DateUpdate);

            var dtoUpdateResultToModel = Mapper.Map<UserModel>(entityToDtoUpdateResult);
            Assert.Equal(modelToEntity.Id, dtoUpdateResultToModel.Id);
            Assert.Equal(modelToEntity.Name, dtoUpdateResultToModel.Name);
            Assert.Equal(modelToEntity.Email, dtoUpdateResultToModel.Email);
            Assert.Equal(modelToEntity.DateCreate, dtoUpdateResultToModel.DateCreate);
            Assert.Equal(modelToEntity.DateUpdate, dtoUpdateResultToModel.DateUpdate);

            var dtoFindToModel = Mapper.Map<UserModel>(entityToDtoFind);
            Assert.Equal(modelToEntity.Name, dtoFindToModel.Name);
            Assert.Equal(modelToEntity.Email, dtoFindToModel.Email);

            var dtoFindResultoToModel = Mapper.Map<UserModel>(entityToDtoFindResult);
            Assert.Equal(modelToEntity.Id, dtoFindResultoToModel.Id);
            Assert.Equal(modelToEntity.Name, dtoFindResultoToModel.Name);
            Assert.Equal(modelToEntity.Email, dtoFindResultoToModel.Email);
            Assert.Equal(modelToEntity.DateCreate, dtoFindResultoToModel.DateCreate);
            Assert.Equal(modelToEntity.DateUpdate, dtoFindResultoToModel.DateUpdate);
        }
    }
}
