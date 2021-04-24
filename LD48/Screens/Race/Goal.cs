using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class Goal : E_Mask
    {
        public override object[] GetConstructorValues() => new object[] { Pos };

        public Goal(Vector2 pos) : base(Textures.goal, new M_Rectangle(pos.X, pos.Y, Tile.size, Tile.size))
        {
        }

        public override void Draw(GameTime gameTime)
        {
            Depth.goal.Set(() =>
            {
                Texture.Draw(Pos);
            });
        }
    }
}
