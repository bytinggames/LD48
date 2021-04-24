using JuliHelper;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class G
    {
        public static Random Rand = new Random();
        public static int ResX => GDevice.Viewport.Width;
        public static int ResY => GDevice.Viewport.Height;

        public static GraphicsDevice GDevice { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }

        internal static void Initialize(GraphicsDevice graphicsDevice)
        {
            GDevice = graphicsDevice;

            Collision.minDist = 0f;
            Drawer.roundPositionTo = 0f;// 0.25f;
        }

        internal static void LoadContent(SpriteBatch spriteBatch)
        {
            SpriteBatch = spriteBatch;
        }
    }
}
