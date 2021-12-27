
using AutoMapper;
using Domain.Business.Mapping.AutoMapper.Profiles;
using Ninject.Modules;

namespace Domain.Business.DependencyResolver.Ninject
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>().ToConstant(CreateConfigruation().CreateMapper());
        }

        private MapperConfiguration CreateConfigruation()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            return config;
        }
    }
}
