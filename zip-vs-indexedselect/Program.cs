using BenchmarkDotNet.Running;

namespace zip_vs_indexedselect
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Benchmarks>();
        }
    }
}