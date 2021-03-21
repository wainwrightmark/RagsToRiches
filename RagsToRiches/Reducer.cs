using Fluxor;

namespace RagsToRiches
{
    public static class Reducer
    {
        [ReducerMethod]
        public static GameState Reduce(GameState gameState, IAction<GameState> action) => action.Act(gameState);
    }
}