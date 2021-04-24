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
        IEnumerator<string> dialogue;

        protected abstract IEnumerable<string> GetDialogue();

        KeyCollection key;

        public enum Voice
        {
            Unknown,
            Player,
            Friend
        }

        Voice currentVoice;

        public DialogueScreen()
        {
            dialogue = GetDialogue().GetEnumerator();
            dialogue.MoveNext();

            key = new KeyCollection(Input.space, Input.enter, Input.x, Input.z, Input.mbLeft);

            Drawer.roundPositionTo = 0.25f;
        }

        public override bool Update(GameTime gameTime)
        {
            key.Update();
            if (key.pressed)
            {
                if (!dialogue.MoveNext())
                    return false;
                
            }

            return true;
        }

        public override void Draw(GameTime gameTime)
        {
            G.GDevice.Clear(Color.Black);
            G.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateScale(4f));

            Fonts.big.Draw(dialogue.Current, Anchor.Center(G.Res / 8f), currentVoice == Voice.Player ? Color.White : currentVoice == Voice.Friend ? Color.Purple : Color.Red);

            G.SpriteBatch.End();
        }

        protected void Friend() => currentVoice = Voice.Friend;
        protected void Player() => currentVoice = Voice.Player;
    }
}
