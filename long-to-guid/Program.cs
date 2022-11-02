using BenchmarkDotNet.Running;

namespace long_to_guid
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Benchmarks>();
        }
    }
}