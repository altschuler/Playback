namespace Playback.Data.Element
{
    public class TriggerObject : GameObject
    {
        public Consequence Consequence { get; private set; }

        public TriggerObject(Consequence consequence)
        {
            this.Consequence = consequence;
        }
    }
}