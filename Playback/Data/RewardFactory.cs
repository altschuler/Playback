using Playback.Data.Element;

namespace Playback.Data
{
    public static class RewardFactory
    {
        public static Reward CreateMessageReward(Level level, string message)
        {
            return new MessageReward(level, message);
        }
    }
}