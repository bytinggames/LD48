using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class UpdrawDelay : Updraw
    {
        int frames;
        public UpdrawDelay(int frames)
        {
            this.frames = frames;
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override bool Update(GameTime gameTime)
        {
            if (--frames <= 0)
                return false;
            return true;
        }
    }
}
