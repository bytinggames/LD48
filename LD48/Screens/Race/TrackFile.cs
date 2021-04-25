using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace LD48
{
    static class TrackFile
    {
        static string GetFilePath(int index) => string.Format(Paths.level, index);

        static int loadedIndex = 0;

        public static Race LoadTrack(int index)
        {
            Race race;

            loadedIndex = index;
            string path = GetFilePath(index);
            if (File.Exists(path))
            {
                using (FileStream ms = File.Open(path, FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(ms))
                    {
                        race = br.Read<Race>();
                    }
                }
            }
            else
                race = new Race(new List<Entity>() { new Player(new Vector2(100) / 2f * Tile.size) }, new bool[100, 100]);
            race.SetLevel(index);

            return race;
        }

        public static void SaveTrack(Race race)
        {
            using (FileStream fs = File.Create(GetFilePath(loadedIndex)))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(race);
                }
            }
        }
    }
}