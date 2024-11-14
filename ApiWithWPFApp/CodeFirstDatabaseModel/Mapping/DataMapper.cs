using AutoMapper;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstDatabaseModel.Mapping
{
    class DataMapper
    {
        public MapperConfiguration config { get; set; }

        public DataMapper()
        {
            config = new MapperConfiguration(c =>
            {
                c.CreateMap<Student, DataManager>();
                c.CreateMap<DataManager, Student>();
            });
        }
    }

    public class MappingModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>().ToMethod(ContentBoundObject => new DataMapper().config.CreateMapper());
        }

    }

    
}
