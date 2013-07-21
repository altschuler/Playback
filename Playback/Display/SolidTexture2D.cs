using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Playback.Display
{
    public class SolidTexture2D : Texture2D
    {
        public static GraphicsDevice SharedGraphicsDevice { get; set; }

        public SolidTexture2D(GraphicsDevice gd, int width, int height, Color color)
            : base(gd, width, height, false, SurfaceFormat.Color)
        {
            var colors = new Color[width * height];
            for (var i = 0; i < colors.Length; i++)
                colors[i] = color;

            this.SetData(colors);
        }

        public SolidTexture2D(int width, int height, Color color)
            : this(SharedGraphicsDevice, width, height, color)
        {}
    }
}