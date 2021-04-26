using JuliHelper;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    public static class Fonts
    {
        public static SpriteFont small, bigSpace, reallyBig;

        public static void LoadContent(ContentManager content)
        {
            ContentFenja.LoadProcessed(typeof(Fonts), "Fonts", content);
        }

        public static void Dispose()
        {
            // fonts don't need to be disposed it seems.
        }
    }
}
