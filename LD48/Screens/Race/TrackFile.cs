using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace LD48
{
    static class TrackFile
    {
        static string GetFilePath(int index) => string.Format(Paths.level, index);
        static string GetFilePathBackup(int index) => 
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LD48", "level-backups", "level_" + index + "_" + DateTime.Now.ToFileTimeUtc());

        static int loadedIndex = 0;

        public static Race LoadTrack(int index)
        {
            G.Rand = new Random(index + 1);

            Race race;

            loadedIndex = index;


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var resourceName = $"LD48.Content.Levels.level_{index}.bin";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (BinaryReader br = new BinaryReader(stream))
            {
                race = br.Read<Race>();
            }

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
            string dir = Path.GetDirectoryName(GetFilePathBackup(loadedIndex));
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            using (FileStream fs = File.Create(GetFilePathBackup(loadedIndex)))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(race);
                }
            }
        }
    }
}