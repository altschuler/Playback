using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Playback.Logic;

namespace Playback.Data.Definition
{
    public class BodyDefinition
    {
        public GameObjectRole Role { get; set; }
        public float Angle { get; set; }
        public float AngularVelocity { get; set; }
        public bool Awake { get; set; }
        public Vector2 LinearVelocity { get; set; }
        public float MassDataI { get; set; }
        public float MassDataMass { get; set; }
        public string Name { get; set; }
        public Vector2 MassDataCenter { get; set; }
        public Vector2 Position { get; set; }
        public BodyType Type { get; set; }

        public List<FixtureDefinition> Fixtures { get; set; }

        public BodyDefinition()
        {
            this.Fixtures = new List<FixtureDefinition>();
        }

        public static BodyDefinition Parse(dynamic model)
        {
            var def = new BodyDefinition();
            def.Name = model.name;
            def.Angle = model.angle;
            def.AngularVelocity = model.angularVelocity;
            def.Awake = model.awake;
            def.LinearVelocity = LevelParser.ParsePosition(model.linearVelocity);
            def.Position = LevelParser.ParsePosition(model.position);
            
            var customProps = model.customProperties;
            if (customProps != null)
            {
                foreach (var prop in customProps)
                {
                    if (prop.name == "Role")
                    {
                        switch ((string)prop["string"])
                        {
                            case "Target": def.Role = GameObjectRole.Target;
                                break;
                            case "Death": def.Role = GameObjectRole.Death;
                                break;
                        }
                    }
                }
            }


            if (model["massData-mass"] != null)
                def.MassDataMass = model["massData-mass"];

            if (model["massData-I"] != null)
                def.MassDataI = model["massData-I"];

            if (model["massData-center"] != null)
                def.MassDataCenter = new Vector2((float)model["massData-center"].x, (float)model["massData-center"].y);


            switch ((int)model.type)
            {
                case 0: def.Type = BodyType.Static; break;
                case 1: def.Type = BodyType.Kinematic; break;
                case 2: def.Type = BodyType.Dynamic; break;
            }

            foreach (var fixture in model.fixture)
                def.Fixtures.Add(FixtureDefinition.Parse(fixture));

            return def;
        }
    }
}