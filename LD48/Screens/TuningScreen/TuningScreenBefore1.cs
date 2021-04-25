using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class TuningScreenBefore1 : TuningScreen
    {
        const float h = 1f;
        public TuningScreenBefore1() : base(h)
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
            yield return new Dialogue1();
            yield return new UpdrawLerp(150, f =>
            {
                height = h - f;
            }, null);
            yield return new Dialogue2();
        }

        class Dialogue1 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                Friend();
                yield return "Lowering that car might be cool";
            }
        }
        class Dialogue2 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                Friend();
                yield return "cool";
            }
        }
    }

}
