using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class Ingame : IDisposable
    {
        SpriteBatch spriteBatch;
        GraphicsDevice gDevice;

        ScreenEnumerator screen;

        public Ingame(SpriteBatch spriteBatch, GraphicsDevice gDevice)
        {
            this.spriteBatch = spriteBatch;
            this.gDevice = gDevice;

            screen = new ScreenEnumerator(WholeGame());
        }
        public bool Update(GameTime gameTime)
        {
            if (!screen.Update(gameTime))
                return false;

            return true;
        }

        public void Draw(GameTime gameTime)
        {
            screen.Current.Draw(gameTime);
        }

        public void Dispose()
        {
            screen.Current?.Dispose();
        }

        public IEnumerable<GameScreen> WholeGame()
        {
            yield return new Race();
        }
    }
}
