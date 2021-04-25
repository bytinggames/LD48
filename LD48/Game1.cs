using JuliHelper;
using JuliHelperShared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LD48
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private WindowHelper windowHelper;

        private Ingame ingame;

        private GameResources resources;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Window.IsBorderless = true;
            Window.Position = new Point(1920, 0);
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.ApplyChanges();

            Input.Initialize();
            DrawM.Initialize(GraphicsDevice);

            windowHelper = new WindowHelper(graphics, Window);

            G.Initialize(GraphicsDevice);

            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);

            base.Initialize();
        }

        private void NewIngame()
        {
            ingame = new Ingame(spriteBatch, GraphicsDevice);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            G.LoadContent(spriteBatch);

            Drawer.Initialize(spriteBatch);

            resources = new GameResources(Content, GraphicsDevice);
            resources.LoadContent();

            NewIngame();
        }

        protected override void UnloadContent()
        {
            ingame.Dispose();
            resources.Dispose();

            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            if (IsActive)
            {
                if (Input.esc.pressed)
                {
                    Exit();
                    return;
                }
                if (Input.leftControl.down && Input.r.pressed)
                {
                    ingame.Dispose();
                    NewIngame();
                }

                windowHelper.UpdateBasicInputFunctions();

                int iterations = 1;
                if (Input.leftShift.down)
                    iterations = 10;
                if (Input.rightControl.down)
                {
                    iterations = 0;
                    if (Input.rightAlt.pressed)
                        iterations = 1;
                }
                for (int i = 0; i < iterations; i++)
                {
                    if (i > 0)
                        Input.Update();
                    if (!ingame.Update(gameTime))
                    {
                        Exit();
                        return;
                    }
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            ingame.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
