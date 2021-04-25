using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    static class Music
    {
        public static SoundEffect dialogueMusic;

        public static void LoadContent(ContentManager content)
        {
            dialogueMusic = content.Load<SoundEffect>("Music/dialogueMusic");
        }
    }
}
