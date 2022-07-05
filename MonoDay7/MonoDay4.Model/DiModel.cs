using Autofac;
using MonoDay4.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MonoDay4.Model
{
    public class DiModel : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Customer>().As<ICustomer>();
            builder.RegisterType<Order>().As<IOrder>();
            base.Load(builder);
        }
    }
}
