using Autofac;
using TestProjectWebApi.Data;
using TestProjectWebApi.Services;

namespace TestProjectWebApi
{
    public class AutofacModule //: Module
    {
        //protected override void Load(ContainerBuilder builder)
        //{
        //}

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.Register(c => new PersonServices(c.Resolve<AppDBContext>()))
                .As<IPersonServices>()
                .InstancePerLifetimeScope();
            builder.Register(c => new AccountServices(c.Resolve<AppDBContext>()))
                .As<IAccountServices>()
                .InstancePerLifetimeScope();

            return builder.Build();
        }

    }
}

