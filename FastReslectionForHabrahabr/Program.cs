using BenchmarkDotNet.Running;
using System;

namespace FastReslectionForHabrahabr
{
    public class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run(typeof(Benchmarks));
            Console.ReadKey();
        }
    }
}
