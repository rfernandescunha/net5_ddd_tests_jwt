using AutoMapper;
using crosscutting.AutoMapper;
using System;
using Xunit;

namespace Api.Service.Test
{
    public abstract class BaseServiceTest
    {
        public IMapper Mapper { get; set; }
        public BaseServiceTest()
        {
            Mapper = new AutoMapperFixture().GetMapper();
        }

        public class AutoMapperFixture : IDisposable
        {
            public IMapper GetMapper()
            {
                var config = new MapperConfiguration(x =>
                {
                    x.AddProfile(new ModelToEntityProfile());
                    x.AddProfile(new DtoToModelProfile());
                    x.AddProfile(new EntityToDtoProfile());
                });

                return config.CreateMapper();
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }
    }
}
