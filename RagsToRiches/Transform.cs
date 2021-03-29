using System.Collections.Generic;

namespace RagsToRiches
{
    public record Transform(TransformType TransformType, int Result)
    {
        public static IEnumerable<Transform> GetPossibleTransforms(int i)
        {
            var used = new HashSet<int>() {i};
            foreach (var transformType in TransformType.All)
            {
                var n = transformType.Function(i);
                if (n.HasValue && used.Add(n.Value) && n.Value > 0 && n.Value < 100000)
                    yield return new Transform(transformType, n.Value);
            }
        }

        public string Text => TransformType.Symbol + "\t" + Result;
    }
}