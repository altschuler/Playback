using Playback.Data.Element;

namespace Playback.Data
{
    public static class ConsequenceFactory
    {
        public static Consequence CreateWinLevelConsequence(Level level)
        {
            return new WinLevelConsequence(level);
        }
    }
}