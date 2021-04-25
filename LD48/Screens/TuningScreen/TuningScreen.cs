using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    abstract class TuningScreen : Updraw
    {
        public static TuningScreen instance;

        public M_Rectangle screenView;
        public Matrix screenMatrix = Matrix.CreateScale(4f);
        public Matrix screenMatrixInverse;

        protected float height;

        const float carHeight = 16f;

        UpdrawEnumerator stateMachine;

        Vector2 playerPos, friendPos;
        public Vector2 playerSpeechPos => playerPos - new Vector2(0, Textures.playerBig.Height);
        public Vector2 friendSpeechPos => friendPos - new Vector2(0, Textures.friendBig.Height);


        public TuningScreen(float height)
        {
            if (instance != null)
                throw new Exception();
            instance = this;

            this.height = height;
            screenView = new M_Rectangle(0, 0, G.ResX, G.ResY);
            screenMatrixInverse = Matrix.Invert(screenMatrix);
            screenView.Transform(screenMatrixInverse);

            stateMachine = new UpdrawEnumerator(StateMachine());

            playerPos = screenView.BottomLeft + new Vector2(200, 0);
            friendPos = screenView.BottomLeft + new Vector2(350, 0);
        }

        public override void Draw(GameTime gameTime)
        {
            G.GDevice.Clear(Color.White);
            Drawer.roundPositionTo = 0.25f;
            G.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: screenMatrix);

            Textures.wheel.Draw(Anchor.BottomLeft(screenView.BottomLeft));
            Textures.carBody.Draw(Anchor.BottomLeft(screenView.BottomLeft + new Vector2(0, -carHeight * height)));

            Textures.playerBig.Draw(Anchor.Bottom(playerPos));
            Textures.friendBig.Draw(Anchor.Bottom(friendPos));

            stateMachine.Current.DrawScreen();

            G.SpriteBatch.End();
        }

        public override bool Update(GameTime gameTime)
        {
            return stateMachine.Update(gameTime);
        }

        public override void Dispose()
        {
            instance = null;
        }

        protected abstract IEnumerable<Updraw> StateMachine();
    }
}
