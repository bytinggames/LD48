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
        public static SoundItem textPlop, collision, carSlideOverGroundLoop, horn123, hornGo, transition, carDriveAway, loose, win, ripCar, clickTextBox
            , fastForwardTextbox, kickAir, moveRippedCarPart, lowerCar, engineStart, engineLoop;
        public static SoundItemCollection steps, kickCar;

        public static void LoadContent(string contentPath)
        {
            ContentFenja.LoadRaw(typeof(Sounds), contentPath, "Sounds", null);

            textPlop.Volume /= 2f;
        }

        public static void Dispose()
        {
            ContentFenja.DisposeContent(typeof(Sounds));
        }
    }
}
