using JuliHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD48
{
    class DialogueBox : Updraw
    {
        public float currentChar;
        public int currentLine;

        const float textSpeed = 1f;

        KeyCollection key = new KeyCollection(Input.space, Input.enter, Input.x, Input.z, Input.mbLeft);

        M_Rectangle viewInScreenSpace;

        M_Rectangle box;
        M_Rectangle boxInner;
        M_Rectangle boxInnerToDraw;

        SpriteFont font = Fonts.big;
        Vector2 boxBorder = new Vector2(8f);

        Color color;

        Texture2D corner;
        int cornerRadius = 4;

        Color backColor = Color.DarkSlateBlue;

        List<string> lines;

        Vector2 newLineSpace;

        public DialogueBox(M_Rectangle viewInScreenSpace, string text, Color? color = null)
        {
            this.viewInScreenSpace = viewInScreenSpace.CloneRectangle();
            this.viewInScreenSpace.Enlarge(-8f);
            this.color = color ?? Color.White;

            Vector2 textSize = CropText(text);


            textSize += boxBorder  *2f;
            box = new M_Rectangle(0, 0, textSize.X, textSize.Y);
            boxInner = new M_Rectangle(0, 0, textSize.X - cornerRadius, textSize.Y - cornerRadius);

            boxInnerToDraw = new M_Rectangle(0, 0, textSize.X, textSize.Y);
            boxInnerToDraw.Enlarge(-cornerRadius);

            corner = DrawM.Sprite.CreateCircleTexture(cornerRadius, Color.White);
        }

        private Vector2 CropText(string text, float maxLength = 300)
        {
            newLineSpace = font.MeasureString("@");
            newLineSpace.X = 0;

            if (text == "")
                return Vector2.Zero;

            float maxWidth = 0f;

            lines = text.Split(new char[] { '\n' }).ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                Vector2 lineSize = font.MeasureString(lines[i]);
                if (lineSize.X < maxLength)
                {
                    if (lineSize.X > maxWidth)
                        maxWidth = lineSize.X;
                    break;
                }

                int jOver;
                int lastSpace = -1;
                for (jOver = 1; jOver < lines[i].Length; jOver++)
                {
                    lineSize = font.MeasureString(lines[i].Remove(jOver));
                    if (lines[i][jOver] == ' ')
                        lastSpace = jOver;
                    if (lineSize.X > maxLength)
                    {
                        break;
                    }
                }

                int split = lastSpace == -1 ? jOver - 1 : lastSpace;
                if (lastSpace != -1)
                    lines[i] = lines[i].Remove(lastSpace, 1);
                lines.Insert(i + 1, lines[i].Substring(split));
                lines[i] = lines[i].Remove(split);
                lineSize = font.MeasureString(lines[i]);

                if (lineSize.X > maxWidth)
                    maxWidth = lineSize.X;
            }

            return new Vector2(maxWidth, lines.Count * newLineSpace.Y);
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: draw textbox + text showing up
            throw new Exception();

        }

        public void DrawCustom(Vector2 pos)
        {
            box.SetToAnchor(Anchor.Bottom(pos - new Vector2(0, 16f)));
            box.PushIntoRectangle(viewInScreenSpace);

            boxInner.pos = box.pos + new Vector2(cornerRadius);

            boxInnerToDraw.pos = box.pos + new Vector2(0, cornerRadius);
            boxInnerToDraw.Width = box.Width;
            boxInnerToDraw.Height = box.Height - cornerRadius * 2f;
            boxInnerToDraw.Draw(backColor);

            boxInnerToDraw.pos = box.pos + new Vector2(cornerRadius, 0);
            boxInnerToDraw.Width = box.Width - cornerRadius * 2f;
            boxInnerToDraw.Height = box.Height;
            boxInnerToDraw.Draw(backColor);

            Vector2 speechStart = pos + Vector2.Normalize(boxInner.GetCenter() - pos) * 8f; 

            Vector2 dist = speechStart - boxInner.GetCenter();
            CollisionResult cr = boxInner.DistToVector(speechStart, dist);
            if (cr.distance.HasValue)
            {
                Drawer.DrawLineRelative(speechStart, -dist * cr.distance.Value, backColor, 2f);
            }

            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    Vector2 p = box.pos + new Vector2(x * (box.Width - cornerRadius), y * (box.Height - cornerRadius));
                    corner.Draw(p, backColor, new Rectangle(x * cornerRadius, y * cornerRadius, cornerRadius, cornerRadius));
                }
            }


            Vector2 drawPos = box.pos + boxBorder;

            for (int i = 0; i < currentLine; i++)
            {
                font.Draw(lines[i], drawPos, color);
                drawPos += newLineSpace;
            }

            if (currentLine < lines.Count)
            {
                string t1;
                Vector2 s;
                if (currentChar > 0)
                {
                    t1 = lines[currentLine].Remove((int)currentChar);
                    font.Draw(t1, drawPos, color);
                    s = font.MeasureString(t1);
                }
                else
                {
                    s = font.MeasureString("A");
                    s.X = 0;
                    t1 = "";
                }
                string t2 = lines[currentLine][(int)currentChar].ToString();
                Vector2 s2 = font.MeasureString(t2);
                font.Draw(t2, Anchor.Left(drawPos + s + new Vector2(0, -s2.Y) / 2f), color, Vector2.One * 1.5f);
            }
        }

        public override bool Update(GameTime gameTime)
        {
            key.Update();

            if (currentLine > 0 || currentChar > 0)
            {
                if (key.pressed)
                {
                    if (currentLine < lines.Count)
                    {
                        currentLine = lines.Count;
                    }
                    else
                        return false;
                }
            }

            if (currentLine < lines.Count)
            {
                currentChar += textSpeed;
                if (currentChar >= lines[currentLine].Length)
                {
                    currentChar = 0;
                    currentLine++;
                }
            }
            return true;
        }
    }
}
