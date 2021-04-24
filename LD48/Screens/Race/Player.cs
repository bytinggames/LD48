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

        const float maskRadius = 8f;

        public Player(Vector2 pos) : base(Textures.player, new M_Circle(pos, maskRadius))
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
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                keys[i].Update();
            }

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
                move *= 2f;

                Move(move);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            DrawMask();

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
