using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using FastReslectionForHabrahabr.Hydrators;
using FastReslectionForHabrahabr.Interfaces;


namespace FastReslectionForHabrahabr.IoCModules
{
    public class ManualModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ManualContactHydrator>().As<IEntityHydrator<Contact>>();
        }
    }
}
