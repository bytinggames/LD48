using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class G
    {
        public static GraphicsDevice GDevice { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }

        internal static void Initialize(GraphicsDevice graphicsDevice)
        {
            GDevice = graphicsDevice;
        }

        internal static void LoadContent(SpriteBatch spriteBatch)
        {
            SpriteBatch = spriteBatch;
        }
    }
}
