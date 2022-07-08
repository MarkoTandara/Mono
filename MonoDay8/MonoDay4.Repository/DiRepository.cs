using Autofac;
using MonoDay4.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoDay4.Repository
{
    public class DiRepository : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            base.Load(builder);
        }
    }
}
