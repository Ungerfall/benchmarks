using BenchmarkDotNet.Running;

namespace password_array_resize_copy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Benchmarks>();
        }
    }
}