using Autofac;
using BenchmarkDotNet.Attributes;
using FastReslectionForHabrahabr.Helpers;
using FastReslectionForHabrahabr.Interfaces;
using FastReslectionForHabrahabr.IoCModules;
using FastReslectionForHabrahabr.Hydrators;
using FastReslectionForHabrahabr.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FastReslectionForHabrahabr
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class Benchmarks
    {
        [Params(1, 10, 100, 1000)]
        public int N = 1;

        public static IEnumerable<string> GetBenchData()
        {
            var rn = Environment.NewLine;
            yield return $"ФИО:Иванов Иван Иванович{rn}Тел.:+78886543422{rn}Возраст:22";
            yield return $"Фамилия Имя Отчество:Иванов Иван Иванович{rn}Тел.:+78886543422{rn}Возраст:22";
            yield return $"ФИО:Иванов Иван Иванович{rn}Телефон:+78886543422{rn}Возраст:22";
            yield return $"Фамилия Имя Отчество:Иванов Иван Иванович{rn}Телефон:+78886543422{rn}Полных лет:22";
            yield return $"ФИО:Иванов Иван Иванович{rn}Тел.:+78886543422{rn}Возраст:22";
            yield return $"Фамилия Имя Отчество:Иванов Иван Иванович{rn}Тел.:+78886543422{rn}Возраст:22";
            yield return $"ФИО:Иванов Иван Иванович{rn}Телефон:+78886543422{rn}Возраст:22";
            yield return $"Фамилия Имя Отчество:Иванов Иван Иванович{rn}Телефон:+78886543422{rn}Полных лет:22";
        }

        [Benchmark]
        public async Task FastHydrationLinq()
        {
            var parser = new FastContactHydrator(new DefaultRawStringParser(), MockHelper.InstanceDb());
            for (int i = 0; i < N; i++)
            {
                foreach (var data in GetBenchData())
                {
                    await HydrateWithLinq(parser, data);
                }
            }
        }
        [Benchmark]
        public async Task FastHydration()
        {
            var parser = new FastContactHydrator(new DefaultRawStringParser(), MockHelper.InstanceDb());
            for (int i = 0; i < N; i++)
            {
                foreach (var data in GetBenchData())
                {
                    await HydrateWithoutLinq(parser, data);
                }
            }
        }
        [Benchmark]
        public async Task SlowHydrationLinq()
        {
            var parser = new SlowContactHydrator(new DefaultRawStringParser(), MockHelper.InstanceDb());
            for (int i = 0; i < N; i++)
            {
                foreach (var data in GetBenchData())
                {
                    await HydrateWithLinq(parser, data);
                }
            }
        }
        [Benchmark]
        public async Task SlowHydration()
        {
            var parser = new SlowContactHydrator(new DefaultRawStringParser(), MockHelper.InstanceDb());
            for (int i = 0; i < N; i++)
            {
                foreach (var data in GetBenchData())
                {
                    await HydrateWithoutLinq(parser, data);
                }
            }
        }
        [Benchmark]
        public async Task ManualHydrationLinq()
        {
            var parser = new ManualContactHydrator(new DefaultRawStringParser(), MockHelper.InstanceDb());
            for (int i = 0; i < N; i++)
            {
                foreach (var data in GetBenchData())
                {
                    await HydrateWithLinq(parser, data);
                }
            }
        }
        [Benchmark]
        public async Task ManualHydration()
        {
            var parser = new ManualContactHydrator(new DefaultRawStringParser(), MockHelper.InstanceDb());
            for (int i = 0; i < N; i++)
            {
                foreach (var data in GetBenchData())
                {
                    await HydrateWithoutLinq(parser, data);
                }
            }
        }
        private static async Task HydrateWithoutLinq(IEntityHydrator<Contact> parser, string data)
        {
            var contact = await parser.HydrateWithoutLinq(data, CancellationToken.None);
        }

        private static async Task HydrateWithLinq(IEntityHydrator<Contact> parser, string data)
        {
            var contact = await parser.HydrateWithLinq(data, CancellationToken.None);
        }
    }
}
