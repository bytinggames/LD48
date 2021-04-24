using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class House : EM_Solid
    {
        public override object[] GetConstructorValues() => new object[] { };

        M_Polygon drawPolygon;

        Vector3 buildingColor = new Vector3(0.5f);

        public House(M_Rectangle rect) : base(null, rect)
        {
            drawPolygon = new M_Rectangle(0,0,0,0).ToPolygon();
        }
        public override void Draw(GameTime gameTime)
        {
            Mask.Draw(G.SpriteBatch, Color.Black * 0.5f);

        }
        public override void DrawOverlay(GameTime gameTime)
        {
            Vector2 flucht = Race.instance.camera.pos;
            M_Rectangle rect = Mask as M_Rectangle;

            var poly = rect.ToPolygon();
            for (int i = 0; i < poly.vertices.Count; i++)
            {
                poly.vertices[i] += poly.pos + poly.vertices[i] - flucht;
            }

            if (flucht.X < rect.Left) // show left side ?
            {
                drawPolygon.vertices[0] = rect.TopLeft;
                drawPolygon.vertices[1] = poly.pos + poly.vertices[0];
                drawPolygon.vertices[2] = poly.pos + poly.vertices[3];
                drawPolygon.vertices[3] = rect.BottomLeft;
                drawPolygon.Draw(new Color(buildingColor * Colors.brightnessW));
            }
            else if (flucht.X > rect.Right)
            {
                drawPolygon.vertices[0] = rect.BottomRight;
                drawPolygon.vertices[1] = poly.pos + poly.vertices[2];
                drawPolygon.vertices[2] = poly.pos + poly.vertices[1];
                drawPolygon.vertices[3] = rect.TopRight;
                drawPolygon.Draw(new Color(buildingColor * Colors.brightnessO));
            }
            if (flucht.Y < rect.Top) // show left side ?
            {
                drawPolygon.vertices[0] = rect.TopRight;
                drawPolygon.vertices[1] = poly.pos + poly.vertices[1];
                drawPolygon.vertices[2] = poly.pos + poly.vertices[0];
                drawPolygon.vertices[3] = rect.TopLeft;
                drawPolygon.Draw(new Color(buildingColor * Colors.brightnessN));
            }
            else if (flucht.Y > rect.Bottom)
            {
                drawPolygon.vertices[0] = rect.BottomLeft;
                drawPolygon.vertices[1] = poly.pos + poly.vertices[3];
                drawPolygon.vertices[2] = poly.pos + poly.vertices[2];
                drawPolygon.vertices[3] = rect.BottomRight;
                drawPolygon.Draw(new Color(buildingColor * Colors.brightnessS));
            }


            poly.Draw(new Color(buildingColor * Colors.brightnessTop));
        }
    }
}
