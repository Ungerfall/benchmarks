using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace long_to_guid
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        public static byte[] e = new byte[8] { 255, 255, 255, 255, 255, 255, 255, 255 };
        private readonly Consumer consumer = new Consumer();

        [Params(1, 100, 100 * 100, 100 * 100 * 100)]
        public int N { get; set; }

        [Benchmark]
        public void GuidWithShifts()
        {
            GetGuid0().Consume(consumer);
        }

        [Benchmark]
        public void GuidWithSpan()
        {
            GetGuid1().Consume(consumer);
        }

        private IEnumerable<Guid> GetGuid0()
        {
            long end = long.MinValue + N;
            for (long n = long.MinValue; n <= end; n++)
            {
                int a = (int)(n >> 32);
                short b = (short)(n >> 48);
                short c = (short)n;

                yield return new Guid(a, b, c, e);
            }
        }

        public IEnumerable<Guid> GetGuid1()
        {
            long end = long.MinValue + N;
            for (long value = long.MinValue; value <= end; value++)
            {
                yield return GetGuid(value);
            }
        }

        private Guid GetGuid(long value)
        {
            Span<long> span = stackalloc long[2];
            span[0] = value;
            ref Guid guid = ref Unsafe.As<long, Guid>(ref span[0]);
            return guid;
        }
    }
}
