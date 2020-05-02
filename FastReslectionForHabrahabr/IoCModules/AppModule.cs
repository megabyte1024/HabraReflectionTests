using Autofac;
using FastReslectionForHabrahabr.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FastReslectionForHabrahabr.Hydrators;
using FastReslectionForHabrahabr.Models;
using FastReslectionForHabrahabr.Interfaces;
using FastReslectionForHabrahabr.Services;
using FastReslectionForHabrahabr.Helpers;

namespace FastReslectionForHabrahabr.IoCModules
{
    public class AppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var fakeStorage = MockHelper.InstanceDb();
            builder.RegisterInstance(fakeStorage).As<IStorage>();
            builder.RegisterType<DefaultRawStringParser>().As<IRawStringParser>();
        }
    }
}
