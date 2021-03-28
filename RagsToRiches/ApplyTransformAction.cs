using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace RagsToRiches
{
    public record ApplyTransformAction(TransformType Transform, int Index, bool Start) : IAction<GameState>
    {
        /// <inheritdoc />
        public GameState Act(GameState state)
        {
            return state.TryApply(Transform, Index, Start) ?? state;
        }
    }

    public record CheatAction : IAction<GameState>
    {
        /// <inheritdoc />
        public GameState Act(GameState state)
        {
            var solution = Solver.SolveGame(state.Start, state.Finish, TransformType.All);

            var transforms = solution.Skip(1).Select(x => new Transform(TransformType.Increment.Instance, x)); //cheeky

            //var previous = state.Start;

            //foreach (var i in solution)
            //{
            //    var t = TransformType.All.First(x => x.Function(previous) == i);
            //    transforms.Add(new Transform(t, i));
            //    previous = i;
            //}


            state = state with
            {
                StartTransforms = transforms.ToImmutableList(), FinishTransforms = ImmutableList<Transform>.Empty
            };

            return state;
        }
    }
}