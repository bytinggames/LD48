using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class TuningScreenBefore2 : TuningScreen
    {
        const float h = 0f;
        public TuningScreenBefore2() : base(h)
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
                throw new NotImplementedException();
            }
        }
        class Dialogue2 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                throw new NotImplementedException();
            }
        }
    }
}
