using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class Player : E_Mask
    {
        KeyCollection right, up, left, down;
        List<KeyCollection> keys = new List<KeyCollection>();

        const float radius = 7f;
        const float kickRadius = 16;

        M_Polygon kickMask;
        float orientation;
        Vector2 orientationDir;

        const float moveSpeed = 2f;

        public bool enabled = false;

        public bool blockInput = true;
        public Vector2 moveInput;
        public bool movedByMyself, kickedByMyself;

        float walkFrame;
        const float walkFrameSpeed = 0.1f;
        bool pushing = false;
        float drawOrientation = MathHelper.Pi;

        public override object[] GetConstructorValues() => new object[] { Pos };

        public Player(Vector2 pos) : base(Textures.player, new M_Circle(pos, radius))
        {
            Pos = pos;

            right = new KeyCollection(Input.d);
            up = new KeyCollection(Input.w);
            left = new KeyCollection(Input.a);
            down = new KeyCollection(Input.s);
            keys.AddRange(new List<KeyCollection>()
            {
                right, up, left, down
            });

            kickMask = new M_Polygon(Pos, new List<Vector2>());
            kickMask.vertices.Add(Vector2.Zero);
            kickMask.vertices.Add(Vector2.Zero);
            kickMask.vertices.Add(Vector2.Zero);
            UpdateKickVertices();
        }

        private void UpdateKickVertices()
        {
            kickMask.pos = Pos;
            kickMask.vertices[0] = new Vector2(0, -radius);
            kickMask.vertices[1] = new Vector2(kickRadius, 0);
            kickMask.vertices[2] = new Vector2(0, radius);
            kickMask.RotateRadians(orientation);
        }

        bool colLastFrame;
        Vector2 lastMoveInput;

        public override void Update(GameTime gameTime)
        {
            if (!enabled)
                return;

            for (int i = 0; i < keys.Count; i++)
            {
                keys[i].Update();
            }

            if (!blockInput)
            {
                if (right.down)
                    moveInput.X++;
                if (left.down)
                    moveInput.X--;
                if (up.down)
                    moveInput.Y--;
                if (down.down)
                    moveInput.Y++;
            }

            Vector2 distToMouse = Race.instance.camera.mousePos - Pos;
            orientation = (float)Math.Atan2(distToMouse.Y, distToMouse.X);
            orientationDir = Vector2.Normalize(distToMouse);


            Vector2 move = Vector2.Zero;

            if (moveInput != Vector2.Zero)
            {
                drawOrientation = (float)Math.Atan2(moveInput.Y, moveInput.X);

                moveInput.Normalize();
                move = moveInput;
                lastMoveInput = moveInput;
                moveInput = Vector2.Zero;
                move *= moveSpeed;

                if (!blockInput)
                    movedByMyself = true;

                walkFrame += walkFrameSpeed;
                if (walkFrame > 4f)
                    walkFrame -= 4f;

            }
            else
                walkFrame = 0;

            #region Push out of half-solids

            Vector2 pushBack = Vector2.Zero;

            pushing = false;
            foreach (var e in Race.instance.Entities)
            {
                switch (e)
                {
                    case Car car:
                        CollisionResult cr = Mask.DistToMask(car.Mask);
                        if (cr.distance.HasValue)
                        {
                            pushBack += cr.axisCol * cr.distance.Value * 0.3f;
                            if (move != Vector2.Zero)
                            {
                                if (Vector2.Dot(move, cr.axisCol) < 0)
                                {
                                    car.ApplyForce(Pos, move, 0.1f);
                                    pushing = true;
                                }
                            }
                        }
                        break;
                }
            }

            move += pushBack;

            #endregion

            if (move != Vector2.Zero)
            {
                if (move.Length() > moveSpeed)
                    move = Vector2.Normalize(move) * moveSpeed;

                if (Move(move))
                {
                    if (!colLastFrame)
                    {
                        Sounds.CollisionPlayerWall.Play();
                        colLastFrame = true;
                    }
                }
                else
                    colLastFrame = false;
            }

            #region Kick

            UpdateKickVertices();

            if (!blockInput)
            {
                if (Input.mbLeft.pressed)
                {
                    bool hitAny = false;
                    foreach (var e in Race.instance.Entities)
                    {
                        switch (e)
                        {
                            case Car car:
                                if (kickMask.ColMask(car.Mask))
                                {
                                    car.ApplyForce(Pos, orientationDir, 10f);
                                    kickedByMyself = true;
                                    hitAny = true;
                                }
                                break;
                        }
                    }

                    if (hitAny)
                        Sounds.kickCar.Play();
                    else
                        Sounds.kickAir.Play();
                }
            }

            #endregion
        }

        public override void Draw(GameTime gameTime)
        {
            if (!enabled)
                return;

            //DrawMask();

            int frame;
            if (Input.mbLeft.down && !blockInput)
            {
                frame = 5;
                drawOrientation = orientation;
            }
            else if (pushing)
                frame = 4;
            else
                frame = (int)walkFrame;

            Depth.humans.Set(() =>
            {
                Texture.Draw(Anchor.Center(Pos), null, new Rectangle(frame * Texture.Height, 0, Texture.Height, Texture.Height), null, drawOrientation);
            });
        }

        public override void DrawOverlay(GameTime gameTime)
        {
            //if (!blockInput)
            //{
            //    if (Input.mbLeft.down)
            //        kickMask.Draw(G.SpriteBatch, Color.Black * 0.5f);
            //}
        }
    }

    class CollisionInfo
    {
        public Entity Entity { get; set; }
        public Vector3 AxisCol { get; set; }
        public float Strength { get; set; }
    }
}
