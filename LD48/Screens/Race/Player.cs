using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class Player : E_Mask
    {
        KeyCollection right, up, left, down;
        List<KeyCollection> keys = new List<KeyCollection>();

        const float radius = 8f;
        const float kickRadius = 24;

        M_Polygon kickMask;
        float orientation;
        Vector2 orientationDir;

        const float moveSpeed = 2f;

        public override object[] GetConstructorValues() => new object[] { Pos };

        public Player(Vector2 pos) : base(Textures.player, new M_Circle(pos, radius))
        {
            Pos = pos;

            right = new KeyCollection(Input.d);
            up = new KeyCollection(Input.w);
            left = new KeyCollection(Input.a);
            down = new KeyCollection(Input.s);
            keys.AddRange(new List<KeyCollection>()
            {
                right, up, left, down
            });

            kickMask = new M_Polygon(Pos, new List<Vector2>());
            kickMask.vertices.Add(Vector2.Zero);
            kickMask.vertices.Add(Vector2.Zero);
            kickMask.vertices.Add(Vector2.Zero);
            UpdateKickVertices();
        }

        private void UpdateKickVertices()
        {
            kickMask.pos = Pos;
            kickMask.vertices[0] = new Vector2(0, -radius);
            kickMask.vertices[1] = new Vector2(kickRadius, 0);
            kickMask.vertices[2] = new Vector2(0, radius);
            kickMask.RotateRadians(orientation);
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                keys[i].Update();
            }

            Vector2 distToMouse = Race.instance.camera.mousePos - Pos;
            orientation = (float)Math.Atan2(distToMouse.Y, distToMouse.X);
            orientationDir = Vector2.Normalize(distToMouse);

            Vector2 move = Vector2.Zero;
            if (right.down)
                move.X++;
            if (left.down)
                move.X--;
            if (up.down)
                move.Y--;
            if (down.down)
                move.Y++;

            if (move != Vector2.Zero)
            {
                move.Normalize();
                move *= moveSpeed;
            }

            #region Push out of half-solids

            Vector2 pushBack = Vector2.Zero;

            foreach (var e in Race.instance.Entities)
            {
                switch (e)
                {
                    case Car car:
                        CollisionResult cr = Mask.DistToMask(car.Mask);
                        if (cr.distance.HasValue)
                        {
                            pushBack += cr.axisCol * cr.distance.Value * 0.3f;
                            if (move != Vector2.Zero)
                                car.ApplyForce(Pos, move, 0.1f);
                        }
                        break;
                }
            }

            move += pushBack;

            #endregion

            if (move != Vector2.Zero)
            {
                if (move.Length() > moveSpeed)
                    move = Vector2.Normalize(move) * moveSpeed;
                Move(move);
            }

            #region Kick

            UpdateKickVertices();

            if (Input.mbLeft.pressed)
            {
                foreach (var e in Race.instance.Entities)
                {
                    switch (e)
                    {
                        case Car car:
                            if (kickMask.ColMask(car.Mask))
                            {
                                car.ApplyForce(Pos, orientationDir, 10f);
                            }
                            break;
                    }
                }
            }

            #endregion
        }

        public override void Draw(GameTime gameTime)
        {
            DrawMask();

            if (Input.mbLeft.down)
                kickMask.Draw(G.SpriteBatch, Color.Black * 0.5f, Drawer.depth);

            base.Draw(gameTime);
        }
    }

    class CollisionInfo
    {
        public Entity Entity { get; set; }
        public Vector3 AxisCol { get; set; }
        public float Strength { get; set; }
    }
}
