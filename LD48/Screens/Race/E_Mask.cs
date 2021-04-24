﻿using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class E_Mask : Entity
    {
        public Mask Mask { get; set; }
        public Vector2 MaskOffset { get; set; }

        public E_Mask(Texture2D tex, Mask mask) : base(tex)
        {
            Mask = mask;
        }

        public override Vector2 Pos
        {
            get => Mask.pos - MaskOffset;
            set => Mask.pos = value + MaskOffset;
        }
        public override float PosX
        {
            get => Mask.pos.X - MaskOffset.X;
            set => Mask.X = value + MaskOffset.X;
        }
        public override float PosY
        {
            get => Mask.pos.Y - MaskOffset.Y;
            set => Mask.Y = value + MaskOffset.Y;
        }

        public override void Draw(GameTime gameTime)
        {
            Texture.Draw(Anchor.Center(Pos));
        }

        public void DrawMask()
        {
            Mask.Draw(G.SpriteBatch, Color.Black * 0.5f, Depth.maskDebug);
        }

        public void Move(Vector2 move)
        {

            float t = 1f;
            int c = 0;
            while (t > 0 && c < 5)
            {
                bool collision = false;
                Vector2 originalMove = move;
                CollisionResult crTotal = default;

                for (int i = 0; i < Race.instance.entities.Count; i++)
                {
                    switch (Race.instance.entities[i])
                    {
                        case EM_Solid obs:
                            CollisionResult cr = Mask.DistToMask(obs.Mask, move);
                            if (cr.distance != null)
                            {
                                if (cr.distance >= -1f)
                                {
                                    float minDist = -1f / Vector2.Dot(originalMove, cr.axisCol);
                                    cr.distance -= 0.0001f * minDist;

                                    if (cr.distance.Value >= -1f) // check again, just in case minDist is exceptionally bug and moves distance past -1 (half pipe bug)
                                    {
                                        if (cr.distance.Value <= t)
                                        {
                                            if (cr.distanceReversed.HasValue)
                                            {
                                                // is collision in positive distance?
                                                // or is a collision happening right now?
                                                if (cr.distanceReversed >= 0)
                                                {
                                                    if (crTotal.MinResult(cr))
                                                    {
                                                        //dist = cr.distance.Value;
                                                        //axisCol = cr.axisCol;
                                                        collision = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }

                if (!collision)
                {
                    Mask.pos += t * move;
                    t = 0;
                }
                else
                {
                    float dist = crTotal.distance.Value;
                    Vector2 axisCol = crTotal.axisCol;

                    Mask.pos += dist * move;
                    t -= dist;
                    axisCol = new Vector2(axisCol.Y, -axisCol.X);
                    move = Vector2.Dot(move, axisCol) * axisCol;
                }
                c++;
            }
        }
    }
}
