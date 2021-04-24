using JuliHelper;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    abstract class EM_Solid : E_Mask
    {
        public EM_Solid(Texture2D tex, Mask mask) : base(tex, mask)
        {
        }
    }
}
