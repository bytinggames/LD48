using System;
using System.Collections.Generic;
using System.IO;

namespace LD48
{
    static class TrackFile
    {
        static string filePath = Path.Combine(Environment.CurrentDirectory, "test.txt");

        public static Race LoadTrack()
        {
            using (FileStream ms = File.Open(filePath, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    return br.Read<Race>();
                }
            }
        }

        public static void SaveTrack(Race race)
        {
            using (FileStream fs = File.Create(filePath))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(race);
                }
            }
        }
    }
}