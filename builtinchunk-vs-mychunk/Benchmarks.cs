using System;
using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;

namespace builtinchunk_vs_mychunk
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        [Params(5, 20, 100)]
        public int NumberOfClusters { get; set; }

        [Benchmark]
        public int[][] BuiltInChunk()
        {
            int[] input = Enumerable.Range(0, 100 * 1000).ToArray();
            return input.Chunk(NumberOfClusters).ToArray();
        }

        [Benchmark]
        public int[][] MyChunk()
        {
            int[] input = Enumerable.Range(0, 100 * 1000).ToArray();
            return input
                .Select((item, index) => new
                    {
                        Item = item,
                        GroupIndex = index % NumberOfClusters,
                    })
                .GroupBy(
                    x => x.GroupIndex,
                    el => el.Item,
                    (k, items) => items.ToArray())
                .ToArray();
        }
    }
}
