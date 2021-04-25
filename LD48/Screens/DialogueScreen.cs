using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    abstract class DialogueScreen : Updraw
    {
        protected IEnumerator<string> dialogue;

        protected abstract IEnumerable<string> GetDialogue();


        protected DialogueBox box;

        public enum Voice
        {
            Unknown,
            Player,
            Friend
        }

        protected Voice currentVoice;

        protected M_Rectangle screenView;

        static Matrix screenMatrix = Matrix.CreateScale(4f);

        static M_Rectangle GetScreenView()
        {
            M_Rectangle rect = new M_Rectangle(0, 0, G.ResX, G.ResY);
            rect.Transform(screenMatrix);
            return rect;
        }

        public DialogueScreen() : this(GetScreenView()) { }

        public DialogueScreen(M_Rectangle screenView)
        {
            this.screenView = screenView;
            dialogue = GetDialogue().GetEnumerator();
            dialogue.MoveNext();
            box = new DialogueBox(screenView, dialogue.Current);
        }

        public override bool Update(GameTime gameTime)
        {
            if (!box.Update(gameTime))
            {
                if (!dialogue.MoveNext())
                    return false;
                box.Dispose();
                box = new DialogueBox(screenView, dialogue.Current);
            }
            

            return true;
        }

        public override void Draw(GameTime gameTime)
        {
            Drawer.roundPositionTo = 0.25f;
            G.GDevice.Clear(Color.Black);
            G.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: screenMatrix);

            box.DrawCustom(G.Res / 2f);

            G.SpriteBatch.End();
        }

        //protected void DrawInner()
        //{
        //    Fonts.big.Draw(dialogue.Current, Anchor.Center(G.Res / 8f), currentVoice == Voice.Player ? Color.White : currentVoice == Voice.Friend ? Color.Purple : Color.Red);
        //}
        protected void Swap() => currentVoice = currentVoice == Voice.Friend ? Voice.Player : Voice.Friend;
        protected void Friend() => currentVoice = Voice.Friend;
        protected void Player() => currentVoice = Voice.Player;
    }
}
