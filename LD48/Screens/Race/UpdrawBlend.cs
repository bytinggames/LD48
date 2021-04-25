using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class UpdrawBlend : Updraw
    {
        const int totalFrames = 60;
        int frames = totalFrames;
        bool blackFadeIn;
        public UpdrawBlend(bool blackFadeIn)
        {
            this.blackFadeIn = blackFadeIn;

            if (blackFadeIn)
                Sounds.transition.Play();
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
            float lerp = (float)frames / totalFrames;
            if (blackFadeIn)
                lerp = 1f - lerp;
            lerp *= lerp;
            new M_Rectangle(0, 0, G.ResX, G.ResY).Draw(Color.Black * lerp);
        }
    }
}
