using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class PathTrack : IStorable
    {
        public List<Vector2> nodes;

        public object[] GetConstructorValues() => new object[] { nodes };
        public PathTrack(List<Vector2> nodes)
        {
            this.nodes = nodes;
        }

        public Vector2 GetNextPointOnPath(Vector2 pos, ref int currentIndex)
        {
            if (nodes.Count == 0)
                return default;

            float minDist = float.MaxValue;
            float bestIndex = 0;
            Vector2 bestT = Vector2.Zero;

            for (int i = Math.Max(0, currentIndex - 1); i < nodes.Count - 1; i++)
            {
                Vector2 a = nodes[i];
                Vector2 b = nodes[i + 1];
                Vector2 t = b - a;
                Vector2 t1 = Vector2.Normalize(t);
                Vector2 n = new Vector2(-t.Y, t.X);
                Vector2 n1 = Vector2.Normalize(n);

                float dist;
                float onLine = Vector2.Dot(pos - a, t1);

                float index;
                if (onLine < 0) // before point a
                {
                    index = i;
                    dist = (a - pos).Length();
                }
                else if (onLine * onLine > t.LengthSquared()) // after point b
                {
                    index = i + 1;
                    dist = (b - pos).Length();
                }
                else // between point a and b
                {
                    index = i + onLine / t.Length();
                    dist = Math.Abs(Vector2.Dot(a - pos, n1));
                }

                if (dist < minDist)
                {
                    minDist = dist;
                    bestIndex = index;
                    bestT = t;
                }
            }

            if (minDist < 64f)
                bestIndex += (64f - minDist) / bestT.Length();

            currentIndex = (int)bestIndex;

            return IndexToPos(bestIndex);
        }

        internal float GetStartOrientation()
        {
            if (nodes.Count < 2)
                return default;
            Vector2 d = nodes[1] - nodes[0];
            return (float)Math.Atan2(d.Y, d.X);
        }

        internal Vector2 GetStartPos()
        {
            if (nodes.Count == 0)
                return default;
            return nodes[0];
        }

        public void Draw()
        {
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                Drawer.DrawLine(nodes[i], nodes[i + 1], Color.Blue, 1f);
            }
        }

        private Vector2 IndexToPos(float index)
        {
            if (index < 0)
                return nodes[0];
            if (index >= nodes.Count - 1)
                return nodes[nodes.Count - 1];
            float a = index % 1;
            return a * nodes[(int)Math.Ceiling(index)] + (1f - a) * nodes[(int)Math.Floor(index)];
        }
    }
}
