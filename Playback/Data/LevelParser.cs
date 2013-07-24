using System.Linq;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Playback.Data.Definition;
using Playback.Data.Element;

namespace Playback.Data
{
    public class LevelParser
    {
        public static Level ParseWorldDefinition(WorldDefinition worldDef)
        {
            var level = new Level();
            level.World = new World(worldDef.Gravity * -Vector2.UnitY);

            foreach (var bodyDef in worldDef.Bodies)
            {
                // TODO implement all properties of the defs
                var body = new Body(level.World)
                    {
                        Position = bodyDef.Position,
                        Rotation = bodyDef.Angle * -1,
                        BodyType = bodyDef.Type,
                        LinearVelocity = bodyDef.LinearVelocity,
                        AngularVelocity = bodyDef.AngularVelocity
                    };

                // add fixtures to body
                foreach (var fixtureDef in bodyDef.Fixtures)
                {
                    Fixture fixture;
                    if (fixtureDef.Circle != null)
                        fixture = FixtureFactory.AttachCircle(fixtureDef.Circle.Radius, fixtureDef.Density, body,
                                                              fixtureDef.Circle.Center);
                    else
                        fixture = FixtureFactory.AttachPolygon(new Vertices(fixtureDef.Polygon.Vertices
                                                                                      .Select(v => new Vector2(v.X, v.Y)).ToArray()), fixtureDef.Density, body);

                    fixture.Restitution = fixtureDef.Restitution;
                }

                var gameObject = new GameObject
                    {
                        Body = body,
                        HistoryEnabled = bodyDef.HistoryEnabled,
                        Name = bodyDef.Name
                    };

                body.UserData = gameObject;

                level.AddGameObject(gameObject);
            }

            return level;
        }

        public static Vector2 ParsePosition(dynamic model)
        {
            try
            {
                return new Vector2((float)model);
            }
            catch
            {
                return new Vector2((float)model.x, -(float)model.y);
            }
        }
    }
}