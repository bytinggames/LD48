using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    abstract class GameScreen : IDisposable
    {
        public abstract void Draw(GameTime gameTime);

        public abstract bool Update(GameTime gameTime);

        public virtual void Dispose()
        {
        }
    }
}
