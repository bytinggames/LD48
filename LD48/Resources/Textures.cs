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
        public static Texture2D player, car1Color, car1NoColor, floor, friend, goal, trafficLight, controlsMouse, controlsKeys, wheel, carBodyColor, carBodyNoColor, playerBig, friendBig, tuningBG,
            car2NoColor, car2Color;

        public static void LoadContent(GraphicsDevice gDevice, ContentManager content, string contentPath)
        {
#if DEBUG
            ContentFenja.LoadRaw(typeof(Textures), contentPath, "Textures", gDevice);
#else
            ContentFenja.LoadProcessed(typeof(Textures), "Textures", content);
#endif

            GeneratedTextures.LoadContent();
        }

        public static void Dispose()
        {
            ContentFenja.DisposeContent(typeof(Textures));
        }
    }

    public static class GeneratedTextures
    {
        public static Texture2D[,] carParts;

        internal static void LoadContent()
        {
            int height = TuningScreen.CarHeight;

            carParts = new Texture2D[2, 3];
            int w = Textures.carBodyColor.Width;

            for (int i = 0; i < 2; i++)
            {
                int y = 0;
                Color[] colors = (i == 0 ? Textures.carBodyColor : Textures.carBodyNoColor).ToColor();
                carParts[i,2] = colors.Crop(w, new Rectangle(0, y, w, Textures.carBodyColor.Height - height * 2)).ToTexture(w, G.GDevice);
                y += carParts[i, 2].Height;
                carParts[i, 1] = colors.Crop(w, new Rectangle(0, y, w, height)).ToTexture(w, G.GDevice);
                y += carParts[i, 1].Height;
                carParts[i, 0] = colors.Crop(w, new Rectangle(0, y, w, height)).ToTexture(w, G.GDevice);
            }
        }
    }
}
