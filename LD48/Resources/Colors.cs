using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    public static class Colors
    {
        public static readonly Color idk = Color.Black
            , player = Calculate.HexToColor(0x1ab768)
            , friend = new Color(128, 0, 255)
            ;

        public static readonly Color[] botColors = new Color[]
            {
                Color.Red,
                Color.Black
            };

        public static readonly float brightnessTop = 1f;
        public static readonly float brightnessO = 0.5f;
        public static readonly float brightnessS = 0.6f;
        public static readonly float brightnessN = 0.7f;
        public static readonly float brightnessW = 0.8f;
    }
}
