using Microsoft.Xna.Framework;

namespace Playback.Data.Element
{
    public struct GameObjectState
    {
        public GameTime Time;

        public Vector2 Position;
        public float Rotation;

        public Vector2 LinearVelocity;
        public float AngularVelocity;

        public float LinearDamping;
        public float AngularDamping;
    }
}