using FastReslectionForHabrahabr.Interfaces;
using FastReslectionForHabrahabr.Models;
using FastReslectionForHabrahabr.Hydrators;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastReslectionForHabrahabr.Helpers
{
    public static class MockHelper
    {
        public static IStorage InstanceDb()
        {
            var mock = new Mock<HabraDbContext>();
            mock.Setup(x => x.ContactMapSchemas).ReturnsDbSet(GetFakeData());

            return mock.Object;
        }
        public static List<ContactMapSchema> GetFakeData()
            => new List<ContactMapSchema>
            {
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Телефон", Property = _contactPhoneAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Контактный телефон", Property = _contactPhoneAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Тел.", Property = _contactPhoneAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "ФИО", Property = _contactFullNameAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Фамилия Имя Отчество", Property = _contactFullNameAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Имя", Property = _contactFullNameAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Возраст", Property = _contactAgeAssemblyName},
                new ContactMapSchema { EntityName = _contactAssemblyName, Key = "Полных лет", Property = _contactAgeAssemblyName},
            };

        private static string _contactAssemblyName = typeof(Contact).FullName;
        private static string _contactPhoneAssemblyName = nameof(Contact.Phone);
        private static string _contactFullNameAssemblyName = nameof(Contact.FullName);
        private static string _contactAgeAssemblyName = nameof(Contact.Age);
    }
}
