using JuliHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    public static class Depth
    {
        [Order] public static DepthLayer zero;
        [Order] public static DepthLayer floor;
        [Order] public static DepthLayer goal;
        [Order] public static DepthLayer houseShadow;
        [Order] public static DepthLayer entities;
        [Order] public static DepthLayer humans;
        [Order] public static DepthLayer maskDebug;
        [Order] public static DepthLayer house;
        [Order] public static DepthLayer editorTools;
        [Order] public static DepthLayer ui;
        [Order] public static DepthLayer blackFade;
        [Order] public static DepthLayer one;
    }
}
