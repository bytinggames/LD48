using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class TuningScreen1 : TuningScreen
    {
        public TuningScreen1() : base(1f)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override bool Update(GameTime gameTime)
        {
            return base.Update(gameTime);
        }

        protected override IEnumerable<Updraw> StateMachine()
        {
            yield return new DialogueIntro();
            yield return new UpdrawLerp(150, f =>
            {
                height = 1f - f;
            }, null);
            yield return new DialogueIntro();
            // dialogue
        }
    }
}
