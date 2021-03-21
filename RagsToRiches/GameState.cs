using System.Collections.Immutable;
using System.Linq;

namespace RagsToRiches
{
    public record GameState(int Start, int Finish, ImmutableList<Transform> StartTransforms, int ExpectedTurns)
    {
        public bool IsWon => LeftNumber == Finish;

        public int LeftNumber => StartTransforms.Any() ? StartTransforms.Last().Result : Start;

        public int Turns => 1 + StartTransforms.Count;

        public int? Number(int index) =>
            index == 0 ? Start :
            index > StartTransforms.Count ? null:
            StartTransforms[index - 1].Result;

        public GameState? TryApply(TransformType transformType, int index)
        {
            var number = Number(index);

            if(number is null)return null;
            var newLeft = transformType.Function(number.Value);
            if (newLeft is null) return null;

            var newTransforms = StartTransforms.GetRange(0, index).Add(new Transform(transformType, newLeft.Value));

            return this with {StartTransforms = newTransforms};
        }
    }
}