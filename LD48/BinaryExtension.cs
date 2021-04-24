using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LD48
{
    public static class BinaryExtension
    {
        static Dictionary<uint, Type> types = new Dictionary<uint, Type>()
        {
            { 1, typeof(Car) },
            { 2, typeof(Player) },
            { 3, typeof(Race) },
            { 4, typeof(EMS_Polygon) },
            { 5, typeof(House) },
            { 6, typeof(Bot) },
            { 7, typeof(Rival) },
            { 8, typeof(PathTrack) },
            { 9, typeof(Goal) },
            { 10, typeof(PlayerCar) },
        };
        static Dictionary<Type, uint> ids = types.ToDictionary(i => i.Value, i => i.Key);

        static Dictionary<Type, Func<BinaryReader, object>> switchRead = new Dictionary<Type, Func<BinaryReader, object>>()
        {
            { typeof(int), br => br.ReadInt32() },
            { typeof(float), br => br.ReadSingle() },
            { typeof(Vector2), br => br.ReadVector2() },
            { typeof(string), br => br.ReadString() },
            { typeof(bool), br => br.ReadBoolean() },
            { typeof(M_Rectangle), br => br.ReadRectangle() },
        };
        static Dictionary<Type, Action<BinaryWriter, object>> switchWrite = new Dictionary<Type, Action<BinaryWriter, object>>()
        {
            { typeof(int), (bw, v) => bw.Write((int)v) },
            { typeof(float), (bw, v) => bw.Write((float)v) },
            { typeof(Vector2), (bw, v) => bw.Write((Vector2)v) },
            { typeof(string), (bw, v) => bw.Write((string)v) },
            { typeof(bool), (bw, v) => bw.Write((bool)v) },
            { typeof(M_Rectangle), (bw, v) => bw.Write((M_Rectangle)v) },
        };

        public static T Read<T>(this BinaryReader br) => (T)br.ReadFromType(typeof(T));

        public static object ReadFromType(this BinaryReader br, Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                int count = br.ReadInt32();

                Type itemType = type.GenericTypeArguments[0];

                //Type listType = typeof(List<>).MakeGenericType(genericListType);

                IList list = (IList)Activator.CreateInstance(type);

                if (typeof(IStorable).IsAssignableFrom(itemType))
                {
                    for (int i = 0; i < count; i++)
                    {
                        list.Add(br.ReadStorable());
                    }
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        list.Add(br.ReadFromType(itemType));
                    }
                }
                return list;
            }
            else if (typeof(IStorable).IsAssignableFrom(type))
            {
                return br.ReadStorable();
            }
            else if (type.IsArray)
            {
                int dimensions = br.ReadInt32();
                int[] lengths = new int[dimensions];
                for (int i = 0; i < dimensions; i++)
                {
                    lengths[i] = br.ReadInt32();
                }
                Type itemType = type.GetElementType();

                Array array = Array.CreateInstance(itemType, lengths);

                int[] indices = new int[dimensions];
                while (true)
                {
                    array.SetValue(br.ReadFromType(itemType), indices);

                    indices[0]++;

                    int i = 0;
                    while (indices[i] == lengths[i])
                    {
                        indices[i] = 0;
                        i++;
                        if (i == dimensions)
                            break;
                        indices[i]++;
                    }
                    if (i == dimensions)
                        break;
                }

                return array;
            }
            else
                return switchRead[type](br);
        }

        public static void WriteObject(this BinaryWriter bw, object value)
        {
            Type t = value.GetType();
            if (value is IList l && t.IsGenericType)
            {
                bw.Write(l.Count);
                foreach (var item in l)
                {
                    bw.WriteObject(item);
                }
            }
            else if (value is IStorable s)
            {
                bw.Write(s);
            }
            else if (t.IsArray)
            {
                int dimensions = t.GetArrayRank();
                bw.Write(dimensions);
                int[] lengths = new int[dimensions];
                for (int i = 0; i < dimensions; i++)
                {
                    lengths[i] = ((Array)value).GetLength(i);
                    bw.Write(lengths[i]);
                }

                int[] indices = new int[dimensions];
                while (true)
                {
                    bw.WriteObject(((Array)value).GetValue(indices));

                    indices[0]++;

                    int i = 0;
                    while (indices[i] == lengths[i])
                    {
                        indices[i] = 0;
                        i++;
                        if (i == dimensions)
                            break;
                        indices[i]++;
                    }
                    if (i == dimensions)
                        break;
                }
            }
            else
                switchWrite[t](bw, value);
        }


        public static void Write(this BinaryWriter bw, IStorable storable)
        {
            bw.Write(ids[storable.GetType()]);

            object[] values = storable.GetConstructorValues();
            foreach (var v in values)
            {
                bw.WriteObject(v);
            }
        }

        public static object ReadStorable(this BinaryReader br)
        {
            uint id = br.ReadUInt32();
            Type t = types[id];

            var constructor = t.GetConstructors()[0];

            var parameters = constructor.GetParameters();

            object[] parameterValues = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameterValues[i] = br.ReadFromType(parameters[i].ParameterType);
            }

            return Activator.CreateInstance(t, parameterValues);
        }
    }
}
