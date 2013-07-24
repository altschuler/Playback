using Playback.Logic;

namespace Playback.Data.Element
{
    public abstract class Reward : IInvokeable
    {
        public Level Level { get; set; }

        protected Reward(Level level)
        {
            this.Level = level;
        }

        public abstract void Invoke();
    }
}