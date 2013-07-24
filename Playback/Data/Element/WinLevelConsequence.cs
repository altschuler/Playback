using System.Diagnostics;

namespace Playback.Data.Element
{
    public class WinLevelConsequence : Consequence
    {
        public WinLevelConsequence(Level level)
            : base(level)
        {
        }

        public override void Invoke()
        {
            Debug.WriteLine("YOU WIN!");
        }
    }
}