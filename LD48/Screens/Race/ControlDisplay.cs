using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class ControlDisplay : Updraw
    {
        int frame = 0;
        public override void Draw(GameTime gameTime)
        {
        }

        public override bool Update(GameTime gameTime)
        {
            frame++;
            if (Race.instance.player.movedByMyself && Race.instance.player.kickedByMyself)
                return false;
            return true;
        }

        public override void DrawScreen()
        {
            Textures.controlsKeys.Draw(Anchor.BottomRight(Race.instance.screenView.BottomRight));

            if (frame < 60* 10 || frame % 60 < 30)
            {
                Textures.controlsMouse.Draw(Anchor.BottomRight(Race.instance.screenView.BottomRight));
            }
        }
    }
}
