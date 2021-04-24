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

        UpdrawEnumerator screen;

        public Ingame(SpriteBatch spriteBatch, GraphicsDevice gDevice)
        {
            this.spriteBatch = spriteBatch;
            this.gDevice = gDevice;

            screen = new UpdrawEnumerator(WholeGame());
        }
        public bool Update(GameTime gameTime)
        {
            if (screen.Current is Race race)
            {
                if (Input.leftControl.down && Input.s.pressed)
                {
                    TrackFile.SaveTrack(race);
                }
                if (Input.leftControl.down)
                {
                    if (Input.numberPressed.HasValue)
                    {
                        race.Dispose();
                        screen.Current = TrackFile.LoadTrack(Input.numberPressed.Value);
                    }
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

        public IEnumerable<Updraw> WholeGame()
        {
            //int w = 100;
            //int h = 100;d

            //List<Entity> entities = new List<Entity>()
            //{
            //    new Player(new Vector2(w,h) / 2f * Tile.size)
            //};
            //entities.Add(new House(new M_Rectangle(10, 10, 100, 100)));

            int level = 1;
            //yield return new DialogueIntro();
            foreach (var item in Level(level++)) yield return item;
            yield return new DialogueIntro();
            foreach (var item in Level(level++)) yield return item;
            yield return new DialogueIntro();
            foreach (var item in Level(level++)) yield return item;
            yield return new DialogueIntro();
        }

        IEnumerable<Updraw> Level(int level)
        {
            Race race;
            while (true)
            {
                yield return race = TrackFile.LoadTrack(level);
                if (race.won.Value)
                    break;
                yield return new DialogueLost();
            }
        }
    }
}
