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
            if (screen.Current is Race race)
            {
                if (Input.leftControl.down && Input.s.pressed)
                {
                    TrackFile.SaveTrack(race);
                }
                if (Input.leftControl.down && Input.o.pressed)
                {
                    race.Dispose();
                    screen.Current = TrackFile.LoadTrack();
                }
            }

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
            List<Entity> entities = new List<Entity>()
            {
                new Player(Vector2.Zero)
            };

            entities.Add(new House(new M_Rectangle(10, 10, 100, 100)));

            yield return new Race(entities, new bool[100,100]);
        }
    }
}
