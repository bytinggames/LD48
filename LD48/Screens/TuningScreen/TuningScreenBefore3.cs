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
            yield return new UpdrawBlend(false);
            yield return new Dialogue1();

            // rip off from car
            yield return new UpdrawDelay(30);
            angles[1] = 0.05f;
            yield return new UpdrawDelay(30);
            yield return new UpdrawLerp(60, f =>
            {
                offsets[1].X = -(Textures.carBody.Width + 10) * f;
            });
            yield return new UpdrawDelay(30);

            yield return new UpdrawLerp(150, f =>
            {
                height = h - f;
            }, null);
            yield return new Dialogue2();
            yield return new UpdrawBlend(true);
            // dialogue
        }
        class Dialogue1 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                Friend();
                yield return "damn...";
                yield return "now I lost for good... :(";
                Swap();
                yield return "...";
                Swap();
                yield return "I need to tell you something";
                yield return "you see all this fuss about the race";
                yield return "I just thought I could...";
                yield return "I could win, grab the money and get my life back on track";
                Swap();
                yield return "okay, but-";
                Swap();
                yield return "I NEED YOU TO WIN FOR ME!";
                Swap();
                yield return "ok";
                Swap();
                yield return "you know what needs to be done now";
                Swap();
                yield return "sure";
            }
        }
        class Dialogue2 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                Friend();
                yield return "good luck old friend";
            }
        }
    }
}
