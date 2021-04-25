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
        public TuningScreenBefore1() : base(h, 0)
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
            yield return new UpdrawLerp(100, f =>
            {
                height = h - f * 1f / 2f;
            }, null);
            yield return new Dialogue2();
            yield return new UpdrawLerp(100, f =>
            {
                height = h - 1f / 2f - f * 1f / 2f;
            }, null);
            yield return new Dialogue3();
        }

        class Dialogue1 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                Friend();
                yield return "Dialogue1";
                // [dudes are chilling]
                // "I deepened my car"
                // "you mean lowering?"
                // ".. yup, much better now, you should do it too"
                // why?
                // "theres this event..."
                // ok
            }
        }
        class Dialogue2 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                yield return "Dialogue2";
                // "das nennst du lowern?"
            }
        }
        class Dialogue3 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                yield return "Dialogue3";
                // "cool"
                // "yeah"
                // "so about the tournament 4 dudes, last place looses. it's on the street"
                // "but I have family, can't do that anymore"
                // "but it's so cool, and you know I'm getting nervous when being alone on the track"
                // "ok"
            }
        }
    }

}
