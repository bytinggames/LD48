using JuliHelper;
using JuliHelper.Camera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace LD48
{
    class Race : GameScreen, IStorable
    {
        public List<Entity> Entities { get; set; } = new List<Entity>();
        public Camera camera;
        public Player player;

        public static Race instance;


        public object[] GetConstructorValues() => new object[] { Entities };

        public Race(List<Entity> entities)
        {
            this.Entities = entities;

            player = entities.Find(f => f is Player) as Player;

            if (instance != null)
                throw new Exception();
            instance = this;

            camera = new Camera()
            {
                moveSpeed = 0.1f
            };
            camera.zoom = camera.targetZoom = 4f;
            camera.targetPos = player.Pos;
            camera.JumpToTarget();
        }

        public override bool Update(GameTime gameTime)
        {
            camera.UpdateBegin();

            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].Update(gameTime);
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
                for (int i = 0; i < Entities.Count; i++)
                {
                    Entities[i].Draw(gameTime);
                }
            });
            G.SpriteBatch.End();
        }

        public override void Dispose()
        {
            instance = null;
        }

        public void GetStorables()
        {
            //return 
        }
    }
}
