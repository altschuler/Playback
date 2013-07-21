using System.Diagnostics;
using FarseerPhysics.Dynamics;
using Playback.Control;
using Playback.Display;

namespace Playback.Logic
{
    public class GameObject
    {
        public GameObjectRole Role { get; set; }
        public Body Body { get; set; }
        public DisplayObject Display { get; set; }
        public GameObjectType Type { get; set; }

        // for debugging purposes
        public string Name { get; set; }
    }

    public class CollectibleObject : GameObject
    {
        public Reward Reward { get; private set; }

        public CollectibleObject(Reward reward)
        {
            this.Reward = reward;
        }
    }

    public class TriggerObject : GameObject
    {
        public Consequence Consequence { get; private set; }

        public TriggerObject(Consequence consequence)
        {
            this.Consequence = consequence;
        }
    }

    public abstract class Consequence : IInvokeable
    {
        public Level Level { get; set; }

        protected Consequence(Level level)
        {
            this.Level = level;
        }

        public abstract void Invoke();
    }

    public class WinLevelConsequence : Consequence
    {
        public WinLevelConsequence(Level level) : base(level)
        {
        }

        public override void Invoke()
        {
            Debug.WriteLine("YOU WIN!");
        }
    }
}