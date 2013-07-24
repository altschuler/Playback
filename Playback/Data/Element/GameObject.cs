using System.Collections.Generic;
using System.Linq;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Playback.Display;
using Playback.Logic;

namespace Playback.Data.Element
{
    public class GameObject
    {
        public Body Body { get; set; }
        public DisplayObject Display { get; set; }
        public GameObjectType Type { get; set; }
        public bool HistoryEnabled { get; set; }
        public List<GameObjectState> History { get; private set; }

        // for debugging purposes
        public string Name { get; set; }

        public GameObject()
        {
            this.History = new List<GameObjectState>();
        }

        public GameObjectState PopLastHistoryState()
        {
            var last = this.History.LastOrDefault();
            this.History.Remove(last);
            return last;
        }

        public GameObjectState GetCurrentState(GameTime gameTime)
        {
            return new GameObjectState()
            {
                Time = gameTime,

                Position = this.Body.Position,
                Rotation = this.Body.Rotation,

                LinearVelocity = this.Body.LinearVelocity,
                AngularVelocity = this.Body.AngularVelocity,

                LinearDamping = this.Body.LinearDamping,
                AngularDamping = this.Body.AngularDamping,
            };
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