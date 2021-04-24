using JuliHelper;
using JuliHelperShared;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LD48
{
    public static class Sounds
    {
        public static SoundItem knock, knock2, knock3;

        public static void LoadContent(string contentPath)
        {
            ContentFenja.LoadRaw(typeof(Sounds), contentPath, "Sounds", null);

            knock2.Pitch = 1f;
        }

        public static void Dispose()
        {
            ContentFenja.DisposeContent(typeof(Sounds));
        }
    }
}
