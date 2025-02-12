using AutoMapper;

namespace Application.Mappings
{
    public static class AppMapper<TEntity> where TEntity : Profile, new()
    {
        private readonly static Lazy<IMapper> _lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TEntity>();
            });
            return config.CreateMapper();
        });

        public static IMapper Mapper => _lazy.Value;
    }
}
