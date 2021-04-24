using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD48
{
    class Car : EM_HalfSolid
    {
        float orientation;
        public float orientationVelocity;
        public Vector2 velocity;

        List<Vector2> baseVertices;
        float maskOrientation;

        M_Polygon poly;

        public Car(Vector2 pos, float orientation) : base(Textures.car, new M_Rectangle(0,0, Textures.car.Width, Textures.car.Height).ToPolygon())
        {
            Pos = pos;
            this.orientation = orientation;
            poly = Mask as M_Polygon;
            for (int i = 0; i < poly.vertices.Count; i++)
            {
                poly.vertices[i] -= Texture.GetSize() / 2f;
            }
            baseVertices = poly.vertices.ToList();

            UpdateMask();
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

        public override void Update(GameTime gameTime)
        {
            Pos += velocity;

            Vector2 lonDir = new Vector2((float)Math.Cos(orientation), (float)Math.Sin(orientation));
            float lon = Vector2.Dot(lonDir, velocity);
            Vector2 latDir = new Vector2(lonDir.Y, -lonDir.X);
            float lat = Vector2.Dot(latDir, velocity);
            if (Math.Abs(lat) < 1f)
                lat = 0f;
            if (Math.Abs(lon) < 0.2f)
                lon = 0f;

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
        }

        public override void Draw(GameTime gameTime)
        {
            Texture.Draw(Anchor.Center(Pos), null, null, null, orientation);
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
    }
}
