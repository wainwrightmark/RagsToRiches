using System.Collections.Immutable;
using System.Linq;

namespace RagsToRiches
{
    public record GameState(int Start, int Finish, ImmutableList<Transform> StartTransforms,
        ImmutableList<Transform> FinishTransforms, int ExpectedTurns)
    {
        public bool IsWon => LeftNumber == RightNumber;

        public int LeftNumber => StartTransforms.Any() ? StartTransforms.Last().Result : Start;

        public int RightNumber => FinishTransforms.Any() ? FinishTransforms.Last().Result : Finish;

        public int Turns => 1 + StartTransforms.Count + FinishTransforms.Count;

        public int? Number(int index, bool start)
        {
            if (start)
            {
                return index == 0 ? Start :
                    index > StartTransforms.Count ? null :
                    StartTransforms[index - 1].Result;
            }

            return index == 0 ? Finish :
                index > FinishTransforms.Count ? null :
                FinishTransforms[index - 1].Result;
        }

        public GameState? TryApply(TransformType transformType, int index, bool start)
        {
            var number = Number(index, start);
            if (number is null) return null;

            if (start)
            {
                var newLeft = transformType.Function(number.Value);
                if (newLeft is null) return null;

                var newTransforms = StartTransforms.GetRange(0, index).Add(new Transform(transformType, newLeft.Value));

                return this with {StartTransforms = newTransforms};
            }
            else
            {
                var newRight = transformType.Function(number.Value);
                if (newRight is null) return null;

                var newTransforms = FinishTransforms.GetRange(0, index).Add(new Transform(transformType, newRight.Value));

                return this with { FinishTransforms = newTransforms };
            }
        }
    }
}