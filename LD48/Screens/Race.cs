using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class Race : GameScreen
    {

        public override bool Update(GameTime gameTime)
        {
            if (Input.space.pressed)
                return false;
            return true;
        }
        public override void Draw(GameTime gameTime)
        {
            G.GDevice.Clear(Color.CornflowerBlue);

            G.SpriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp);

            Textures.player.Draw(new Vector2(100, 100));
            Textures.car.Draw(new Vector2(200, 100));

            Fonts.big.Draw("Hello World", Anchor.TopLeft(0, 0));

            G.SpriteBatch.End();
        }
    }
}
