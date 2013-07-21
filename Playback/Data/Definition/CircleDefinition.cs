using Microsoft.Xna.Framework;

namespace Playback.Data.Definition
{
    public class CircleDefinition
    {
        public Vector2 Center { get; set; }
        public float Radius { get; set; }

        public static CircleDefinition Parse(dynamic model)
        {
            return new CircleDefinition() { Center = LevelParser.ParsePosition(model.center), Radius = model.radius };
        }
    }
}