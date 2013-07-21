using System.Collections.Generic;
using System.Linq;

namespace Playback.Logic
{
    public class MovingObject : GameObject
    {
        public List<GameObjectState> History { get; private set; } 

        public MovingObject()
        {
            this.History = new List<GameObjectState>();
        }

        public GameObjectState PopLastHistoryState()
        {
            var last = this.History.LastOrDefault();
            this.History.Remove(last);
            return last;
        }

        public void ApplyState(GameObjectState state)
        {
            this.Body.Position = state.Position;
            this.Body.Rotation = state.Rotation;

            this.Body.LinearVelocity = state.LinearVelocity;
            this.Body.AngularVelocity = state.AngularVelocity;

            this.Body.LinearDamping = state.LinearDamping;
            this.Body.AngularDamping = state.AngularDamping;
        }
    }
}