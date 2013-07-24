namespace Playback.Data.Element
{
    public class CollectibleObject : GameObject
    {
        public Reward Reward { get; private set; }

        public CollectibleObject(Reward reward)
        {
            this.Reward = reward;
        }
    }
}