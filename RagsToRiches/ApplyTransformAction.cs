using System.Collections.Generic;

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
}