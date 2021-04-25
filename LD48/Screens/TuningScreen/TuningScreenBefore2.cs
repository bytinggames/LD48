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

            // rip off from car
            yield return new UpdrawDelay(30);
            angles[0] = 0.05f;
            yield return new UpdrawDelay(30);
            yield return new UpdrawLerp(60, f =>
            {
                offsets[0].X = -(Textures.carBody.Width  + 10)* f;
            });
            yield return new UpdrawDelay(30);

            yield return new UpdrawLerp(150, f =>
            {
                height = h - f;
            });
            yield return new UpdrawDelay(60);
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
                yield return "they really didn't see that coming";
                yield return "I'm SO hyped for next race!";
                Swap();
                yield return "but you lost";
                yield return "you are disqualified";
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
                yield return "I don't have money to fix it";
                yield return "my children are worried";
                yield return "and I have to look out for aunt perry's dog";
                Swap();
                yield return "shhh...";
                yield return "just go deeper";
                Swap();
                yield return "not possible";
                Swap();
                yield return "and yet it is";
                yield return "your car will be lighter to push";
                yield return "just watch...";
            }
        }
        class Dialogue2 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                Player();
                yield return "...";
                yield return "neat";
            }
        }
    }
}
