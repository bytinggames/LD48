using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class ScreenEnd : Updraw
    {
        public override void Draw(GameTime gameTime)
        {
            G.GDevice.Clear(Color.Black);
            Drawer.roundPositionTo = 0.25f;
            float zoom = G.ResY / 1080f * 4f;
            G.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateScale(zoom));

            Fonts.bigSpace.Draw("Thank you for playing my game :D\n\nThis was created in 48 hours for\nthe Ludum Dare Game Jam 48.\nTheme: Deeper and Deeper\n\n[ESC] to exit", Anchor.Center(G.Res / 2f / zoom), Color.Yellow);

            G.SpriteBatch.End();
        }
    

        public override bool Update(GameTime gameTime)
        {
            if (Input.esc.pressed)
            {
                return false;
            }
            return true;
        }
    }
}
