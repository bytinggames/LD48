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
            //yield return new CutCar();
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
                Friend();
                yield return "well that didn't go as planned";
                Swap();
                yield return "yup";
                Swap();
                yield return "but MAAAAN it was awesome!";
                yield return "did you see the look on their faces?";
                Swap();
                yield return "no";
                Swap();
                yield return "They really didn't see that coming";
                yield return "I'm SO hyped for next race!";
                Swap();
                yield return "but you lost";
                Swap();
                yield return "aah, don't worry, the winner died in a car accident when driving home";
                yield return "how ironic";
                Swap();
                yield return "...";
                yield return "that's actually really tragic";
                Swap();
                yield return "yeah, so how about we prepare ourselves for next week?";
                Swap();
                yield return "what?";
                yield return "how?";
                yield return "the bottom of my car is demolished";
                yield return "I don't have the money";
                yield return "my children are worried";
                Swap();
                yield return "just go deeper";
                Swap();
                yield return "not possible";
                Swap();
                yield return "just watch...";
            }
        }
        class Dialogue2 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                Player();
                yield return "dope";
            }
        }
    }
}
