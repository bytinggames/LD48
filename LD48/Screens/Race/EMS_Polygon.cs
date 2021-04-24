using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LD48
{
    class EMS_Polygon : EM_Solid
    {
        public override object[] GetConstructorValues() => new object[] { Pos };

        public EMS_Polygon(Vector2 pos) : base(null, M_Polygon.GetRandomConvex(pos, G.Rand, 32f))
        {
        }

        public override void Draw(GameTime gameTime)
        {
            Mask.Draw(G.SpriteBatch, Color.Black * 0.5f, Drawer.depth);
        }
    }
}
