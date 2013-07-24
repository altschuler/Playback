using System.Threading.Tasks;
using FarseerPhysics;
using FarseerPhysics.DebugViews;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Playback.Control;
using Playback.Data;
using Playback.Helpers;
using Playback.Utils;
using Playback.Display;

namespace Playback
{
    public class PlaybackGame : Game
    {
        private GraphicsDeviceManager Graphics;
        private GameWorld GameWorld;

        private DebugViewXNA DebugView;

        public PlaybackGame()
        {
            this.Graphics = new GraphicsDeviceManager(this)
            {
                PreferMultiSampling = true,
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1080,
                IsFullScreen = false,
            };

            IsFixedTimeStep = true;
            IsMouseVisible = true;

            this.Content.RootDirectory = "Content";

            ConvertUnits.SetDisplayUnitToSimUnitRatio(24f);
        }

        protected async override void Initialize()
        {
            this.GameWorld = new GameWorld();
            var level = await LevelParser.Parse("testlevel.json");
            
            this.GameWorld.LoadLevel(level);

            this.DebugView = new DebugViewXNA(this.GameWorld.Level.World);
            this.DebugView.AppendFlags(DebugViewFlags.ContactPoints);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.DebugView.LoadContent(this.GraphicsDevice, this.Content);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            var inputState = InputHelper.CreateInputState(Keyboard.GetState(), Mouse.GetState());

            this.GameWorld.Update(gameTime, inputState);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);

            var proj = Matrix.CreateOrthographic(ConvertUnits.ToSimUnits(
                this.Graphics.PreferredBackBufferWidth),
                -ConvertUnits.ToSimUnits(this.Graphics.PreferredBackBufferHeight), 0, 1);
            var view = Matrix.CreateTranslation(-ConvertUnits.ToSimUnits(
                this.Graphics.PreferredBackBufferWidth) / 2f,
                -ConvertUnits.ToSimUnits(this.Graphics.PreferredBackBufferHeight) / 2f, 0);

            this.DebugView.RenderDebugData(ref proj, ref view);

            base.Draw(gameTime);
        }
    }
}