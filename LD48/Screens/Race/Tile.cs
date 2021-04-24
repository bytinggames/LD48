using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class Tile
    {
        public int indexX, indexY;
        public const int size = 16;

        public void Draw(Vector2 pos)
        {
            Textures.floor.Draw(pos, null, new Rectangle(indexX * size, indexY * size, size, size));
        }
    }
}
