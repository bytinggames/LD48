using JuliHelper;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
            string contentPath = Path.Combine(new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName, "Content");
#else
            string contentPath = Path.Combine(Environment.CurrentDirectory, @"Content");
#endif

#if DEBUG
            string[] files = null;
#else
            string filesFile;
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "LD48.Content.ContentListGenerated_do-not-edit.txt";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                filesFile = reader.ReadToEnd();
            }

            string[] files = filesFile.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
#endif

            Textures.LoadContent(gDevice, content, contentPath);
            Fonts.LoadContent(content);
            Sounds.LoadContent(content, contentPath, files);
            Music.LoadContent(content);
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
