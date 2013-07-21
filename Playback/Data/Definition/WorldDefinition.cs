using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Playback.Data.Definition
{
    public class WorldDefinition
    {
        public Vector2 Gravity { get; set; }
        public List<BodyDefinition> Bodies { get; set; }

        public WorldDefinition()
        {
            this.Bodies = new List<BodyDefinition>();
        }

        public static WorldDefinition Parse(dynamic model)
        {
            var def = new WorldDefinition();
            def.Gravity = new Vector2((float)model.gravity.x, (float)model.gravity.y);

            foreach (var body in model.body)
                def.Bodies.Add(BodyDefinition.Parse(body));

            return def;
        }
    }
}