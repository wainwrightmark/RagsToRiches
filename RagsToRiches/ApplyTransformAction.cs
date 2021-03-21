namespace RagsToRiches
{
    public record ApplyTransformAction(TransformType Transform) : IAction<GameState>
    {
        /// <inheritdoc />
        public GameState Act(GameState state)
        {
            return state.TryApply(Transform) ?? state;
        }
    }
}