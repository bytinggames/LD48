using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class UpdrawFade : Updraw
    {
        const int totalFrames = 120;
        int frames = totalFrames;
        bool blackFadeIn;
        public UpdrawFade(bool blackFadeIn)
        {
            this.blackFadeIn = blackFadeIn;
        }

        public override bool Update(GameTime gameTime)
        {
            frames--;
            if (frames <= 0)
                return false;
            return true;
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void DrawScreen()
        {
            Depth.blackFade.Set(() =>
            {
                float lerp = (float)frames / totalFrames;
                if (blackFadeIn)
                    lerp = 1f - lerp;
                lerp *= lerp;
                new M_Rectangle(0, 0, G.ResX, G.ResY).Draw(Color.Black * lerp);
            });
        }
    }
}
