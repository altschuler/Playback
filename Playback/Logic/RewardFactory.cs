using Playback.Control;

namespace Playback.Logic
{
    public static class RewardFactory
    {
        public static Reward CreateMessageReward(Level level, string message)
        {
            return new MessageReward(level, message);
        }
    }
}