using System.Collections.Immutable;

namespace RagsToRiches
{
    public record NewGameAction(int Start, int Finish) : IAction<GameState>
    {
        /// <inheritdoc />
        public GameState Act(GameState state)
        {
            var sol = Solver.SolveGame(Start, Finish, TransformType.All).Count;

            return new(Start, Finish, ImmutableList<Transform>.Empty, ImmutableList<Transform>.Empty, sol);
        }
    }
}