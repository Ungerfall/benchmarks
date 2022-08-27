using System;
using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;

namespace zip_vs_indexedselect
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        private int[] input = Enumerable.Range(0, 10 * 1000).ToArray();

        [Benchmark]
        public OutputDto[] Zip()
        {
            return input
                .Zip(
                    Enumerable.Range(0, input.Length),
                    (first, second) => new OutputDto
                    {
                        Name = first.ToString(),
                        Index = second
                    })
                .ToArray();
        }

        [Benchmark]
        public OutputDto[] IndexedSelect()
        {
            return input
                .Select((item, index) => new OutputDto
                    {
                        Name = item.ToString(),
                        Index = index
                    })
                .ToArray();
        }
    }

    public class OutputDto
    {
        public string Name { get; set; }
        public int Index { get; set; }
    }
}
