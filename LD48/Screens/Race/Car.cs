using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LD48
{
    class Car : EM_HalfSolid
    {
        public float orientation;
        public float orientationVelocity;
        public Vector2 velocity;

        List<Vector2> baseVertices;
        float maskOrientation;

        M_Polygon poly;

        protected bool lowered = false;

        SoundEffectInstance slideSound;

        protected Color carColor;

        public bool engineOn;

        protected Texture2D noColorTex;

        class Spark
        {
            Vector2 pos;
            Vector2 velocity;
            Color color;
            float rotation = G.Rand.NextFloat() * MathHelper.TwoPi;

            public Spark(Vector2 pos, float orientation)
            {
                Vector2 r = G.Rand.NextVector2Box();
                r *= Textures.car1Color.GetSize() / 2f;
                r = Vector2.Transform(r, Matrix.CreateRotationZ(orientation));
                this.pos = pos + r;
                velocity = G.Rand.NextVector2Box() * 0.5f;
                if (G.Rand.Next(2) == 0)
                    color = Color.Lerp(Color.Orange, Color.Yellow, G.Rand.NextFloat());
                else
                    color = Color.Lerp(Color.Yellow, Color.White, G.Rand.NextFloat());
            }

            public bool Draw()
            {
                pos += velocity;
                velocity *= 0.95f;
                DrawM.pixel.Draw(pos, color, null, null, rotation);

                if (velocity.LengthSquared() < 0.01f)
                    return false;
                return true;
            }
        }

        List<Spark> sparks;

        public override object[] GetConstructorValues() => new object[] { Pos, orientation };

        public Car(Vector2 pos, float orientation) : base(Textures.car1Color, new M_Rectangle(0, 0, Textures.car1Color.Width, Textures.car1Color.Height - 4).ToPolygon())
        {
            noColorTex = Textures.car1NoColor;
            Pos = pos;
            this.orientation = orientation;
            poly = Mask as M_Polygon;
            for (int i = 0; i < poly.vertices.Count; i++)
            {
                poly.vertices[i] -= Texture.GetSize() / 2f;
            }
            baseVertices = poly.vertices.ToList();

            carColor = G.Rand.NextColor();

            UpdateMask();
        }

        protected void InitLowered()
        {
            lowered = true;
            slideSound = Sounds.carSlideOverGroundLoop.SoundEffect.CreateInstance();
            slideSound.Volume = 0f;
            slideSound.IsLooped = true;
            slideSound.Play();

            sparks = new List<Spark>();
        }

        private void UpdateMask()
        {
            if (maskOrientation != orientation)
            {
                maskOrientation = orientation;
                for (int i = 0; i < poly.vertices.Count; i++)
                {
                    poly.vertices[i] = baseVertices[i];
                }
                poly.RotateRadians(orientation);
            }
            poly.pos = Pos;
        }

        bool colLastFrame;

        private bool IsBotWithoutCollisions() => this is Bot bot && bot.botID != 0
            && bot.pathIndex < bot.path.nodes.Count - 1;

        public override void Update(GameTime gameTime)
        {
            #region Push out of half-solids

            Vector2 pushBack = Vector2.Zero;

            //if (!IsBotWithoutCollisions())
            //{
                foreach (var e in Race.instance.Entities)
                {
                    if (e == this)
                        continue;
                    switch (e)
                    {
                        case Car car:
                            if (car.IsBotWithoutCollisions() != IsBotWithoutCollisions())
                                break;

                            CollisionResult cr = Mask.DistToMask(car.Mask);
                            if (cr.distance.HasValue)
                            {
                                pushBack += cr.axisCol * cr.distance.Value * 0.3f;
                            }
                            break;
                    }
                }
            //}
            #endregion


            Vector2 posPast = Pos;
            if (Move(velocity + pushBack))
            {
                if (!colLastFrame)
                {
                    Sounds.CollisionCarWall.Play();
                    colLastFrame = true;
                }
            }
            else
                colLastFrame = false;

            velocity = Pos - posPast - pushBack;

            Vector2 lonDir = GetLonDir();
            float lon = Vector2.Dot(lonDir, velocity);
            Vector2 latDir = GetLatDir();
            float lat = Vector2.Dot(latDir, velocity);

            if (!(this is Friend))
            {
                if (Math.Abs(lat) < 1f)
                    lat = 0f;
                if (Math.Abs(lon) < 0.2f)
                    lon = 0f;
            }





            #region Slide Sound

            if (lowered)
            {
                slideSound.Volume = Math.Clamp(velocity.Length() * 0.4f, 0f, 1f);

                float speed = velocity.Length();
                if (speed > 0.001f)
                {

                    for (int i = 0; i < Math.Max(1,speed * 2); i++)
                    sparks.Add(new Spark(Pos, orientation));
                }
            }

            #endregion


            Vector2 lonV = lon * lonDir;
            Vector2 latV = lat * latDir;

            lonV *= 0.9f;
            latV *= 0.7f;
            velocity = latV + lonV;

            //if (velocity == Vector2.Zero)
            //    orientationVelocity = 0f;
            //else
            orientationVelocity *= 0.9f;

            orientation += orientationVelocity;
            UpdateMask();

            if (Race.instance.Entities.Any(f => f is EM_Solid s && s.Mask.ColMask(Mask)))
            {
                orientation -= orientationVelocity;
                UpdateMask();

                // MAYBE TODO: impulse calculation (but not really necessary)
                // bounce in other direction
                orientationVelocity = -orientationVelocity * 0.5f;
            }
        }

        public Vector2 GetLonDir() => new Vector2((float)Math.Cos(orientation), (float)Math.Sin(orientation));
        public Vector2 GetLatDir() => new Vector2((float)Math.Sin(orientation), (float)-Math.Cos(orientation));

        public override void Draw(GameTime gameTime)
        {
            if (sparks != null)
            {
                Depth.sparks.Set(() =>
                {
                    for (int i = sparks.Count - 1; i >= 0; i--)
                    {
                        if (!sparks[i].Draw())
                            sparks.RemoveAt(i);
                    }
                });
            }


            Vector2 drawPos = Pos;
            if (engineOn)
                drawPos += G.Rand.NextVector2Box() * 0.25f;

            Depth.cars.Set(() =>
            {
                Texture.Draw(Anchor.Center(drawPos), carColor, null, null, orientation);
                noColorTex.Draw(Anchor.Center(drawPos), null, null, null, orientation);
            });
        }

        internal void ApplyForce(Vector2 fromPosition, Vector2 inDirection, float force)
        {
            Vector2 forceDir = inDirection;
            forceDir.Normalize();
            CollisionResult cr = Mask.DistToVector(fromPosition, -forceDir);

            if (cr.distance.HasValue)
            {
                Vector2 impactPos = fromPosition + cr.distance.Value * forceDir;
                Vector2 impactToOrigin = Pos - impactPos;
                Vector2 impactToOriginN = Vector2.Normalize(impactToOrigin);
                Vector2 impactToOriginNOrth = new Vector2(impactToOriginN.Y, -impactToOriginN.X);
                float forceToOrigin = Vector2.Dot(impactToOriginN, forceDir);
                velocity += impactToOriginN * forceToOrigin * force;

                float forceToOrientation = Vector2.Dot(impactToOriginNOrth, forceDir);
                orientationVelocity += forceToOrientation * force * 0.01f;
            }
        }
        
        public override void Dispose()
        {
            if (slideSound != null)
            {
                slideSound.Stop();
                slideSound.Dispose();
            }
        }
    }
}
