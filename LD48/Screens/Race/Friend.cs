using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class Friend : Bot
    {
        float playerTexOrientation;

        protected override float GetSpeed() => 0.2f;

        public override object[] GetConstructorValues() => new object[] { path };

        public bool enabled = false;

        public Friend(PathTrack path) : base(0, path)
        {
            playerTexOrientation = orientation;
        }

        public override void Update(GameTime gameTime)
        {
            if (!enabled)
                return;

            base.Update(gameTime);

            foreach (var goal in Race.instance.goals)
            {
                if (goal.Mask.ColMask(Mask))
                    Race.instance.Loose();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            float o = orientation + orientationVelocity * 5f;
            playerTexOrientation += (o - playerTexOrientation) * 0.5f;
            Vector2 dir = new Vector2((float)Math.Cos(playerTexOrientation), (float)Math.Sin(playerTexOrientation));

            Textures.friend.Draw(Anchor.Center(Pos + -dir * (Textures.car.Width / 2f + Textures.friend.Width / 2f)), null, null, null, playerTexOrientation);

            base.Draw(gameTime);
        }
    }
}
