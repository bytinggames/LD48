using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class Car : E_Mask
    {
        float orientation;

        public Car(Vector2 pos, float orientation) : base(Textures.car, new M_Rectangle(0,0, Textures.car.Width, Textures.car.Height).ToPolygon())
        {
            this.orientation = orientation;
            M_Polygon poly = Mask as M_Polygon;
            for (int i = 0; i < poly.vertices.Count; i++)
            {
                poly.vertices[i] -= Texture.GetSize() / 2f;
            }
            poly.RotateRadians(orientation);
        }

        public override void Draw(GameTime gameTime)
        {
            Texture.Draw(Anchor.Center(Pos), null, null, null, orientation);
        }
    }
}
