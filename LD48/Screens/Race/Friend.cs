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

        //FriendDialogue dialog;
        //IEnumerator<string> dialogEnumerator;

        protected override float GetSpeed()
        {
            switch (Ingame.instance.level)
            {
                case 1: return 0.02f;
                case 2: return 0.1f;
                default: return 0.13f;
            }
        }

        public override object[] GetConstructorValues() => new object[] { path };

        public bool enabled = false;

        public Friend(PathTrack path) : base(0, path)
        {
            playerTexOrientation = orientation;

            //dialog = new FriendDialogue();
            //dialogEnumerator = dialog.

            carColor = Colors.friend;

            InitLowered();
        }

        public override void Update(GameTime gameTime)
        {
            if (!enabled)
                return;

            base.Update(gameTime);

            foreach (var goal in Race.instance.goals)
            {
                if (goal.Mask.ColVector(Pos))
                    Race.instance.Loose();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (enabled)
            {
                float targetOrientation = orientation + orientationVelocity * 5f;
                playerTexOrientation += (targetOrientation - playerTexOrientation) * 0.5f;
                Vector2 dir = new Vector2((float)Math.Cos(playerTexOrientation), (float)Math.Sin(playerTexOrientation));

                Textures.friend.Draw(Anchor.Center(Pos + -dir * (Textures.car1Color.Width / 2f + Textures.friend.Width / 2f)), null, null, null, playerTexOrientation);
            }
            base.Draw(gameTime);
        }
    }
}
