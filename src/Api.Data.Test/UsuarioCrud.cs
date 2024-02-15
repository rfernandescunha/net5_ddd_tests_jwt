using Api.Data.Context;
using Api.Domain.Entities;
using data.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Data.Test
{
    public class UsuarioCrud : BaseDataTest, IClassFixture<BaseDataTest.DbTest>
    {
        private ServiceProvider _serviceProvider;

        public UsuarioCrud(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
        }

        //[Fact(DisplayName = "Crud de Usuario")]
        //[Trait("Crud", "User")]
        //public async Task E_Possivel_Realizar_Crud_Usuario()
        //{
        //    using (var context = _serviceProvider.GetService<ApiContext>())
        //    {
        //        var _repository = new UserRepository(context);

        //        var entity = new User
        //        {
        //            Name = Faker.Name.FullName(),
        //            Email = Faker.Internet.Email()
        //        };

        //        var retornoInsert = await _repository.InsertAsync(entity);

        //        Assert.NotNull(retornoInsert);
        //        Assert.Equal(entity.Email, retornoInsert.Email);
        //        Assert.Equal(entity.Name, retornoInsert.Name);
        //        Assert.False(retornoInsert.Id == Guid.Empty);


        //        entity.Id = retornoInsert.Id;
        //        entity.Name = Faker.Name.First();
        //        var retornoUpdate = await _repository.UpdateAsync(entity);

        //        Assert.NotNull(retornoUpdate);
        //        Assert.Equal(entity.Email, retornoUpdate.Email);
        //        Assert.Equal(entity.Name, retornoUpdate.Name);
        //        Assert.NotEmpty(retornoUpdate.DateUpdate.ToString());


        //        var retornoFind = await _repository.FindAsync(retornoUpdate.Id);
        //        Assert.NotNull(retornoFind);
        //        Assert.Equal(retornoUpdate.Id, retornoFind.Id);

        //        var retornoFindExpression = await _repository.FindAsync();
        //        Assert.NotNull(retornoFindExpression);
        //        Assert.True(retornoFindExpression.Count() >= 1);
        //        Assert.True(retornoFindExpression.Where(x=> x.Id == retornoFind.Id).Any());
        //    }
        //}

        [Fact(DisplayName = "Insert de Usuario")]
        [Trait("Crud", "User")]
        public async Task E_Possivel_Realizar_Insert_Usuario()
        {
            using (var context = _serviceProvider.GetService<ApiContext>())
            {
                var _repository = new UserRepository(context);

                var entity = new User
                {
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                };

                var retornoInsert = await _repository.InsertAsync(entity);

                Assert.NotNull(retornoInsert);
                Assert.Equal(entity.Email, retornoInsert.Email);
                Assert.Equal(entity.Name, retornoInsert.Name);
                Assert.False(retornoInsert.Id == Guid.Empty);
            }
        }


        [Fact(DisplayName = "Update de Usuario")]
        [Trait("Crud", "User")]
        public async Task E_Possivel_Realizar_Update_Usuario()
        {
            using (var context = _serviceProvider.GetService<ApiContext>())
            {
                var _repository = new UserRepository(context);


                var retornoInsert = await _repository.InsertAsync(new User
                                                                            {
                                                                                Name = Faker.Name.FullName(),
                                                                                Email = Faker.Internet.Email()
                                                                            });


                retornoInsert.Name = Faker.Name.First();

                var retornoUpdate = await _repository.UpdateAsync(retornoInsert);

                Assert.NotNull(retornoUpdate);
                Assert.Equal(retornoInsert.Email, retornoUpdate.Email);
                Assert.Equal(retornoInsert.Name, retornoUpdate.Name);
                Assert.NotEmpty(retornoUpdate.DateUpdate.ToString());

            }
        }

        [Fact(DisplayName = "Delete de Usuario")]
        [Trait("Crud", "User")]
        public async Task E_Possivel_Realizar_Delete_Usuario()
        {
            using (var context = _serviceProvider.GetService<ApiContext>())
            {
                var _repository = new UserRepository(context);



                var retornoInsert = await _repository.InsertAsync(new User
                                                                            {
                                                                                Name = Faker.Name.FullName(),
                                                                                Email = Faker.Internet.Email()
                                                                            });


                var retornoDelete = await _repository.DeleteAsync(retornoInsert.Id);
                Assert.True(retornoDelete);

            }
        }

        [Fact(DisplayName = "GetBy de Usuario")]
        [Trait("Crud", "User")]
        public async Task E_Possivel_Realizar_GetBy_Usuario()
        {
            using (var context = _serviceProvider.GetService<ApiContext>())
            {
                var _repository = new UserRepository(context);

                var retornoInsert = await _repository.InsertAsync(new User
                {
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                });


                var retornoFind = await _repository.FindAsync(retornoInsert.Id);

                Assert.NotNull(retornoFind);
                Assert.Equal(retornoInsert.Id, retornoFind.Id);

            }
        }

    }
}
