using Microsoft.Xna.Framework;
using Playback.Logic;

namespace Playback.Helpers
{
    public static class HistoryHelper
    {
        // TODO move this to gameobject itself?
        public static GameObjectState CreateState(GameObject gameObject, GameTime gameTime)
        {
            return new GameObjectState()
                {
                    Time = gameTime,

                    Position = gameObject.Body.Position,
                    Rotation = gameObject.Body.Rotation,

                    LinearVelocity = gameObject.Body.LinearVelocity,
                    AngularVelocity = gameObject.Body.AngularVelocity,

                    LinearDamping = gameObject.Body.LinearDamping,
                    AngularDamping = gameObject.Body.AngularDamping,
                };
        }
    }
}