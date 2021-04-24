using JuliHelper;
using JuliHelper.Camera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class Race : GameScreen
    {
        public List<Entity> entities = new List<Entity>();
        public Camera camera;
        public Player player;

        public static Race instance;

        public Race()
        {
            if (instance != null)
                throw new Exception();
            instance = this;

            entities = new List<Entity>()
            {
                (player = new Player(Vector2.Zero)),
            };

            for (int i = 0; i < 10; i++)
            {
                entities.Add(new EMS_Polygon((G.Rand.NextVector2Box() * 0.5f + Vector2.One) * 100f));
            }

            for (int i = 0; i < 10; i++)
            {
                entities.Add(new Car(Vector2.Zero, G.Rand.NextFloat() * 6f));
            }

            camera = new Camera()
            {
                 moveSpeed = 0.1f
            };
            camera.zoom = camera.targetZoom = 4f;
        }

        public override bool Update(GameTime gameTime)
        {
            camera.UpdateBegin();

            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update(gameTime);
            }

            camera.targetPos = player.Pos;

            camera.UpdateEnd(G.ResX, G.ResY);
            return true;
        }
        public override void Draw(GameTime gameTime)
        {
            G.GDevice.Clear(Color.CornflowerBlue);

            G.SpriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, camera.matrix);
            DrawM.basicEffect.World = camera.matrix;
            Depth.zero.Set(() =>
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    entities[i].Draw(gameTime);
                }
            });
            G.SpriteBatch.End();
        }

        public override void Dispose()
        {
            instance = null;
        }
    }
}
