using BenchmarkDotNet.Running;
using Bogus.Platform;
using TestDataGeneration.Benchmarks.Published;

Console.WriteLine("Hello, Published World!");
//var summary = BenchmarkRunner.Run<IntGenerationBenchmarks>();
var summary = BenchmarkRunner.Run(typeof(Program).GetAssembly());
Console.WriteLine("Bye, Published World!");