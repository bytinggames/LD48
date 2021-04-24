using JuliHelper;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LD48
{
    public static class Textures
    {
        public static Texture2D player, car, floor;

        public static void LoadContent(GraphicsDevice gDevice, string contentPath)
        {
            ContentFenja.LoadRaw(typeof(Textures), contentPath, "Textures", gDevice);
        }

        public static void Dispose()
        {
            ContentFenja.DisposeContent(typeof(Textures));
        }
    }
}
