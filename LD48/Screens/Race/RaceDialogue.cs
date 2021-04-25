using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class RaceDialogue : DialogueScreen
    {
        protected override IEnumerable<string> GetDialogue()
        {
            yield return "noooo, the car does not work";
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void DrawScreen()
        {
            base.DrawInner();
        }
    }
}
