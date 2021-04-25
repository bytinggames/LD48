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

        public const int CarHeight = 14;

        UpdrawEnumerator stateMachine;

        Vector2 playerPos, friendPos;
        public Vector2 playerSpeechPos => playerPos - new Vector2(0, Textures.playerBig.Height);
        public Vector2 friendSpeechPos => friendPos - new Vector2(0, Textures.friendBig.Height);
        public float[] angles = new float[3];
        public Vector2[] offsets = new Vector2[3];


        public TuningScreen(float height, int index)
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
            friendPos.X -= index * 25f;

            if (index >= 2)
                offsets[0].X = -10000;
            if (index >= 3)
                offsets[1].X = -10000;
        }

        public override void Draw(GameTime gameTime)
        {
            G.GDevice.Clear(Color.White);
            Drawer.roundPositionTo = 0.25f;
            G.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: screenMatrix);

            Textures.tuningBG.Draw(Anchor.BottomRight(screenView.BottomRight));

            Textures.wheel.Draw(Anchor.BottomLeft(screenView.BottomLeft));

            Color[] colors = new Color[2]
                {
                    Colors.player,
                    Color.White
                };

            for (int i = 0; i < 2; i++)
            {
                Vector2 pos = screenView.BottomLeft + new Vector2(0, -CarHeight * height);
                for (int j = 0; j < GeneratedTextures.carParts.GetLength(1); j++)
                {
                    pos.Y -= GeneratedTextures.carParts[i,j].Height;
                    GeneratedTextures.carParts[i,j].Draw(Anchor.TopLeft(pos + offsets[j]), colors[i], null, null, angles[j]);
                }
            }

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
