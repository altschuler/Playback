namespace Playback.Data.Element
{
    public abstract class Consequence : IInvokeable
    {
        public Level Level { get; set; }

        protected Consequence(Level level)
        {
            this.Level = level;
        }

        public abstract void Invoke();
    }
}