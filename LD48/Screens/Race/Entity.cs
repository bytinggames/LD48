using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace LD48
{
    abstract class Entity : IStorable
    {
        public virtual Vector2 Pos { get; set; }
        public virtual float PosX { get; set; }
        public virtual float PosY { get; set; }
        public Texture2D Texture;

        public abstract object[] GetConstructorValues();

        public Entity(Texture2D tex)
        {
            Texture = tex;
        }

        public virtual void Draw(GameTime gameTime)
        {
            Texture.Draw(Pos);
        }

        public virtual void DrawOverlay(GameTime gameTime)
        {
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public void GetStoreVariables()
        {
        }
    }
}
