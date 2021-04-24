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

        readonly int width = 100;
        readonly int height = 100;
        Tile[,] tiles;
        bool[,] street;

        enum EditorTool
        {
            Tile
        }
        EditorTool editorTool;

        public object[] GetConstructorValues() => new object[] { Entities, street };

        int pressedX, pressedY;
        KeyP pressed;

        public Race(List<Entity> entities, bool[,] street)
        {
            this.Entities = entities;
            this.street = street;
            width = street.GetLength(0);
            height = street.GetLength(1);

            tiles = new Tile[width, height];

            player = entities.Find(f => f is Player) as Player;

            if (instance != null)
                throw new Exception();
            instance = this;

            camera = new Camera()
            {
                moveSpeed = 0.1f,
                zoomControl = false,
            };
            camera.zoom = camera.targetZoom = 4f;
            camera.targetPos = player.Pos;
            camera.JumpToTarget();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tiles[x, y] = new Tile()
                    {
                        indexX = 3,
                        indexY = 2
                    };
                }
            }
            UpdateTiles(0, width, 0, height);
        }

        public override bool Update(GameTime gameTime)
        {
            camera.UpdateBegin();

            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].Update(gameTime);
            }



            #region Editor


            if (Input.leftControl.down)
            {
                camera.zoomControl = true;
                //if (Input.mbWheel != 0)
                //{
                //    if (Input.mbWheel > 0)
                //    {
                //        camera.targetZoom /= 2f;
                //    }
                //    else
                //    {
                //        camera.targetZoom *= 2f;
                //    }
                //}
            }
            else
            {
                camera.zoomControl = false;

                if (Input.mbWheel != 0)
                {
                    if (Input.mbWheel > 0)
                    {
                        editorTool++;
                        if ((int)editorTool >= Enum.GetNames(editorTool.GetType()).Length)
                            editorTool = 0;
                    }
                    else
                    {
                        editorTool--;
                        if ((int)editorTool < 0)
                            editorTool = (EditorTool)(Enum.GetNames(editorTool.GetType()).Length - 1);
                    }
                }
            }

            switch (editorTool)
            {
                case EditorTool.Tile:

                    if (Input.mbLeft.pressed || Input.mbRight.pressed)
                    {
                        pressedX = (int)Math.Floor(camera.mousePos.X / Tile.size);
                        pressedY = (int)Math.Floor(camera.mousePos.Y / Tile.size);
                        pressed = Input.mbLeft.pressed ? Input.mbLeft : Input.mbRight;
                    }
                    if (pressed != null && pressed.released)
                    {
                        int x = (int)Math.Floor(camera.mousePos.X / Tile.size);
                        int y = (int)Math.Floor(camera.mousePos.Y / Tile.size);

                        int xStart = Math.Min(x, pressedX);
                        int xEnd = Math.Max(x, pressedX);
                        int yStart = Math.Min(y, pressedY);
                        int yEnd = Math.Max(y, pressedY);

                        bool setStreet = pressed == Input.mbLeft;

                        if (xStart < 0)
                            xStart = 0;
                        if (yStart < 0)
                            yStart = 0;
                        if (xStart >= width)
                            xStart = width - 1;
                        if (yStart >= height)
                            yStart = height - 1;

                        for (int y1 = yStart; y1 <= yEnd; y1++)
                            for (int x1 = xStart; x1 <= xEnd; x1++)
                            {
                                street[x1, y1] = setStreet;
                            }
                        UpdateTiles(xStart - 1, xEnd + 1, yStart - 1, yEnd + 1);

                        pressed = null;
                    }
                    break;
                default:
                    break;
            }

            #endregion


            camera.targetPos = player.Pos;

            camera.UpdateEnd(G.ResX, G.ResY);

            return true;
        }

        static Race()
        {
            InitRules();
        }

        static readonly Point[] neighbours = new Point[]
            {
                    new Point(1,0),
                    new Point(1,1),
                    new Point(0, 1),
                    new Point(-1,1),
                    new Point(-1,0),
                    new Point(-1,-1),
                    new Point(0,-1),
                    new Point(1,-1),
            };


        struct Rule
        {
            public int texX, texY;
            public int[] rules;

            public Rule(int x, int y, int[] rules)
            {
                this.texX = x;
                this.texY = y;
                this.rules = rules;
            }
        }
        static Rule[] rules;
        static void InitRules()
        {
            rules = new Rule[]
            {
                new Rule(1, 1, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, }),
                new Rule(4, 1, new int[] { 6, 7, 0, 1, 2, 3, 4 }),
                new Rule(3, 0, new int[] { 0, 2, 3, 4, 5, 6, 7 }),
                new Rule(4, 0, new int[] { 4, 5, 6, 7, 0, 1, 2 }),
                new Rule(3, 1, new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                new Rule(1, 0, new int[] { 0, 1, 2, 3, 4 }),
                new Rule(0, 1, new int[] { 6, 7, 0, 1, 2 }),
                new Rule(2, 1, new int[] { 2, 3, 4, 5, 6 }),
                new Rule(1, 2, new int[] { 4, 5, 6, 7, 0 }),
                new Rule(0, 0, new int[] { 0, 1, 2 }),
                new Rule(2, 0, new int[] { 2, 3, 4 }),
                new Rule(0, 2, new int[] { 6, 7, 0 }),
                new Rule(2, 2, new int[] { 4, 5, 6 }),
            };
        }


        private void UpdateTiles(int xStart, int xEnd, int yStart, int yEnd)
        {
            xStart = Math.Max(xStart, 0);
            yStart = Math.Max(yStart, 0);
            xEnd = Math.Min(xEnd, width - 1);
            yEnd = Math.Min(yEnd, height - 1);

            for (int y = yStart; y <= yEnd; y++)
            {
                for (int x = xStart; x <= xEnd; x++)
                {
                    UpdateTile(x, y);
                }
            }

            bool IsStreet(int x, int y)
            {
                if (x < 0 || y < 0 || x >= width || y >= height)
                    return false;
                return street[x, y];
            }

            void UpdateTile(int x, int y)
            {
                if (street[x, y])
                {
                    // go through all rules
                    foreach (Rule rule in rules)
                    {
                        // check all rules
                        bool broken = false;
                        foreach (int neighbourIndex in rule.rules)
                        {
                            Point p = neighbours[neighbourIndex];
                            bool s = IsStreet(x + p.X, y + p.Y);
                            if (!s)
                            {
                                broken = true;
                                break; // rule broken
                            }
                        }

                        if (!broken)
                        {
                            tiles[x, y].indexX = rule.texX;
                            tiles[x, y].indexY = rule.texY;
                            return;
                        }
                    }
                }
                tiles[x, y].indexX = 3;
                tiles[x, y].indexY = 2;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            G.GDevice.Clear(Color.CornflowerBlue);

            //DepthStencilState state = new DepthStencilState()
            //{
            //    DepthBufferEnable = true,
            //    DepthBufferFunction = CompareFunction.LessEqual,
            //};

            G.SpriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, camera.matrix);
            DrawM.basicEffect.World = camera.matrix;
            //G.GDevice.DepthStencilState = state;

            //DrawM.basicEffect.Projection = Matrix.Create
            Depth.entities.Set(() =>
            {
                for (int i = 0; i < Entities.Count; i++)
                {
                    Entities[i].Draw(gameTime);
                }
            });

            Depth.floor.Set(() =>
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    for (int x = 0; x < tiles.GetLength(0); x++)
                    {
                        tiles[x, y].Draw(new Vector2(x * Tile.size, y * Tile.size));
                    }
                }
            });
            //Depth.floorOver.Set(() =>
            //{
            //    M_Rectangle rect = new M_Rectangle();
            //    rect.size = new Vector2(Tile.size);
            //    for (int y = 0; y < tiles.GetLength(1); y++)
            //    {
            //        for (int x = 0; x < tiles.GetLength(0); x++)
            //        {
            //            if (street[x, y])
            //            {
            //                rect.pos = new Vector2(x * Tile.size, y * Tile.size);
            //                rect.Draw(Color.Black * 0.5f);
            //            }
            //        }
            //    }
            //});

            Depth.editorTools.Set(() =>
            {
            if (pressed != null)
            {
                    Vector2 start = new Vector2(pressedX + 0.5f, pressedY + 0.5f) * Tile.size;
                    Vector2 end = ((camera.mousePos / Tile.size).FloorVector() + Vector2.One * 0.5f) * Tile.size;
                    new M_Rectangle(start, end - start).Enlarge(Tile.size / 2f).Draw(Color.Black * 0.5f);
                }
            });

            G.SpriteBatch.End();


            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].DrawOverlay(gameTime);
            }

            G.SpriteBatch.Begin();

            Fonts.big.Draw("Editor Tool: " + editorTool, Vector2.One * 16f, Color.Black);

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
