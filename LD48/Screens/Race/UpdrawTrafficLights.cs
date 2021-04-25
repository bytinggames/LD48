using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class UpdrawTrafficLights : Updraw
    {
        const int framesPerLight = 60;

        public int frame = 0;
        float fade = 0f;

        float scale = 1f;
        Action onFadeOut;
        Color[] colors;

        UpdrawEnumerator state;

        public UpdrawTrafficLights(Action onFadeOut)
        {
            this.onFadeOut = onFadeOut;

            colors = new Color[3];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.Gray;
            }

            state = new UpdrawEnumerator(GetStates());
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override bool Update(GameTime gameTime)
        {
            return state.Update(gameTime);
        }

        public override void DrawScreen()
        {
            Vector2 screenPos = new Vector2(G.ResX / 2f, fade * G.ResY / 4f);
            screenPos = Vector2.Transform(screenPos, Race.instance.screenMatrixInverse);

            Vector2 pos = screenPos - new Vector2(Textures.trafficLight.Width * 1.5f, Textures.trafficLight.Height / 2f) * scale;
            for (int i = 0; i < 3; i++)
            {
                Textures.trafficLight.Draw(pos, colors[i] * fade, null, Vector2.One * scale);
                pos.X += Textures.trafficLight.Width * scale;
            }
        }

        IEnumerable<Updraw> GetStates()
        {
            yield return new UpdrawLerp(30, f => fade = f, null);
            yield return new UpdrawDelay(60);
            colors[0] = Color.Red;
            yield return new UpdrawDelay(60);
            colors[1] = Color.Red;
            yield return new UpdrawDelay(60);
            colors[2] = Color.Red;
            yield return new UpdrawDelay(60);
            for (int i = 0; i < colors.Length; i++)
                colors[i] = Color.Lime;
            onFadeOut();
            yield return new UpdrawLerp(30, f => fade = 1f - f, null);
        }
    }
}
