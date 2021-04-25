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
        public TuningScreenBefore2() : base(h, 1)
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
            yield return new UpdrawBlend(false);
            yield return new Dialogue1();
            yield return new UpdrawLerp(150, f =>
            {
                height = h - f;
            }, null);
            yield return new Dialogue2();
            yield return new UpdrawBlend(true);
        }
        class Dialogue1 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                throw new NotImplementedException();
                // "man that was awesome"
                // "but you lost"
                // "..."
                // "don't worry, I'm still in the game, the winner [died of an Unfall / vllt iwas subtileres]"
                // "you want to continue?"
                // "klar, hab PS meines AUtos getuned"
                // "I need the money for the children"
                // "then lower it"
                // "not possible"
                // "yeah let me show me how"
                // [cuts car in half]
                // [lowers]
            }
        }
        class Dialogue2 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                throw new NotImplementedException();
                // "dope"
            }
        }
    }
}
