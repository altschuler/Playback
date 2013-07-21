using System.Diagnostics;
using Playback.Control;

namespace Playback.Logic
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

    public class MessageReward : Reward
    {
        public string Message { get; set; }

        public MessageReward(Level level, string message) : base(level)
        {
            this.Message = message;
        }

        public override void Invoke()
        {
            Debug.WriteLine(this.Message);
        }
    }
}