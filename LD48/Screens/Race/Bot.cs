using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class Bot : Car
    {
        public PathTrack path;

        public int botID;

        Vector2 targetPos;
        int pathIndex;

        protected virtual float GetSpeed() => 0.5f;

        public override object[] GetConstructorValues() => new object[] { botID, path };

        public Bot(int botID, PathTrack path) : base(path.GetStartPos(), path.GetStartOrientation())
        {
            this.path = path;
            this.botID = botID;
        }

        public override void Update(GameTime gameTime)
        {
            if (pathIndex < path.nodes.Count - 1)
            {
                targetPos = path.GetNextPointOnPath(Pos, ref pathIndex);
                Vector2 dist = targetPos - Pos;
                if (dist != Vector2.Zero)
                {
                    velocity += Vector2.Normalize(dist) * GetSpeed();

                    float targetOrientation = (float)Math.Atan2(dist.Y, dist.X);
                    float angleDist = Calculate.AngleDistance(orientation, targetOrientation);
                    orientationVelocity = angleDist * GetSpeed() * 0.1f;////Math.Sign(angleDist) * Math.Max(0.1f, angleDist;
                }
            }
            base.Update(gameTime);

            foreach (var goal in Race.instance.goals)
            {
                if (goal.Mask.ColMask(Mask))
                    Race.instance.Loose();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            path.Draw();

            new M_Point(targetPos).Draw(G.SpriteBatch, Color.Blue, Drawer.depth);

            base.Draw(gameTime);
        }
    }
}
