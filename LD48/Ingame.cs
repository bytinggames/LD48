using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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

        public static Ingame instance;

        public int level = 0;
        public bool getOutCutscene = true;

        public int editorLevel = 0;

        SoundEffectInstance music;

        public Ingame(SpriteBatch spriteBatch, GraphicsDevice gDevice)
        {
            if (instance != null)
                throw new Exception();
            instance = this;

            this.spriteBatch = spriteBatch;
            this.gDevice = gDevice;

            screen = new UpdrawEnumerator(WholeGame());

            music = Music.dialogueMusic.CreateInstance();
            music.IsLooped = true;
            music.Volume = 0.3f;
            music.Play();
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

            music.Dispose();

            instance = null;
        }

        public IEnumerable<Updraw> WholeGame()
        {
            if (editorLevel > 0)
            {
                getOutCutscene = false;
                yield return TrackFile.LoadTrack(editorLevel);
                yield break;
            }
            //int w = 100;
            //int h = 100;d

            //List<Entity> entities = new List<Entity>()
            //{
            //    new Player(new Vector2(w,h) / 2f * Tile.size)
            //};
            //entities.Add(new House(new M_Rectangle(10, 10, 100, 100)));


            //yield return new TuningScreenBefore1();
            //foreach (var item in Level(level = 1)) yield return item;
            //yield return new TuningScreenBefore2();
            //foreach (var item in Level(level = 2)) yield return item;
            //yield return new TuningScreenBefore3();
            foreach (var item in Level(level = 3)) yield return item;
            yield return new TuningScreenEnd();
        }

        IEnumerable<Updraw> Level(int level)
        {
            Race race;
            while (true)
            {
                yield return race = TrackFile.LoadTrack(level);
                getOutCutscene = false;
                if (race.won.Value)
                    break;
                yield return new DialogueLost();
            }
        }
    }
}
