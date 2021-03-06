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
#if DEBUG
        public bool getOutCutscene = false;
#else
        public bool getOutCutscene = true;
#endif

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
            if (Input.esc.TimeDown >= 60)
                return false;

            if (screen.Current is Race race)
            {
#if DEBUG
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
#endif
            }

            if (!screen.Update(gameTime))
                return false;

            return true;
        }

        public void Draw(GameTime gameTime)
        {
            screen.Current.Draw(gameTime);

            if (Input.esc.down)
            {
                spriteBatch.Begin(samplerState:SamplerState.PointClamp);

                new M_Rectangle(0, 0, G.ResX, G.ResY).Draw(Color.Black * 0.75f);
                Fonts.reallyBig.Draw("Hold Esc to exit...", Anchor.Center(G.Res / 2f), Color.White, new Vector2(2f));

                spriteBatch.End();
            }
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


            yield return new TuningScreenBefore1();
            foreach (var item in Level(level = 1)) yield return item;
            yield return new TuningScreenBefore2();
            foreach (var item in Level(level = 2)) yield return item;
            yield return new TuningScreenBefore3();
            foreach (var item in Level(level = 3)) yield return item;
            yield return new TuningScreenEnd();
            yield return new ScreenEnd();
        }

        IEnumerable<Updraw> Level(int level)
        {
            Race race;
            while (true)
            {
                yield return race = TrackFile.LoadTrack(level);
                getOutCutscene = false;
                if (race.won.Value)
                {
                    break;
                }
            }
        }
    }
}
