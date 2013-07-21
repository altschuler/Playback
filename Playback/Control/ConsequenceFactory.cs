using Playback.Logic;

namespace Playback.Control
{
    public static class ConsequenceFactory
    {
        public static Consequence CreateWinLevelConsequence(Level level)
        {
            return new WinLevelConsequence(level);
        }
    }
}