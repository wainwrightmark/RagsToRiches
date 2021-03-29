using System.Collections.Immutable;
namespace RagsToRiches
{
    public record CheatAction : IAction<GameState>
    {
        /// <inheritdoc />
        public GameState Act(GameState state)
        {
            var solution = Solver.SolveGame2(state.Start, state.Finish, TransformType.All);

            state = state with
            {
                StartTransforms = solution, FinishTransforms = ImmutableList<Transform>.Empty
            };

            return state;
        }
    }
}