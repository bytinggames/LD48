using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class UpdrawLerp : Updraw
    {
        int frames;
        int frame = 0;
        Action<float> lerpDraw;
        Action<float> lerpUpdate;

        float lerp => (float)frame / frames;

        public UpdrawLerp(int frames, Action<float> lerpUpdate, Action<float> lerpDraw)
        {
            this.frames = frames;
            this.lerpUpdate = lerpUpdate;
        }

        public override void Draw(GameTime gameTime)
        {
            lerpDraw?.Invoke(lerp);
        }

        public override bool Update(GameTime gameTime)
        {
            frame++;

            lerpUpdate?.Invoke(lerp);

            if (frame >= frames)
                return false;
            return true;
        }
    }
}
