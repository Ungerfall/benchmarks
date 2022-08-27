using BenchmarkDotNet.Running;

namespace builtinchunk_vs_mychunk
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Benchmarks>();
        }
    }
}
