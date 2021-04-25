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
            yield return new UpdrawBlend(true);
        }

        class Dialogue1 : TuningDialogue
        {
            protected override IEnumerable<string> GetDialogue()
            {
                Player();
                //yield return "how come you 

                yield return "Dialogue1";
                throw new NotImplementedException();
                // is pissed
                // why did you fall for my trap [subtiler]
                // 
            }
        }
    }
}
