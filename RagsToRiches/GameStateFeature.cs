using System.Collections.Immutable;
using Fluxor;

namespace RagsToRiches
{
    public class GameStateFeature : Feature<GameState>
    {
        /// <inheritdoc />
        public override string GetName() => "GameState";

        /// <inheritdoc />
        protected override GameState GetInitialState()
        {
            var start = 28;
            var finish = 33;
            var eTurns = Solver.SolveGame(start, finish, TransformType.All);

            return new(28, 33, ImmutableList<Transform>.Empty, eTurns.Count);
        }
    }
}