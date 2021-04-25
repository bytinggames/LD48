using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class TuningScreenBefore3 : TuningScreen
    {
        const float h = -1f;
        public TuningScreenBefore3() : base(h, 2)
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
            // dialogue
        }
        class Dialogue1 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                throw new NotImplementedException();
                // verzweifelt
                // man chill
                // nee!
                // you have to win!
                // there's only one way... going deeper, losing weight!
                // ok
            }
        }
        class Dialogue2 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                throw new NotImplementedException();
                // good luck
            }
        }
    }
}
