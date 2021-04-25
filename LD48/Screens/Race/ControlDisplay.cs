using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class ControlDisplay : Updraw
    {
        public override void Draw(GameTime gameTime)
        {
        }

        public override bool Update(GameTime gameTime)
        {
            if (Race.instance.player.movedByMyself && Race.instance.player.kickedByMyself)
                return false;
            return true;
        }

        public override void DrawScreen()
        {
            Textures.controls.Draw(Anchor.BottomRight(Race.instance.screenView.BottomRight));
        }
    }
}
