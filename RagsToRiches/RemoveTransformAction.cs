namespace RagsToRiches
{
    public record RemoveTransformAction(int Index) : IAction<GameState>
    {
        /// <inheritdoc />
        public GameState Act(GameState state)
        {
            var newList = state.StartTransforms.GetRange(0, Index);
            return state with {StartTransforms = newList};
        }
    }
}