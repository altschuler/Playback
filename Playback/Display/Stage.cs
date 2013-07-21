using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Playback.Display
{
    public class Stage : DisplayObjectContainer
    {
        public int StageWidth { get; set; }
        public int StageHeight { get; set; }

        public float RealWidth { get { return this.StageWidth * this.Scale; } }
        public float RealHeight { get { return this.StageHeight * this.Scale; } }

        public GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;
        public Color BackgroundColor;
        
        public Stage(GraphicsDeviceManager gdm, int stageWidth, int stageHeight)
        {
            this.StageWidth = stageWidth;
            this.StageHeight = stageHeight;

            this.Graphics = gdm;

            this.Graphics.PreferredBackBufferWidth = this.StageWidth;
            this.Graphics.PreferredBackBufferHeight = this.StageHeight;

            this.BackgroundColor = Color.White;
        }

        public void LoadContent()
        {
            this.SpriteBatch = new SpriteBatch(this.Graphics.GraphicsDevice);
        }

        public void Draw()
        {
            this.Graphics.GraphicsDevice.Clear(this.BackgroundColor);

            this.SpriteBatch.Begin();

            base.Draw(this.SpriteBatch);
            
            this.SpriteBatch.End();
        }

        public Viewport Viewport { get { return this.Graphics.GraphicsDevice.Viewport; } }
    }
}
