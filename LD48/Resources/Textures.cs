using JuliHelper;
using Microsoft.Xna.Framework;
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
        public static Texture2D player, car, floor, friend, goal, trafficLight, controlsMouse, controlsKeys, wheel, carBody, playerBig, friendBig;

        public static void LoadContent(GraphicsDevice gDevice, string contentPath)
        {
            ContentFenja.LoadRaw(typeof(Textures), contentPath, "Textures", gDevice);

            GeneratedTextures.LoadContent();
        }

        public static void Dispose()
        {
            ContentFenja.DisposeContent(typeof(Textures));
        }
    }

    public static class GeneratedTextures
    {
        public static Texture2D[] carParts;

        internal static void LoadContent()
        {
            int height = TuningScreen.CarHeight;

            carParts = new Texture2D[3];
            int y = 0;
            int w = Textures.carBody.Width;
            Color[] colors = Textures.carBody.ToColor();
            carParts[2] = colors.Crop(w, new Rectangle(0, y, w, Textures.carBody.Height - height * 2)).ToTexture(w, G.GDevice);
            y += carParts[2].Height;
            carParts[1] = colors.Crop(w, new Rectangle(0, y, w, height)).ToTexture(w, G.GDevice);
            y += carParts[1].Height;
            carParts[0] = colors.Crop(w, new Rectangle(0, y, w, height)).ToTexture(w, G.GDevice);
        }
    }
}
