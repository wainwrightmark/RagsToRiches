using System.Collections.Immutable;
using System.Linq;

namespace RagsToRiches
{
    public record GameState(int Start, int Finish, ImmutableList<Transform> StartTransforms, int ExpectedTurns)
    {
        public bool IsWon => LeftNumber == Finish;

        public int LeftNumber => StartTransforms.Any() ? StartTransforms.Last().Result : Start;


        public GameState? TryApply(TransformType transformType)
        {
            var newLeft = transformType.Function(LeftNumber);
            if (newLeft is null)
                return null;
            var newTransforms = StartTransforms.Add(new Transform(transformType, newLeft.Value));

            return this with {StartTransforms = newTransforms};
        }
    }
}