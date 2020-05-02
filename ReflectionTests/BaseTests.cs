using Autofac;
using FastReslectionForHabrahabr;
using FastReslectionForHabrahabr.Interfaces;
using FastReslectionForHabrahabr.IoCModules;
using FastReslectionForHabrahabr.Hydrators;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ReflectionTests
{
    public class BaseTests
    {
        private readonly IContainer _fastContainer;
        private readonly IContainer _slowContainer;
        private readonly IContainer _manualContainer;
        public BaseTests()
        {
            var fastBuilder =
                new ContainerBuilder();
            fastBuilder
                .RegisterModule<AppModule>()
                .RegisterModule<FastModule>();
           _fastContainer = fastBuilder.Build();

            var slowBuilder =
                new ContainerBuilder();
            slowBuilder
                .RegisterModule<AppModule>()
                .RegisterModule<SlowModule>();
            _slowContainer = slowBuilder.Build();

            var manualBuilder =
                new ContainerBuilder();
            manualBuilder
                .RegisterModule<AppModule>()
                .RegisterModule<ManualModule>();
            _manualContainer = manualBuilder.Build();
        }
        public static IEnumerable<object[]> TestData()
        {
            var rn = Environment.NewLine;
            yield return new object[] {$"ФИО:Иванов Иван Иванович{rn}Тел.:+78886543422{rn}Возраст:22", "Иванов Иван Иванович", "+78886543422", 22 };
            yield return new object[] { $"Фамилия Имя Отчество:Иванов Иван Иванович{rn}Тел.:+78886543422{rn}Возраст:22", "Иванов Иван Иванович", "+78886543422", 22 };
            yield return new object[] { $"ФИО:Иванов Иван Иванович{rn}Телефон:+78886543422{rn}Возраст:22", "Иванов Иван Иванович", "+78886543422", 22 };
            yield return new object[] { $"Фамилия Имя Отчество:Иванов Иван Иванович{rn}Телефон:+78886543422{rn}Полных лет:22", "Иванов Иван Иванович", "+78886543422", 22 };
        }
        [Theory]
        [MemberData(nameof(TestData))]
        public async Task ShouldHydrateFast(string data, string etalonFullName, string etalonPhone, int etalonAge)
        {
            using (var scope = _fastContainer.BeginLifetimeScope())
            {
                var parser = scope.Resolve<IEntityHydrator<Contact>>();
                Func<Task<Contact>> parseFunc = async () => await parser.HydrateWithLinq(data, CancellationToken.None);
                await RoutineTest(etalonFullName, etalonPhone, etalonAge, parseFunc);
            }
        }
        [Theory]
        [MemberData(nameof(TestData))]
        public async Task ShouldHydrateFastOpt(string data, string etalonFullName, string etalonPhone, int etalonAge)
        {
            using (var scope = _fastContainer.BeginLifetimeScope())
            {
                var parser = scope.Resolve<IEntityHydrator<Contact>>();
                Func<Task<Contact>> parseFunc = async () => await parser.HydrateWithoutLinq(data, CancellationToken.None);
                await RoutineTest(etalonFullName, etalonPhone, etalonAge, parseFunc);
            }
        }
        [Theory]
        [MemberData(nameof(TestData))]
        public async Task ShouldHydrateSlow(string data, string etalonFullName, string etalonPhone, int etalonAge)
        {
            using (var scope = _slowContainer.BeginLifetimeScope())
            {
                var parser = scope.Resolve<IEntityHydrator<Contact>>();
                Func<Task<Contact>> parseFunc = async () => await parser.HydrateWithLinq(data, CancellationToken.None);
                await RoutineTest(etalonFullName, etalonPhone, etalonAge, parseFunc);
            }
        }
        [Theory]
        [MemberData(nameof(TestData))]
        public async Task ShouldHydrateSlowOpt(string data, string etalonFullName, string etalonPhone, int etalonAge)
        {
            using (var scope = _slowContainer.BeginLifetimeScope())
            {
                var parser = scope.Resolve<IEntityHydrator<Contact>>();
                Func<Task<Contact>> parseFunc = async () => await parser.HydrateWithoutLinq(data, CancellationToken.None);
                await RoutineTest(etalonFullName, etalonPhone, etalonAge, parseFunc);
            }
        }
        [Theory]
        [MemberData(nameof(TestData))]
        public async Task ShouldHydrateManual(string data, string etalonFullName, string etalonPhone, int etalonAge)
        {
            using (var scope = _manualContainer.BeginLifetimeScope())
            {
                var parser = scope.Resolve<IEntityHydrator<Contact>>();
                Func<Task<Contact>> parseFunc = async () => await parser.HydrateWithLinq(data, CancellationToken.None);
                await RoutineTest(etalonFullName, etalonPhone, etalonAge, parseFunc);
            }
        }
        [Theory]
        [MemberData(nameof(TestData))]
        public async Task ShouldHydrateManualOpt(string data, string etalonFullName, string etalonPhone, int etalonAge)
        {
            using (var scope = _manualContainer.BeginLifetimeScope())
            {
                var parser = scope.Resolve<IEntityHydrator<Contact>>();
                Func<Task<Contact>> parseFunc = async () => await parser.HydrateWithoutLinq(data, CancellationToken.None);
                await RoutineTest(etalonFullName, etalonPhone, etalonAge, parseFunc);
            }
        }
        private static async Task RoutineTest(string etalonFullName, string etalonPhone, int etalonAge, Func<Task<Contact>> parseFunc)
        {
            var result = await parseFunc();
            Assert.Equal(etalonFullName, result.FullName);
            Assert.Equal(etalonPhone, result.Phone);
            Assert.Equal(etalonAge.ToString(), result.Age);
        }
    }
}
