using System;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace RagsToRiches.Test
{
    public class SolverTests
    {
        public SolverTests(ITestOutputHelper testOutputHelper)
        {
            TestOutputHelper = testOutputHelper;
        }

        public ITestOutputHelper TestOutputHelper { get; }

        [Theory]
        [InlineData(1,2,2)]
        [InlineData(1,3,3)]
        [InlineData(552, 693,8)]
        [InlineData(328,484,7)]
        public void TestDistances(int start, int finish, int expectedDistance)
        {
            var r = Solver.SolveGame2(start, finish, TransformType.All.ToList());

            foreach (var transform in r)
            {
                TestOutputHelper.WriteLine($"{transform.Result} {transform.TransformType.Name}");
            }


            r.Count.Should().Be(expectedDistance - 1);
        }

        [Fact]
        public void TestAllDistances()
        {
            var sw = Stopwatch.StartNew();
            var transforms = TransformType.All.ToList();
            for (var start = 1; start < 100; start++)
            {
                for (var finish = start + 1; finish <= 100; finish++)
                {
                    var list = Solver.SolveGame(start, finish, transforms);

                    TestOutputHelper.WriteLine($"{sw.Elapsed}: {start}-->{finish}: {list.Count} terms: {string.Join(',', list)}");
                }
            }
        }
    }
}
