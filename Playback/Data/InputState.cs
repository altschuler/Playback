using Microsoft.Xna.Framework;
using Playback.Logic;

namespace Playback.Data
{
    public struct InputState
    {
        public TimeDirection TimeDirection;

        public Vector2 MousePosition { get; set; }
        public bool LeftMousePressed { get; set; }
        public bool RightMousePressed { get; set; }
        public bool LeftMouseClicked { get; set; }
    }
}