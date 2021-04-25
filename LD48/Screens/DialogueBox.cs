using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48.Screens
{
    class DialogueBox : Updraw
    {
        public string text;
        public float shownCharacters;

        const float textSpeed = 4f;

        KeyCollection key = new KeyCollection(Input.space, Input.enter, Input.x, Input.z, Input.mbLeft);

        M_Rectangle viewInScreenSpace;

        public DialogueBox(M_Rectangle viewInScreenSpace)
        {
            this.viewInScreenSpace = viewInScreenSpace;
        }

        public override void Draw(GameTime gameTime)
        {
             // TODO: draw textbox + text showing up
        }

        public override bool Update(GameTime gameTime)
        {
            key.Update();

            if (key.pressed)
            {
                if (shownCharacters < text.Length)
                {
                    shownCharacters = text.Length;
                }
                else
                    return false;
            }
            if (shownCharacters < text.Length)
                shownCharacters += textSpeed;
            return true;
        }
    }
}
