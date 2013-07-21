using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace Playback.Display
{
    public class WorldStage : Stage
    {
        public World World { get; set; }

        public WorldStage(World world, GraphicsDeviceManager gdm, int stageWidth, int stageHeight)
            : base(gdm, stageWidth, stageHeight)
        {
            this.World = world;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var body in this.World.BodyList)
            {
                var disp = (DisplayObject)body.UserData;
                disp.Position = body.Position;
                disp.Rotation = body.Rotation;
            }

            base.Update(gameTime);
        }
    }
}