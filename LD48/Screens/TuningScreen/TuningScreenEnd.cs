using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class TuningScreenEnd : TuningScreen
    {
        public TuningScreenEnd() : base(-2f, 3)
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
            yield return new UpdrawDelay(60);
            yield return new UpdrawBlend(true);
        }

        class Dialogue1 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                Friend();
                yield return "I lost again...";
                yield return "wow";
                yield return "man I could have sworn you hit my car!";
                yield return "I was so busy pushing, anything could have happened";
                Swap();
                yield return "nah";
                Swap();
                yield return "how did you even win actually?";
                yield return "since when are you the tougher one?";
                yield return "I've been to gym for the last six years";
                Swap();
                yield return "the car might be lighter";
                Swap();
                yield return "bullshit!";
                yield return "you cheated";
                yield return "while I wasn't looking";
                Swap();
                yield return "?";
                yield return "how?";
                Swap();
                yield return "I didn't saw you at the goal";
                Swap();
                yield return "you weren't even there";
                Swap();
                yield return "that's enough";
                Swap();
                yield return "ok";
                Swap();
                yield return "yeah";
                Swap();
                yield return "...";
                Swap();
                yield return "you got the price money?";
                Swap();
                yield return "sure";
                Swap();
                yield return "...";
                yield return "..";
                yield return ".";
                yield return "hope you won't go missing too";
            }
        }
    }
}
