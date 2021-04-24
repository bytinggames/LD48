using JuliHelper;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LD48
{
    public class GameResources : IDisposable
    {
        ContentManager content;
        GraphicsDevice gDevice;

        public GameResources(ContentManager content, GraphicsDevice gDevice)
        {
            this.content = content;
            this.gDevice = gDevice;
        }

        public void LoadContent()
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            FileInfo exeInfo = new FileInfo(exePath);
            ContentFenja.Initialize(exeInfo.LastWriteTime);

#if DEBUG
            string contentPath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\Content");
#else
            string contentPath = Path.Combine(Environment.CurrentDirectory, @"Content");
#endif

            Textures.LoadContent(gDevice, contentPath);
            Fonts.LoadContent(content);
            Sounds.LoadContent(contentPath);
            DepthManager.Initialize(typeof(Depth));
            Paths.Initialize(contentPath);
        }

        public void Dispose()
        {
            Sounds.Dispose();
            Fonts.Dispose();
            Textures.Dispose();
        }
    }
}
