using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    abstract class RaceDialogue : DialogueScreen
    {
        protected RaceDialogue(M_Rectangle screenView) : base(screenView)
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
                    talkingSource = Race.instance.player.Pos;
                    break;
                case Voice.Friend:
                    talkingSource = Race.instance.friend.Pos;
                    break;
            }

            Vector2 talkingSourceOnScreen = Vector2.Transform(talkingSource, Race.instance.camera.matrix * Race.instance.screenMatrixInverse);

            box.DrawCustom(talkingSourceOnScreen);
            //Fonts.big.Draw(dialogue.Current, Anchor.Bottom(talkingSourceOnScreen + new Vector2(0, 16f)), currentVoice == Voice.Player ? Color.White : currentVoice == Voice.Friend ? Color.Purple : Color.Red);
        }
    }
}
