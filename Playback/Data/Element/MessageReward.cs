using System.Diagnostics;

namespace Playback.Data.Element
{
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