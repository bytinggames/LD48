using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class UpdrawWhile : Updraw
    {
        Func<bool> whileCondition;
        public UpdrawWhile(Func<bool> whileCondition)
        {
            this.whileCondition = whileCondition;
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override bool Update(GameTime gameTime)
        {
            return whileCondition();
        }
    }
}
