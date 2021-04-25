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
            yield return new UpdrawBlend(false);
            yield return new UpdrawDelay(60);
            yield return new Dialogue1();
            Sounds.lowerCar.Play();
            yield return new UpdrawLerp(100, f =>
            {
                height = h - f * 1f / 2f;
            }, null);
            yield return new Dialogue2();
            Sounds.lowerCar.Play();
            yield return new UpdrawLerp(100, f =>
            {
                height = h - 1f / 2f - f * 1f / 2f;
            }, null);
            yield return new Dialogue3();
            yield return new UpdrawBlend(true);
        }

        class Dialogue1 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                Friend();
                yield return "dude";
                yield return "I tuned my car";
                Swap();
                yield return "again?";
                Swap();
                yield return "yup!";
                yield return "it's much faster now";
                Swap();
                yield return "what did you change?";
                Swap();
                yield return "It's much deeper to the ground now";
                Swap();
                yield return "?";
                yield return "you lowered it?";
                Swap();
                yield return "yeaaah";
                yield return "heard lowering the mass of a car improves it's grip";
                yield return "you know...";
                yield return "physics!";
                Swap();
                yield return "sure";
                Swap();
                yield return "tomorrow I'm gonna test it out in a street race";
                yield return "you in?";
                Swap();
                yield return "naah.. have family";
                yield return "can't do that anymore";
                Swap();
                yield return "maan, but it will be awesome!";
                yield return "there's a big price money";
                Swap();
                yield return "ok";
                yield return "but my car...";
                yield return "I only have this family car now";
                Swap();
                yield return "just lower it too";
                Swap();
                yield return "ok";
            }
        }
        class Dialogue2 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                Friend();
                yield return "that all you got?";
                Swap();
                yield return "...";
            }
        }
        class Dialogue3 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                Friend();
                yield return "...";
                yield return "cool";
                Swap();
                yield return "yeah";
                Swap();
                yield return "so about the tournament:";
                yield return "4 dudes";
                yield return "last is eliminated each round";
                Swap();
                yield return "sure";
                yield return "let's go";
            }
        }
    }

}
