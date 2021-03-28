using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace RagsToRiches
{
    public static class Solver
    {

        public static ImmutableList<Transform> SolveGame2(int start, int finish,
            IReadOnlyCollection<TransformType> transforms)
        {
            var l = SolveGame(start, finish, transforms);

            var result = new List<Transform>();

            var previous = l.First();

            foreach (var i in l.Skip(1))
            {
                var t = TransformType.All.First(x => x.Function(previous) == i);
                result.Add(new Transform(t, i));
                previous = i;
            }

            return result.ToImmutableList();
        }

        public static ImmutableList<int> SolveGame(int start, int finish, IReadOnlyCollection<TransformType> transforms)
        {
            if(start == finish)
                return ImmutableList.Create(start);

            var startList1 = ImmutableList.Create(start);
            var finishList1 = ImmutableList.Create(finish);
            var startPaths = new Dictionary<int, ImmutableList<int>>(){{start, startList1}};
            var finishPaths = new Dictionary<int, ImmutableList<int>>(){{finish, finishList1}};

            var currentStarts = new List<ImmutableList<int>>(){startList1};
            var currentFinishes = new List<ImmutableList<int>>() {finishList1};


            while (true)
            {
                var nextCurrentStarts = new List<ImmutableList<int>>();
                foreach (var path in currentStarts)
                {

                    foreach (var transform in transforms)
                    {
                        var r = transform.Function(path.Last());
                        if (r.HasValue && !startPaths.ContainsKey(r.Value))
                        {
                            var newPath = path.Add(r.Value);
                            startPaths.Add(r.Value, newPath);
                            if (finishPaths.TryGetValue(r.Value, out var finishList))
                            {
                                var result = path.AddRange(finishList.Reverse());
                                return result;
                            }
                            nextCurrentStarts.Add(newPath);
                        }
                    }
                }

                currentStarts = nextCurrentStarts;

                var nextCurrentFinishes = new List<ImmutableList<int>>();
                foreach (var finishPath in currentFinishes)
                {
                    foreach (var transform in transforms)
                    {
                        var r = transform.Function(finishPath.Last());
                        if (r.HasValue && !finishPaths.ContainsKey(r.Value))
                        {
                            var newPath = finishPath.Add(r.Value);
                            finishPaths.Add(r.Value, newPath);
                            if (startPaths.TryGetValue(r.Value, out var startList))
                            {
                                var result = startList.AddRange(finishPath.Reverse());
                                return result;
                            }
                            nextCurrentFinishes.Add(newPath);
                        }
                    }
                }

                currentFinishes = nextCurrentFinishes;
            }



        }


    }
}