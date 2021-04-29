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
        public static SoundItem textPlop, collision, horn123, hornGo, transition, carDriveAway, loose, win, ripCar, clickTextBox
            , fastForwardTextbox, kickAir, moveRippedCarPart, lowerCar, engineStart, engineLoop, carSlideOverGroundLoop /* TODO */;
        public static SoundItemCollection steps /* TODO */, kickCar;

        public static SoundItem None { get; set; } = new SoundItem(null);
        public static SoundItem CollisionPlayerWall => collision;
        public static SoundItem CollisionCarWall => collision;
        public static SoundItem ClickTextBox => None;
        public static SoundItem FastForwardTextbox => None;

        public static void LoadContent(ContentManager content, string contentPath, string[] files)
        {
#if DEBUG
            ContentFenja.LoadRaw(typeof(Sounds), contentPath, "Sounds", null);
#else
            ContentFenja.LoadProcessed(typeof(Sounds), "Sounds", content, files);
#endif

            textPlop.Volume /= 4f;
            engineLoop.Volume *= 0.5f;
            carDriveAway.Volume *= 0.5f;
            kickCar.Volume *= 2f;
            kickAir.Volume *= 0.5f;
            loose.Volume *= 2f;
        }

        public static void Dispose()
        {
            ContentFenja.DisposeContent(typeof(Sounds));
        }
    }
}
