﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyCodeLibrary.SystemInfo
{
    /// <summary>
    /// Converts objects to and from a string
    /// </summary>
    public static class ObjectConverters
    {
        /// <summary>
        /// Breaks a dictionary into a string value
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static String BreakDictionaryToString(Dictionary<String, String> dictionary)
        {
            var sb = new StringBuilder();
            const char KeySeparator = '=';
            const char PairSeparator = '&';
            foreach (var pair in dictionary)
            {
                sb.Append(pair.Key);
                sb.Append(KeySeparator);
                sb.Append(pair.Value);
                sb.Append(PairSeparator);
            }
            return sb.ToString(0, sb.Length - 1);
        }

        /// <summary>
        /// Breaks a byte string value into a object
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static object ByteStringToObject(String bytes)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                const char Separator = '&';
                List<string> newlist = new List<String>(bytes.Split(Separator));
                Byte[] ObjectToRecieve = new byte[newlist.Count];
                long Counter = 0;

                foreach (string inbyte in newlist)
                {
                    ObjectToRecieve[Counter] = Convert.ToByte(inbyte, CultureInfo.CurrentCulture);
                    Counter++;
                }

                BinaryFormatter bf = new BinaryFormatter();
                ms.Write(ObjectToRecieve, 0, ObjectToRecieve.Length);
                ms.Seek(0, SeekOrigin.Begin);
                return bf.Deserialize(ms);
            }
        }

        /// <summary>
        /// Converts an object into a byte string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static String ObjectToByteString(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);

                List<String> Output = new List<String>(1000);
                const char Separator = '&';

                foreach (var outbyte in ms.ToArray())
                {
                    Output.Add(outbyte.ToString(CultureInfo.CurrentCulture));
                }

                StringBuilder sb = new StringBuilder();
                foreach (var outbyte in Output)
                {
                    sb.Append(outbyte);
                    sb.Append(Separator);
                }

                return sb.ToString(0, sb.Length - 1);
            }
        }
    }
}
