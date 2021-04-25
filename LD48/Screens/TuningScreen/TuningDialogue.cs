using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    abstract class TuningDialogue : DialogueScreen
    {
        protected TuningDialogue() : base(TuningScreen.instance.screenView)
        {
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void DrawScreen()
        {
            Vector2 talkingSource = default;

            switch (currentVoice)
            {
                case Voice.Unknown:
                    break;
                case Voice.Player:
                    talkingSource = TuningScreen.instance.playerSpeechPos;
                    break;
                case Voice.Friend:
                    talkingSource = TuningScreen.instance.friendSpeechPos;
                    break;
            }

            //Vector2 talkingSourceOnScreen = Vector2.Transform(talkingSource, TuningScreen.instance.screenMatrixInverse);

            box.DrawCustom(talkingSource);
            //Fonts.big.Draw(dialogue.Current, Anchor.Bottom(talkingSourceOnScreen + new Vector2(0, 16f)), currentVoice == Voice.Player ? Color.White : currentVoice == Voice.Friend ? Color.Purple : Color.Red);
        }
    }
}
