using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD48
{
    class House : EM_Solid
    {
        public override object[] GetConstructorValues() => new object[] { Mask };

        M_Rectangle baseRect;
        M_Polygon basePoly;
        M_Polygon[] drawPolies = new M_Polygon[3];

        Vector3 buildingColor = new Vector3(0.5f);
        float[] shading = new float[3];

        public List<House> above = new List<House>();
        public List<House> under = new List<House>();
        public bool rendered;

        float height = G.Rand.NextFloat() * 2f;

        public House(M_Rectangle rect) : base(null, rect)
        {
            baseRect = Mask as M_Rectangle;
            basePoly = rect.ToPolygon();
            for (int i = 0; i < basePoly.vertices.Count; i++)
            {
                basePoly.vertices[i] += basePoly.pos;
            }
            basePoly.pos = Vector2.Zero;
            drawPolies = new M_Polygon[3];
            for (int i = 0; i < drawPolies.Length; i++)
                drawPolies[i] = new M_Rectangle(0,0,0,0).ToPolygon();
            shading[0] = Colors.brightnessTop;
        }
        public override void Draw(GameTime gameTime)
        {
#if DEBUG
            if (!Input.space.down)
#endif
                Depth.houseShadow.Set(() =>
                {
                    DrawM.Sprite.DrawRectangleOutlineOutsideGradient(G.SpriteBatch, baseRect, Color.Black * 0.5f, Color.Transparent, 16f * height * 0.5f, (float)Math.Max(1,Math.Ceiling(16 * height * 0.125f)), true, Drawer.depth);
                });
            Depth.maskDebug.Set(() =>
            {
                Mask.Draw(G.SpriteBatch, Color.Black * 0.5f, Drawer.depth);
            });

            rendered = false;
            above.Clear();
            under.Clear();
        }

        public void DrawOverlayRecursive(GameTime gameTime)
        {
#if DEBUG
            if (Input.space.down)
                return;
#endif
            if (under.Any(f => !f.rendered))
                return;

            rendered = true;
            DrawMyOverlay(gameTime);


            for (int i = 0; i < above.Count; i++)
            {
                if (!above[i].rendered)
                    above[i].DrawOverlayRecursive(gameTime);
            }
        }
        private void DrawMyOverlay(GameTime gameTime)
        {
            AnyCastedPoly((poly, i) =>
            {
                poly.Draw(new Color(buildingColor * shading[i]));
                return false;
            });
        }

        public void PrepareExtrudedPolygons()
        {
            Vector2 flucht = Race.instance.camera.pos;
            var topPoly = drawPolies[0];
            for (int i = 0; i < topPoly.vertices.Count; i++)
            {
                topPoly.vertices[i] = basePoly.vertices[i] + topPoly.pos + (basePoly.vertices[i] - flucht) * height;
            }
            if (flucht.X < baseRect.Left) // show left side ?
            {
                drawPolies[1].vertices[0] = baseRect.TopLeft;
                drawPolies[1].vertices[1] = topPoly.pos + topPoly.vertices[0];
                drawPolies[1].vertices[2] = topPoly.pos + topPoly.vertices[3];
                drawPolies[1].vertices[3] = baseRect.BottomLeft;
                shading[1] = Colors.brightnessW;
            }
            else if (flucht.X > baseRect.Right)
            {
                drawPolies[1].vertices[0] = baseRect.BottomRight;
                drawPolies[1].vertices[1] = topPoly.pos + topPoly.vertices[2];
                drawPolies[1].vertices[2] = topPoly.pos + topPoly.vertices[1];
                drawPolies[1].vertices[3] = baseRect.TopRight;
                shading[1] = Colors.brightnessO;
            }
            else
                drawPolies[1].vertices[0] = new Vector2(float.NaN);

            if (flucht.Y < baseRect.Top)
            {
                drawPolies[2].vertices[0] = baseRect.TopRight;
                drawPolies[2].vertices[1] = topPoly.pos + topPoly.vertices[1];
                drawPolies[2].vertices[2] = topPoly.pos + topPoly.vertices[0];
                drawPolies[2].vertices[3] = baseRect.TopLeft;
                shading[2] = Colors.brightnessN;
            }
            else if (flucht.Y > baseRect.Bottom)
            {
                drawPolies[2].vertices[0] = baseRect.BottomLeft;
                drawPolies[2].vertices[1] = topPoly.pos + topPoly.vertices[3];
                drawPolies[2].vertices[2] = topPoly.pos + topPoly.vertices[2];
                drawPolies[2].vertices[3] = baseRect.BottomRight;
                shading[2] = Colors.brightnessS;
            }
            else
                drawPolies[2].vertices[0] = new Vector2(float.NaN);

        }

        public bool AnyCastedPoly(Func<M_Polygon, int, bool> action)
        {
            for (int i = 0; i < drawPolies.Length; i++)
            {
                if (!float.IsNaN(drawPolies[i].vertices[0].X))
                {
                    if (action(drawPolies[i], i))
                        return true;
                }
            }
            return false;
        }
    }
}
