using Autofac;
using Buisness.Abstract;
using Buisness.Concrete;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<BusinessUserManager>().As<IBusinessUserService>().InstancePerLifetimeScope();
            builder.RegisterType<Mongo_BusinessUserDal>().As<IBusinessUserDal>().InstancePerLifetimeScope();
            builder.RegisterType<CityManager>().As<ICityService>().InstancePerLifetimeScope();
            builder.RegisterType<Mongo_CityDal>().As<ICityDal>().InstancePerDependency();
            builder.RegisterType<ShipmentManager>().As<IShipmentService>().InstancePerLifetimeScope();
            builder.RegisterType<Mongo_ShipmentDal>().As<IShipmentDal>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeManager>().As<IEmployeeService>().InstancePerLifetimeScope();
            builder.RegisterType<Mongo_EmployeeDal>().As<IEmployeeDal>().InstancePerLifetimeScope();
            builder.RegisterType<WarehouseManager>().As<IWarehouseService>().InstancePerLifetimeScope();
            builder.RegisterType<Mongo_WarehouseDal>().As<IWarehouseDal>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeAssignmentManager>().As<IEmployeeAssignmentService>().InstancePerLifetimeScope();


        }
    }
}
