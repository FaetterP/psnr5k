﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    class ReadString
    {
        private static Dictionary<string, string> s_storage;

        private readonly string _key;

        public ReadString(string key)
        {
            _key = key;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            s_storage = new Dictionary<string, string>();

            var textAsset = Resources.Load<TextAsset>(@"Messages");

            string text = textAsset.text;
            string[] lines = text.Split('\n');

            foreach (var line in lines)
            {
                if (line.StartsWith("#") || line.Contains("=") == false)
                    continue;

                int index = line.IndexOf("=");
                string key = line.Substring(0, index);
                string value = line.Substring(index + 1, line.Length - index - 1);
                value = value.Replace("\\n", "\n");

                s_storage.Add(key, value);
            }
        }


        public string GetValue()
        {
            try
            {
                return s_storage[_key];
            }
            catch
            {
                return _key;
            }
        }
    }
}
