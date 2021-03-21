namespace RagsToRiches
{
    public record ApplyTransformAction(TransformType Transform, int Index) : IAction<GameState>
    {
        /// <inheritdoc />
        public GameState Act(GameState state)
        {
            return state.TryApply(Transform, Index) ?? state;
        }
    }
}