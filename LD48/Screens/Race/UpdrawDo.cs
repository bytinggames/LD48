using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class UpdrawDo : Updraw
    {
        int frames;
        Action update;

        public UpdrawDo(int frames, Action update)
        {
            this.frames = frames;
            this.update = update;
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override bool Update(GameTime gameTime)
        {
            frames--;

            update?.Invoke();

            if (frames <= 0)
                return false;
            return true;
        }
    }
}
